using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Prodction
{
    public partial class webDeliveryOrder : System.Web.UI.Page
    {
        public int pageid = 83;

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
            var Rows = from a in mdb.DeliveryDataHeaders orderby a.Id descending select a;
            if (Rows.Count() == 0)
                txtDeliveryOrderNo.Text = "1";
            else
            {
                ADAM.DataBase.DeliveryDataHeader dr = Rows.First();
                txtDeliveryOrderNo.Text = (dr.DeliveryNo + 1).ToString();
            }
        }

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Prodction/webDeliveryOrder.aspx");
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

            txtDeliveryOrderNo.Enabled = false;
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
                    Response.Write("<script>alert('من فضلك ادخل تاريخ طلب التسليم')</script>");
                    return;
                }

                if (ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك ادخل نوع الصنف')</script>");
                    return;
                }

                if (ddlProductionLine.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك أختر خط الانتاج')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text) || decimal.Parse(txtQty.Text) <= 0)
                {
                    Response.Write("<script>alert('من فضلك أدخل الكميات بشكل صحيح')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.DeliveryDataHeaders where a.DeliveryNo == long.Parse(txtDeliveryOrderNo.Text) select a;
                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن تكرار رقم طلب التسليم')</script>");
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
                var Rows = from a in Mdb.DeliveryDataHeaders where a.DeliveryNo == long.Parse(txtDeliveryOrderNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    #region ShowHeader
                    ADAM.DataBase.DeliveryDataHeader dr = Mdb.DeliveryDataHeaders.Single(a => a.DeliveryNo == long.Parse(txtDeliveryOrderNo.Text));
                    txtDeliveryOrderNo.Text = dr.DeliveryNo.ToString();
                    txtDate.Text = dr.DeliveryDate.ToString("yyyy-MM-dd");
                    ddlItemType.SelectedValue = dr.ItemTypeId.ToString();
                    ddlProductionLine.SelectedValue = dr.ProductionLineId.ToString();
                    hfId.Value = dr.Id.ToString();
                    ddlItemType.Enabled = false;
                    var EmpRows = from a in Mdb.EmployeeDatas where a.Id == dr.EmpId select a;
                    if (EmpRows.Count() > 0)
                    {
                        ADAM.DataBase.EmployeeData Empdr = Mdb.EmployeeDatas.Single(a => a.Id == dr.EmpId);
                        ddlDepartment.SelectedValue = Empdr.DepartmentId.ToString();
                        ddlDivision.DataBind();
                        ddlDivision.SelectedValue = Empdr.DivisionId.ToString();
                        ddlEmployee.DataBind();
                        ddlEmployee.SelectedValue = Empdr.Id.ToString();
                    }
                    #endregion
                    gvDeliveryData.DataBind();
                }
                else { Response.Write("<script>alert('من فضلك تأكد من رقم طلب التسليم')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EditData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDate.Text) || ddlItemType.SelectedValue == "0" || ddlProductionLine.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.DeliveryDataHeader dr = Mdb.DeliveryDataHeaders.Single(a => a.DeliveryNo == long.Parse(txtDeliveryOrderNo.Text));
                if (Validation())
                {
                    dr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                    dr.DeliveryDate = DateTime.Parse(txtDate.Text);
                    dr.DeliveryNo = long.Parse(txtDeliveryOrderNo.Text);
                    dr.ProductionLineId = int.Parse(ddlProductionLine.SelectedValue);
                    dr.RecoredDate = DateTime.Now;
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
                ADAM.DataBase.DeliveryDataHeader dr = new DataBase.DeliveryDataHeader();
                dr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                dr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                dr.DeliveryDate = DateTime.Parse(txtDate.Text);
                dr.DeliveryNo = long.Parse(txtDeliveryOrderNo.Text);
                dr.EmpId = long.Parse(ddlEmployee.SelectedValue);
                dr.RecoredDate = DateTime.Now;
                dr.ProductionLineId = int.Parse(ddlProductionLine.SelectedValue);
                Mdb.DeliveryDataHeaders.Add(dr);
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
                ADAM.DataBase.DeliveryDataHeader dr = Mdb.DeliveryDataHeaders.Single(a => a.DeliveryNo == long.Parse(txtDeliveryOrderNo.Text));
                var DeliveryDetailsRows = from a in Mdb.DeliveryDataDetails where a.DeliveryDataHeaderId == dr.Id select a;

                foreach (ADAM.DataBase.DeliveryDataDetail ddr in DeliveryDetailsRows)
                {
                    if (ddr.Status == 1)
                    {
                        Response.Write("<script>alert('لا يمكن الحذف الان لانه قد تم تسليم هذه الكمية')</script>");
                        return;
                    }
                }

                foreach (ADAM.DataBase.DeliveryDataDetail ddr in DeliveryDetailsRows)
                {
                    if (ddr.Status == 0)
                    {
                        Mdb.DeliveryDataDetails.Remove(ddr);
                    }
                    Mdb.SaveChanges();
                }

                Mdb.DeliveryDataHeaders.Remove(dr);
                Mdb.SaveChanges();
                txtDate.Text = txtDeliveryOrderNo.Text = "";
                ddlItemType.SelectedValue = ddlProductionLine.SelectedValue = "0";
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private bool Validation()
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in Mdb.DeliveryDataHeaders where a.DeliveryNo == long.Parse(txtDeliveryOrderNo.Text) select a;
            if (Rows.Count() > 0)
                return true;
            else
                return false;
        }
        #endregion

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            //if (Session["UserID"] == null)
            //    Response.Redirect("~/BasicData/webLogIn.aspx");
            //int userid = int.Parse(Session["UserID"].ToString());
            //int operationid = 5;

            //csGetPermission Per = new csGetPermission();
            //if (!Per.getPermission(userid, pageid, operationid))
            //    Response.Redirect("~/BasicData/webHomePage.aspx");

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
                var Rows = from a in Mdb.Items where a.Code ==long.Parse( txtItemCode.Text) select a;
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

            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.DeliveryDataDetail Detailsdr = new DataBase.DeliveryDataDetail();
            Detailsdr.DeliveryDataHeaderId = PurchaseHeaderID;
            Detailsdr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
            Detailsdr.ItemId = long.Parse(ddlItemName.SelectedValue);
            Detailsdr.Qty = decimal.Parse(txtQty.Text);
            Detailsdr.Tester = decimal.Parse(txtTester.Text);
            Detailsdr.Status = 0;
            Mdb.DeliveryDataDetails.Add(Detailsdr);
            Mdb.SaveChanges();
            gvDeliveryData.DataBind();
        }

        protected void gvPurchaseDetailsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.DeliveryDataDetail PurchaseDetailsdr = Mdb.DeliveryDataDetails.Single(a => a.Id == long.Parse(gvDeliveryData.SelectedDataKey.Value.ToString()));
            hfPurchaseDetailsId.Value = PurchaseDetailsdr.Id.ToString();
            GetItemDatabyID(PurchaseDetailsdr.ItemId);
            ddlItemColor.SelectedValue = PurchaseDetailsdr.ItemColorId.ToString();
            txtQty.Text = PurchaseDetailsdr.Qty.ToString();
        }

        protected void btnEditPurchaseItem_Click(object sender, ImageClickEventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.DeliveryDataDetail PurcahseOrderdetaildr = Mdb.DeliveryDataDetails.Single(a => a.Id == long.Parse(hfPurchaseDetailsId.Value));
            if (PurcahseOrderdetaildr.Status > 1)
            {
                Response.Write("<script>alert('لا يمكن تعديل هذا الصنف حيث انه تم تسليمه')</script>");
                return;
            }
            else
            {
                PurcahseOrderdetaildr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
                PurcahseOrderdetaildr.ItemId = long.Parse(ddlItemName.SelectedValue);
                PurcahseOrderdetaildr.Qty = decimal.Parse(txtQty.Text);
                PurcahseOrderdetaildr.Tester = decimal.Parse(txtTester.Text);
                Mdb.SaveChanges();
                gvDeliveryData.DataBind();
            }
        }

        protected void btndeletePurchaseItem_Click(object sender, ImageClickEventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.DeliveryDataDetail PurcahseOrderdetaildr = Mdb.DeliveryDataDetails.Single(a => a.Id == long.Parse(hfPurchaseDetailsId.Value));
            if (PurcahseOrderdetaildr.Status > 1)
            {
                Response.Write("<script>alert('لا يمكن حذف هذا الصنف حيث انه قد تم تسليمه')</script>");
                return;
            }
            else
            {
                Mdb.DeliveryDataDetails.Remove(PurcahseOrderdetaildr);
                Mdb.SaveChanges();
                gvDeliveryData.DataBind();

                var PurchaseOrderHeaderRows = from a in Mdb.DeliveryDataDetails where a.DeliveryDataHeaderId == long.Parse(hfId.Value) select a;
                if (PurchaseOrderHeaderRows.Count() <= 0)
                {
                    ADAM.DataBase.DeliveryDataHeader PurchaseHeadedr = Mdb.DeliveryDataHeaders.Single(a => a.Id == long.Parse(hfId.Value));
                    Mdb.DeliveryDataHeaders.Remove(PurchaseHeadedr);
                    Mdb.SaveChanges();
                    btnNew_Click(sender, e);
                }
            }
        }
    }
}