
using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webUsers : System.Web.UI.Page
    {
        public int pageid = 13;

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
            Response.Redirect("~/MainData/webUsers.aspx");
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
                if (string.IsNullOrEmpty(txtNickName.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل اسم الدخول')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل كلمة المرور')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.UserDatas where a.NickName == txtNickName.Text select a;
                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن تكرار اسم الدخول')</script>");
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
                var Rows = from a in Mdb.UserDatas where a.UserID == long.Parse(ddlEmployee.SelectedValue) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.UserData dr = Mdb.UserDatas.Single(a => a.UserID == long.Parse(ddlEmployee.SelectedValue));
                    txtNickName.Text = dr.NickName;
                    txtPassword.Text = dr.Password;
                    ddlIsLock.SelectedValue = dr.IsLock.ToString();
                }
                else { Response.Write("<script>alert('من فضلك تأكد من كود القسم')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EditData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNickName.Text) || string.IsNullOrEmpty(txtPassword.Text) || 
                    ddlEmployee.SelectedValue == "0" || ddlIsLock.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.UserData dr = Mdb.UserDatas.Single(a => a.UserID == long.Parse(ddlEmployee.SelectedValue));
                if (Validation())
                {
                    dr.NickName = txtNickName.Text;
                    dr.Password = txtPassword.Text;
                    dr.IsLock = int.Parse(ddlIsLock.SelectedValue);
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
                ADAM.DataBase.UserData dr = new DataBase.UserData();
                dr.UserID = long.Parse(ddlEmployee.SelectedValue);
                dr.NickName = txtNickName.Text;
                dr.Password = txtPassword.Text;
                dr.IsLock = int.Parse(ddlIsLock.SelectedValue);
                Mdb.UserDatas.Add(dr);
                Mdb.SaveChanges(); Response.Redirect("~/MainData/webUsers.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.UserData dr = Mdb.UserDatas.Single(a => a.UserID == long.Parse(ddlEmployee.SelectedValue));
                Mdb.UserDatas.Remove(dr);
                Mdb.SaveChanges();
                txtNickName.Text = txtPassword.Text = "";
                ddlEmployee.SelectedValue = ddlIsLock.SelectedValue = "0";
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.UserDatas where a.UserID == long.Parse(ddlEmployee.SelectedValue) select a;
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

            Response.Redirect("~/MainReport/webUsersReport.aspx");
        }

        protected void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
                txtPassword.TextMode = TextBoxMode.SingleLine;
            else
                txtPassword.TextMode = TextBoxMode.Password;
        }
    }
}