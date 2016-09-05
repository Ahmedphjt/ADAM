using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webDisRefluxOrder : System.Web.UI.Page
    {
        public int pageid = 80;

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

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/StoreData/webUpdateRefluxOrederData.aspx");
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

        private void ShowData()
        {
            try
            {
                if (ddlRefluxType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر نوع طلب الارتجاع')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtRefluxNo.Text))
                {
                    Response.Write("<script>alert('من فضلك ادخل رقم الطلب')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in mdb.RefluxHeaderDatas
                           where a.RefluxNo == long.Parse(txtRefluxNo.Text) && a.OrderType == int.Parse(ddlRefluxType.SelectedValue)
                           select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.RefluxHeaderData headerdr = mdb.RefluxHeaderDatas.Single(a => a.RefluxNo == long.Parse(txtRefluxNo.Text) && a.OrderType == int.Parse(ddlRefluxType.SelectedValue));
                    RefluxHeaderId.Value = headerdr.Id.ToString();
                    gvReflux.DataBind();
                }
            }
            catch { }
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 5;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");
        }

        protected void gvReflux_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.RefluxDetailsData dr = mdb.RefluxDetailsDatas.Single(a => a.Id == long.Parse(gvReflux.SelectedDataKey.Value.ToString()));
            ADAM.DataBase.Item itmdr = mdb.Items.Single(a => a.Id == dr.ItemId);
            ADAM.DataBase.ItemColor itmcolordr = mdb.ItemColors.Single(a => a.Id == dr.ItemColorId);
            ADAM.DataBase.ItemUnit unitdr = mdb.ItemUnits.Single(a => a.Id == itmdr.ItemunitId);
            txtItemCode.Text = itmdr.Code.ToString();
            ddlItemName.SelectedValue = itmdr.Id.ToString();
            ddlItemColor.SelectedValue = itmcolordr.Id.ToString();
            txtQty.Text = dr.RefluxQty.ToString();
            txtFreeQty.Text = dr.RefluxFreeQty.ToString();
            lblItemUnit.Text = unitdr.Name;
            RefluxDetailsId.Value = dr.Id.ToString();
            txtIncommingOrder.Text = dr.IncommingOrderNo.ToString();
            txtIncommingOrder.Enabled = false;
        }

        //protected void btnEditOrderItem_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
        //        ADAM.DataBase.RefluxDetailsData ddr = mdb.RefluxDetailsDatas.Single(a => a.Id == long.Parse(RefluxDetailsId.Value));
        //        if (ddr.RefluxHeaderData.OrderType == 1)
        //        {
        //            ADAM.DataBase.PurchaseOredrDetail purchasedr = mdb.PurchaseOredrDetails.Single(a => a.Id == ddr.ExchangeOrPurchaseDetailsId);
        //            if (decimal.Parse(txtQty.Text) > purchasedr.ConformQty)
        //            {
        //                Response.Write("<script>alert('لا يمكن ان تكون الكمية المرتجعة اكبر من الكمية الموجوده في طلب الشراء')</script>");
        //                return;
        //            }
        //        }

        //        if (ddr.RefluxHeaderData.OrderType == 2)
        //        {
        //            ADAM.DataBase.ExchangeRequestDetailsData detailsdr = mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == ddr.ExchangeOrPurchaseDetailsId);
        //            if (decimal.Parse(txtQty.Text) > detailsdr.Qty)
        //            {
        //                Response.Write("<script>alert('لا يمكن ان تكون الكمية المرتجعة اكبر من الكمية الموجوده في طلب الصرف')</script>");
        //                return;
        //            }

        //            if (decimal.Parse(txtFreeQty.Text) > detailsdr.FreeQty)
        //            {
        //                Response.Write("<script>alert('لا يمكن ان تكون الكمية المجانية المرتجعة اكبر من الكمية الموجوده في طلب الصرف')</script>");
        //                return;
        //            }
        //        }

        //        ddr.RefluxQty = decimal.Parse(txtQty.Text);
        //        ddr.RefluxFreeQty = decimal.Parse(txtFreeQty.Text);

        //        mdb.SaveChanges();
        //        gvReflux.DataBind();
        //    }
        //    catch { }
        //}

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.RefluxDetailsData ddr = mdb.RefluxDetailsDatas.Single(a => a.Id == long.Parse(RefluxDetailsId.Value));
            if (ddr.RefluxHeaderData.OrderType == 1)
            {
                ADAM.DataBase.PurchaseOredrDetail Pdr = mdb.PurchaseOredrDetails.Single(a => a.Id == ddr.ExchangeOrPurchaseDetailsId);
                if (Pdr.Status == 2)
                {
                    Pdr.IsClosed = 1;
                }
                else
                {
                    Response.Write("<script>alert('لا يمكن ارتجاع هذا الصنف حيث انه ليس في حالة جديد لطلب الشراء')</script>");
                    return;
                }
            }
            else if (ddr.RefluxHeaderData.OrderType == 2)
            {
                ADAM.DataBase.ExchangeRequestDetailsData Edr = mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == ddr.ExchangeOrPurchaseDetailsId);
                if (Edr.Status == 0)
                {
                    Response.Write("<script>alert('لا يمكن ارتجاع هذا الصنف حيث انه لم يتم صرفه')</script>");
                    return;
                }
                else if (Edr.Status != 0)
                {

                    long incommingOrder = 0;
                    long AuditDetailsId = 0;
                    long MovementnameId = 6;

                    var incomeRows = from a in mdb.ItemMovements
                                     where (a.MovmentnameId == 3 || a.MovmentnameId == 6) && a.ItemId == ddr.ItemId && a.ItemColorId == ddr.ItemColorId && a.IncommingOrderNo == long.Parse(txtIncommingOrder.Text)
                                     select a;

                    if (incomeRows.Count() <= 0)
                    {
                        Response.Write("<script>alert('من فضلك تأكد من رقم أذن الوارد')</script>");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtIncommingOrder.Text) && long.Parse(txtIncommingOrder.Text) > 0)
                    {
                        incommingOrder = long.Parse(txtIncommingOrder.Text);
                        MovementnameId = 3;
                    }

                    ADAM.DataBase.ItemMovement itmmovdr = mdb.ItemMovements.Single(a => a.IncommingOrderNo == incommingOrder && a.AuditDetailsId == AuditDetailsId &&
                        a.MovmentnameId == MovementnameId && a.ItemColorId == ddr.ItemColorId && a.ItemId == ddr.ItemId);

                    itmmovdr.MainQtyOut = itmmovdr.MainQtyOut - (ddr.RefluxQty + ddr.Bounce);
                    itmmovdr.AdditionalQtyOut = itmmovdr.AdditionalQtyOut - ddr.RefluxFreeQty;

                    ADAM.DataBase.ItemMovement Nmov = new DataBase.ItemMovement();
                    Nmov.AdditionalQty = ddr.RefluxFreeQty;
                    Nmov.AuditDetailsId = AuditDetailsId;
                    Nmov.DocmentId = ddr.Id;
                    Nmov.IncommingOrderNo = incommingOrder;
                    Nmov.ItemColorId = itmmovdr.ItemColorId;
                    Nmov.ItemId = ddr.ItemId;
                    Nmov.LocatioId = itmmovdr.LocatioId;
                    Nmov.ItemUnitId = itmmovdr.ItemUnitId;
                    Nmov.MainQty = ddr.RefluxQty + ddr.Bounce;
                    Nmov.MainQtyOut = Nmov.AdditionalQtyOut = 0;
                    Nmov.MovementDate = DateTime.Now;
                    Nmov.MovmentnameId = 13;
                    Nmov.RecDate = DateTime.Now;
                    Nmov.StoreId = itmmovdr.StoreId;
                    Nmov.SupplyOrderDetailsId = itmmovdr.SupplyOrderDetailsId;
                    Nmov.ParentItemMoveMentId = itmmovdr.Id;
                    mdb.ItemMovements.Add(Nmov);
                }
                ddr.Status = 1;
            }
            mdb.SaveChanges();
            gvReflux.DataBind();
        }
    }
}