using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.PurchaseData
{
    public partial class webFollowUpPurchaseOrder : System.Web.UI.Page
    {
        public int pageid = 37;
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

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 3;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            gvBrowsePurchaseOrder.Visible = true;
            gvBrowsePurchaseOrder.DataBind();
        }

        protected void gvBrowsePurchaseOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                if (e.Row.Cells[14].Text == "5") e.Row.Cells[14].Text = "علي امر توريد";
                else if (e.Row.Cells[14].Text == "6") e.Row.Cells[14].Text = "تحت الفحص";
                else if (e.Row.Cells[14].Text == "7") e.Row.Cells[14].Text = "داخل المخزن";
                else if (e.Row.Cells[14].Text == "8") e.Row.Cells[14].Text = "تم الرفض بعد الفحص";
                else if (e.Row.Cells[14].Text == "0") e.Row.Cells[14].Text = "علي طلب شراء";
            }
        }
    }
}