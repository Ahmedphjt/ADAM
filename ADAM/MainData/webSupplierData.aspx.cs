
using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webSupplierData : System.Web.UI.Page
    {
        public int pageid = 12;

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

                //GetNum();
            }
        }

        private void GetNum()
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in mdb.SupplierDatas where a.SupplierTypeId == long.Parse(ddlSupplierType.SelectedValue) orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtCode.Text = "1";
            else
            {
                ADAM.DataBase.SupplierData dr = Rows.First();
                txtCode.Text = (dr.Code + 1).ToString();
            }

        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/MainData/webSupplierData.aspx");
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

                if (ValidationData())
                {
                    ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                    var RepCode = from a in Mdb.SupplierDatas where a.Code == long.Parse(txtCode.Text) && a.SupplierTypeId == long.Parse(ddlSupplierType.SelectedValue) select a;
                    if (RepCode.Count() > 0)
                    {
                        Response.Write("<script>alert('لا يمكن تكرار الكود')</script>");
                        return;
                    }

                    SaveData();
                }
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
                var Rows = from a in Mdb.SupplierDatas where a.Code == long.Parse(txtCode.Text) && a.SupplierTypeId == long.Parse(ddlSupplierType.SelectedValue) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.SupplierData dr = Mdb.SupplierDatas.Single(a => a.Code == long.Parse(txtCode.Text) && a.SupplierTypeId == long.Parse(ddlSupplierType.SelectedValue));

                    txtFirstName.Text = dr.FirstName;
                    txtLastName.Text = dr.LastName;
                    txtFirstPhone.Text = dr.FirstPhone;
                    txtSecondPhone.Text = dr.SecondPhone;
                    txtFirstMobile.Text = dr.FirstMobile;
                    txtSecondMobile.Text = dr.SecondMobile;
                    txtFax.Text = dr.Fax;
                    txtEmail.Text = dr.Email;
                    txtAddress.Text = dr.Address;
                    txtIdNo.Text = dr.IdNo;
                    ddlJob.SelectedValue = dr.JobId.ToString();
                    ddlSex.SelectedValue = dr.Sex.ToString();
                    ddlCountry.SelectedValue = dr.CountryId.ToString();
                    ddlStatus.SelectedValue = dr.Status.ToString();
                    ddlCity.DataBind();
                    ddlCity.SelectedValue = dr.CityId.ToString();
                    ddlSupplierType.SelectedValue = dr.SupplierTypeId.ToString();
                    ddlGovernorate.DataBind();
                    ddlGovernorate.SelectedValue = dr.GovernorateId.ToString();
                    ddlArea.DataBind();
                    ddlArea.SelectedValue = dr.AreaId.ToString();

                    txtAccountCode.Text = dr.AccountId.ToString();
                    var AccountRow = from a in Mdb.Accounts where a.Id == dr.AccountId select a;
                    if (AccountRow.Count() > 0)
                    {
                        ADAM.DataBase.Account accdr = Mdb.Accounts.Single(a => a.Id == dr.AccountId);
                        txtAccountCode.Text = accdr.AccountCode.ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('هذا المورد ليس له حساب من فضلك تأكد من انشاء حساب')</script>");
                    }
                }
                else { Response.Write("<script>alert('من فضلك تأكد من كود المورد')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EditData()
        {
            try
            {
                if (ValidationData())
                {
                    ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                    ADAM.DataBase.SupplierData dr = Mdb.SupplierDatas.Single(a => a.Code == long.Parse(txtCode.Text) && a.SupplierTypeId == long.Parse(ddlSupplierType.SelectedValue));

                    if (Validation())
                    {
                        dr.Address = txtAddress.Text;
                        dr.Email = txtEmail.Text;
                        dr.Fax = txtFax.Text;
                        dr.FirstMobile = txtFirstMobile.Text;
                        dr.FirstName = txtFirstName.Text;
                        dr.FirstPhone = txtFirstPhone.Text;
                        dr.IdNo = txtIdNo.Text;
                        dr.LastName = txtLastName.Text;
                        dr.SecondMobile = txtSecondMobile.Text;
                        dr.SecondPhone = txtSecondPhone.Text;
                        dr.JobId = long.Parse(ddlJob.SelectedValue);
                        dr.Sex = int.Parse(ddlSex.SelectedValue);
                        dr.CountryId = long.Parse(ddlCountry.SelectedValue);
                        dr.CityId = long.Parse(ddlCity.SelectedValue);
                        dr.Status = int.Parse(ddlStatus.SelectedValue);
                        dr.GovernorateId = long.Parse(ddlGovernorate.SelectedValue);
                        dr.SupplierTypeId = long.Parse(ddlSupplierType.SelectedValue);
                        dr.AreaId = long.Parse(ddlArea.SelectedValue);

                        var AccountRow = from a in Mdb.Accounts where a.AccountCode == long.Parse(txtAccountCode.Text) select a;
                        if (AccountRow.Count() > 0)
                        {
                            ADAM.DataBase.Account accdr = Mdb.Accounts.Single(a => a.AccountCode == long.Parse(txtAccountCode.Text));
                            dr.AccountId = accdr.Id;
                        }
                        else
                        {
                            Response.Write("<script>alert('هذا المورد ليس له حساب من فضلك تأكد من انشاء حساب اولا قبل التعديل')</script>");
                            return;
                        }

                        Mdb.SaveChanges();
                        Response.Write("<script>alert('تمت عملية التعديل بنجاح')</script>");
                    }
                    else
                        Response.Write("<script>alert('هذا الكود غير موجود بقاعدة البيانات')</script>");
                }
            }
            catch { Response.Write("<script>alert('خطأ أثناء التعديل من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات ')</script>"); }
        }

        private void SaveData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.SupplierData dr = new DataBase.SupplierData();
                dr.SupplierTypeId = long.Parse(ddlSupplierType.SelectedValue);
                dr.Code = long.Parse(txtCode.Text);
                dr.Address = txtAddress.Text;
                dr.Email = txtEmail.Text;
                dr.Fax = txtFax.Text;
                dr.FirstMobile = txtFirstMobile.Text;
                dr.FirstName = txtFirstName.Text;
                dr.FirstPhone = txtFirstPhone.Text;
                dr.IdNo = txtIdNo.Text;
                dr.LastName = txtLastName.Text;
                dr.SecondMobile = txtSecondMobile.Text;
                dr.SecondPhone = txtSecondPhone.Text;
                dr.JobId = long.Parse(ddlJob.SelectedValue);
                dr.Sex = int.Parse(ddlSex.SelectedValue);
                dr.CountryId = long.Parse(ddlCountry.SelectedValue);
                dr.CityId = long.Parse(ddlCity.SelectedValue);
                dr.Status = int.Parse(ddlStatus.SelectedValue);
                dr.GovernorateId = long.Parse(ddlGovernorate.SelectedValue);
                dr.AreaId = long.Parse(ddlArea.SelectedValue);

                var AccountRow = from a in Mdb.Accounts where a.AccountCode == long.Parse(txtAccountCode.Text) select a;
                if (AccountRow.Count() > 0)
                {
                    ADAM.DataBase.Account accdr = Mdb.Accounts.Single(a => a.AccountCode == long.Parse(txtAccountCode.Text));
                    dr.AccountId = accdr.Id;
                }
                else
                {
                    Response.Write("<script>alert('هذا المورد ليس له حساب من فضلك تأكد من انشاء حساب اولا قبل الحفظ')</script>");
                    return;
                }

                Mdb.SupplierDatas.Add(dr);
                Mdb.SaveChanges(); Response.Redirect("~/MainData/webSupplierData.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.SupplierData dr = Mdb.SupplierDatas.Single(a => a.Code == long.Parse(txtCode.Text) && a.SupplierTypeId == long.Parse(ddlSupplierType.SelectedValue));
                Mdb.SupplierDatas.Remove(dr);
                Mdb.SaveChanges();
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.SupplierDatas where a.Code == long.Parse(txtCode.Text) && a.SupplierTypeId == long.Parse(ddlSupplierType.SelectedValue) select a;
            if (Rows.Count() > 0)
                return true;
            else
                return false;
        }

        private bool ValidationData()
        {
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل العنوان')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtCode.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل كود الموظف')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل الايميل')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtFax.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم الفاكس')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtFirstMobile.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم الموبايل الاول')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل  الاسم الاول')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtFirstPhone.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل الهاتف الاول')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtIdNo.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم البطاقة')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل الاسم الثاني')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtSecondMobile.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم الموبايل الثاني')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtSecondPhone.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم الهاتف الثاني')</script>");
                return false;
            }

            if (ddlJob.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل الوظيفة')</script>");
                return false;
            }

            if (ddlSex.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل النوع')</script>");
                return false;
            }

            if (ddlSupplierType.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل نوع المورد')</script>");
                return false;
            }

            if (ddlCountry.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل الدولة')</script>");
                return false;
            }

            if (ddlCity.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل المحافظة')</script>");
                return false;
            }

            if (ddlStatus.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل حالة المورد')</script>");
                return false;
            }
            if (ddlGovernorate.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل المدينة')</script>");
                return false;
            }

            if (ddlArea.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل المنطقة')</script>");
                return false;
            }
            return true;
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

            Response.Redirect("~/MainReport/webSupplierReport.aspx");
        }

        protected void ddlSupplierType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetNum();
        }
    }
}