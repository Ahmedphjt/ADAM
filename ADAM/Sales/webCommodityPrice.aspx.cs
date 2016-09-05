using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Sales
{
    public partial class webCommodityPrice : System.Web.UI.Page
    {
        public int pageid = 99;

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
            Response.Redirect("~/Sales/webMaterialPrice.aspx");
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

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtExchangeRequestNo.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم طلب الصرف')</script>");
                return;
            }

            txtExchangeRequestNo.Enabled = false;

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var HRows = from a in mdb.ExchangeRequestHeaderDatas
                        where a.OrderType == int.Parse(hfOrderValue.Value) && a.ExchangeRequestNo == int.Parse(txtExchangeRequestNo.Text)
                        select a;
            if (HRows.Count() > 0)
            {
                ADAM.DataBase.ExchangeRequestHeaderData Hdr = mdb.ExchangeRequestHeaderDatas.Single(a => a.ExchangeRequestNo == int.Parse(txtExchangeRequestNo.Text)
                    && a.OrderType == int.Parse(hfOrderValue.Value) );

                hfId.Value = Hdr.Id.ToString();
                ADAM.DataBase.ClientData client = mdb.ClientDatas.Single(a => a.Id == Hdr.ClientId);
                ADAM.DataBase.division Ddr = mdb.divisions.Single(a => a.Id == Hdr.DivisionId);
                ADAM.DataBase.Department Depdr = mdb.Departments.Single(a => a.Id == Ddr.DepartmentId);
                ddlDepartment.SelectedValue = Depdr.Id.ToString();
                ddlDivision.DataBind();
                ddlDivision.SelectedValue = Ddr.Id.ToString();
                ddlEmployee.DataBind();
                ddlEmployee.SelectedValue = Hdr.EmpId.ToString();
                txtDate.Text = Hdr.ExchangeRequestDate.ToString("yyyy-MM-dd");
                txtClientAddress.Text = client.Address;
                txtClientMob.Text = client.FirstMobile;
                txtClientName.Text = client.FirstName + " " + client.LastName;
                txtClientPhone.Text = client.FirstPhone;
                gvExchangeRequestData.DataBind();
            }
        }

        protected void gvExchangeRequestData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlSalsType = gvExchangeRequestData.SelectedRow.FindControl("ddlSalsType") as DropDownList;
                if (ddlSalsType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك أدخل نوع السعر')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ExchangeRequestDetailsData ddr = mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == long.Parse(gvExchangeRequestData.SelectedDataKey.Value.ToString()));
                var Rows = from a in mdb.ExchangeRequestPricings where a.ExchangeRequestDetailsId == ddr.Id select a;
                if (Rows.Count() > 0)
                {
                    Response.Write("<script>alert('لقد تم التسعير من قبل')</script>");
                    return;
                }
                ADAM.DataBase.ExchangeRequestPricing dr = new DataBase.ExchangeRequestPricing();
                dr.DiscPresent = 0;
                dr.DiscQty = 0;
                dr.ExchangeRequestDetailsId = ddr.Id;
                dr.Note = "";

                long ItemPriceId = long.Parse(gvExchangeRequestData.SelectedDataKey[1].ToString());
                ADAM.DataBase.ItemPrice itmpricdr = mdb.ItemPrices.Single(a => a.Id == ItemPriceId);
                dr.MainClausePrice = itmpricdr.MainClausePrice;
                dr.MainSalesPrice = itmpricdr.MainSalesPrice;
                dr.MainShowsPrice = itmpricdr.MainShowsPrice;
                dr.TesterClausePrice = itmpricdr.TesterClausePrice;
                dr.TesterSalesPrice = itmpricdr.TesterSalesPrice;
                dr.TesterShowsPrice = itmpricdr.TesterShowsPrice;

                if (ddlSalsType.SelectedValue == "1")
                {
                    dr.TInvoicePrice = itmpricdr.TesterSalesPrice;
                    dr.InvoicePrice = itmpricdr.MainSalesPrice;
                }

                if (ddlSalsType.SelectedValue == "2")
                {
                    dr.InvoicePrice = itmpricdr.MainClausePrice;
                    dr.TInvoicePrice = itmpricdr.TesterClausePrice;
                }

                mdb.ExchangeRequestPricings.Add(dr);
                mdb.SaveChanges();
                Response.Write("<script>alert('تم التسعير بنجاح')</script>");
            }
            catch { return; }
        }

        protected void gvExchangeRequestData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                long ExchangeRequestDetailsDataId = long.Parse(gvExchangeRequestData.DataKeys[e.Row.RowIndex].Value.ToString());
                var Rows = from a in mdb.ExchangeRequestPricings where a.ExchangeRequestDetailsId == ExchangeRequestDetailsDataId select a;
                if (Rows.Count() > 0)
                    e.Row.Visible = false;
            }
        }
    }
}