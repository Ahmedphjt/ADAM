using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Account
{
    public partial class webSaleBill : System.Web.UI.Page
    {
        public int pageid = 134;

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

        protected void btnGetAllAccountforBox_Click(object sender, EventArgs e)
        {
            divAccount.Visible = true;
            divCostCenter.Visible = divData.Visible = false;
        }

        protected void btnGetAllAccountforCostCenter_Click(object sender, EventArgs e)
        {
            divCostCenter.Visible = true;
            divAccount.Visible = divData.Visible = false;
        }

        protected void gvAcccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
            ADAM.DataBase.Account dr = db.Accounts.Single(a => a.Id == long.Parse(gvAcccount.SelectedDataKey.Value.ToString()));
            ddlBoxName.SelectedValue = dr.Id.ToString();
            txtBoxNo.Text = dr.AccountCode.ToString();
            divAccount.Visible = divCostCenter.Visible = false;
            divData.Visible = true;
        }

        protected void gvCostCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCostCenterName.SelectedValue = gvCostCenter.SelectedDataKey.Value.ToString();
            divAccount.Visible = divCostCenter.Visible = false;
            divData.Visible = true;
        }

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (ddlExchangeRequestType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك أختر نوع طلب الصرف')</script>");
                    return;
                }

                if (ddlExchangeRequestType.SelectedValue == "10") hfExchangeRequestType.Value = "8";
                if (ddlExchangeRequestType.SelectedValue == "11") hfExchangeRequestType.Value = "9";
                if (ddlExchangeRequestType.SelectedValue == "12") hfExchangeRequestType.Value = "10";
                if (ddlExchangeRequestType.SelectedValue == "13") hfExchangeRequestType.Value = "11";
                if (ddlExchangeRequestType.SelectedValue == "14") hfExchangeRequestType.Value = "12";

                if (string.IsNullOrEmpty(txtExchangeRequestNo.Text))
                {
                    Response.Write("<script>alert('من فضلك أدخل رقم طلب الصرف')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                var ExchangeRequestRow = from a in db.ExchangeRequestHeaderDatas where a.ExchangeRequestNo == long.Parse(txtExchangeRequestNo.Text) && a.OrderType == long.Parse(hfExchangeRequestType.Value) select a;
                if (ExchangeRequestRow.Count() > 0)
                {
                    ADAM.DataBase.ExchangeRequestHeaderData ERHdr = db.ExchangeRequestHeaderDatas.Single(a => a.ExchangeRequestNo == long.Parse(txtExchangeRequestNo.Text) && a.OrderType == long.Parse(hfExchangeRequestType.Value)); ;
                    if (ERHdr.Posted == 1)
                    {
                        Response.Write("<script>alert('لقد تم انشاء فاتورة لهذا الطلب من قبل')</script>");
                        hfExchangeheaderId.Value = "0";
                        gvExchangeData.DataBind();
                        return;
                    }
                    txtExchangeRequestDate.Text = ERHdr.ExchangeRequestDate.ToString("yyyy-MM-dd");
                    ddlClient.SelectedValue = ERHdr.ClientId.ToString();
                    txtClientCode.Text = db.ClientDatas.Single(a => a.Id == ERHdr.ClientId).Code.ToString();
                    hfExchangeheaderId.Value = ERHdr.Id.ToString();
                    gvExchangeData.DataBind();

                    decimal AllPrice = 0;
                    decimal AllItemCost = 0;
                    ADAM.DataBase.IncommingOrderData incomdr = new DataBase.IncommingOrderData();

                    for (int GRow = 0; GRow < gvExchangeData.Rows.Count; GRow++)
                    {
                        decimal Qty = 0;
                        decimal Price = 0;
                        decimal Tester = 0;
                        decimal TPrice = 0;

                        Qty = decimal.Parse(gvExchangeData.Rows[GRow].Cells[3].Text);
                        Price = decimal.Parse(gvExchangeData.Rows[GRow].Cells[4].Text);
                        Tester = decimal.Parse(gvExchangeData.Rows[GRow].Cells[5].Text);
                        TPrice = decimal.Parse(gvExchangeData.Rows[GRow].Cells[6].Text);

                        incomdr = db.IncommingOrderDatas.Single(a => a.Id == long.Parse(gvExchangeData.DataKeys[GRow].Value.ToString()));
                        AllItemCost += incomdr.ItemPrice * Qty;
                        
                        AllPrice += (Qty * Price) + (Tester * TPrice);
                    }

                    lblBillPrice.Text = AllPrice.ToString();
                    txtExchangeRequestNo.Enabled = false;
                    ViewState["AllItemCost"] = AllItemCost.ToString();
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم طلب الصرف')</script>");
                    return;
                }
            }
            catch { }
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
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();

                long SalesCost = db.AccountHelpers.Single(a => a.Id == 2).AccountId;
                long StoreAccount = db.ItemTypes.Single(a => a.Id == db.ExchangeRequestDetailsDatas.Single(aa => aa.ExchangeRequestHeaderDataId == long.Parse(hfExchangeheaderId.Value.ToString())).ItemTypeId).AccountId;


                long ClientAccount = db.ClientDatas.Single(a => a.Id == long.Parse(ddlClient.SelectedValue)).AccountId;
                long SalesAccount = db.AccountHelpers.Single(a => a.Id == 1).AccountId;

                csJournal csAddjournal = new csJournal();
                long JournalHeaderId = csAddjournal.InsertIntoJournalHeader(0, DateTime.Now, int.Parse(ddlExchangeRequestType.SelectedValue), "قيد فاتورة بيع", 0, long.Parse(hfExchangeheaderId.Value));

                csAddjournal.InsertIntoJournalDetails(SalesAccount, long.Parse(ddlCostCenterName.SelectedValue), decimal.Parse(ViewState["AllItemCost"].ToString()), 0, JournalHeaderId, "من حـ / تكلفة المبيعات");
                csAddjournal.InsertIntoJournalDetails(StoreAccount, long.Parse(ddlCostCenterName.SelectedValue), 0, decimal.Parse(ViewState["AllItemCost"].ToString()), JournalHeaderId, "الي حـ / المخزن");
                
                csAddjournal.InsertIntoJournalDetails(ClientAccount, long.Parse(ddlCostCenterName.SelectedValue), decimal.Parse(lblBillPrice.Text), 0, JournalHeaderId, "من حـ / العميل");
                csAddjournal.InsertIntoJournalDetails(SalesAccount, long.Parse(ddlCostCenterName.SelectedValue), 0, decimal.Parse(lblBillPrice.Text), JournalHeaderId, "الي حـ / المبيعات");

                ADAM.DataBase.ExchangeRequestHeaderData ERHdr = db.ExchangeRequestHeaderDatas.Single(a => a.Id == long.Parse(hfExchangeheaderId.Value));
                ERHdr.Posted = 1;
                db.SaveChanges();

                hfExchangeheaderId.Value = "0";
                gvExchangeData.DataBind();
            }
            catch { }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Account/webSaleBill.aspx");
        }

        protected void txtBoxNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                var Rows = from a in db.Accounts where a.AccountCode == long.Parse(txtBoxNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.Account accdr = db.Accounts.Single(a => a.AccountCode == long.Parse(txtBoxNo.Text));
                    ddlBoxName.SelectedValue = accdr.Id.ToString();
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                    return;
                }
            }
            catch { }
        }

        protected void txtCostCenter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                var Rows = from a in db.CostCenters where a.CostCenterCode == long.Parse(txtCostCenter.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.CostCenter accdr = db.CostCenters.Single(a => a.CostCenterCode == long.Parse(txtCostCenter.Text));
                    ddlCostCenterName.SelectedValue = accdr.Id.ToString();
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم مركز التكلفة')</script>");
                    return;
                }
            }
            catch { }
        }

        protected void ddlBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Account accdr = db.Accounts.Single(a => a.Id == long.Parse(ddlBoxName.SelectedValue));
                txtBoxNo.Text = accdr.AccountCode.ToString();
            }
            catch { }
        }

        protected void ddlCostCenterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                ADAM.DataBase.CostCenter accdr = db.CostCenters.Single(a => a.Id == long.Parse(ddlCostCenterName.SelectedValue));
                txtCostCenter.Text = accdr.CostCenterCode.ToString();
            }
            catch { }
        }

      
    }
}