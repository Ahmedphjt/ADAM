using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.PurchaseData
{
    public partial class webPurchaseOrderData : System.Web.UI.Page
    {
        public int pageid = 35;

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
            var Rows = from a in mdb.PurchaseOrderHeaders orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtPurchaseOrderNo.Text = "1";
            else
            {
                ADAM.DataBase.PurchaseOrderHeader dr = Rows.First();
                txtPurchaseOrderNo.Text = (dr.PurchaseOrderNo + 1).ToString();
            }
        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/PurchaseData/webPurchaseOrderData.aspx");
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

            txtPurchaseOrderNo.Enabled = false;
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

                if (string.IsNullOrEmpty(txtDate.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل تاريخ طلب الشراء')</script>");
                    return;
                }

                if (ddlDepartment.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل الادارة')</script>");
                    return;
                }

                if (ddlDivision.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل القسم')</script>");
                    return;
                }

                if (ddlEmployee.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل الموظف')</script>");
                    return;
                }

                if (ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل نوع الصنف')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.PurchaseOrderHeaders where a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text) select a;
                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن تكرار رقم طلب الشراء')</script>");
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
                var Rows = from a in Mdb.PurchaseOrderHeaders where a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    #region ShowHeader
                    ADAM.DataBase.PurchaseOrderHeader dr = Mdb.PurchaseOrderHeaders.Single(a => a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text));
                    txtPurchaseOrderNo.Text = dr.PurchaseOrderNo.ToString();
                    txtDate.Text = dr.PurchaseDate.ToString("yyyy-MM-dd");
                    ddlDepartment.SelectedValue = dr.DepartmentId.ToString();
                    dbDivision.DataBind();
                    ddlDivision.DataBind();
                    ddlDivision.SelectedValue = dr.DivisionId.ToString();
                    ddlEmployee.SelectedValue = dr.EmployeeId.ToString();
                    ddlItemType.SelectedValue = dr.ItemTypeId.ToString();
                    ddlSupplierName.SelectedValue = dr.SupplierId.ToString();
                    txtNote.Text = dr.Note;
                    hfId.Value = dr.Id.ToString();
                    ddlItemType.Enabled = false;
                    #endregion
                    gvPurchaseDetailsData.DataBind();
                }
                else { Response.Write("<script>alert('من فضلك تأكد من رقم طلب الشراء')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EditData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtNote.Text) || string.IsNullOrEmpty(txtPurchaseOrderNo.Text)
                    || ddlDivision.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlEmployee.SelectedValue == "0" || ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.PurchaseOrderHeader dr = Mdb.PurchaseOrderHeaders.Single(a => a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text));
                if (Validation())
                {
                    dr.DepartmentId = long.Parse(ddlDepartment.SelectedValue);
                    dr.DivisionId = long.Parse(ddlDivision.SelectedValue);
                    dr.EmployeeId = long.Parse(ddlEmployee.SelectedValue);
                    dr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                    dr.Note = txtNote.Text;
                    dr.PurchaseDate = DateTime.Parse(txtDate.Text);
                    dr.PurchaseOrderNo = long.Parse(txtPurchaseOrderNo.Text);
                    dr.SupplierId = long.Parse(ddlSupplierName.SelectedValue);
                    dr.RecoredDate = DateTime.Now;
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
                ADAM.DataBase.PurchaseOrderHeader dr = new DataBase.PurchaseOrderHeader();
                dr.DepartmentId = long.Parse(ddlDepartment.SelectedValue);
                dr.DivisionId = long.Parse(ddlDivision.SelectedValue);
                dr.EmployeeId = long.Parse(ddlEmployee.SelectedValue);
                dr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                dr.Note = txtNote.Text;
                dr.PurchaseDate = DateTime.Parse(txtDate.Text);
                dr.PurchaseOrderNo = long.Parse(txtPurchaseOrderNo.Text);
                dr.RecoredDate = DateTime.Now;
                dr.SupplierId = long.Parse(ddlSupplierName.SelectedValue);
                Mdb.PurchaseOrderHeaders.Add(dr);
                Mdb.SaveChanges();
                hfId.Value = dr.Id.ToString();
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.PurchaseOrderHeader dr = Mdb.PurchaseOrderHeaders.Single(a => a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text));
                var PurchaseOrderDetailsRows = from a in Mdb.PurchaseOredrDetails where a.PurchaseOredeHeaderId == dr.Id select a;

                foreach (ADAM.DataBase.PurchaseOredrDetail ddr in PurchaseOrderDetailsRows)
                {
                    if (ddr.Status != 1)
                    {
                        Response.Write("<script>alert('لا يمكن الحذف الان لانه اصبح في مرحلة غير مرحلة الانشاء')</script>");
                        return;
                    }
                }

                foreach (ADAM.DataBase.PurchaseOredrDetail ddr in PurchaseOrderDetailsRows)
                {
                    if (ddr.Status == 1)
                    {
                        Mdb.PurchaseOredrDetails.Remove(ddr);
                    }
                    Mdb.SaveChanges();
                }

                Mdb.PurchaseOrderHeaders.Remove(dr);
                Mdb.SaveChanges();
                txtDate.Text = txtNote.Text = txtPurchaseOrderNo.Text = "";
                ddlEmployee.SelectedValue = ddlDepartment.SelectedValue = ddlDivision.SelectedValue = ddlItemType.SelectedValue = "0";
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.PurchaseOrderHeaders where a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text) select a;
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

            Response.Redirect("~/PurchaseReport/webPurchaseOrderRepot.aspx");
        }

        protected void btnGetItemData_Click(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                if (ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر المخزن')</script>");
                    return;
                }
                var Rows = from a in Mdb.Items where a.Code ==long.Parse( txtItemCode.Text) select a;
                if (Rows.Count() <= 0)
                {
                    Response.Write("<script>alert('هذا الكود غير موجود بقاعدة البيانات')</script>");
                    return;
                }
                else
                {
                    ADAM.DataBase.Item itmdr = Mdb.Items.Single(a => a.Code ==long.Parse( txtItemCode.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue));
                    GetItemDatabyID(itmdr.Id);
                }
            }
        }

        private void GetItemDatabyID(long itemID)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.Item itmdr = Mdb.Items.Single(a => a.Id == itemID);
            txtItemCode.Text = itmdr.Code.ToString();
            ddlItemName.SelectedValue = itmdr.Id.ToString();
            ADAM.DataBase.ItemUnit unitdr = Mdb.ItemUnits.Single(a => a.Id == itmdr.ItemunitId);
            lblItemUnit.Text = unitdr.Name;
            lblLimitQty.Text = itmdr.LimitQty.ToString();
            ADAM.DataBase.SexData sexdr = Mdb.SexDatas.Single(a => a.Id == itmdr.Sex);
            lblSex.Text = sexdr.Sex;
            ADAM.DataBase.ItemStatus itmstatsudr = Mdb.ItemStatus.Single(a => a.Id == itmdr.ItemStatus);
            lblItemstatus.Text = itmstatsudr.Status;
            lblSpecification.Text = itmdr.Specification;
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemType.SelectedValue == "0")
            {
                ddlItemName.SelectedValue = "0";
                Response.Write("<script>alert('من فضلك اختر المخزن')</script>");
                return;
            }
            GetItemDatabyID(long.Parse(ddlItemName.SelectedValue));
        }

        protected void btnSavePurchaseItem_Click(object sender, ImageClickEventArgs e)
        {
            if (hfId.Value == "0")
            {
                ddlItemType.Enabled = false;
                btnSave_Click(sender, e);
                SaveItempurcahseDetailsData(long.Parse(hfId.Value));
            }
            else
                SaveItempurcahseDetailsData(long.Parse(hfId.Value));
        }

        private void SaveItempurcahseDetailsData(long PurchaseHeaderID)
        {
            if (PurchaseHeaderID == 0)
                return;

            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.PurchaseOredrDetail Detailsdr = new DataBase.PurchaseOredrDetail();
            Detailsdr.ConformQty = 0;
            Detailsdr.IsChecked = 0;
            Detailsdr.IsClosed = 0;
            Detailsdr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
            Detailsdr.ItemId = long.Parse(ddlItemName.SelectedValue);
            Detailsdr.Note = txtPurchaseNote.Text;
            Detailsdr.Qty = decimal.Parse(txtQty.Text);
            Detailsdr.Status = 1;
            Detailsdr.PurchaseOredeHeaderId = PurchaseHeaderID;
            Mdb.PurchaseOredrDetails.Add(Detailsdr);
            Mdb.SaveChanges();
            gvPurchaseDetailsData.DataBind();
        }

        protected void gvPurchaseDetailsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.PurchaseOredrDetail PurchaseDetailsdr = Mdb.PurchaseOredrDetails.Single(a => a.Id == long.Parse(gvPurchaseDetailsData.SelectedDataKey.Value.ToString()));
            hfPurchaseDetailsId.Value = PurchaseDetailsdr.Id.ToString();
            GetItemDatabyID(PurchaseDetailsdr.ItemId);
            ddlItemColor.SelectedValue = PurchaseDetailsdr.ItemColorId.ToString();
            txtQty.Text = PurchaseDetailsdr.Qty.ToString();
            txtPurchaseNote.Text = PurchaseDetailsdr.Note;
        }

        protected void btnEditPurchaseItem_Click(object sender, ImageClickEventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.PurchaseOredrDetail PurcahseOrderdetaildr = Mdb.PurchaseOredrDetails.Single(a => a.Id == long.Parse(hfPurchaseDetailsId.Value));
            if (PurcahseOrderdetaildr.Status > 1)
            {
                Response.Write("<script>alert('لا يمكن تعديل هذا الصنف حيث انه لم يعُد جديد')</script>");
                return;
            }
            else
            {
                PurcahseOrderdetaildr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
                PurcahseOrderdetaildr.ItemId = long.Parse(ddlItemName.SelectedValue);
                PurcahseOrderdetaildr.Note = txtPurchaseNote.Text;
                PurcahseOrderdetaildr.Qty = decimal.Parse(txtQty.Text);
                Mdb.SaveChanges();
                gvPurchaseDetailsData.DataBind();
            }
        }

        protected void btndeletePurchaseItem_Click(object sender, ImageClickEventArgs e)
        {
             ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.PurchaseOredrDetail PurcahseOrderdetaildr = Mdb.PurchaseOredrDetails.Single(a => a.Id == long.Parse(hfPurchaseDetailsId.Value));
            if (PurcahseOrderdetaildr.Status > 1)
            {
                Response.Write("<script>alert('لا يمكن حذف هذا الصنف حيث انه لم يعُد جديد')</script>");
                return;
            }
            else
            {
                Mdb.PurchaseOredrDetails.Remove(PurcahseOrderdetaildr);
                Mdb.SaveChanges();
                gvPurchaseDetailsData.DataBind();
                var PurchaseOrderHeaderRows = from a in Mdb.PurchaseOredrDetails where a.PurchaseOredeHeaderId == long.Parse(hfId.Value) select a;
                if (PurchaseOrderHeaderRows.Count() <= 0)
                {
                    ADAM.DataBase.PurchaseOrderHeader PurchaseHeadedr = Mdb.PurchaseOrderHeaders.Single(a => a.Id == long.Parse(hfId.Value));
                    Mdb.PurchaseOrderHeaders.Remove(PurchaseHeadedr);
                    Mdb.SaveChanges();
                    btnNew_Click(sender, e);
                }
            }
        }

        protected void ddlItemColor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}