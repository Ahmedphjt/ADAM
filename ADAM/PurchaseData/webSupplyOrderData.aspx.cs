using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.PurchaseData
{
    public partial class webSupplyOrderData : System.Web.UI.Page
    {
        public int pageid = 38;

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
            var Rows = from a in mdb.SupplyOrderHeaders orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtSupplyOrderNo.Text = "1";
            else
            {
                ADAM.DataBase.SupplyOrderHeader dr = Rows.First();
                txtSupplyOrderNo.Text = (dr.SupplyOrderNo + 1).ToString();
            }
        }

        #region btnFunction

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/PurchaseData/webSupplyOrderData.aspx");
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

                if (string.IsNullOrEmpty(txtSupplyOrderNo.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل رقم امر التوريد')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtDate.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل تاريخ طلب الشراء')</script>");
                    return;
                }

                if (ddlSupplier.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل المورد')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtSupplierCode.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل كود المورد')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.SupplyOrderHeaders where a.SupplyOrderNo == long.Parse(txtSupplyOrderNo.Text) select a;
                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن تكرار رقم طلب التوريد')</script>");
                    return;
                }

                SaveData();
                gvSupplyOrder.DataBind();
            }
            catch { }
        }

        #endregion

        #region Function

        private void SaveData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.SupplyOrderHeader dr = new DataBase.SupplyOrderHeader();
                dr.RecDate = DateTime.Now;
                dr.SupplierId = long.Parse(ddlSupplier.SelectedValue);
                dr.SupplyOrderDate = DateTime.Parse(txtDate.Text);
                dr.SupplyOrderNo = long.Parse(txtSupplyOrderNo.Text);
                dr.Posted = 0;
                dr.CostCenter = long.Parse(ddlHeaderCostCenter.SelectedValue);
                Mdb.SupplyOrderHeaders.Add(dr);
                Mdb.SaveChanges();

                for (int Row = 0; Row < gvSupplyOrder.Rows.Count; Row++)
                {
                    CheckBox chkChoose = gvSupplyOrder.Rows[Row].FindControl("chkChoose") as CheckBox;
                    if (chkChoose.Checked)
                    {
                        TextBox txtNote = gvSupplyOrder.Rows[Row].FindControl("txtNote") as TextBox;
                        TextBox txtItemPrice = gvSupplyOrder.Rows[Row].FindControl("txtItemPrice") as TextBox;
                        if (string.IsNullOrEmpty(txtNote.Text))
                            txtNote.Text = "لا يوجد";
                        ADAM.DataBase.SupplyOrderDetail ddr = new DataBase.SupplyOrderDetail();
                        if (string.IsNullOrEmpty(txtItemPrice.Text))
                            ddr.ItemPrice = 0;
                        else
                            ddr.ItemPrice = decimal.Parse(txtItemPrice.Text);
                        ddr.Note = txtNote.Text;
                        ADAM.DataBase.PurchaseOredrDetail Pdr = Mdb.PurchaseOredrDetails.Single(a => a.Id == long.Parse(gvSupplyOrder.DataKeys[Row].Value.ToString()));
                        ddr.PurchaseOrderDetailsId = Pdr.Id;
                        Pdr.IsChecked = 5;
                        ddr.SupplyOrderHeaderId = dr.Id;
                        Mdb.SupplyOrderDetails.Add(ddr);
                    }
                }
                Mdb.SaveChanges();
                Response.Redirect("~/PurchaseData/webSupplyOrderData.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }
        #endregion

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.SupplierData dr = Mdb.SupplierDatas.Single(a => a.Id == long.Parse(ddlSupplier.SelectedValue));
            txtSupplierCode.Text = dr.Code.ToString();
        }

        protected void txtSupplierCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.SupplierData dr = Mdb.SupplierDatas.Single(a => a.Code == long.Parse(txtSupplierCode.Text));
                ddlSupplier.SelectedValue = dr.Id.ToString();
            }
            catch { return; }
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

            Response.Redirect("~/PurchaseReport/webSupplyOrderWithOutPrice.aspx");
        }

        protected void txtPurchaseOrderNo_TextChanged(object sender, EventArgs e)
        {
            try {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                var POHRow = from a in db.PurchaseOrderHeaders where a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text) select a;
                if (POHRow.Count() > 0)
                {
                    ADAM.DataBase.PurchaseOrderHeader POHdr = db.PurchaseOrderHeaders.Single(a => a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text));
                    txtPurchaseOrderDate.Text = POHdr.PurchaseDate.ToString("yyyy-MM-dd");
                    ddlSupplier.SelectedValue = POHdr.SupplierId.ToString();
                    txtSupplierCode.Text = db.SupplierDatas.Single(a => a.Id == POHdr.SupplierId).Code.ToString();
                    gvSupplyOrder.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم طلب الشراء')</script>");
                    return;
                }
            }
            catch { }
        }
    }
}