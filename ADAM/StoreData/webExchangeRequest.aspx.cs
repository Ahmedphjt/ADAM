using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webExchangeRequest : System.Web.UI.Page
    {
        public int pageid = 71;

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
            Response.Redirect("~/StoreData/webExchangeRequest.aspx");
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
                GetExchangeRequestCode();

                if (string.IsNullOrEmpty(txtDate.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل تاريخ طلب الصرف')</script>");
                    return;
                }

                if (ddlExchangeRequestType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر نوع الصرف')</script>");
                    return;
                }

                if (ddlDepartment.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل الادارة')</script>");
                    return;
                }

                if (ddlDivision.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل القسم')</script>");
                    return;
                }

                if (ddlEmployee.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل الموظف')</script>");
                    return;
                }

                if (ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل نوع الصنف')</script>");
                    return;
                }

                if (ddlClient.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل العميل')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.ExchangeRequestHeaderDatas
                              where a.ExchangeRequestNo == long.Parse(txtExchangeRequestNo.Text) && a.OrderType == int.Parse(ddlExchangeRequestType.SelectedValue)
                             
                              select a;
                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن تكرار رقم طلب الصرف')</script>");
                    return;
                }

               // ddlItemType.Enabled = false;

                SaveData();
            }
            catch { }
        }

        #endregion

        #region Function

        private void EditData()
        {
            Response.Redirect("~/StoreData/webUpdateExchangeRequest.aspx");
        }

        private void SaveData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ExchangeRequestHeaderData dr = new DataBase.ExchangeRequestHeaderData();

                dr.ClientId = long.Parse(ddlClient.SelectedValue);
                dr.DivisionId = long.Parse(ddlDivision.SelectedValue);
                dr.EmpId = long.Parse(ddlEmployee.SelectedValue);
                
                dr.ExchangeRequestDate = DateTime.Parse(txtDate.Text);
                dr.ExchangeRequestNo = long.Parse(txtExchangeRequestNo.Text);
                dr.RecoredDate = DateTime.Now;
                dr.OrderType = int.Parse(ddlExchangeRequestType.SelectedValue);

                dr.Posted = 0;
                Mdb.ExchangeRequestHeaderDatas.Add(dr);
                Mdb.SaveChanges();
                hfId.Value = dr.Id.ToString();
                Mdb.SaveChanges();
                disableControl();
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void disableControl()
        {
            ddlClient.Enabled = ddlDivision.Enabled = ddlEmployee.Enabled = txtDate.Enabled = txtExchangeRequestNo.Enabled = ddlExchangeRequestType.Enabled =
             ddlDepartment.Enabled = false;
            btnClient.Visible = false;
        }

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
        }

        protected void btnGetItemData_Click(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                if (ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر المخزن')</script>");
                    return;
                }
                var Rows = from a in Mdb.Items where a.Code == long.Parse(txtItemCode.Text) select a;
                if (Rows.Count() <= 0)
                {
                    Response.Write("<script>alert('هذا الكود غير موجود بقاعدة البيانات')</script>");
                    return;
                }
                else
                {
                    ADAM.DataBase.Item itmdr = Mdb.Items.Single(a => a.Code == long.Parse(txtItemCode.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue));
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
            if (ddlItemType.SelectedValue == "0")
            {
                ddlItemName.SelectedValue = "0";
                Response.Write("<script>alert('من فضلك اختر المخزن')</script>");
                return;
            }
            GetItemDatabyID(long.Parse(ddlItemName.SelectedValue));
        }

        protected void btnSaveExchangeRequerstItem_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBounce.Text == "") txtBounce.Text = "0";
            if (txtFreeQty.Text == "") txtFreeQty.Text = "0";

            if ((decimal.Parse(txtQty.Text) + decimal.Parse(txtBounce.Text)) > decimal.Parse(lblCurrentBalance.Text))
            {
                Response.Write("<script>alert('لا يمكن ان تكون الكمية المصروفه اكبر من الكمية الحالية')</script>");
                return;
            }

            if (decimal.Parse(txtFreeQty.Text) > decimal.Parse(lblFreeQty.Text))
            {
                Response.Write("<script>alert('لا يمكن ان تكون الكمية المجانية المصروفه اكبر من الكمية الحالية')</script>");
                return;
            }

            if (hfId.Value == "0")
            {
                //ddlItemType.Enabled = false;
                btnSave_Click(sender, e);

                SaveExchangeRequestDetailsData(long.Parse(hfId.Value));
            }
            else
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
            Detailsdr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
            Detailsdr.Bounce = decimal.Parse(txtBounce.Text);
            Detailsdr.ItemColorId = int.Parse(ddlItemColor.Text);
            Detailsdr.ProdctionLineId = int.Parse(ddlProdctionLine.SelectedValue);
            
            if (chkPrice.Checked)
                Detailsdr.PriceTester = 1;
            else
                Detailsdr.PriceTester = 0;

            Mdb.ExchangeRequestDetailsDatas.Add(Detailsdr);
            Mdb.SaveChanges();
            gvExchangeRequestData.DataBind();
        }

        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hfId.Value == "0")
                GetExchangeRequestCode();
        }

        private void GetExchangeRequestCode()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.ExchangeRequestHeaderDatas
                       where  a.OrderType == long.Parse(ddlExchangeRequestType.SelectedValue)
                       orderby a.Id descending
                       select a;
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
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                decimal CurrentBalance = 0;
                decimal FreeBalance = 0;
                var Rows = from a in Mdb.ItemMovements where a.ItemId == long.Parse(ddlItemName.SelectedValue) && a.StoreId == 2 && a.ItemColorId == int.Parse(ddlItemColor.SelectedValue) select a;
                foreach (ADAM.DataBase.ItemMovement itmmovdr in Rows)
                {
                    CurrentBalance = CurrentBalance + itmmovdr.MainQty;
                    FreeBalance = FreeBalance + itmmovdr.AdditionalQty;
                }
                lblCurrentBalance.Text = CurrentBalance.ToString();
                lblFreeQty.Text = FreeBalance.ToString();
            }
            catch { }
        }

        protected void ddlExchangeRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemType.SelectedValue != "0")
                ddlItemType_SelectedIndexChanged(sender, e);
        }

        protected void btnClient_Click(object sender, EventArgs e)
        {
            data.Visible = false;
            Clients.Visible = true;
        }

        protected void gvClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clients.Visible = false;
            data.Visible = true;
            ddlClient.SelectedValue = gvClient.SelectedDataKey.Value.ToString();
        }

        protected void gvClient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {

            }
        }
    }
}