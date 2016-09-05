using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webUpdateExchangeRequest : System.Web.UI.Page
    {
        public int pageid = 72;

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

        #region btnFunction

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/StoreData/webUpdateExchangeRequest.aspx");
        }

        #endregion

        #region Function

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.ExchangeRequestHeaderDatas where a.ExchangeRequestNo == long.Parse(txtExchangeRequestNo.Text) select a;
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

            //Response.Redirect("~/PurchaseReport/webPurchaseOrderRepot.aspx");
        }

        protected void btnGetItemData_Click(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
              
                var Rows = from a in Mdb.Items where a.Code == long.Parse(txtItemCode.Text) select a;
                if (Rows.Count() <= 0)
                {
                    Response.Write("<script>alert('هذا الكود غير موجود بقاعدة البيانات')</script>");
                    return;
                }
                else
                {
                    ADAM.DataBase.Item itmdr = Mdb.Items.Single(a => a.Code == long.Parse(txtItemCode.Text));
                    GetItemDatabyID(itmdr.Id);
                }
            }
        }

        private void GetItemDatabyID(long itemID)
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

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlItemColor.SelectedValue = "0";
          
            GetItemDatabyID(long.Parse(ddlItemName.SelectedValue));
        }

        protected void btnSaveExchangeRequerstItem_Click(object sender, ImageClickEventArgs e)
        {
            if (decimal.Parse(txtQty.Text) > decimal.Parse(lblCurrentBalance.Text))
            {
                Response.Write("<script>alert('لا يمكن ان تكون الكمية المصروفه اكبر من الكمية الحالية')</script>");
                return;
            }

            SaveExchangeRequestDetailsData(long.Parse(hfId.Value));
        }

        private void SaveExchangeRequestDetailsData(long ExchangeRequestHeaderID)
        {
            if (ExchangeRequestHeaderID == 0)
                return;

            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ExchangeRequestDetailsData Detailsdr = new DataBase.ExchangeRequestDetailsData();

            Detailsdr.ExchangeRequestHeaderDataId = ExchangeRequestHeaderID;
            Detailsdr.ItemId = long.Parse(ddlItemName.SelectedValue);
            Detailsdr.Qty = decimal.Parse(txtQty.Text);
            Detailsdr.FreeQty = decimal.Parse(txtFreeQty.Text);
            Detailsdr.Note = txtExchangeRequestNote.Text;
            Detailsdr.Status = 0;
            Detailsdr.IncommingOrderId = 0;
            Detailsdr.LocationId = 0;
            Detailsdr.ItemColorId = int.Parse(ddlItemColor.Text);
            Detailsdr.Bounce = decimal.Parse(txtBounce.Text);
            Mdb.ExchangeRequestDetailsDatas.Add(Detailsdr);
            Mdb.SaveChanges();
            gvExchangeRequestData.DataBind();
        }

        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetExchangeRequestCode();
        }

        private void GetExchangeRequestCode()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.ExchangeRequestHeaderDatas where a.OrderType == long.Parse(ddlExchangeRequestType.SelectedValue) orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtExchangeRequestNo.Text = "1";
            else
            {
                ADAM.DataBase.ExchangeRequestHeaderData dr = Rows.First();
                txtExchangeRequestNo.Text = (dr.ExchangeRequestNo + 1).ToString();
            }
        }

        protected void ddlItemColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetCurrentBalance();
            }
            catch { }
        }

        private void GetCurrentBalance()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            decimal CurrentBalance = 0;
            decimal FreeQty = 0;
            var Rows = from a in Mdb.ItemMovements where a.ItemId == long.Parse(ddlItemName.SelectedValue) && a.StoreId == 2 && a.ItemColorId == int.Parse(ddlItemColor.SelectedValue) select a;
            foreach (ADAM.DataBase.ItemMovement itmmovdr in Rows)
            {
                CurrentBalance = CurrentBalance + itmmovdr.MainQty;
                FreeQty = FreeQty + itmmovdr.AdditionalQty;
            }
            lblCurrentBalance.Text = CurrentBalance.ToString();
            lblFreeQty.Text = FreeQty.ToString();
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

            if (string.IsNullOrEmpty(txtExchangeRequestNo.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم طلب الصرف')</script>");
                return;
            }

             txtExchangeRequestNo.Enabled = false;

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var HRows = from a in mdb.ExchangeRequestHeaderDatas
                        where a.OrderType == int.Parse(ddlExchangeRequestType.SelectedValue)
                            && a.ExchangeRequestNo == int.Parse(txtExchangeRequestNo.Text)
                        select a;
            if (HRows.Count() > 0)
            {
                ADAM.DataBase.ExchangeRequestHeaderData Hdr = mdb.ExchangeRequestHeaderDatas.Single(a => a.ExchangeRequestNo == int.Parse(txtExchangeRequestNo.Text)
                    && a.OrderType == int.Parse(ddlExchangeRequestType.SelectedValue));
                hfId.Value = Hdr.Id.ToString();
                ddlClient.SelectedValue = Hdr.ClientId.ToString();
                ADAM.DataBase.division Ddr = mdb.divisions.Single(a => a.Id == Hdr.DivisionId);
                ADAM.DataBase.Department Depdr = mdb.Departments.Single(a => a.Id == Ddr.DepartmentId);
                ddlDepartment.SelectedValue = Depdr.Id.ToString();
                ddlDivision.DataBind();
                ddlDivision.SelectedValue = Ddr.Id.ToString();
                ddlEmployee.DataBind();
                ddlEmployee.SelectedValue = Hdr.EmpId.ToString();
                txtDate.Text = Hdr.ExchangeRequestDate.ToString("yyyy-MM-dd");
                //gvExchangeRequestData.DataBind();
            }
        }

        protected void gvExchangeRequestData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ExchangeRequestDetailsData Detailsdr = mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == long.Parse(gvExchangeRequestData.SelectedDataKey.Value.ToString()));
            ADAM.DataBase.Item itmdr = mdb.Items.Single(a => a.Id == Detailsdr.ItemId);
            txtItemCode.Text = itmdr.Code.ToString();
            ddlItemName.SelectedValue = itmdr.Id.ToString();
            ADAM.DataBase.ItemUnit Unitdr = mdb.ItemUnits.Single(a => a.Id == itmdr.ItemunitId);
            txtFreeQty.Text = Detailsdr.FreeQty.ToString();
            lblItemUnit.Text = Unitdr.Name;
            lblLimitQty.Text = itmdr.LimitQty.ToString();
            ddlItemColor.SelectedValue = Detailsdr.ItemColorId.ToString();
            ADAM.DataBase.ItemStatus stdr = mdb.ItemStatus.Single(a => a.Id == itmdr.ItemStatus);
            lblItemstatus.Text = stdr.Status;
            txtBounce.Text = Detailsdr.Bounce.ToString();
            ADAM.DataBase.SexData sexdr = mdb.SexDatas.Single(a => a.Id == itmdr.Sex);
            lblSex.Text = sexdr.Sex;
            txtQty.Text = Detailsdr.Qty.ToString();
            txtExchangeRequestNote.Text = Detailsdr.Note;
            hfDetailsId.Value = Detailsdr.Id.ToString();
            GetCurrentBalance();
        }

        protected void btnEditOrderItem_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 2;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");


            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ExchangeRequestDetailsData Detailsdr = Mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == long.Parse(hfDetailsId.Value));
            if (Detailsdr.Status == 0)
            {
                if (decimal.Parse(txtQty.Text) > decimal.Parse(lblCurrentBalance.Text))
                {
                    Response.Write("<script>alert('لا يمكن ان تكون الكمية المصروفه اكبر من الرصيد الحالي')</script>");
                    return;
                }
                Detailsdr.ItemId = long.Parse(ddlItemName.SelectedValue);
                Detailsdr.Qty = decimal.Parse(txtQty.Text);
                Detailsdr.FreeQty = decimal.Parse(txtFreeQty.Text);
                Detailsdr.Note = txtExchangeRequestNote.Text;
                Detailsdr.ItemColorId = int.Parse(ddlItemColor.Text);
                Mdb.SaveChanges();
                gvExchangeRequestData.DataBind();
            }
            else
            {
                Response.Write("<script>alert('لقد تم صرف هذا الصنف  من المخزن')</script>");
                return;
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 6;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");


            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ExchangeRequestDetailsData Detailsdr = Mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == long.Parse(hfDetailsId.Value));
            if (Detailsdr.Status == 0)
            {
                Mdb.ExchangeRequestDetailsDatas.Remove(Detailsdr);
                Mdb.SaveChanges();
                gvExchangeRequestData.DataBind();

                var Rows = from a in Mdb.ExchangeRequestDetailsDatas where a.ExchangeRequestHeaderDataId == long.Parse(hfId.Value) select a;
                if (Rows.Count() <= 0)
                {
                    ADAM.DataBase.ExchangeRequestHeaderData Hdr = Mdb.ExchangeRequestHeaderDatas.Single(a => a.Id == long.Parse(hfId.Value));
                    Mdb.ExchangeRequestHeaderDatas.Remove(Hdr);
                    Mdb.SaveChanges();  
                    Response.Redirect("~/StoreData/webUpdateExchangeRequest.aspx");
                }
            }
            else
            {
                Response.Write("<script>alert('لقد تم صرف هذا الصنف  من المخزن')</script>");
                return;
            }
        }
    }
}