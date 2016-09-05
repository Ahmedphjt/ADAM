using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Prodction
{
    public partial class webAcceptedDeliveryOrder : System.Web.UI.Page
    {
        public int pageid = 84;

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
            Response.Redirect("~/Prodction/webAcceptedDeliveryOrder.aspx");
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
                    var detailsRow = from a in Mdb.DeliveryDataDetails
                                     where a.Status == 0 && a.DeliveryDataHeader.DeliveryNo == long.Parse(txtDeliveryOrderNo.Text)
                                     select a;

                    if (detailsRow.Count() <= 0)
                    {
                        Response.Write("<script>alert('لقد تم تسليم هذا الطلب بالكامل من قبل')</script>");
                        return;
                    }

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

        protected void gvPurchaseDetailsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.DeliveryDataDetail PurchaseDetailsdr = Mdb.DeliveryDataDetails.Single(a => a.Id == long.Parse(gvDeliveryData.SelectedDataKey.Value.ToString()));
            hfPurchaseDetailsId.Value = PurchaseDetailsdr.Id.ToString();
            PurchaseDetailsdr.Status = 1;

            DropDownList ddlLoction = gvDeliveryData.SelectedRow.FindControl("ddlLocation") as DropDownList;
            if (ddlLoction.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك أدخل الـ Location')</script>");
                return;
            }

            TextBox txtQty = gvDeliveryData.SelectedRow.FindControl("txtRealQty") as TextBox;
            TextBox txtRealTester = gvDeliveryData.SelectedRow.FindControl("txtRealTester") as TextBox;

            if (string.IsNullOrEmpty(txtQty.Text) || (decimal.Parse(txtQty.Text) == 0 && PurchaseDetailsdr.Qty > 0))
            {
                Response.Write("<script>alert('من فضلك أدخل الكمية بشكل صحيح')</script>");
                return;
            }

            #region Insert Into Movement

            ADAM.DataBase.ItemMovement itmmovdr = new DataBase.ItemMovement();
            itmmovdr.AdditionalQty = decimal.Parse(txtRealTester.Text);
            itmmovdr.AdditionalQtyOut = 0;
            itmmovdr.AuditDetailsId = 0;
            itmmovdr.DocmentId = PurchaseDetailsdr.Id;
            itmmovdr.IncommingOrderNo = 0;
            itmmovdr.ItemColorId = PurchaseDetailsdr.ItemColorId;
            itmmovdr.ItemId = PurchaseDetailsdr.ItemId;
            ADAM.DataBase.Item itmdr = Mdb.Items.Single(a => a.Id == PurchaseDetailsdr.ItemId);
            itmmovdr.ItemUnitId = itmdr.ItemunitId;
            itmmovdr.LocatioId = long.Parse(ddlLoction.SelectedValue);
            itmmovdr.MainQty = decimal.Parse(txtQty.Text);
            itmmovdr.MainQtyOut = 0;
            itmmovdr.MovementDate = DateTime.Now;
            itmmovdr.MovmentnameId = 16;
            itmmovdr.RecDate = DateTime.Now;
            itmmovdr.StoreId = 2;
            itmmovdr.SupplyOrderDetailsId = 0;
            itmmovdr.ParentItemMoveMentId = 0;

            Mdb.ItemMovements.Add(itmmovdr);
            Mdb.SaveChanges();
            #endregion
            gvDeliveryData.DataBind();
        }
    }
}