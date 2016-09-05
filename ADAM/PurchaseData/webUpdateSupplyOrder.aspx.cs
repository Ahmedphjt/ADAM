using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.PurchaseData
{
    public partial class webUpdateSupplyOrder : System.Web.UI.Page
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

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/PurchaseData/webUpdateSupplyOrder.aspx");
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
                if (string.IsNullOrEmpty(txtSupplyOrderNo.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل كود امر التوريد')</script>");
                    return;
                }

                SaveData();
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

            DeleteData();
        }

        #endregion

        #region Function

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
                }
                else { Response.Write("<script>alert('من فضلك تأكد من رقم امر التوريد')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void SaveData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                for (int Row = 0; Row < gvSupplyOrder.Rows.Count; Row++)
                {
                    TextBox txtItemPrice = gvSupplyOrder.Rows[Row].FindControl("txtItemPrice") as TextBox;
                    long SupplyOrderDetailsId = long.Parse(gvSupplyOrder.DataKeys[Row].Value.ToString());
                    if (!string.IsNullOrEmpty(txtItemPrice.Text))
                    {
                        ADAM.DataBase.SupplyOrderDetail ddr = Mdb.SupplyOrderDetails.Single(a => a.Id == SupplyOrderDetailsId);
                        ddr.ItemPrice = decimal.Parse(txtItemPrice.Text);
                    }
                }
                Mdb.SaveChanges();
                Response.Write("<script>alert('تمت عملية الحفظ بنجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.SupplyOrderHeader dr = Mdb.SupplyOrderHeaders.Single(a => a.SupplyOrderNo == long.Parse(txtSupplyOrderNo.Text));
                var SupplyOrderDetailsRows = from a in Mdb.SupplyOrderDetails where a.SupplyOrderHeaderId == dr.Id select a;

                foreach (ADAM.DataBase.SupplyOrderDetail ddr in SupplyOrderDetailsRows)
                {
                    ADAM.DataBase.PurchaseOredrDetail pdr = Mdb.PurchaseOredrDetails.Single(a => a.Id == ddr.PurchaseOrderDetailsId);
                    if (pdr.IsChecked != 5)
                    {
                        Response.Write("<script>alert('لا يمكن الحذف الان لانه اصبح في مرحلة غير امر التوريد')</script>");
                        return;
                    }
                }

                foreach (ADAM.DataBase.SupplyOrderDetail ddr in SupplyOrderDetailsRows)
                {
                    Mdb.SupplyOrderDetails.Remove(ddr);
                    ADAM.DataBase.PurchaseOredrDetail pdr = Mdb.PurchaseOredrDetails.Single(a => a.Id == ddr.PurchaseOrderDetailsId);
                    pdr.IsChecked = 0;
                    Mdb.SaveChanges();
                }

                Mdb.SupplyOrderHeaders.Remove(dr);
                Mdb.SaveChanges();
                txtDate.Text = txtSupplierCode.Text = txtSupplyOrderNo.Text = "";
                ddlSupplier.SelectedValue = "0";
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }
        #endregion

        protected void gvSupplyOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex != -1)
                {
                    ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                    TextBox txtItemPrice = e.Row.FindControl("txtItemPrice") as TextBox;
                    long SupplyOrderDetailsId = long.Parse(gvSupplyOrder.DataKeys[e.Row.RowIndex].Value.ToString());
                    txtItemPrice.Text = Mdb.SupplyOrderDetails.Single(a => a.Id == SupplyOrderDetailsId).ItemPrice.ToString();
                }
            }
            catch { }
        }

        protected void gvSupplyOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                long SupplyOrderDetailsId = long.Parse(gvSupplyOrder.SelectedDataKey.Value.ToString());
                ADAM.DataBase.SupplyOrderDetail dr = Mdb.SupplyOrderDetails.Single(a => a.Id == SupplyOrderDetailsId);
                ADAM.DataBase.PurchaseOredrDetail pdr = Mdb.PurchaseOredrDetails.Single(a => a.Id == dr.PurchaseOrderDetailsId);
                if (pdr.IsChecked != 5)
                {
                    Response.Write("<script>alert('لا يمكن الحذف الان لانه اصبح في مرحلة غير امر التوريد')</script>");
                    return;
                }
                Mdb.SupplyOrderDetails.Remove(dr);
                pdr.IsChecked = 0;
                Mdb.SaveChanges();
                gvSupplyOrder.DataBind();
            }
            catch { }
        }
    }
}