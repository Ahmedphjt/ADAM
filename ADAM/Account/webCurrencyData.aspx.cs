using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Account
{
    public partial class webCurrencyData : System.Web.UI.Page
    {
        public int pageid = 118;

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
            Response.Redirect("~/Account/webCurrencyData.aspx");
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
              
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل اسم العملة')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtPrice.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل سعر العملة')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtStyle.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل رمز العملة')</script>");
                    return;
                }

                if (ddlType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر نوع العملة')</script>");
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
                var Rows = from a in Mdb.CurrencyDatas where a.Id == long.Parse(hfCurrenyId.Value) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.CurrencyData dr = Mdb.CurrencyDatas.Single(a => a.Id == long.Parse(hfCurrenyId.Value));
                    txtName.Text = dr.CurrencyName;
                    txtPrice.Text = dr.CurrencyPrice.ToString();
                    txtStyle.Text = dr.CurrencyStyle;
                    ddlType.SelectedValue = dr.CurrencyType.ToString();
                }
                else {  }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EditData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtStyle.Text) || ddlType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.CurrencyData dr = Mdb.CurrencyDatas.Single(a => a.Id == long.Parse(hfCurrenyId.Value));
                if (Validation())
                {
                    dr.CurrencyName = txtName.Text;
                    dr.CurrencyPrice = decimal.Parse(txtPrice.Text);
                    dr.CurrencyStyle = txtStyle.Text;
                    dr.CurrencyType = int.Parse(ddlType.SelectedValue);
                    Mdb.SaveChanges();
                    Response.Redirect("~/Account/webCurrencyData.aspx");
                }
                else
                    Response.Write("<script>alert('هذا الكود غير موجود بقاعدة البيانات')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء التعديل من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات ')</script>"); }
        }

        private void SaveData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.CurrencyData dr = new DataBase.CurrencyData();

                dr.CurrencyName = txtName.Text;
                dr.CurrencyPrice = decimal.Parse(txtPrice.Text);
                dr.CurrencyStyle = txtStyle.Text;
                dr.CurrencyType = int.Parse(ddlType.SelectedValue);

                Mdb.CurrencyDatas.Add(dr);
                Mdb.SaveChanges();
                
                Response.Write("<script>alert('تمت عملية الحفظ بنجاح')</script>");
                Response.Redirect("~/Account/webCurrencyData.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }
        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.CurrencyData dr = Mdb.CurrencyDatas.Single(a => a.Id == long.Parse(hfCurrenyId.Value));
                var AccountRows = from a in Mdb.Accounts where a.AccountCurrency == dr.Id select a;
                if (AccountRows.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن الحذف لوجود حسابات مرتبطة بها')</script>");
                    return;
                }
                Mdb.CurrencyDatas.Remove(dr);
                Mdb.SaveChanges();
                EmptyData();
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EmptyData()
        {
            txtName.Text = txtPrice.Text = txtStyle.Text = "";
            ddlType.SelectedValue = "0";
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.CurrencyDatas where a.CurrencyName == txtName.Text select a;
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

           // Response.Redirect("~/MainReport/webAreaReport.aspx");
        }

        protected void gvCurrency_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                if (e.Row.Cells[2].Text == "1")
                    e.Row.Cells[2].Text = "محلية";
                if (e.Row.Cells[2].Text == "2")
                    e.Row.Cells[2].Text = "اجنبية";
            }
        }

        protected void gvCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfCurrenyId.Value = gvCurrency.SelectedDataKey.Value.ToString();
        }
    }
}