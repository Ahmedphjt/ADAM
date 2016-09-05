using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webItemStatus : System.Web.UI.Page
    {
        public int pageid = 111;

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
            Response.Redirect("~/MainData/webItemStatus.aspx");
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
                    Response.Write("<script>alert('من فضلك ادخل نوع المنتج')</script>");
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

        //protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (Session["UserID"] == null)
        //        Response.Redirect("~/BasicData/webLogIn.aspx");
        //    int userid = int.Parse(Session["UserID"].ToString());
        //    int operationid = 5;

        //    csGetPermission Per = new csGetPermission();
        //    if (!Per.getPermission(userid, pageid, operationid))
        //        Response.Redirect("~/BasicData/webHomePage.aspx");

        //    Response.Redirect("~/MainReport/webCountryReport.aspx");
        //}
        #endregion

        #region Function

        private void ShowData()
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 3;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemStatus dr = Mdb.ItemStatus.Single(a => a.Id == long.Parse(hfId.Value));
                txtName.Text = dr.Status;
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EditData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemStatus dr = Mdb.ItemStatus.Single(a => a.Id == long.Parse(hfId.Value));
                dr.Status = txtName.Text;
                Mdb.SaveChanges();
                gvItemStatus.DataBind();
                Response.Write("<script>alert('تمت عملية التعديل بنجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء التعديل من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات ')</script>"); }
        }

        private void SaveData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemStatus dr = new DataBase.ItemStatus();

                dr.Status = txtName.Text;
                Mdb.ItemStatus.Add(dr);
                Mdb.SaveChanges();
                Response.Redirect("~/MainData/webItemStatus.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemStatus dr = Mdb.ItemStatus.Single(a => a.Id == long.Parse(hfId.Value));
                var ItemRows = from a in Mdb.Items where a.ItemStatus == dr.Id select a;
                if (ItemRows.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن حذف نوع المنتج لوجود أصناف تابعه له')</script>");
                    return;
                }
                Mdb.ItemStatus.Remove(dr);
                Mdb.SaveChanges();
                txtName.Text = "";
                gvItemStatus.DataBind();
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        protected void gvItemStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfId.Value = gvItemStatus.SelectedDataKey.Value.ToString();
            ShowData();
        }

        //private bool Validation()
        //{
        //    ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
        //    var Rows = from a in Mdb.CountryDatas where a.Code == long.Parse(txtCode.Text) select a;
        //    if (Rows.Count() > 0)
        //        return true;
        //    else
        //        return false;
        //}
        #endregion
    }
}