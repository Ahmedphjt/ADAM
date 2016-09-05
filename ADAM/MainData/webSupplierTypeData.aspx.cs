using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webSupplierTypeData : System.Web.UI.Page
    {
        public int pageid = 88;

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
            var Rows = from a in mdb.SupplierTypes orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtCode.Text = "1";
            else
            {
                ADAM.DataBase.SupplierType dr = Rows.First();
                txtCode.Text = (dr.SupplierTypeCode + 1).ToString();
            }

        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/MainData/webSupplierTypeData.aspx");
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
                    Response.Write("<script>alert('من فضلك ادخل كود نوع المورد')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل اسم نوع المورد')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.SupplierTypes where a.SupplierTypeCode == long.Parse(txtCode.Text) select a;
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
                var Rows = from a in Mdb.SupplierTypes where a.SupplierTypeCode == long.Parse(txtCode.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.SupplierType dr = Mdb.SupplierTypes.Single(a => a.SupplierTypeCode == long.Parse(txtCode.Text));
                    txtName.Text = dr.SupplierTypeName;

                    txtAccountCode.Text = dr.AccountId.ToString();
                    var AccountRow = from a in Mdb.Accounts where a.Id == dr.AccountId select a;
                    if (AccountRow.Count() > 0)
                    {
                        ADAM.DataBase.Account accdr = Mdb.Accounts.Single(a => a.Id == dr.AccountId);
                        txtAccountCode.Text = accdr.AccountCode.ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('نوع الموردين ليس له حساب من فضلك تأكد من انشاء حساب')</script>");
                    }

                }
                else { Response.Write("<script>alert('من فضلك تأكد من الكود')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EditData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text))
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.SupplierType dr = Mdb.SupplierTypes.Single(a => a.SupplierTypeCode == long.Parse(txtCode.Text));

                if (Validation())
                {
                    dr.SupplierTypeName = txtName.Text;

                    var AccountRow = from a in Mdb.Accounts where a.AccountCode == long.Parse(txtAccountCode.Text) select a;
                    if (AccountRow.Count() > 0)
                    {
                        ADAM.DataBase.Account accdr = Mdb.Accounts.Single(a => a.AccountCode == long.Parse(txtAccountCode.Text));
                        dr.AccountId = accdr.Id;
                    }
                    else
                    {
                        Response.Write("<script>alert('نوع الموردين ليس له حساب من فضلك تأكد من انشاء حساب اولا قبل التعديل')</script>");
                        return;
                    }

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
                ADAM.DataBase.SupplierType dr = new DataBase.SupplierType();
                dr.SupplierTypeCode = int.Parse(txtCode.Text);
                dr.SupplierTypeName = txtName.Text;
                var AccountRow = from a in Mdb.Accounts where a.AccountCode == long.Parse(txtAccountCode.Text) select a;
                if (AccountRow.Count() > 0)
                {
                    ADAM.DataBase.Account accdr = Mdb.Accounts.Single(a => a.AccountCode == long.Parse(txtAccountCode.Text));
                    dr.AccountId = accdr.Id;
                }
                else
                {
                    Response.Write("<script>alert('نوع الموردين ليس له حساب من فضلك تأكد من انشاء حساب اولا قبل الحفظ')</script>");
                    return;
                }
                Mdb.SupplierTypes.Add(dr);
                Mdb.SaveChanges();
                Response.Redirect("~/MainData/webSupplierTypeData.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.SupplierType dr = Mdb.SupplierTypes.Single(a => a.SupplierTypeCode == long.Parse(txtCode.Text));
                Mdb.SupplierTypes.Remove(dr);
                Mdb.SaveChanges();
                txtName.Text = txtCode.Text = "";
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.SupplierTypes where a.SupplierTypeCode == long.Parse(txtCode.Text) select a;
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

            //Response.Redirect("~/MainReport/webItemTypeReport.aspx");
        }
    }
}