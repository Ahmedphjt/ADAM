using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.PurchaseData
{
    public partial class webPricingSupplyOrder : System.Web.UI.Page
    {
        public int pageid = 151;
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
            Response.Redirect("~/PurchaseData/webPricingSupplyOrder.aspx");
        }

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {
            No.Value = txtSupplyOrder.Text;
            gvSupplyOrder.DataBind();
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {

            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 7;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();

            for (int Row = 0; Row < gvSupplyOrder.Rows.Count; Row++)
            {
                TextBox txtPrice = gvSupplyOrder.Rows[Row].FindControl("txtPrice") as TextBox;
                if (!string.IsNullOrEmpty(txtPrice.Text))
                {
                    long SupplyOrderDetailsId = long.Parse(gvSupplyOrder.DataKeys[Row].Value.ToString());
                    ADAM.DataBase.SupplyOrderDetail dr = db.SupplyOrderDetails.Single(a => a.Id == SupplyOrderDetailsId);
                    dr.ItemPrice = decimal.Parse(txtPrice.Text);
                }
            }
            db.SaveChanges();
            gvSupplyOrder.DataBind();
        }
    }
}