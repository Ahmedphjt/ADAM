using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Account
{
    public partial class webAccountHelper : System.Web.UI.Page
    {
        public int pageid = 142;

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

                ShowData();
            }
        }

        private void ShowData()
        {
            try {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Account Accdr = new DataBase.Account();
                var Rows = from a in db.AccountHelpers select a;
                foreach (ADAM.DataBase.AccountHelper dr in Rows)
                {
                    if (dr.AccountId != 0)
                    {
                        Accdr = db.Accounts.Single(a => a.Id == dr.AccountId);
                        if (dr.Id == 1)
                        {
                            ddlSalesAccount.SelectedValue = Accdr.Id.ToString();
                            txtSalesAccount.Text = Accdr.AccountCode.ToString();
                        }

                        if (dr.Id == 2)
                        {
                            ddlSalesCost.SelectedValue = Accdr.Id.ToString();
                            txtSalesCost.Text = Accdr.AccountCode.ToString();
                        }

                        if (dr.Id == 3)
                        {
                            ddlReturnSalesAccount.SelectedValue = Accdr.Id.ToString();
                            txtReturnSalesAccount.Text = Accdr.AccountCode.ToString();
                        }

                        if (dr.Id == 4)
                        {
                            ddlAllowedDiscount.SelectedValue = Accdr.Id.ToString();
                            txtAllowedDiscount.Text = Accdr.AccountCode.ToString();
                        }

                        if (dr.Id == 5)
                        {
                            ddlReturnOldeYears.SelectedValue = Accdr.Id.ToString();
                            txtReturnOldeYears.Text = Accdr.AccountCode.ToString();
                        }

                        if (dr.Id == 6)
                        {
                            ddlRetuenYearsCost.SelectedValue = Accdr.Id.ToString();
                            txtRetuenYearsCost.Text = Accdr.AccountCode.ToString();
                        }

                        if (dr.Id == 7)
                        {
                            ddlReturnPurchase.SelectedValue = Accdr.Id.ToString();
                            txtReturnPurchase.Text = Accdr.AccountCode.ToString();
                        }

                        if (dr.Id == 10)
                        {
                            ddlWinDiscount.SelectedValue = Accdr.Id.ToString();
                            txtWinDiscount.Text = Accdr.AccountCode.ToString();
                        }

                        if (dr.Id == 11)
                        {
                            ddlFreeQty.SelectedValue = Accdr.Id.ToString();
                            txtFreeQty.Text = Accdr.AccountCode.ToString();
                        }
                    }
                }
            }
            catch { }
        }

        public string GetAccountId(string Code)
        {
            try
            {
                long AccountCode = long.Parse(Code);
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                var Rows = from a in db.Accounts where a.AccountCode == AccountCode select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.Account dr = db.Accounts.Single(a => a.AccountCode == AccountCode);
                    return dr.Id.ToString();
                }
                else
                    return "";
            }
            catch { return ""; }
        }

        protected void txtSalesAccount_TextChanged(object sender, EventArgs e)
        {
            if (GetAccountId(txtSalesAccount.Text) == "")
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                return;
            }

            ddlSalesAccount.SelectedValue = GetAccountId(txtSalesAccount.Text);
        }

        protected void txtSalesCost_TextChanged(object sender, EventArgs e)
        {
            if (GetAccountId(txtSalesCost.Text) == "")
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                return;
            }

            ddlSalesCost.SelectedValue = GetAccountId(txtSalesCost.Text);
        }

        protected void txtReturnSalesAccount_TextChanged(object sender, EventArgs e)
        {
            if (GetAccountId(txtReturnSalesAccount.Text) == "")
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                return;
            }

            ddlReturnSalesAccount.SelectedValue = GetAccountId(txtReturnSalesAccount.Text);
        }

        protected void txtAllowedDiscount_TextChanged(object sender, EventArgs e)
        {
            if (GetAccountId(txtAllowedDiscount.Text) == "")
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                return;
            }

            ddlAllowedDiscount.SelectedValue = GetAccountId(txtAllowedDiscount.Text);
        }

        protected void txtReturnOldeYears_TextChanged(object sender, EventArgs e)
        {
            if (GetAccountId(txtReturnOldeYears.Text) == "")
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                return;
            }

            ddlReturnOldeYears.SelectedValue = GetAccountId(txtReturnOldeYears.Text);
        }

        protected void txtRetuenYearsCost_TextChanged(object sender, EventArgs e)
        {
            if (GetAccountId(txtRetuenYearsCost.Text) == "")
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                return;
            }

            ddlRetuenYearsCost.SelectedValue = GetAccountId(txtRetuenYearsCost.Text);
        }

        protected void txtReturnPurchase_TextChanged(object sender, EventArgs e)
        {
            if (GetAccountId(txtReturnPurchase.Text) == "")
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                return;
            }

            ddlReturnPurchase.SelectedValue = GetAccountId(txtReturnPurchase.Text);
        }

        protected void txtWinDiscount_TextChanged(object sender, EventArgs e)
        {
            if (GetAccountId(txtWinDiscount.Text) == "")
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                return;
            }

            ddlWinDiscount.SelectedValue = GetAccountId(txtWinDiscount.Text);
        }

        protected void txtFreeQty_TextChanged(object sender, EventArgs e)
        {
            if (GetAccountId(txtFreeQty.Text) == "")
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                return;
            }

            ddlFreeQty.SelectedValue = GetAccountId(txtFreeQty.Text);
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Account/webAccountHelper.aspx");
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                ADAM.DataBase.AccountHelper dr = new DataBase.AccountHelper();
                if (ddlSalesAccount.SelectedValue != "0")
                {
                    dr = db.AccountHelpers.Single(a => a.Id == 1);
                    dr.AccountId = long.Parse(ddlSalesAccount.SelectedValue);
                }

                if (ddlSalesCost.SelectedValue != "0")
                {
                    dr = db.AccountHelpers.Single(a => a.Id == 2);
                    dr.AccountId = long.Parse(ddlSalesCost.SelectedValue);
                }

                if (ddlReturnSalesAccount.SelectedValue != "0")
                {
                    dr = db.AccountHelpers.Single(a => a.Id == 3);
                    dr.AccountId = long.Parse(ddlReturnSalesAccount.SelectedValue);
                }

                if (ddlAllowedDiscount.SelectedValue != "0")
                {
                    dr = db.AccountHelpers.Single(a => a.Id == 4);
                    dr.AccountId = long.Parse(ddlAllowedDiscount.SelectedValue);
                }

                if (ddlReturnOldeYears.SelectedValue != "0")
                {
                    dr = db.AccountHelpers.Single(a => a.Id == 5);
                    dr.AccountId = long.Parse(ddlReturnOldeYears.SelectedValue);
                }

                if (ddlRetuenYearsCost.SelectedValue != "0")
                {
                    dr = db.AccountHelpers.Single(a => a.Id == 6);
                    dr.AccountId = long.Parse(ddlRetuenYearsCost.SelectedValue);
                }

                if (ddlReturnPurchase.SelectedValue != "0")
                {
                    dr = db.AccountHelpers.Single(a => a.Id == 7);
                    dr.AccountId = long.Parse(ddlReturnPurchase.SelectedValue);
                }

                if (ddlWinDiscount.SelectedValue != "0")
                {
                    dr = db.AccountHelpers.Single(a => a.Id == 10);
                    dr.AccountId = long.Parse(ddlWinDiscount.SelectedValue);
                }

                if (ddlFreeQty.SelectedValue != "0")
                {
                    dr = db.AccountHelpers.Single(a => a.Id == 11);
                    dr.AccountId = long.Parse(ddlFreeQty.SelectedValue);
                }

                db.SaveChanges();
            }
            catch { }
        }

        public string GetAccountCode(long Id)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Account dr = db.Accounts.Single(a => a.Id == Id);
                return dr.AccountCode.ToString();
            }
            catch { return ""; }
        }

        protected void ddlSalesAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSalesAccount.Text = GetAccountCode(long.Parse(ddlSalesAccount.SelectedValue));
        }

        protected void ddlSalesCost_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSalesCost.Text = GetAccountCode(long.Parse(ddlSalesCost.SelectedValue));
        }

        protected void ddlReturnSalesAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReturnSalesAccount.Text = GetAccountCode(long.Parse(ddlReturnSalesAccount.SelectedValue));
        }

        protected void ddlAllowedDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAllowedDiscount.Text = GetAccountCode(long.Parse(ddlAllowedDiscount.SelectedValue));
        }

        protected void ddlReturnOldeYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReturnOldeYears.Text = GetAccountCode(long.Parse(ddlReturnOldeYears.SelectedValue));
        }

        protected void ddlRetuenYearsCost_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRetuenYearsCost.Text = GetAccountCode(long.Parse(ddlRetuenYearsCost.SelectedValue));
        }

        protected void ddlReturnPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReturnPurchase.Text = GetAccountCode(long.Parse(ddlReturnPurchase.SelectedValue));
        }

        protected void ddlWinDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtWinDiscount.Text = GetAccountCode(long.Parse(ddlWinDiscount.SelectedValue));
        }

        protected void ddlFreeQty_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFreeQty.Text = GetAccountCode(long.Parse(ddlFreeQty.SelectedValue));
        }

        protected void gvAcccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
            ADAM.DataBase.Account dr = db.Accounts.Single(a => a.Id == long.Parse(gvAcccount.SelectedDataKey.Value.ToString()));

            if (hfControlName.Value == "btnSalesAccount")
            {
                ddlSalesAccount.SelectedValue = dr.Id.ToString();
                txtSalesAccount.Text = dr.AccountCode.ToString();
            }

            if (hfControlName.Value == "btnSalesCost")
            {
                ddlSalesCost.SelectedValue = dr.Id.ToString();
                txtSalesCost.Text = dr.AccountCode.ToString();
            }

            if (hfControlName.Value == "btnReturnSalesAccount")
            {
                ddlReturnSalesAccount.SelectedValue = dr.Id.ToString();
                txtReturnSalesAccount.Text = dr.AccountCode.ToString();
            }

            if (hfControlName.Value == "btnAllowedDiscount")
            {
                ddlAllowedDiscount.SelectedValue = dr.Id.ToString();
                txtAllowedDiscount.Text = dr.AccountCode.ToString();
            }

            if (hfControlName.Value == "btnReturnOldeYears")
            {
                ddlReturnOldeYears.SelectedValue = dr.Id.ToString();
                txtReturnOldeYears.Text = dr.AccountCode.ToString();
            }

            if (hfControlName.Value == "btnRetuenYearsCost")
            {
                ddlRetuenYearsCost.SelectedValue = dr.Id.ToString();
                txtRetuenYearsCost.Text = dr.AccountCode.ToString();
            }

            if (hfControlName.Value == "btnReturnPurchase")
            {
                ddlReturnPurchase.SelectedValue = dr.Id.ToString();
                txtReturnPurchase.Text = dr.AccountCode.ToString();
            }

            if (hfControlName.Value == "btnWinDiscount")
            {
                ddlWinDiscount.SelectedValue = dr.Id.ToString();
                txtWinDiscount.Text = dr.AccountCode.ToString();
            }

            if (hfControlName.Value == "btnFreeQty")
            {
                ddlFreeQty.SelectedValue = dr.Id.ToString();
                txtFreeQty.Text = dr.AccountCode.ToString();
            }

            divAccount.Visible = false;
            divData.Visible = true;
        }

        protected void btnSalesAccount_Click(object sender, EventArgs e)
        {
            hfControlName.Value = "btnSalesAccount";
            divAccount.Visible = true;
            divData.Visible = false;
        }

        protected void btnSalesCost_Click(object sender, EventArgs e)
        {
            hfControlName.Value = "btnSalesCost";
            divAccount.Visible = true;
            divData.Visible = false;
        }

        protected void btnReturnSalesAccount_Click(object sender, EventArgs e)
        {
            hfControlName.Value = "btnReturnSalesAccount";
            divAccount.Visible = true;
            divData.Visible = false;
        }

        protected void btnAllowedDiscount_Click(object sender, EventArgs e)
        {
            hfControlName.Value = "btnAllowedDiscount";
            divAccount.Visible = true;
            divData.Visible = false;
        }

        protected void btnReturnOldeYears_Click(object sender, EventArgs e)
        {
            hfControlName.Value = "btnReturnOldeYears";
            divAccount.Visible = true;
            divData.Visible = false;
        }

        protected void btnRetuenYearsCost_Click(object sender, EventArgs e)
        {
            hfControlName.Value = "btnRetuenYearsCost";
            divAccount.Visible = true;
            divData.Visible = false;
        }

        protected void btnReturnPurchase_Click(object sender, EventArgs e)
        {
            hfControlName.Value = "btnReturnPurchase";
            divAccount.Visible = true;
            divData.Visible = false;
        }

        protected void btnWinDiscount_Click(object sender, EventArgs e)
        {
            hfControlName.Value = "btnWinDiscount";
            divAccount.Visible = true;
            divData.Visible = false;
        }

        protected void btnFreeQty_Click(object sender, EventArgs e)
        {
            hfControlName.Value = "btnFreeQty";
            divAccount.Visible = true;
            divData.Visible = false;
        }
    }
}