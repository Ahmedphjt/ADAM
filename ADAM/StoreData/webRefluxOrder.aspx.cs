using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webRefluxOrder : System.Web.UI.Page
    {
        public int pageid = 78;

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

        protected void ddlRefluxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfExchangeRequestHeaderId.Value = hfPurchaseHeaderId.Value = "0";
            gvExchangeRequestData.DataBind();
            //gvPurchaseDetailsData.DataBind();
            txtPurchaseOrExchangeOrderNo.Text = "";
            ddlDepartment.SelectedValue = ddlDivision.SelectedValue = ddlEmployee.SelectedValue = "0";
            txtDate.Text = "";

            if (ddlRefluxType.SelectedValue == "1")
                GetPurchaseOrderData();
            else if (ddlRefluxType.SelectedValue == "2")
                GetExchangeOrderData();

            GetNum();
        }

        private void GetPurchaseOrderData()
        {
            lblExchangeOrderType.Visible = ddlExchangeRequestType.Visible = false;
            ddlSupplierName.Visible = lblSupplierName.Visible = true;
            lblOrderName.Text = "رقم طلب الشراء";
            lblDate.Text = "تاريخ طلب الشراء";

            if (!string.IsNullOrEmpty(txtPurchaseOrExchangeOrderNo.Text))
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                #region GetPurchaseOrderHeader
                var Rows = from a in mdb.PurchaseOrderHeaders where a.PurchaseOrderNo == long.Parse(txtPurchaseOrExchangeOrderNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.PurchaseOrderHeader Headerdr = mdb.PurchaseOrderHeaders.Single(a => a.PurchaseOrderNo == long.Parse(txtPurchaseOrExchangeOrderNo.Text));
                    var DetailsRows = from a in mdb.PurchaseOredrDetails where a.Status != 0 && a.PurchaseOredeHeaderId == Headerdr.Id && a.ConformQty > 0 select a;
                    if (DetailsRows.Count() > 0)
                    {
                        ddlDepartment.SelectedValue = Headerdr.DepartmentId.ToString();
                        ddlDivision.DataBind();
                        ddlDivision.SelectedValue = Headerdr.DivisionId.ToString();
                        ddlEmployee.DataBind();
                        ddlEmployee.SelectedValue = Headerdr.EmployeeId.ToString();
                        ddlItemType.SelectedValue = Headerdr.ItemTypeId.ToString();
                        txtDate.Text = Headerdr.PurchaseDate.ToString("yyyy-MM-dd");
                        hfPurchaseHeaderId.Value = Headerdr.Id.ToString();
                        ddlSupplierName.SelectedValue = Headerdr.SupplierId.ToString();
                        hfExchangeRequestHeaderId.Value = "0";
                        //gvPurchaseDetailsData.DataBind();
                        gvExchangeRequestData.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('هذا الطلب لم يعتمد من قبل')</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('من تأكد من رقم طلب الشراء')</script>");
                    return;
                }
                #endregion
            }
        }

        private void GetExchangeOrderData()
        {
            lblExchangeOrderType.Visible = ddlExchangeRequestType.Visible = true;
            ddlSupplierName.Visible = lblSupplierName.Visible = false;
            lblOrderName.Text = "رقم أذن الصرف";
            lblDate.Text = "تاريخ طلب الصرف";

            if (!string.IsNullOrEmpty(txtPurchaseOrExchangeOrderNo.Text))
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                #region GetExchangeOrderHeader

                var Rows = from a in mdb.ExchangeRequestDetailsDatas
                           where a.ExchangeRequestOrder == long.Parse(txtPurchaseOrExchangeOrderNo.Text) &&
                           a.ExchangeRequestHeaderData.OrderType == int.Parse(ddlExchangeRequestType.SelectedValue) 
                           orderby a.Id descending
                           select a;

                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.ExchangeRequestHeaderData Headerdr = mdb.ExchangeRequestHeaderDatas.Single(a => a.Id == Rows.First().ExchangeRequestHeaderDataId);
                    ADAM.DataBase.division div = mdb.divisions.Single(a => a.Id == Headerdr.DivisionId); 
                    ddlDepartment.SelectedValue = div.DepartmentId.ToString();
                    ddlDivision.DataBind();
                    ddlDivision.SelectedValue = Headerdr.DivisionId.ToString();
                    ddlEmployee.DataBind();
                    ddlEmployee.SelectedValue = Headerdr.EmpId.ToString();
                    txtDate.Text = Headerdr.ExchangeRequestDate.ToString("yyyy-MM-dd");
                    hfPurchaseHeaderId.Value = "0";
                    hfExchangeRequestHeaderId.Value = Headerdr.Id.ToString();
                    gvExchangeRequestData.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('من تأكد من رقم طلب الصرف')</script>");
                    return;
                }
                #endregion
            }
        }

        private void GetNum()
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in mdb.RefluxHeaderDatas where a.OrderType == int.Parse(ddlRefluxType.SelectedValue) orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtRefluxNo.Text = "1";
            else
            {
                ADAM.DataBase.RefluxHeaderData dr = Rows.First();
                txtRefluxNo.Text = (dr.RefluxNo + 1).ToString();
            }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/StoreData/webRefluxOrder.aspx");
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

            EditData();
        }

        private void EditData()
        {
            if (ddlRefluxType.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك اختر نوع الصرف')</script>");
                return;
            }

            if (string.IsNullOrEmpty(txtRefluxNo.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم الارتجاع')</script>");
                return;
            }

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in mdb.RefluxHeaderDatas
                       where a.OrderType == int.Parse(ddlRefluxType.SelectedValue)
                           && a.RefluxNo == long.Parse(txtRefluxNo.Text)
                       select a;
            if (Rows.Count() > 0)
            {
                ADAM.DataBase.RefluxHeaderData dr = mdb.RefluxHeaderDatas.Single(a => a.OrderType == int.Parse(ddlRefluxType.SelectedValue)
                           && a.RefluxNo == long.Parse(txtRefluxNo.Text));
                RefluxHeaderId.Value = dr.Id.ToString();
                gvReflux.DataBind();
                if (dr.OrderType == 1)
                {
                    ADAM.DataBase.PurchaseOrderHeader Pdr = mdb.PurchaseOrderHeaders.Single(a => a.Id == dr.ExchangeOrPurchaseHeaderId);
                    txtPurchaseOrExchangeOrderNo.Text = Pdr.PurchaseOrderNo.ToString();
                    ddlItemType.SelectedValue = dr.ItemTypeId.ToString();
                    txtReason.Text = dr.Reason;
                    txtRefluxDate.Text = dr.RefluxDate.ToString("yyyy-MM-dd");
                    ShowData(dr.OrderType);

                }
            }
            else
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الارتجاع')</script>");
                return;
            }
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {

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

            ShowData(int.Parse(ddlRefluxType.SelectedValue));
            btnGetExchageNo.Visible = false;
        }

        private void ShowData(int OrderType)
        {

            if (OrderType == 1)
            {
                if (string.IsNullOrEmpty(txtPurchaseOrExchangeOrderNo.Text))
                {
                    Response.Write("<script>alert('من تأكد من رقم طلب الشراء')</script>");
                    return;
                }
                GetPurchaseOrderData();
            }
            else if (OrderType == 2)
            {
                if (string.IsNullOrEmpty(txtPurchaseOrExchangeOrderNo.Text))
                {
                    Response.Write("<script>alert('من تأكد من رقم طلب الصرف')</script>");
                    return;
                }
                if (ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من تأكد من المخزن')</script>");
                    return;
                }
                if (ddlExchangeRequestType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من تأكد من نوع طلب الصرف')</script>");
                    return;
                }
                GetExchangeOrderData();
            }
        }

        protected void gvPurchaseDetailsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            //ADAM.DataBase.PurchaseOredrDetail PurchaseDetailsdr = Mdb.PurchaseOredrDetails.Single(a => a.Id == long.Parse(gvPurchaseDetailsData.SelectedDataKey.Value.ToString()));
            //hfPurchaseDetailsId.Value = PurchaseDetailsdr.Id.ToString();
            //GetItemDatabyID(PurchaseDetailsdr.ItemId);
            //ddlItemColor.SelectedValue = PurchaseDetailsdr.ItemColorId.ToString();
            //txtQty.Text = PurchaseDetailsdr.ConformQty.ToString();
            //txtFreeQty.Text = "0";
        }

        private void GetItemDatabyID(long itemID)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Item itmdr = Mdb.Items.Single(a => a.Id == itemID);
                txtItemCode.Text = itmdr.Code.ToString();
                ddlItemName.SelectedValue = itmdr.Id.ToString();
                ADAM.DataBase.ItemUnit unitdr = Mdb.ItemUnits.Single(a => a.Id == itmdr.ItemunitId);
                lblItemUnit.Text = unitdr.Name;
                lblLimitQty.Text = itmdr.LimitQty.ToString();
                ADAM.DataBase.SexData sexdr = Mdb.SexDatas.Single(a => a.Id == itmdr.Sex);
                lblSex.Text = sexdr.Sex;
                ADAM.DataBase.ItemStatus itmstatsudr = Mdb.ItemStatus.Single(a => a.Id == itmdr.ItemStatus);
                lblItemstatus.Text = itmstatsudr.Status;
            }
            catch
            {
                Response.Write("<script>alert('نأسف لقد حدث خطأ أثناء تحميل البيانات')</script>");
                return; }
        }

        protected void gvExchangeRequestData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ExchangeRequestDetailsData Detailsdr = mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == long.Parse(gvExchangeRequestData.SelectedDataKey.Value.ToString()));

            hfExchangeRequestDetailsId.Value = Detailsdr.Id.ToString();
            ddlItemName.DataBind();
            GetItemDatabyID(Detailsdr.ItemId);
            ddlItemColor.SelectedValue = Detailsdr.ItemColorId.ToString();
            txtQty.Text = Detailsdr.Qty.ToString();
            txtFreeQty.Text = Detailsdr.FreeQty.ToString();
            hfIncommingOrder.Value = Detailsdr.IncommingOrderNo.ToString();
        }

        protected void btnSaveOrderItem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ddlRefluxType.Enabled = txtRefluxNo.Enabled = ddlItemType.Enabled = txtPurchaseOrExchangeOrderNo.Enabled = false;
                SaveHeaderData();
            }
            catch { }
        }

        private void SaveHeaderData()
        {
            if (string.IsNullOrEmpty(txtRefluxNo.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل سبب الارتجاع')</script>");
                return;
            }

            if (string.IsNullOrEmpty(txtRefluxDate.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل تاريخ الارتجاع')</script>");
                return;
            }

            if (decimal.Parse(txtRefluxQty.Text) > decimal.Parse(txtQty.Text))
            {
                Response.Write("<script>alert('لا يمكن ان تكون الكمية المرتجعة اكبر من الكمية المتاحه للارتجاع')</script>");
                return;
            }

            if (decimal.Parse(txtFreeRefluxQty.Text) > decimal.Parse(txtFreeQty.Text))
            {
                Response.Write("<script>alert('لا يمكن ان تكون الكمية المرتجعة اكبر من الكمية المتاحه للارتجاع')</script>");
                return;
            }

            if (decimal.Parse(txtRefluxQty.Text) <= 0)
            {
                Response.Write("<script>alert('من فضلك ادخل كمية الارتجاع بشكل صحيح')</script>");
                return;
            }

            if (RefluxHeaderId.Value == "0")
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.RefluxHeaderData Headerdr = new DataBase.RefluxHeaderData();
                Headerdr.ItemTypeId = int.Parse(ddlItemType.SelectedValue);
                Headerdr.OrderType = int.Parse(ddlRefluxType.SelectedValue);
                Headerdr.Reason = txtReason.Text;
                Headerdr.RecoredDate = DateTime.Now;
                Headerdr.RefluxDate = DateTime.Parse(txtRefluxDate.Text);
                Headerdr.RefluxNo = long.Parse(txtRefluxNo.Text);

                if (ddlRefluxType.SelectedValue == "1" && ddlSupplierName.SelectedValue != "0")
                {
                    Headerdr.SupplierOrDepartmentId = long.Parse(ddlSupplierName.SelectedValue);
                    Headerdr.ExchangeOrPurchaseHeaderId = long.Parse(hfPurchaseHeaderId.Value);
                }
                else if (ddlSupplierName.SelectedValue == "0" && ddlRefluxType.SelectedValue == "2")
                {
                    Headerdr.SupplierOrDepartmentId = long.Parse(ddlDepartment.SelectedValue);
                    Headerdr.ExchangeOrPurchaseHeaderId = long.Parse(hfExchangeRequestHeaderId.Value);
                }

                Headerdr.EmployeeId = long.Parse(ddlEmployee.SelectedValue);

                mdb.RefluxHeaderDatas.Add(Headerdr);
                mdb.SaveChanges();
                RefluxHeaderId.Value = Headerdr.Id.ToString();
            }
            InsertRefluxDetailsData(long.Parse(RefluxHeaderId.Value));
        }

        private void InsertRefluxDetailsData(long RefluxHeader)
        {
            if (RefluxHeaderId.Value == "0")
                SaveHeaderData();
            else
            {

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.RefluxDetailsData Detailsdr = new DataBase.RefluxDetailsData();

                if (hfExchangeRequestDetailsId.Value != "0")
                    Detailsdr.ExchangeOrPurchaseDetailsId = long.Parse(hfExchangeRequestDetailsId.Value);
                else if (hfPurchaseDetailsId.Value != "0")
                    Detailsdr.ExchangeOrPurchaseDetailsId = long.Parse(hfPurchaseDetailsId.Value);

                Detailsdr.RefluxHeaderId = RefluxHeader;

                Detailsdr.RefluxQty = decimal.Parse(txtRefluxQty.Text);
                Detailsdr.RefluxFreeQty = decimal.Parse(txtFreeRefluxQty.Text);

                var DetailsRow = from a in Mdb.RefluxDetailsDatas
                                 where a.ExchangeOrPurchaseDetailsId == Detailsdr.ExchangeOrPurchaseDetailsId && a.RefluxHeaderId == RefluxHeader
                                 && a.ItemId == long.Parse(ddlItemName.SelectedValue) && a.ItemColorId == int.Parse(ddlItemColor.SelectedValue)
                                 select a;

                if (DetailsRow.Count() > 0)
                {
                    Response.Write("<script>alert('لقد تم حفظ الارتجاع من قبل')</script>");
                    return;
                }

                Detailsdr.ItemId = long.Parse(ddlItemName.SelectedValue);
                Detailsdr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
                Detailsdr.Status = 0;
                Detailsdr.IncommingOrderNo = long.Parse(hfIncommingOrder.Value);

                var AllDetailsRow = from a in Mdb.RefluxDetailsDatas
                                    where a.ExchangeOrPurchaseDetailsId == Detailsdr.ExchangeOrPurchaseDetailsId && a.ItemId == long.Parse(ddlItemName.SelectedValue)
                                    && a.ItemColorId == int.Parse(ddlItemColor.SelectedValue)
                                    select a;

                if (AllDetailsRow.Count() > 0)
                {
                    decimal AllQty = 0;
                    foreach (ADAM.DataBase.RefluxDetailsData ddr in AllDetailsRow)
                        AllQty = AllQty + ddr.RefluxQty;

                    if ((AllQty + decimal.Parse(txtRefluxQty.Text)) > decimal.Parse(txtQty.Text))
                    {
                        Response.Write("<script>alert('لا يمكن ان تكون اجمالي الكميات المرتجعة اكبر من كمية الطلب')</script>");
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtBouncefluxQty.Text))
                    txtBouncefluxQty.Text = "0";

                Detailsdr.Bounce = decimal.Parse(txtBouncefluxQty.Text);

                Mdb.RefluxDetailsDatas.Add(Detailsdr);
                Mdb.SaveChanges();
                gvReflux.DataBind();
            }
        }

        protected void gvExchangeRequestOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvExchangeRequestOrder.Visible = false;
            dvInsertData.Visible = true;
            txtPurchaseOrExchangeOrderNo.Text = gvExchangeRequestOrder.SelectedRow.Cells[0].Text;
        }

        protected void btnGetExchageNo_Click(object sender, EventArgs e)
        {
            dvExchangeRequestOrder.Visible = true;
            dvInsertData.Visible = false;
        }
    }
}