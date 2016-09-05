using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webUpdateRefluxOrederData : System.Web.UI.Page
    {
        public int pageid = 79;

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
            Response.Redirect("~/StoreData/webUpdateRefluxOrederData.aspx");
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

            ShowData();
        }

        private void ShowData()
        {
            try
            {
                if (ddlRefluxType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر نوع طلب الارتجاع')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtRefluxNo.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل رقم الطلب')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in mdb.RefluxHeaderDatas
                           where a.RefluxNo == long.Parse(txtRefluxNo.Text) && a.OrderType == int.Parse(ddlRefluxType.SelectedValue)
                           select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.RefluxHeaderData headerdr = mdb.RefluxHeaderDatas.Single(a => a.RefluxNo == long.Parse(txtRefluxNo.Text) && a.OrderType == int.Parse(ddlRefluxType.SelectedValue));
                    RefluxHeaderId.Value = headerdr.Id.ToString();
                    gvReflux.DataBind();
                }
            }
            catch { }
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

        protected void gvReflux_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.RefluxDetailsData dr = mdb.RefluxDetailsDatas.Single(a => a.Id == long.Parse(gvReflux.SelectedDataKey.Value.ToString()));
            ADAM.DataBase.Item itmdr = mdb.Items.Single(a => a.Id == dr.ItemId);
            ADAM.DataBase.ItemColor itmcolordr = mdb.ItemColors.Single(a => a.Id == dr.ItemColorId);
            ADAM.DataBase.ItemUnit unitdr = mdb.ItemUnits.Single(a => a.Id == itmdr.ItemunitId);
            txtItemCode.Text = itmdr.Code.ToString();
            ddlItemName.SelectedValue = itmdr.Id.ToString();
            ddlItemColor.SelectedValue = itmcolordr.Id.ToString();
            txtQty.Text = dr.RefluxQty.ToString();
            txtFreeQty.Text = dr.RefluxFreeQty.ToString();
            lblItemUnit.Text = unitdr.Name;
            RefluxDetailsId.Value = dr.Id.ToString();
        }

        protected void btnEditOrderItem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Session["UserID"] == null)
                    Response.Redirect("~/BasicData/webLogIn.aspx");
                int userid = int.Parse(Session["UserID"].ToString());
                int operationid = 2;

                csGetPermission Per = new csGetPermission();
                if (!Per.getPermission(userid, pageid, operationid))
                    Response.Redirect("~/BasicData/webHomePage.aspx");

                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.RefluxDetailsData ddr = mdb.RefluxDetailsDatas.Single(a => a.Id == long.Parse(RefluxDetailsId.Value));
                if (ddr.RefluxHeaderData.OrderType == 1)
                {
                    ADAM.DataBase.PurchaseOredrDetail purchasedr = mdb.PurchaseOredrDetails.Single(a => a.Id == ddr.ExchangeOrPurchaseDetailsId);
                    if (decimal.Parse(txtQty.Text) > purchasedr.ConformQty)
                    {
                        Response.Write("<script>alert('لا يمكن ان تكون الكمية المرتجعة اكبر من الكمية الموجوده في طلب الشراء')</script>");
                        return;
                    }
                }

                if (ddr.RefluxHeaderData.OrderType == 2)
                {
                    ADAM.DataBase.ExchangeRequestDetailsData detailsdr = mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == ddr.ExchangeOrPurchaseDetailsId);
                    if (decimal.Parse(txtQty.Text) > detailsdr.Qty)
                    {
                        Response.Write("<script>alert('لا يمكن ان تكون الكمية المرتجعة اكبر من الكمية الموجوده في طلب الصرف')</script>");
                        return;
                    }

                    if (decimal.Parse(txtFreeQty.Text) > detailsdr.FreeQty)
                    {
                        Response.Write("<script>alert('لا يمكن ان تكون الكمية المجانية المرتجعة اكبر من الكمية الموجوده في طلب الصرف')</script>");
                        return;
                    }
                }

                ddr.RefluxQty = decimal.Parse(txtQty.Text);
                ddr.RefluxFreeQty = decimal.Parse(txtFreeQty.Text);

                mdb.SaveChanges();
                gvReflux.DataBind();
            }
            catch { }
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

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.RefluxDetailsData ddr = mdb.RefluxDetailsDatas.Single(a => a.Id == long.Parse(RefluxDetailsId.Value));
            long HeaderId = ddr.RefluxHeaderId;
            mdb.RefluxDetailsDatas.Remove(ddr);
            mdb.SaveChanges();
            var Rows = from a in mdb.RefluxHeaderDatas where a.Id == HeaderId select a;
            if (Rows.Count() <= 0)
            {
                ADAM.DataBase.RefluxHeaderData dr = mdb.RefluxHeaderDatas.Single(a => a.Id == HeaderId);
                mdb.RefluxHeaderDatas.Remove(dr);
                mdb.SaveChanges();
            }
        }
    }
}