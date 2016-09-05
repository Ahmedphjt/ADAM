using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Prodction
{
    public partial class webProductionOrder : System.Web.UI.Page
    {
        public int pageid = 93;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 4;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");
            if (!IsPostBack)
                GetNewOrderNo();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Prodction/webProductionOrder.aspx");
            GetNewOrderNo();
        }

        private void GetNewOrderNo()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in mdb.ProductionHeaderOrders orderby a.ProductionNo descending select a;
                if (Rows.Count() <= 0)
                    txtOrderNo.Text = "1";
                else
                {
                    txtOrderNo.Text = (Rows.First().ProductionNo + 1).ToString();
                }
            }
            catch { }
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

            if (string.IsNullOrEmpty(txtOrderNo.Text))
            {
                Response.Write("<script>alert('من فضلك تأكد من ادخال رقم الطلب')</script>");
                return;
            }
            
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Row = from a in mdb.ProductionHeaderOrders where a.ProductionNo == long.Parse(txtOrderNo.Text) select a;
            if (Row.Count() > 0)
            {
                ADAM.DataBase.ProductionHeaderOrder headerdr = mdb.ProductionHeaderOrders.Single(a => a.ProductionNo == long.Parse(txtOrderNo.Text));
                hfHeaderId.Value = headerdr.Id.ToString();
                txtDate.Text = headerdr.ProductionDate.ToString("yyyy-MM-dd");

                for (int GRow = 0; GRow < gvProductionOrderData.Rows.Count; GRow++)
                {
                    TextBox txtQty = gvProductionOrderData.Rows[GRow].FindControl("txtQty") as TextBox;
                    long ItemContentHeaderId = long.Parse(gvProductionOrderData.DataKeys[GRow].Value.ToString());

                    var dRows = from a in mdb.ProductionDetailsOrders
                                where a.ContentHeaderId == ItemContentHeaderId
                                    && a.ProductionHeaderOrderId == long.Parse(hfHeaderId.Value)
                                select a;
                    if (dRows.Count() > 0)
                    {
                        ADAM.DataBase.ProductionDetailsOrder ddr = mdb.ProductionDetailsOrders.Single(a => a.ContentHeaderId == ItemContentHeaderId
                            && a.ProductionHeaderOrderId == long.Parse(hfHeaderId.Value));
                        txtQty.Text = ddr.Qty.ToString();
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الطلب')</script>");
                return;
            }
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

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ProductionHeaderOrder headerdr = new DataBase.ProductionHeaderOrder();
            if (hfHeaderId.Value != "0")
                headerdr = mdb.ProductionHeaderOrders.Single(a => a.Id == long.Parse(hfHeaderId.Value));
            else
            {
                Response.Write("<script>alert('لا يمكن التعديل الا بعد الحفظ في قاعدة البيانات')</script>");
                return;
            }
            for (int GRow = 0; GRow < gvProductionOrderData.Rows.Count; GRow++)
            {
                TextBox txtQty = gvProductionOrderData.Rows[GRow].FindControl("txtQty") as TextBox;
                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    if (decimal.Parse(txtQty.Text) <= 0)
                    {
                        Response.Write("<script>alert('من فضلك يجب ان تكون الكمية اكبر من الصفر')</script>");
                        return;
                    }

                    long ItemContentHeaderId = long.Parse(gvProductionOrderData.DataKeys[GRow].Value.ToString());

                    ADAM.DataBase.ProductionDetailsOrder detailsdr = mdb.ProductionDetailsOrders.Single(a => a.Status == 0 && a.ContentHeaderId == ItemContentHeaderId
                        && a.ProductionHeaderOrderId == long.Parse(hfHeaderId.Value) && a.ProductionHeaderOrder.ProductionNo == long.Parse(txtOrderNo.Text));

                    detailsdr.Qty = decimal.Parse(txtQty.Text);
                }
            }
            mdb.SaveChanges();
            btnNew_Click(sender, e);
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

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ProductionHeaderOrder headerdr = new DataBase.ProductionHeaderOrder();
            if (hfHeaderId.Value != "0")
                headerdr = mdb.ProductionHeaderOrders.Single(a => a.Id == long.Parse(hfHeaderId.Value));
            else
            {
                headerdr.ProductionDate = DateTime.Now;
                headerdr.ProductionNo = long.Parse(txtOrderNo.Text);
                mdb.ProductionHeaderOrders.Add(headerdr);
                mdb.SaveChanges();
                hfHeaderId.Value = headerdr.Id.ToString();
            }

            for (int GRow = 0; GRow < gvProductionOrderData.Rows.Count; GRow++)
            {
                TextBox txtQty = gvProductionOrderData.Rows[GRow].FindControl("txtQty") as TextBox;
                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    if (decimal.Parse(txtQty.Text) <= 0)
                    {
                        Response.Write("<script>alert('من فضلك يجب ان تكون الكمية اكبر من الصفر')</script>");
                        return;
                    }

                    long ItemContentHeaderId = long.Parse(gvProductionOrderData.DataKeys[GRow].Value.ToString());

                    ADAM.DataBase.ProductionDetailsOrder detailsdr = new DataBase.ProductionDetailsOrder();
                    detailsdr.ProductionHeaderOrderId = long.Parse(hfHeaderId.Value);
                    detailsdr.ContentHeaderId = ItemContentHeaderId;
                    detailsdr.Qty = decimal.Parse(txtQty.Text);
                    detailsdr.Status = 0;
                    mdb.ProductionDetailsOrders.Add(detailsdr);

                }
            }
            mdb.SaveChanges();
            btnNew_Click(sender, e);
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
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}