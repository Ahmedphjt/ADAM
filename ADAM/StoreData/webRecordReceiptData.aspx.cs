using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webRecordReceiptData : System.Web.UI.Page
    {
        public int pageid = 47;

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

                GetNum();
            }
        }

        private void GetNum()
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in mdb.RecordReceiptHeaders orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtRecordReceiptNo.Text = "1";
            else
            {
                ADAM.DataBase.RecordReceiptHeader dr = Rows.First();
                txtRecordReceiptNo.Text = (dr.RecordReceiptNo + 1).ToString();
            }
        }

        private long GetAuditNum()
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in mdb.AuditHeaders orderby a.Id descending select a;
            if (Rows.Count() == 0)
                return 1;
            else
            {
                ADAM.DataBase.AuditHeader dr = Rows.First();
                return (dr.AuditNo + 1);
            }
        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/StoreData/webRecordReceiptData.aspx");
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 2;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            Response.Redirect("~/StoreData/webUpdateRecordReceiptData.aspx");
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 1;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            try
            {
                GetNum();

                if (string.IsNullOrEmpty(txtRecordReceiptNo.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل رقم محضر الاستلام')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtRecordReceiptDate.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل التاريخ')</script>");
                    return;
                }

                if (ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر نوع الصنف')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.RecordReceiptHeaders where a.RecordReceiptNo == long.Parse(txtRecordReceiptNo.Text) select a;
                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن تكرار الكود')</script>");
                    return;
                }

                if (!CheckgvRecordReceipt())
                    return;
                else
                    SaveData();
            }
            catch { }
        }

        private bool CheckgvRecordReceipt()
        {
            for (int Row = 0; Row < gvRecordReceipt.Rows.Count; Row++)
            {
                CheckBox chkChoose = gvRecordReceipt.Rows[Row].FindControl("chkChoose") as CheckBox;
                if (chkChoose.Checked)
                {
                    TextBox txtQtyReceived = gvRecordReceipt.Rows[Row].FindControl("txtQtyReceived") as TextBox;
                    decimal Qty = decimal.Parse(gvRecordReceipt.Rows[Row].Cells[8].Text);
                    TextBox txtFreeQty = gvRecordReceipt.Rows[Row].FindControl("txtFreeQty") as TextBox;

                    TextBox txtIndoor = gvRecordReceipt.Rows[Row].FindControl("txtIndoor") as TextBox;
                    TextBox txtNote = gvRecordReceipt.Rows[Row].FindControl("txtNote") as TextBox;

                    if (string.IsNullOrEmpty(txtIndoor.Text) || string.IsNullOrEmpty(txtNote.Text) || string.IsNullOrEmpty(txtQtyReceived.Text)
                        || string.IsNullOrEmpty(txtFreeQty.Text))
                    {
                        Response.Write("<script>alert('من فضلك تأكد من ادخال باقي البيانات')</script>");
                        return false;
                    }

                    if (decimal.Parse(txtQtyReceived.Text) > Qty)
                    {
                        Response.Write("<script>alert('لا يمكن ان تكون الكمية المستملة اكبر من الكمية الموجودة في امر التوريد')</script>");
                        return false;
                    }

                    long SupplyOrderDetailsId = long.Parse(gvRecordReceipt.DataKeys[Row].Value.ToString());
                    decimal AllQty = 0;
                    ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                    var RecDetails = from a in mdb.RecordReceiptDetails where a.SupplyOrderDetailsId == SupplyOrderDetailsId select a;
                    foreach (ADAM.DataBase.RecordReceiptDetail recdr in RecDetails)
                    {
                        var AuditRows = from a in mdb.AuditDetails where a.RecordReceiptDetailsId == recdr.Id select a;
                        if (AuditRows.Count() > 0)
                        {
                            ADAM.DataBase.AuditDetail auddr = mdb.AuditDetails.Single(a => a.RecordReceiptDetailsId == recdr.Id);
                            if (auddr.AcceptQty == 0 && auddr.RefusedQty == 0)
                            {
                                Response.Write("<script>alert('يوجد اخطار فحص لم يتم اغلاقة حتي الان')</script>");
                                return false;
                            }
                            AllQty = AllQty + auddr.AcceptQty;
                        }
                    }

                    if((AllQty + decimal.Parse(txtQtyReceived.Text) > Qty))
                    {
                        Response.Write("<script>alert('لا يمكن ان تكون الكمية المستملة اكبر من الكمية الموجودة في امر التوريد')</script>");
                        return false;
                    }
                    return true;
                }
                else
                    continue;
            }
            Response.Write("<script>alert('من فضلك قم بأختيار صنف واحد علي الاقل')</script>");
            return false;
        }

        #endregion

        #region Function

        private void SaveData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();

                var LocRows = from a in Mdb.ItemLocations where a.ItemTypeId == int.Parse(ddlItemType.SelectedValue) select a;
                if (LocRows.Count() <= 0)
                {
                    Response.Write("<script>alert('لم يتم أنشاء Location لهذا المخزن من فضلك')</script>");
                    return;
                }

                #region Create RecordReceiptHeader
                ADAM.DataBase.RecordReceiptHeader dr = new DataBase.RecordReceiptHeader();
                dr.RecordReceiptDate = DateTime.Parse(txtRecordReceiptDate.Text);
                dr.RecordReceiptNo = long.Parse(txtRecordReceiptNo.Text);
                dr.RecDate = DateTime.Now;
                Mdb.RecordReceiptHeaders.Add(dr);
                Mdb.SaveChanges();
                #endregion

                for (int Row = 0; Row < gvRecordReceipt.Rows.Count; Row++)
                {
                    CheckBox chkChoose = gvRecordReceipt.Rows[Row].FindControl("chkChoose") as CheckBox;
                    if (chkChoose.Checked)
                    {
                        TextBox txtQtyReceived = gvRecordReceipt.Rows[Row].FindControl("txtQtyReceived") as TextBox;
                        TextBox txtFreeQty = gvRecordReceipt.Rows[Row].FindControl("txtFreeQty") as TextBox;

                        decimal Qty = decimal.Parse(gvRecordReceipt.Rows[Row].Cells[8].Text);

                        if (decimal.Parse(txtQtyReceived.Text) > Qty)
                        {
                            Response.Write("<script>alert('لا يمكن ان تكون الكمية المستملة اكبر من الكمية الموجودة في امر التوريد')</script>");
                            return;
                        }

                        TextBox txtIndoor = gvRecordReceipt.Rows[Row].FindControl("txtIndoor") as TextBox;
                        TextBox txtNote = gvRecordReceipt.Rows[Row].FindControl("txtNote") as TextBox;

                        if (string.IsNullOrEmpty(txtIndoor.Text) || string.IsNullOrEmpty(txtNote.Text) || string.IsNullOrEmpty(txtQtyReceived.Text))
                        {
                            Response.Write("<script>alert('من فضلك تأكد من ادخال باقي البيانات')</script>");
                            return;
                        }

                        long ItemId = Mdb.Items.Single(a => a.Code == long.Parse(gvRecordReceipt.Rows[Row].Cells[3].Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue)).Id;
                        long UnitId = Mdb.Items.Single(a => a.Id == ItemId).ItemunitId;

                        #region Create RecordReceiptDetails
                        ADAM.DataBase.RecordReceiptDetail recdetails = new DataBase.RecordReceiptDetail();
                        recdetails.Indoor = txtIndoor.Text;
                        recdetails.ItemId = ItemId;
                        recdetails.Note = txtNote.Text;
                        recdetails.QtyReceived = decimal.Parse(txtQtyReceived.Text);
                        recdetails.FreeQty = decimal.Parse(txtFreeQty.Text);
                        recdetails.RecordReceiptHeaderId = dr.Id;
                        recdetails.SupplyOrderDetailsId = long.Parse(gvRecordReceipt.DataKeys[Row].Value.ToString());
                        Mdb.RecordReceiptDetails.Add(recdetails);
                        #endregion

                        #region Update PurchaseOrderDetail
                        ADAM.DataBase.SupplyOrderDetail supdr = Mdb.SupplyOrderDetails.Single(a => a.Id == long.Parse(gvRecordReceipt.DataKeys[Row].Value.ToString()));
                        ADAM.DataBase.PurchaseOredrDetail Purdr = Mdb.PurchaseOredrDetails.Single(a => a.Id == supdr.PurchaseOrderDetailsId);
                        Purdr.IsChecked = 6;
                        Mdb.SaveChanges();
                        #endregion                       

                        #region Create AuditHeader
                        if (hfAuditHeaderId.Value == "0")
                        {
                            ADAM.DataBase.AuditHeader auddr = new DataBase.AuditHeader();
                            auddr.AuditNo = GetAuditNum();
                            auddr.AuditDate = DateTime.Now;
                            Mdb.AuditHeaders.Add(auddr);
                            Mdb.SaveChanges();
                            hfAuditHeaderId.Value = auddr.Id.ToString();
                        }
                        #endregion

                        #region Create AuditDetail + Create ItemMovement
                        ADAM.DataBase.AuditDetail auddetailsdr = new DataBase.AuditDetail();
                        auddetailsdr.AcceptQty = 0;
                        auddetailsdr.AuditDetailsDate = DateTime.Now;
                        auddetailsdr.AuditHeaderId = long.Parse(hfAuditHeaderId.Value);
                        auddetailsdr.EmployeeId = 0;
                        auddetailsdr.RecDate = DateTime.Now;
                        auddetailsdr.RecordReceiptDetailsId = recdetails.Id;
                        auddetailsdr.RefusedQty = 0;
                        auddetailsdr.Note = "لا يوجد";
                        auddetailsdr.AcceptfreeQty = 0;
                        auddetailsdr.RefusedFreeQty = 0;
                        Mdb.AuditDetails.Add(auddetailsdr);                        
                        
                        ADAM.DataBase.ItemMovement movdr = new DataBase.ItemMovement();
                        movdr.AdditionalQty = decimal.Parse(txtFreeQty.Text);
                        movdr.DocmentId = recdetails.Id;
                        movdr.ItemId = ItemId;
                        movdr.MainQty = decimal.Parse(txtQtyReceived.Text);
                        movdr.MovementDate = DateTime.Now;
                        movdr.MovmentnameId = 1;
                        movdr.RecDate = DateTime.Now;
                        movdr.StoreId = 1;
                        var Rows = from a in Mdb.ItemLocations where a.ItemTypeId == int.Parse(ddlItemType.SelectedValue) select a;
                        movdr.LocatioId = Rows.First().Id;
                        movdr.ItemUnitId = UnitId;
                        movdr.ItemColorId = Purdr.ItemColorId;
                        movdr.SupplyOrderDetailsId = supdr.Id;
                        movdr.ParentItemMoveMentId = 0;

                        Mdb.ItemMovements.Add(movdr);
                        Mdb.SaveChanges();
                        #endregion
                    }
                }
                Response.Write("<script>alert('تمت عملية الحفظ بنجاح')</script>");
                Response.Redirect("~/StoreData/webRecordReceiptData.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EmptyData()
        {
            txtRecordReceiptDate.Text = txtRecordReceiptNo.Text = "";
            ddlItemType.SelectedValue = "0";
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.RecordReceiptHeaders where a.RecordReceiptNo == long.Parse(txtRecordReceiptNo.Text) select a;
            if (Rows.Count() > 0)
                return true;
            else
                return false;
        }
        #endregion

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 5;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            Response.Redirect("~/StoreReport/webRecordReceiptReport.aspx");
        }
    }
}