using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Account
{
    public partial class webJournal : System.Web.UI.Page
    {
        public int pageid = 120;

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

        protected void txtAccountNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                var Rows = from a in db.Accounts where a.AccountCode == long.Parse(txtAccountNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.Account accdr = db.Accounts.Single(a => a.AccountCode == long.Parse(txtAccountNo.Text));
                    ddlAccountName.SelectedValue = accdr.Id.ToString();
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                    return;
                }
            }
            catch { }
        }

        protected void ddlAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();

                ADAM.DataBase.Account accdr = db.Accounts.Single(a => a.Id == long.Parse(ddlAccountName.SelectedValue));
                txtAccountNo.Text = accdr.AccountCode.ToString();

            }
            catch { }
        }

        protected void btnGetAllAccount_Click(object sender, EventArgs e)
        {
            divAccount.Visible = true;
            divCostCenter.Visible = divData.Visible = false;
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Account/webJournal.aspx");
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnGetCostCenter_Click(object sender, EventArgs e)
        {
            divCostCenter.Visible = true;
            divAccount.Visible = divData.Visible = false;
        }

        protected void gvAcccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
            ADAM.DataBase.Account accdr = db.Accounts.Single(a => a.Id == long.Parse(gvAcccount.SelectedDataKey.Value.ToString()));
            ddlAccountName.SelectedValue = accdr.Id.ToString();
            txtAccountNo.Text = accdr.AccountCode.ToString();

            divData.Visible = true;
            divCostCenter.Visible = divAccount.Visible = false;
        }

        protected void gvCostCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
            ADAM.DataBase.CostCenter costdr = db.CostCenters.Single(a => a.Id == long.Parse(gvCostCenter.SelectedDataKey.Value.ToString()));
            ddlCostCenter.SelectedValue = costdr.Id.ToString();

            divData.Visible = true;
            divAccount.Visible = divCostCenter.Visible = false;
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

            if (string.IsNullOrEmpty(txtDebit.Text) || txtDebit.Text == "0")
            {
                if (string.IsNullOrEmpty(txtCredit.Text) || txtCredit.Text == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من أدخال قيمة في المدين او الدائن')</script>");
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtCredit.Text) || txtCredit.Text == "0")
            {
                if (string.IsNullOrEmpty(txtDebit.Text) || txtDebit.Text == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من أدخال قيمة في المدين او الدائن')</script>");
                    return;
                }
            }

            BasicData.csJournal InsertJournal = new csJournal();
            if (hfJournalHeaderId.Value == "0")
            {
                long JournalHeaderId = InsertJournal.InsertIntoJournalHeader(long.Parse(txtjournalCode.Text), DateTime.Parse(txtjournalDate.Text), int.Parse(ddlJournaType.SelectedValue), txtJournalNote.Text, 0, 0);
                hfJournalHeaderId.Value = JournalHeaderId.ToString();
            }

            InsertJournal.InsertIntoJournalDetails(long.Parse(ddlAccountName.SelectedValue), long.Parse(ddlCostCenter.SelectedValue), decimal.Parse(txtDebit.Text), decimal.Parse(txtCredit.Text), long.Parse(hfJournalHeaderId.Value), txtNote.Text);
            gvJournalDetails.DataBind();
            EmpData();
        }

        public void EmpData()
        {
            ddlAccountName.SelectedValue = ddlCostCenter.SelectedValue = "0";
            txtDebit.Text = txtCredit.Text = "0";
            txtNote.Text = txtAccountNo.Text = "";
        }
    }
}