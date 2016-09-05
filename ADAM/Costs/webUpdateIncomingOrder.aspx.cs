using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Costs
{
    public partial class webUpdateIncomingOrder : System.Web.UI.Page
    {
        public int pageid = 39;

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

        protected void gvPurchseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSupplyOrderNo.Text = gvPurchseOrder.SelectedRow.Cells[5].Text;
            divData.Visible = true;
            divPurchseOrder.Visible = false;
        }

        protected void btnShowDiv_Click(object sender, EventArgs e)
        {
            divData.Visible = false;
            divPurchseOrder.Visible = true;
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Costs/webUpdateIncomingOrder.aspx");
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

            txtSupplyOrderNo.Enabled = false;
            ShowData();
        }

        private void ShowData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in Mdb.SupplyOrderHeaders where a.SupplyOrderNo == long.Parse(txtSupplyOrderNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.SupplyOrderHeader dr = Mdb.SupplyOrderHeaders.Single(a => a.SupplyOrderNo == long.Parse(txtSupplyOrderNo.Text));
                    txtDate.Text = dr.SupplyOrderDate.ToString("yyyy-MM-dd");
                    ddlSupplier.SelectedValue = dr.SupplierId.ToString();
                    txtSupplierCode.Text = Mdb.SupplierDatas.Single(a => a.Id == dr.SupplierId).Code.ToString();
                    gvSupplyOrder.DataBind();
                    gvSupplyOrder.Visible = true;
                }
                else { Response.Write("<script>alert('من فضلك تأكد من رقم امر التوريد')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        protected void gvSupplyOrder_SelectedIndexChanged(object sender, EventArgs e)
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
                if (string.IsNullOrEmpty(txtSupplyOrderNo.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل كود امر التوريد')</script>");
                    return;
                }

                TextBox txtQtyPrice = gvSupplyOrder.SelectedRow.FindControl("txtQtyPrice") as TextBox;
                TextBox txtFQtyPrice = gvSupplyOrder.SelectedRow.FindControl("txtFQtyPrice") as TextBox;

                if (string.IsNullOrEmpty(txtFQtyPrice.Text))
                    txtFQtyPrice.Text = "0";

                if (string.IsNullOrEmpty(txtQtyPrice.Text) || decimal.Parse(txtQtyPrice.Text) == 0)
                {
                    Response.Write("<script>alert('من فضلك يجب ان يكون سعر الصنف اكبر من صفر')</script>");
                    return;
                }

                if (decimal.Parse(gvSupplyOrder.SelectedRow.Cells[10].Text) > 0)
                {
                    if (string.IsNullOrEmpty(txtFQtyPrice.Text) || decimal.Parse(txtFQtyPrice.Text) == 0)
                    {
                        Response.Write("<script>alert('من فضلك يجب ان يكون سعر tester اكبر من صفر')</script>");
                        return;
                    }
                }

                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.IncommingOrderData dr = mdb.IncommingOrderDatas.Single(a => a.Id == long.Parse(gvSupplyOrder.SelectedDataKey.Value.ToString()));
                dr.ItemPrice = decimal.Parse(txtQtyPrice.Text);
                dr.FreeItemPrice = decimal.Parse(txtFQtyPrice.Text);
                mdb.SaveChanges();
            }
            catch { }
        }
    }
}