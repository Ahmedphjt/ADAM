﻿
using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class wemPointOfSales : System.Web.UI.Page
    {
        public int pageid = 14;

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
            var Rows = from a in mdb.PointOfSales orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtCode.Text = "1";
            else
            {
                ADAM.DataBase.PointOfSale dr = Rows.First();
                txtCode.Text = (dr.Code + 1).ToString();
            }

        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/MainData/wemPointOfSales.aspx");
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

            txtCode.Enabled = false;
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
                GetNum();

                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل كود المخزن')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل اسم المخزن')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل رقم الهاتف')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtNote.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل الملاحظات')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل التليفون')</script>");
                    return;
                }

                if (ddlCity.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل المحافظة')</script>");
                    return;
                }

                if (ddlCountry.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل المدينة')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.PointOfSales where a.Code == long.Parse(txtCode.Text) select a;
                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن تكرار الكود')</script>");
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
                var Rows = from a in Mdb.PointOfSales where a.Code == long.Parse(txtCode.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.PointOfSale dr = Mdb.PointOfSales.Single(a => a.Code == long.Parse(txtCode.Text));
                    txtName.Text = dr.Name;
                    txtAdress.Text = dr.Address;
                    txtNote.Text = dr.Note;
                    txtPhone.Text = dr.Phone;
                    ddlCountry.SelectedValue = dr.CountryId.ToString();
                    dbCity.DataBind();
                    ddlCity.DataBind();
                    ddlCity.SelectedValue = dr.CityId.ToString();
                }
                else { Response.Write("<script>alert('من فضلك تأكد من كود نقطة البيع')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EditData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtNote.Text) || 
                    string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtAdress.Text))
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }

                if (ddlCity.SelectedValue == "0" || ddlCountry.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.PointOfSale dr = Mdb.PointOfSales.Single(a => a.Code == long.Parse(txtCode.Text));

                if (Validation())
                {
                    dr.Name = txtName.Text;
                    dr.Phone = txtPhone.Text;
                    dr.Note = txtNote.Text;
                    dr.Address = txtAdress.Text;
                    dr.CountryId = long.Parse(ddlCountry.SelectedValue);
                    dr.CityId = long.Parse(ddlCity.SelectedValue);
                    Mdb.SaveChanges();
                    Response.Write("<script>alert('تمت عملية التعديل بنجاح')</script>");
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
                ADAM.DataBase.PointOfSale dr = new DataBase.PointOfSale();
                dr.Code = int.Parse(txtCode.Text);
                dr.Name = txtName.Text;
                dr.Phone = txtPhone.Text;
                dr.Note = txtNote.Text;
                dr.Address = txtAdress.Text;
                dr.CountryId = long.Parse(ddlCountry.SelectedValue);
                dr.CityId = long.Parse(ddlCity.SelectedValue);
                Mdb.PointOfSales.Add(dr);
                Mdb.SaveChanges(); Response.Redirect("~/MainData/wemPointOfSales.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.PointOfSale dr = Mdb.PointOfSales.Single(a => a.Code == long.Parse(txtCode.Text));
                Mdb.PointOfSales.Remove(dr);
                Mdb.SaveChanges();
                txtName.Text = txtCode.Text = txtAdress.Text = txtNote.Text = txtPhone.Text = "";
                ddlCity.SelectedValue = ddlCountry.SelectedValue = "0";
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.PointOfSales where a.Code == long.Parse(txtCode.Text) select a;
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

            Response.Redirect("~/MainReport/webPointOfSaleReport.aspx");
        }
    }
}