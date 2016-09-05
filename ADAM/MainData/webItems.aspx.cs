
using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webItems : System.Web.UI.Page
    {
        public int pageid = 7;

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

        private void GetNum()
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in mdb.Items where a.ItemTypeId == long.Parse(ddlItemType.SelectedValue) orderby a.Code descending select a;
            if (Rows.Count() == 0)
                txtCode.Text = "1";
            else
            {
                ADAM.DataBase.Item dr = Rows.First();
                txtCode.Text = (dr.Code + 1).ToString();
            }
        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/MainData/webItems.aspx");
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
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل الكود')</script>");
                return;
            }

            if (ddlItemType.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك اختر نوع الصنف')</script>");
                return;
            }
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
                    var RepCode = from a in Mdb.Items where a.Code == long.Parse(txtCode.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue) select a;
                    if (RepCode.Count() > 0)
                    {
                        Response.Write("<script>alert('لا يمكن تكرار الكود')</script>");
                        return;
                    }

                    var Rows = from a in Mdb.Items
                               where a.Name == txtName.Text && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue) && a.Sex == long.Parse(ddlSex.SelectedValue)
                                   && a.ItemStatus == long.Parse(ddlItemStatus.SelectedValue)
                               select a;
                    if (Rows.Count() > 0)
                    {
                        Response.Write("<script>alert('لا يمكن تكرار الصنف مع نوع الصنف')</script>");
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
                var Rows = from a in Mdb.Items where a.Code == long.Parse(txtCode.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.Item dr = Mdb.Items.Single(a => a.Code == long.Parse(txtCode.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue));
                    txtLimitQty.Text = dr.LimitQty.ToString();
                    txtName.Text = dr.Name;
                    txtNote.Text = dr.Note;
                    txtSpecification.Text = dr.Specification;
                    ddlItemStatus.SelectedValue = dr.ItemStatus.ToString();
                    ddlItemType.SelectedValue = dr.ItemTypeId.ToString();
                    ddlItemUnit.SelectedValue = dr.ItemunitId.ToString();
                    ddlSex.SelectedValue = dr.Sex.ToString();
                    ddlProductionLine.SelectedValue = dr.ProductionLineId.ToString();
                    
                    #region showImg
                    Binary binary = dr.Image;
                    byte[] bytes;
                    string Base64String = string.Empty;
                    bytes = binary.ToArray();
                    Base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    ImgItem.ImageUrl = "data:image/png;base64," + Base64String;
                    #endregion
                }
                else { Response.Write("<script>alert('من فضلك تأكد من كود الصنف')</script>"); }
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
                    ADAM.DataBase.Item dr = Mdb.Items.Single(a => a.Code == long.Parse(txtCode.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue));

                    if (Validation())
                    {
                        #region InsertImgIntoSql
                        string filePath = fulImage.PostedFile.FileName;
                        string filename = Path.GetFileName(filePath);
                        string ext = Path.GetExtension(filename);
                        Stream fs = fulImage.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        #endregion
                        if (bytes.Count() != 0)
                            dr.Image = bytes;
                        dr.ItemStatus = int.Parse(ddlItemStatus.SelectedValue);
                        dr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                        dr.ItemunitId = long.Parse(ddlItemUnit.SelectedValue);
                        dr.LimitQty = decimal.Parse(txtLimitQty.Text);
                        dr.Note = txtNote.Text;
                        dr.Sex = int.Parse(ddlSex.SelectedValue);
                        dr.Specification = txtSpecification.Text;
                        dr.ProductionLineId = int.Parse(ddlProductionLine.SelectedValue);
                        dr.Name = txtName.Text;

                        Mdb.SaveChanges();

                        #region showImg
                        if (bytes.Count() != 0)
                        {
                            Binary binary = dr.Image;
                            byte[] Nbytes;
                            string Base64String = string.Empty;
                            Nbytes = binary.ToArray();
                            Base64String = Convert.ToBase64String(bytes, 0, Nbytes.Length);
                            ImgItem.ImageUrl = "data:image/png;base64," + Base64String;
                        }
                        #endregion

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
                ADAM.DataBase.Item dr = new DataBase.Item();

                #region InsertImgIntoSql
                string filePath = fulImage.PostedFile.FileName;
                string filename = Path.GetFileName(filePath);
                string ext = Path.GetExtension(filename);
                Stream fs = fulImage.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                #endregion

                dr.Code = long.Parse(txtCode.Text);
                dr.Image = bytes;
                dr.ItemStatus = int.Parse(ddlItemStatus.SelectedValue);
                dr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                dr.ItemunitId = long.Parse(ddlItemUnit.SelectedValue);
                dr.LimitQty = decimal.Parse(txtLimitQty.Text);
                dr.Note = txtNote.Text;
                dr.Sex = int.Parse(ddlSex.SelectedValue);
                dr.Specification = txtSpecification.Text;
                dr.Name = txtName.Text;
                dr.ProductionLineId = int.Parse(ddlProductionLine.SelectedValue);

                Mdb.Items.Add(dr);
                Mdb.SaveChanges();

                #region showImg
                Binary binary = dr.Image;
                byte[] Nbytes;
                string Base64String = string.Empty;
                Nbytes = binary.ToArray();
                Base64String = Convert.ToBase64String(bytes, 0, Nbytes.Length);
                ImgItem.ImageUrl = "data:image/png;base64," + Base64String;
                #endregion

            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Item dr = Mdb.Items.Single(a => a.Code == long.Parse(txtCode.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue));
                Mdb.Items.Remove(dr);
                Mdb.SaveChanges();
                txtName.Text = txtCode.Text = "";
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.Items where a.Code == long.Parse(txtCode.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue) select a;
            if (Rows.Count() > 0)
                return true;
            else
                return false;
        }

        private bool ValidationData()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل كود الصنف')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل اسم الصنف')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtLimitQty.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل حد الطلب')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtSpecification.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل المواصفات')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtNote.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل الملاحظات')</script>");
                return false;
            }

            if (ddlItemType.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل نوع الصنف')</script>");
                return false;
            }

            if (ddlItemUnit.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل وحدة الصنف')</script>");
                return false;
            }

            if (ddlSex.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل النوع')</script>");
                return false;
            }

            if (ddlItemStatus.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك ادخل حالة الصنف')</script>");
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

            Response.Redirect("~/MainReport/webItemReport.aspx");
        }

        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetNum();
        }

        //protected void ShowImg()
        //{
        //    #region InsertImgIntoSql
        //    string filePath = fulImage.PostedFile.FileName;
        //    string filename = Path.GetFileName(filePath);
        //    string ext = Path.GetExtension(filename);
        //    Stream fs = fulImage.PostedFile.InputStream;
        //    BinaryReader br = new BinaryReader(fs);
        //    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //    #endregion

        //    #region showImg
        //    Binary binary = bytes;
        //    byte[] Nbytes;
        //    string Base64String = string.Empty;
        //    Nbytes = binary.ToArray();
        //    Base64String = Convert.ToBase64String(bytes, 0, Nbytes.Length);
        //    ImgItem.ImageUrl = "data:image/png;base64," + Base64String;
        //    #endregion
        //}
    }
}