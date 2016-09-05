using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Account
{
    public partial class webPurchaseBill : System.Web.UI.Page
    {
        public int pageid = 133;

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

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {

            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 3;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                long SupplyOrderNo = 0;
                if (string.IsNullOrEmpty(txtSupplyOrderNo.Text))
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم أمر التوريد')</script>");
                    return;
                }
                else
                    SupplyOrderNo = long.Parse(txtSupplyOrderNo.Text);
                var SupplyRows = from a in db.SupplyOrderHeaders where a.SupplyOrderNo == SupplyOrderNo select a;
                if (SupplyRows.Count() > 0)
                {
                    ADAM.DataBase.SupplyOrderHeader SHdr = db.SupplyOrderHeaders.Single(a => a.SupplyOrderNo == SupplyOrderNo);

                    if (SHdr.Posted == 1)
                    {
                        Response.Write("<script>alert('لقد تم أنشاء فاتورة شراء من قبل هذا الامر')</script>");
                        hfSupplyheaderId.Value = "0";
                        gvSupplyData.DataBind();
                        return;
                    }

                    txtSupplyOrderDate.Text = SHdr.SupplyOrderDate.ToString("yyyy-MM-dd");
                    ddlVendorName.SelectedValue = SHdr.SupplierId.ToString();
                    txtVendorCode.Text = db.SupplierDatas.Single(a => a.Id == SHdr.SupplierId).Code.ToString();
                    hfSupplyheaderId.Value = SHdr.Id.ToString();
                    gvSupplyData.DataBind();

                    decimal AllPrice = 0;

                    for (int GRow = 0; GRow < gvSupplyData.Rows.Count; GRow++)
                    {
                        decimal Qty = 0;
                        decimal Price = 0;
                        Qty = decimal.Parse(gvSupplyData.Rows[GRow].Cells[4].Text);
                        Price = decimal.Parse(gvSupplyData.Rows[GRow].Cells[5].Text);
                        AllPrice += Qty * Price;
                    }

                    lblBillPrice.Text = AllPrice.ToString();
                    txtSupplyOrderNo.Enabled = false;
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم أمر التوريد')</script>");
                    return;
                }
            }
            catch { }
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
                long SupplierAccount = db.SupplierDatas.Single(a => a.Id == long.Parse(ddlVendorName.SelectedValue)).AccountId;
                long BoxAccount = long.Parse(ddlBoxName.SelectedValue);
                csJournal csAddjournal = new csJournal();
                long JournalHeaderId = csAddjournal.InsertIntoJournalHeader(0, DateTime.Now, 15, "قيد فاتورة شراء", 0, long.Parse(hfSupplyheaderId.Value));
                csAddjournal.InsertIntoJournalDetails(SupplierAccount, long.Parse(ddlCostCenterName.SelectedValue), decimal.Parse(lblBillPrice.Text), 0, JournalHeaderId, "من حـ / المورد");
                csAddjournal.InsertIntoJournalDetails(BoxAccount, long.Parse(ddlCostCenterName.SelectedValue), 0, decimal.Parse(lblBillPrice.Text), JournalHeaderId, "الي حـ / الصندوق");
                ADAM.DataBase.SupplyOrderHeader SOHdr = db.SupplyOrderHeaders.Single(a => a.Id == long.Parse(hfSupplyheaderId.Value.ToString()));
                SOHdr.Posted = 1;
                db.SaveChanges();
            }
            catch { }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Account/webPurchaseBill.aspx");
        }
    }
}