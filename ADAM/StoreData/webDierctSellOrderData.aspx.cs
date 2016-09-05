using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webDierctSellOrderData : System.Web.UI.Page
    {
        public int pageid = 76;

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
            var Rows = from a in mdb.DirectSellDatas orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtDirectSellOrderNo.Text = "1";
            else
            {
                ADAM.DataBase.DirectSellData dr = Rows.First();
                txtDirectSellOrderNo.Text = (dr.DirectSellNo + 1).ToString();
            }
        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/StoreData/webDierctSellOrderData.aspx");
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

            txtDirectSellOrderNo.Enabled = false;
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
                    Response.Write("<script>alert('من فضلك ادخل تاريخ طلب الصرف')</script>");
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
                var RepCode = from a in Mdb.DirectSellDatas where a.DirectSellNo == long.Parse(txtDirectSellOrderNo.Text) select a;
                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن تكرار رقم طلب الصرف')</script>");
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
                var Rows = from a in Mdb.DirectSellDatas where a.DirectSellNo == long.Parse(txtDirectSellOrderNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    #region ShowHeader
                    ADAM.DataBase.DirectSellData dr = Mdb.DirectSellDatas.Single(a => a.DirectSellNo == long.Parse(txtDirectSellOrderNo.Text));
                    txtDirectSellOrderNo.Text = dr.DirectSellNo.ToString();
                    txtDate.Text = dr.DirectSellDate.ToString("yyyy-MM-dd");
                    ADAM.DataBase.EmployeeData Empdr = Mdb.EmployeeDatas.Single(a => a.Id == dr.EmpId);
                    ADAM.DataBase.division divdr = Mdb.divisions.Single(a => a.Id == Empdr.DivisionId);
                    ADAM.DataBase.Department depId = Mdb.Departments.Single(a => a.Id == divdr.DepartmentId);

                    ddlDepartment.SelectedValue = depId.Id.ToString();
                    ddlDivision.DataBind();
                    ddlDivision.SelectedValue = divdr.Id.ToString();
                    ddlEmployee.DataBind();
                    ddlEmployee.SelectedValue = dr.EmpId.ToString();
                    ddlItemType.SelectedValue = dr.ItemType.ToString();
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
                if (string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtDirectSellOrderNo.Text)
                    || ddlDivision.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlEmployee.SelectedValue == "0" || ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.DirectSellData dr = Mdb.DirectSellDatas.Single(a => a.DirectSellNo == long.Parse(txtDirectSellOrderNo.Text));
                if (Validation())
                {
                    dr.DirectSellDate = DateTime.Parse(txtDate.Text);
                    dr.DirectSellNo = long.Parse(txtDirectSellOrderNo.Text);
                    dr.EmpId = long.Parse(ddlEmployee.SelectedValue);

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
                ADAM.DataBase.DirectSellData dr = new DataBase.DirectSellData();

                dr.DirectSellDate = DateTime.Parse(txtDate.Text);
                dr.DirectSellNo = long.Parse(txtDirectSellOrderNo.Text);
                dr.EmpId = long.Parse(ddlEmployee.SelectedValue);
                dr.ItemType = long.Parse(ddlItemType.SelectedValue);
                Mdb.DirectSellDatas.Add(dr);

                Mdb.SaveChanges();
                hfId.Value = dr.Id.ToString();
                Mdb.SaveChanges();
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }

        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.DirectSellData dr = Mdb.DirectSellDatas.Single(a => a.DirectSellNo == long.Parse(txtDirectSellOrderNo.Text));
                var DirectSellDetailsRows = from a in Mdb.DierctSellDetails where a.DirectSellHeaderId == dr.Id select a;

                foreach (ADAM.DataBase.DierctSellDetail ddr in DirectSellDetailsRows)
                {
                    Mdb.DierctSellDetails.Remove(ddr);
                    Mdb.SaveChanges();
                }

                Mdb.DirectSellDatas.Remove(dr);
                Mdb.SaveChanges();
                txtDate.Text = txtDirectSellOrderNo.Text = "";
                ddlEmployee.SelectedValue = ddlDepartment.SelectedValue = ddlDivision.SelectedValue = ddlItemType.SelectedValue = "0";
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.DirectSellDatas where a.DirectSellNo == long.Parse(txtDirectSellOrderNo.Text) select a;
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

            //Response.Redirect("~/PurchaseReport/webPurchaseOrderRepot.aspx");
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
                var Rows = from a in Mdb.Items where a.Code == long.Parse(txtItemCode.Text) select a;
                if (Rows.Count() <= 0)
                {
                    Response.Write("<script>alert('هذا الكود غير موجود بقاعدة البيانات')</script>");
                    return;
                }
                else
                {
                    ADAM.DataBase.Item itmdr = Mdb.Items.Single(a => a.Code == long.Parse(txtItemCode.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue));
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

            if (decimal.Parse(txtQty.Text) > decimal.Parse(lblCurrentBalance.Text))
            {
                Response.Write("<script>alert('لا يمكن ان تكون الكمية المصروفة اكبر من رصيد المخزن')</script>");
                return;
            }

            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.DierctSellDetail Detailsdr = new DataBase.DierctSellDetail();

            Detailsdr.DirectSellHeaderId = PurchaseHeaderID;
            Detailsdr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
            Detailsdr.ItemId = long.Parse(ddlItemName.SelectedValue);
            Detailsdr.Note = txtPurchaseNote.Text;
            Detailsdr.Qty = decimal.Parse(txtQty.Text);

            Mdb.DierctSellDetails.Add(Detailsdr);
            Mdb.SaveChanges();
            gvPurchaseDetailsData.DataBind();
        }

        protected void gvPurchaseDetailsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.DierctSellDetail PurchaseDetailsdr = Mdb.DierctSellDetails.Single(a => a.Id == long.Parse(gvPurchaseDetailsData.SelectedDataKey.Value.ToString()));

            hfPurchaseDetailsId.Value = PurchaseDetailsdr.Id.ToString();
            GetItemDatabyID(PurchaseDetailsdr.ItemId);
            ddlItemColor.SelectedValue = PurchaseDetailsdr.ItemColorId.ToString();
            txtQty.Text = PurchaseDetailsdr.Qty.ToString();
            txtPurchaseNote.Text = PurchaseDetailsdr.Note;
            ddlItemColor_SelectedIndexChanged(sender, e);
        }

        protected void btnEditPurchaseItem_Click(object sender, ImageClickEventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.DierctSellDetail PurcahseOrderdetaildr = Mdb.DierctSellDetails.Single(a => a.Id == long.Parse(hfPurchaseDetailsId.Value));

            PurcahseOrderdetaildr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
            PurcahseOrderdetaildr.ItemId = long.Parse(ddlItemName.SelectedValue);
            PurcahseOrderdetaildr.Note = txtPurchaseNote.Text;
            PurcahseOrderdetaildr.Qty = decimal.Parse(txtQty.Text);
            Mdb.SaveChanges();
            gvPurchaseDetailsData.DataBind();

        }

        protected void btndeletePurchaseItem_Click(object sender, ImageClickEventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.DierctSellDetail PurcahseOrderdetaildr = Mdb.DierctSellDetails.Single(a => a.Id == long.Parse(hfPurchaseDetailsId.Value));
            Mdb.DierctSellDetails.Remove(PurcahseOrderdetaildr);
            Mdb.SaveChanges();
            gvPurchaseDetailsData.DataBind();
            var PurchaseOrderHeaderRows = from a in Mdb.DierctSellDetails where a.DirectSellHeaderId == long.Parse(hfId.Value) select a;
            if (PurchaseOrderHeaderRows.Count() <= 0)
            {
                ADAM.DataBase.DirectSellData PurchaseHeadedr = Mdb.DirectSellDatas.Single(a => a.Id == long.Parse(hfId.Value));
                Mdb.DirectSellDatas.Remove(PurchaseHeadedr);
                Mdb.SaveChanges();
                btnNew_Click(sender, e);
            }

        }

        protected void ddlItemColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                decimal CurrentBalance = 0;
                decimal FreeBalance = 0;
                var Rows = from a in Mdb.ItemMovements where a.ItemId == long.Parse(ddlItemName.SelectedValue) && a.StoreId == 2 && a.ItemColorId == int.Parse(ddlItemColor.SelectedValue) select a;
                foreach (ADAM.DataBase.ItemMovement itmmovdr in Rows)
                {
                    CurrentBalance = CurrentBalance + itmmovdr.MainQty;
                    FreeBalance = FreeBalance + itmmovdr.AdditionalQty;
                }
                lblCurrentBalance.Text = CurrentBalance.ToString();
            }
            catch { }
        }
    }
}