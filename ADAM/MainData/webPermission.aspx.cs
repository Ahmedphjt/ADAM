
using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webPermission : System.Web.UI.Page
    {
        public int pageid = 16;

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
            Response.Redirect("~/MainData/webPermission.aspx");
        }

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {
            //ShowData();
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            //EditData();
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
                if (ddlPages.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر الشاشة')</script>");
                    return;
                }

                if (ddlOperation.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر الصلاحية')</script>");
                    return;
                }

                if (ddlUser.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر المستخدم')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new ADAM.DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.Permissions
                              where a.OperationId == long.Parse(ddlOperation.SelectedValue) &&
                                  a.PageId == long.Parse(ddlPages.SelectedValue) && a.UserId == long.Parse(ddlUser.SelectedValue)
                              select a;
                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لقد تم تسجيل هذه الصلاحية من قبل')</script>");
                    return;
                }

                SaveData();
                gvUserPermission.DataBind();
            }
            catch { }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            DeleteData();
        }

        #endregion

        #region Function

        //private void ShowData()
        //{
        //    try
        //    {
        //        ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
        //        var Rows = from a in Mdb.CityDatas where a.Code == long.Parse(txtCode.Text) select a;
        //        if (Rows.Count() > 0)
        //        {
        //            ADAM.DataBase.CityData dr = Mdb.CityDatas.Single(a => a.Code == long.Parse(txtCode.Text));
        //            ddlCountry.SelectedValue = dr.CountryId.ToString();
        //            txtName.Text = dr.Name;
        //        }
        //        else { Response.Write("<script>alert('من فضلك تأكد من كود المحافظة')</script>"); }
        //    }
        //    catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        //}

        //private void EditData()
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text) || ddlCountry.SelectedValue == "0")
        //        {
        //            Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
        //            return;
        //        }
        //        ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
        //        ADAM.DataBase.CityData dr = Mdb.CityDatas.Single(a => a.Code == long.Parse(txtCode.Text));
        //        if (Validation())
        //        {
        //            dr.CountryId = long.Parse(ddlCountry.SelectedValue);
        //            dr.Name = txtName.Text;
        //            Mdb.SaveChanges();
        //            Response.Write("<script>alert('تمت عملية التعديل بنجاح')</script>");
        //        }
        //        else
        //            Response.Write("<script>alert('هذا الكود غير موجود بقاعدة البيانات')</script>");
        //    }
        //    catch { Response.Write("<script>alert('خطأ أثناء التعديل من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات ')</script>"); }
        //}

        private void SaveData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Permission dr = new DataBase.Permission();
                dr.OperationId = int.Parse(ddlOperation.SelectedValue);
                dr.PageId = long.Parse(ddlPages.SelectedValue);
                dr.UserId = long.Parse(ddlUser.SelectedValue);
                Mdb.Permissions.Add(dr);
                Mdb.SaveChanges();
                Response.Write("<script>alert('تمت عملية الحفظ بنجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            //try
            //{
            //    ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            //    ADAM.DataBase.CityData dr = Mdb.CityDatas.Single(a => a.Code == long.Parse(txtCode.Text));
            //    var EmployeeRows = from a in Mdb.EmployeeDatas where a.CityId == dr.Id select a;
            //    if (EmployeeRows.Count() > 0)
            //    {
            //        Response.Write("<script>alert('لا يمكن الحذف لوجود موظفين مرتبطين بها')</script>");
            //        return;
            //    }
            //    Mdb.CityDatas.Remove(dr);
            //    Mdb.SaveChanges();
            //    txtName.Text = txtCode.Text = "";
            //    ddlCountry.SelectedValue = "0";
            //    Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            //}
            //catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        protected void gvUserPermission_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Permission dr = mdb.Permissions.Single(a => a.Id == long.Parse(gvUserPermission.SelectedDataKey.Value.ToString()));
                mdb.Permissions.Remove(dr);
                mdb.SaveChanges();
                gvUserPermission.DataBind();
            }
            catch { }
        }

        //private bool Validation()
        //{
        //    ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
        //    var Rows = from a in Mdb.CityDatas where a.Code == long.Parse(txtCode.Text) select a;
        //    if (Rows.Count() > 0)
        //        return true;
        //    else
        //        return false;
        //}
        #endregion
    }
}