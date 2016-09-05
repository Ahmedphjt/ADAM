using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webGovernorateData : System.Web.UI.Page
    {
        public int pageid = 59;

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
            var Rows = from a in mdb.GovernorateDatas orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtCode.Text = "1";
            else
            {
                ADAM.DataBase.GovernorateData dr = Rows.First();
                txtCode.Text = (dr.GovernorateCode + 1).ToString();
            }
        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/MainData/webGovernorateData.aspx");
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
                    Response.Write("<script>alert('من فضلك ادخل كود المدينة')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل اسم المدينة')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.GovernorateDatas where a.GovernorateCode == long.Parse(txtCode.Text) select a;
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
                var Rows = from a in Mdb.GovernorateDatas where a.GovernorateCode == long.Parse(txtCode.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.GovernorateData dr = Mdb.GovernorateDatas.Single(a => a.GovernorateCode == long.Parse(txtCode.Text));
                    ddlCountry.SelectedValue = dr.CityData.CountryId.ToString();
                    ddlCity.DataBind();
                    ddlCity.SelectedValue = dr.CityId.ToString();
                    txtName.Text = dr.GovernorateName;
                }
                else { Response.Write("<script>alert('من فضلك تأكد من كود المدينة')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EditData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text) || ddlCountry.SelectedValue == "0" || ddlCity.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.GovernorateData dr = Mdb.GovernorateDatas.Single(a => a.GovernorateCode == long.Parse(txtCode.Text));
                if (Validation())
                {
                    dr.CityId = long.Parse(ddlCity.SelectedValue);
                    dr.GovernorateName = txtName.Text;
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
                ADAM.DataBase.GovernorateData dr = new DataBase.GovernorateData();
                dr.CityId = long.Parse(ddlCity.SelectedValue);
                dr.GovernorateCode = int.Parse(txtCode.Text);
                dr.GovernorateName = txtName.Text;
                Mdb.GovernorateDatas.Add(dr);
                Mdb.SaveChanges();
                Response.Write("<script>alert('تمت عملية الحفظ بنجاح')</script>");
                Response.Redirect("~/MainData/webGovernorateData.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }
        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.GovernorateData dr = Mdb.GovernorateDatas.Single(a => a.GovernorateCode == long.Parse(txtCode.Text));
                var EmployeeRows = from a in Mdb.EmployeeDatas where a.GovernorateId == dr.Id select a;
                if (EmployeeRows.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن الحذف لوجود موظفين مرتبطين بها')</script>");
                    return;
                }
                Mdb.GovernorateDatas.Remove(dr);
                Mdb.SaveChanges();
                EmptyData();
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EmptyData()
        {
            txtName.Text = txtCode.Text = "";
            ddlCountry.SelectedValue = ddlCity.SelectedValue = "0";
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.GovernorateDatas where a.GovernorateCode == long.Parse(txtCode.Text) select a;
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

            Response.Redirect("~/MainReport/webGovernorateReport.aspx");
        }
    }
}