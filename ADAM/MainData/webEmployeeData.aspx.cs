
using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webEmployeeData : System.Web.UI.Page
    {

        public int pageid = 6;

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
            var Rows = from a in mdb.EmployeeDatas orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtCode.Text = "1";
            else
            {
                ADAM.DataBase.EmployeeData dr = Rows.First();
                txtCode.Text = (dr.Code + 1).ToString();
            }
        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/MainData/webEmployeeData.aspx");
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
                    var RepCode = from a in Mdb.EmployeeDatas where a.Code == long.Parse(txtCode.Text) select a;
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
                var Rows = from a in Mdb.EmployeeDatas where a.Code == long.Parse(txtCode.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.EmployeeData dr = Mdb.EmployeeDatas.Single(a => a.Code == long.Parse(txtCode.Text));
                    txtAddress.Text = dr.Address;
                    txtEmail.Text = dr.Email;
                    txtFaxNo.Text = dr.FaxNo;
                    txtFirstMobileNo.Text = dr.FirstMobileNo;
                    txtFirstName.Text = dr.FirstName;
                    txtFirstPhone.Text = dr.FirstPhone;
                    txtIdNo.Text = dr.IdNo;
                    txtLastName.Text = dr.LastName;
                    txtSecondMobileNo.Text = dr.SecondMobileNo;
                    txtSecondPhone.Text = dr.SecondPhone;
                    txtBirthDate.Text = dr.BirthDate.ToString("yyyy-MM-dd");
                    txtStartJobDate.Text = dr.StartJobDate.ToString("yyyy-MM-dd");
                    
                    ddlContractType.SelectedValue = dr.ContractType.ToString();
                    ddlDepartment.SelectedValue = dr.DepartmentId.ToString();
                    dbDivision.DataBind();
                    ddlDivision.DataBind();
                    ddlDivision.SelectedValue = dr.DivisionId.ToString();
                    ddlInsuranceStatus.SelectedValue = dr.InsuranceStatus.ToString();
                    ddlJob.SelectedValue = dr.JobId.ToString();
                    ddlMaritalStatus.SelectedValue = dr.MaritalStatus.ToString();
                    ddlMilitaryStatus.SelectedValue = dr.MilitaryStatus.ToString();
                    ddlQualification.SelectedValue = dr.QualificationId.ToString();
                    ddlReligion.SelectedValue = dr.Religion.ToString();
                    ddlSex.SelectedValue = dr.Sex.ToString();

                    ddlCountry.SelectedValue = dr.CountryId.ToString();
                    ddlCity.DataBind();
                    ddlCity.SelectedValue = dr.CityId.ToString();
                    ddlGovernorate.DataBind();
                    ddlGovernorate.SelectedValue = dr.GovernorateId.ToString();
                    ddlArea.DataBind();
                    ddlArea.SelectedValue = dr.AreaId.ToString();

                    if (dr.ISSalesRepresentative)
                        chkISSalesRepresentative.Checked = true;
                    else
                        chkISSalesRepresentative.Checked = false;

                    txtAccountCode.Text = dr.AccountId.ToString();
                    var AccountRow = from a in Mdb.Accounts where a.Id == dr.AccountId select a;
                    if (AccountRow.Count() > 0)
                    {
                        ADAM.DataBase.Account accdr = Mdb.Accounts.Single(a => a.Id == dr.AccountId);
                        txtAccountCode.Text = accdr.AccountCode.ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('هذا الموظف ليس له حساب من فضلك تأكد من انشاء حساب')</script>");
                    }
                }
                else { Response.Write("<script>alert('من فضلك تأكد من كود الموظف')</script>"); }
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
                    ADAM.DataBase.EmployeeData dr = Mdb.EmployeeDatas.Single(a => a.Code == long.Parse(txtCode.Text));

                    if (Validation())
                    {
                        dr.Address = txtAddress.Text;
                        dr.BirthDate = DateTime.Parse(txtBirthDate.Text);
                        dr.Email = txtEmail.Text;
                        dr.FaxNo = txtFaxNo.Text;
                        dr.FirstMobileNo = txtFirstMobileNo.Text;
                        dr.FirstName = txtFirstName.Text;
                        dr.FirstPhone = txtFirstPhone.Text;
                        dr.IdNo = txtIdNo.Text;
                        dr.LastName = txtLastName.Text;
                        dr.SecondMobileNo = txtSecondMobileNo.Text;
                        dr.SecondPhone = txtSecondPhone.Text;
                        dr.StartJobDate = DateTime.Parse(txtStartJobDate.Text);
                        dr.ContractType = int.Parse(ddlContractType.SelectedValue);
                        dr.DepartmentId = long.Parse(ddlDepartment.SelectedValue);
                        dr.DivisionId = long.Parse(ddlDivision.SelectedValue);
                        dr.InsuranceStatus = int.Parse(ddlInsuranceStatus.SelectedValue);
                        dr.JobId = long.Parse(ddlJob.SelectedValue);
                        dr.MaritalStatus = int.Parse(ddlMaritalStatus.SelectedValue);
                        dr.MilitaryStatus = int.Parse(ddlMilitaryStatus.SelectedValue);
                        dr.QualificationId = long.Parse(ddlQualification.SelectedValue);
                        dr.Religion = int.Parse(ddlReligion.SelectedValue);
                        dr.Sex = int.Parse(ddlSex.SelectedValue);
                        dr.CountryId = long.Parse(ddlCountry.SelectedValue);
                        dr.CityId = long.Parse(ddlCity.SelectedValue);
                        dr.GovernorateId = long.Parse(ddlGovernorate.SelectedValue);
                        dr.AreaId = long.Parse(ddlArea.SelectedValue);
                        if (chkISSalesRepresentative.Checked)
                            dr.ISSalesRepresentative = true;
                        else
                            dr.ISSalesRepresentative = false;

                        var AccountRow = from a in Mdb.Accounts where a.AccountCode == long.Parse(txtAccountCode.Text) select a;
                        if (AccountRow.Count() > 0)
                        {
                            ADAM.DataBase.Account accdr = Mdb.Accounts.Single(a => a.AccountCode == long.Parse(txtAccountCode.Text));
                            dr.AccountId = accdr.Id;
                        }
                        else
                        {
                            Response.Write("<script>alert('هذا الموظف ليس له حساب من فضلك تأكد من انشاء حساب اولا قبل التعديل')</script>");
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
                ADAM.DataBase.EmployeeData dr = new DataBase.EmployeeData();
                dr.Code = long.Parse(txtCode.Text);
                dr.Address = txtAddress.Text;
                dr.BirthDate = DateTime.Parse(txtBirthDate.Text);
                dr.Email = txtEmail.Text;
                dr.FaxNo = txtFaxNo.Text;
                dr.FirstMobileNo = txtFirstMobileNo.Text;
                dr.FirstName = txtFirstName.Text;
                dr.FirstPhone = txtFirstPhone.Text;
                dr.IdNo = txtIdNo.Text;
                dr.LastName = txtLastName.Text;
                dr.SecondMobileNo = txtSecondMobileNo.Text;
                dr.SecondPhone = txtSecondPhone.Text;
                dr.StartJobDate = DateTime.Parse(txtStartJobDate.Text);
                dr.ContractType = int.Parse(ddlContractType.SelectedValue);
                dr.DepartmentId = long.Parse(ddlDepartment.SelectedValue);
                dr.DivisionId = long.Parse(ddlDivision.SelectedValue);
                dr.InsuranceStatus = int.Parse(ddlInsuranceStatus.SelectedValue);
                dr.JobId = long.Parse(ddlJob.SelectedValue);
                dr.MaritalStatus = int.Parse(ddlMaritalStatus.SelectedValue);
                dr.MilitaryStatus = int.Parse(ddlMilitaryStatus.SelectedValue);
                dr.QualificationId = long.Parse(ddlQualification.SelectedValue);
                dr.Religion = int.Parse(ddlReligion.SelectedValue);
                dr.Sex = int.Parse(ddlSex.SelectedValue);
                dr.CountryId = long.Parse(ddlCountry.SelectedValue);
                dr.CityId = long.Parse(ddlCity.SelectedValue);
                dr.GovernorateId = long.Parse(ddlGovernorate.SelectedValue);
                dr.AreaId = long.Parse(ddlArea.SelectedValue);
                if (chkISSalesRepresentative.Checked)
                    dr.ISSalesRepresentative = true;
                else
                    dr.ISSalesRepresentative = false;

                var AccountRow = from a in Mdb.Accounts where a.AccountCode == long.Parse(txtAccountCode.Text) select a;
                if (AccountRow.Count() > 0)
                {
                    ADAM.DataBase.Account accdr = Mdb.Accounts.Single(a => a.AccountCode == long.Parse(txtAccountCode.Text));
                    dr.AccountId = accdr.Id;
                }
                else
                {
                    Response.Write("<script>alert('هذا الموظف ليس له حساب من فضلك تأكد من انشاء حساب اولا قبل الحفظ')</script>");
                    return;
                }

                Mdb.EmployeeDatas.Add(dr);
                Mdb.SaveChanges(); Response.Redirect("~/MainData/webEmployeeData.aspx");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.EmployeeData dr = Mdb.EmployeeDatas.Single(a => a.Code == long.Parse(txtCode.Text));
                Mdb.EmployeeDatas.Remove(dr);
                Mdb.SaveChanges();
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.EmployeeDatas where a.Code == long.Parse(txtCode.Text) select a;
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

            if (string.IsNullOrEmpty(txtBirthDate.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل تاريخ الميلاد')</script>");
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

            if (string.IsNullOrEmpty(txtFaxNo.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم الفاكس')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtFirstMobileNo.Text))
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

            if (string.IsNullOrEmpty(txtSecondMobileNo.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم الموبايل الثاني')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtSecondPhone.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم الهاتف الثاني')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtStartJobDate.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل تاريخ بداية العمل')</script>");
                return false;
            }

            if (ddlContractType.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل نوع التعاقد')</script>");
                return false;
            }

            if (ddlDepartment.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل الادارة')</script>");
                return false;
            }

            if (ddlDivision.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل القسم')</script>");
                return false;
            }

            if (ddlInsuranceStatus.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل حالة التأمينات')</script>");
                return false;
            }

            if (ddlCountry.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل المدينة')</script>");
                return false;
            }

            if (ddlCity.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل المحافظة')</script>");
                return false;
            }

            if (ddlJob.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل الوظيفة')</script>");
                return false;
            }

            if (ddlMaritalStatus.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل الحالة الاجتماعية')</script>");
                return false;
            }

            if (ddlMilitaryStatus.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل الموقف من التجنيد')</script>");
                return false;
            }

            if (ddlQualification.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل المؤهل')</script>");
                return false;
            }

            if (ddlReligion.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل الديانه')</script>");
                return false;
            }

            if (ddlSex.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل النوع')</script>");
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

            Response.Redirect("~/MainReport/webEmployeeReport.aspx");
        }
    }
}