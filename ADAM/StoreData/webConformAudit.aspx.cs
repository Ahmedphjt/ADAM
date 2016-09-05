using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webConformAudit : System.Web.UI.Page
    {
        public int pageid = 53;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                    Response.Redirect("~/BasicData/webLogIn.aspx");
                int userid = int.Parse(Session["UserID"].ToString());
                int operationid = 4;

                csGetPermission Per = new csGetPermission();
                if (!Per.getPermission(userid, pageid, operationid))
                    Response.Redirect("~/BasicData/webHomePage.aspx");
            }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/StoreData/webConformAudit.aspx");
        }

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 3;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            if (string.IsNullOrEmpty(txtAuditNo.Text))
            {
                Response.Write("<script>alert('من فضلك تأكد من ادخال رقم اخطار الفحص')</script>");
                return;
            }
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in mdb.AuditHeaders where a.AuditNo == long.Parse(txtAuditNo.Text) select a;
            if (Rows.Count() > 0)
            {
                gvAuditData.Visible = true;
                gvAuditData.DataBind();
                txtAuditNo.Enabled = false;
            }
            else
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم اخطار الفحص')</script>");
                return;
            }
        }

        protected void btnConform_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 7;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            if (string.IsNullOrEmpty(txtAuditNo.Text))
            {
                Response.Write("<script>alert('من فضلك تأكد من ادخال رقم اخطار الفحص')</script>");
                return;
            }

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            for (int Row = 0; Row < gvAuditData.Rows.Count; Row++)
            {
                CheckBox chkChoose = gvAuditData.Rows[Row].FindControl("chkChoose") as CheckBox;
                if (chkChoose.Checked)
                {
                    DropDownList ddlEmployee = gvAuditData.Rows[Row].FindControl("ddlEmployee") as DropDownList;
                    TextBox txtAcceptQty = gvAuditData.Rows[Row].FindControl("txtAcceptQty") as TextBox;
                    TextBox txtRefused = gvAuditData.Rows[Row].FindControl("txtRefused") as TextBox;
                    TextBox txtNote = gvAuditData.Rows[Row].FindControl("txtNote") as TextBox;
                    DropDownList ddlLocation = gvAuditData.Rows[Row].FindControl("ddlLocation") as DropDownList;
                    TextBox txtFreeAcceptQty = gvAuditData.Rows[Row].FindControl("txtFreeAcceptQty") as TextBox;
                    TextBox txtfreeRefusedQty = gvAuditData.Rows[Row].FindControl("txtfreeRefusedQty") as TextBox;

                    if (ddlEmployee.SelectedValue == "0" || ddlLocation.SelectedValue == "0" || string.IsNullOrEmpty(txtAcceptQty.Text) ||
                        string.IsNullOrEmpty(txtRefused.Text) || string.IsNullOrEmpty(txtFreeAcceptQty.Text) || string.IsNullOrEmpty(txtfreeRefusedQty.Text))
                    {
                        Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                        return;
                    }

                    decimal Qty = decimal.Parse(gvAuditData.Rows[Row].Cells[2].Text);
                    if ((decimal.Parse(txtAcceptQty.Text) + decimal.Parse(txtRefused.Text)) != Qty)
                    {
                        Response.Write("<script>alert('يجب ان يساوي مجموع الكمية المقبولة والكمية المرفوضة الكمية المستلمة')</script>");
                        Response.Write("<script>alert(' الكمية المستلمة هي " + Qty + "')</script>");
                        return;
                    }

                    decimal AllAuditQty = 0;

                    long AuditDetailId = long.Parse(gvAuditData.DataKeys[Row].Value.ToString());
                    ADAM.DataBase.AuditDetail auddr = mdb.AuditDetails.Single(a => a.Id == AuditDetailId);

                    ADAM.DataBase.RecordReceiptDetail rrdr = mdb.RecordReceiptDetails.Single(a => a.Id == auddr.RecordReceiptDetailsId);

                    ADAM.DataBase.SupplyOrderDetail supdr = mdb.SupplyOrderDetails.Single(a => a.Id == rrdr.SupplyOrderDetailsId);
                    if (supdr.ItemPrice <= 0)
                    {
                        Response.Write("<script>alert('امر التوريد رقم " + supdr.SupplyOrderHeader.SupplyOrderNo + "والمرتبط بهذا الاجراء من فضلك قم بتسعير امر التوريد')</script>");

                        #region DelJournal

                        for (int DelRow = 0; DelRow < gvAuditData.Rows.Count; DelRow++)
                        {
                            if (DelRow > Row)
                                continue;

                            chkChoose = gvAuditData.Rows[DelRow].FindControl("chkChoose") as CheckBox;
                            if (chkChoose.Checked)
                            {
                                ADAM.DataBase.AuditDetail Delauddr = mdb.AuditDetails.Single(a => a.Id == AuditDetailId);
                                ADAM.DataBase.RecordReceiptDetail Delrrdr = mdb.RecordReceiptDetails.Single(a => a.Id == auddr.RecordReceiptDetailsId);
                                ADAM.DataBase.SupplyOrderDetail Delsupdr = mdb.SupplyOrderDetails.Single(a => a.Id == rrdr.SupplyOrderDetailsId);
                                var JourHeaderRows = from a in mdb.JournalHeaders where a.DocId == Delsupdr.Id select a;
                                if (JourHeaderRows.Count() > 0)
                                {
                                    ADAM.DataBase.JournalHeader Deljournal = mdb.JournalHeaders.Single(a => a.DocId == Delsupdr.Id);
                                    var DelJournalRow = from a in mdb.JournalDetails where a.JournalId == Deljournal.Id select a;
                                    if (DelJournalRow.Count() > 0)
                                    {
                                        foreach (ADAM.DataBase.JournalDetail delJornalDetail in DelJournalRow)
                                            mdb.JournalDetails.Remove(delJornalDetail);
                                        mdb.SaveChanges();
                                    }
                                }
                            }
                        }

                        #endregion

                        return;
                    }

                    ADAM.DataBase.PurchaseOredrDetail Purddr = mdb.PurchaseOredrDetails.Single(a => a.Id == supdr.PurchaseOrderDetailsId);
                    int ItemColorId = Purddr.ItemColorId;

                    var RecDetails = from a in mdb.RecordReceiptDetails where a.SupplyOrderDetailsId == supdr.Id select a;
                    foreach (ADAM.DataBase.RecordReceiptDetail recdr in RecDetails)
                    {
                        ADAM.DataBase.AuditDetail auditdr = mdb.AuditDetails.Single(a => a.RecordReceiptDetailsId == recdr.Id);
                        AllAuditQty = AllAuditQty + auditdr.AcceptQty;
                    }
                    AllAuditQty = AllAuditQty + decimal.Parse(txtAcceptQty.Text);

                    if (AllAuditQty > Purddr.ConformQty)
                    {
                        Response.Write("<script>alert('لا يمكن ان تكون اجمالي الكميات المستلمة اكبر من كمية طلب الشراء المعتمدة')</script>");
                        return;
                    }

                    #region Update AuditDetails
                    auddr.EmployeeId = long.Parse(ddlEmployee.SelectedValue);
                    auddr.AcceptQty = decimal.Parse(txtAcceptQty.Text);
                    auddr.RefusedQty = decimal.Parse(txtRefused.Text);
                    if (!string.IsNullOrEmpty(txtNote.Text))
                        auddr.Note = txtNote.Text;
                    auddr.AcceptfreeQty = decimal.Parse(txtFreeAcceptQty.Text);
                    auddr.RefusedFreeQty = decimal.Parse(txtfreeRefusedQty.Text);
                    #endregion

                    long UnitId = mdb.Items.Single(a => a.Id == rrdr.ItemId).ItemunitId;

                    //-----------------------------------------------------------------------
                    #region Insert IntoIncommingOrderTable

                    long IncomOrderNo = GetIncoOrderData(long.Parse(ddlItemType.SelectedValue.ToString()));
                    ADAM.DataBase.IncommingOrderData Incdr = new DataBase.IncommingOrderData();
                    Incdr.AuditDetailsId = auddr.Id;
                    Incdr.FreeItemPrice = 0;
                    Incdr.IncommingOrderNo = IncomOrderNo;
                    Incdr.ItemPrice = supdr.ItemPrice;
                    Incdr.ItemTypeId = long.Parse(ddlItemType.SelectedValue.ToString());
                    Incdr.RecordReceiptDetailsId = rrdr.Id;
                    Incdr.LocationId = int.Parse(ddlLocation.SelectedValue);
                    mdb.IncommingOrderDatas.Add(Incdr);

                    #endregion
                    //------------------------------------------------------------------------

                    #region Insert AcceptQty
                    if (decimal.Parse(txtAcceptQty.Text) > 0)
                    {
                        ADAM.DataBase.ItemMovement itmdr = new DataBase.ItemMovement();
                        itmdr.AdditionalQty = decimal.Parse(txtFreeAcceptQty.Text) * -1;
                        itmdr.DocmentId = auddr.Id;
                        itmdr.ItemId = rrdr.ItemId;
                        itmdr.MainQty = decimal.Parse(txtAcceptQty.Text) * -1;
                        itmdr.MovementDate = DateTime.Now;
                        itmdr.MovmentnameId = 2;
                        itmdr.RecDate = DateTime.Now;
                        itmdr.StoreId = 1;
                        var Rows = from a in mdb.ItemLocations where a.ItemTypeId == int.Parse(ddlItemType.SelectedValue) select a;
                        itmdr.LocatioId = Rows.First().Id;
                        itmdr.ItemUnitId = UnitId;
                        itmdr.SupplyOrderDetailsId = rrdr.SupplyOrderDetailsId;
                        itmdr.ItemColorId = ItemColorId;
                        itmdr.IncommingOrderNo = 0;
                        itmdr.AuditDetailsId = auddr.Id;
                        itmdr.AdditionalQtyOut = 0;
                        itmdr.MainQtyOut = 0;
                        itmdr.ParentItemMoveMentId = 0;
                        mdb.ItemMovements.Add(itmdr);

                        ADAM.DataBase.ItemMovement Nitmdr = new DataBase.ItemMovement();
                        Nitmdr.AdditionalQty = decimal.Parse(txtFreeAcceptQty.Text);
                        Nitmdr.DocmentId = auddr.Id;
                        Nitmdr.ItemId = rrdr.ItemId;
                        Nitmdr.MainQty = decimal.Parse(txtAcceptQty.Text);
                        Nitmdr.MovementDate = DateTime.Now;
                        Nitmdr.MovmentnameId = 3;
                        Nitmdr.RecDate = DateTime.Now;
                        Nitmdr.StoreId = 2;
                        Nitmdr.LocatioId = int.Parse(ddlLocation.SelectedValue);
                        Nitmdr.ItemUnitId = UnitId;
                        Nitmdr.SupplyOrderDetailsId = rrdr.SupplyOrderDetailsId;
                        Nitmdr.ItemColorId = ItemColorId;
                        Nitmdr.IncommingOrderNo = IncomOrderNo;
                        Nitmdr.AuditDetailsId = auddr.Id;
                        Nitmdr.AdditionalQtyOut = 0;
                        Nitmdr.MainQtyOut = 0;
                        Nitmdr.ParentItemMoveMentId = 0;
                        mdb.ItemMovements.Add(Nitmdr);
                    }
                    #endregion

                    #region Insert RefuseQty
                    if (decimal.Parse(txtRefused.Text) > 0)
                    {
                        ADAM.DataBase.ItemMovement itmdr = new DataBase.ItemMovement();
                        itmdr.AdditionalQty = decimal.Parse(txtfreeRefusedQty.Text) * -1;
                        itmdr.DocmentId = auddr.Id;
                        itmdr.ItemId = rrdr.ItemId;
                        itmdr.MainQty = decimal.Parse(txtRefused.Text) * -1;
                        itmdr.MovementDate = DateTime.Now;
                        itmdr.MovmentnameId = 4;
                        itmdr.RecDate = DateTime.Now;
                        itmdr.StoreId = 1;
                        var Rows = from a in mdb.ItemLocations where a.ItemTypeId == int.Parse(ddlItemType.SelectedValue) select a;
                        itmdr.LocatioId = Rows.First().Id;
                        itmdr.ItemUnitId = UnitId;
                        itmdr.SupplyOrderDetailsId = rrdr.SupplyOrderDetailsId;
                        itmdr.ItemColorId = ItemColorId;
                        itmdr.IncommingOrderNo = 0;
                        itmdr.AuditDetailsId = auddr.Id;
                        itmdr.AdditionalQtyOut = 0;
                        itmdr.MainQtyOut = 0;
                        itmdr.ParentItemMoveMentId = 0;
                        mdb.ItemMovements.Add(itmdr);

                        ADAM.DataBase.ItemMovement Nitmdr = new DataBase.ItemMovement();
                        Nitmdr.AdditionalQty = decimal.Parse(txtfreeRefusedQty.Text);
                        Nitmdr.DocmentId = auddr.Id;
                        Nitmdr.ItemId = rrdr.ItemId;
                        Nitmdr.MainQty = decimal.Parse(txtRefused.Text);
                        Nitmdr.MovementDate = DateTime.Now;
                        Nitmdr.MovmentnameId = 5;
                        Nitmdr.RecDate = DateTime.Now;
                        Nitmdr.StoreId = 3;
                        Nitmdr.LocatioId = Rows.First().Id;
                        Nitmdr.ItemUnitId = UnitId;
                        Nitmdr.SupplyOrderDetailsId = rrdr.SupplyOrderDetailsId;
                        Nitmdr.ItemColorId = ItemColorId;
                        Nitmdr.IncommingOrderNo = IncomOrderNo;
                        Nitmdr.AuditDetailsId = auddr.Id;
                        Nitmdr.AdditionalQtyOut = 0;
                        Nitmdr.MainQtyOut = 0;
                        Nitmdr.ParentItemMoveMentId = 0;
                        mdb.ItemMovements.Add(Nitmdr);
                    }
                    #endregion

                    Purddr.IsChecked = 7;
                    if (AllAuditQty == Purddr.ConformQty)
                        Purddr.IsClosed = 1;

                    #region InsertJournalCode

                    //      من حـ / المخزن 
                    // الي حـ / المورد

                    ADAM.DataBase.ItemType itemTypedr = mdb.ItemTypes.Single(a => a.Id == long.Parse(ddlItemType.SelectedValue));
                    ADAM.DataBase.SupplierData Supplierdr = mdb.SupplierDatas.Single(a => a.Id == supdr.SupplyOrderHeader.SupplierId);
                    csJournal CsJornalRow = new csJournal();

                    long JournalHeaderId = CsJornalRow.InsertIntoJournalHeader(0, DateTime.Now, 9, "دخول البضاعة الي المخزن", 0, supdr.SupplyOrderHeaderId);
                    CsJornalRow.InsertIntoJournalDetails(itemTypedr.AccountId, supdr.SupplyOrderHeader.CostCenter, (decimal.Parse(txtAcceptQty.Text) * supdr.ItemPrice), 0, JournalHeaderId, "من ح/ المخزن");
                    CsJornalRow.InsertIntoJournalDetails(Supplierdr.AccountId, supdr.SupplyOrderHeader.CostCenter, 0, (decimal.Parse(txtAcceptQty.Text) * supdr.ItemPrice), JournalHeaderId, "الي حـ / المورد");

                    #endregion

                }
            }
            mdb.SaveChanges();
            Response.Redirect("~/StoreData/webConformAudit.aspx");
        }

        private long GetIncoOrderData(long ItemType)
        {
            long IncommingOrder = 0;
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var IncRows = from a in Mdb.IncommingOrderDatas where a.ItemTypeId == ItemType orderby a.Id descending select a;
            if (IncRows.Count() == 0)
                IncommingOrder = 1;
            else
            {
                ADAM.DataBase.IncommingOrderData dr = IncRows.First();
                IncommingOrder = dr.IncommingOrderNo + 1;
            }

            return IncommingOrder;
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 5;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

        }
    }
}