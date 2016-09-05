using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.AccountReport
{
    public partial class webrptAccountStatement : System.Web.UI.Page
    {
        public int pageid = 131;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 5;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");
        }

        protected void txtAccountCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                var Rows = from a in db.Accounts where a.AccountCode == long.Parse(txtAccountCode.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.Account dr = db.Accounts.Single(a => a.AccountCode == long.Parse(txtAccountCode.Text));
                    ddlAccount.SelectedValue = dr.Id.ToString();
                }
            }
            catch { }
        }

        protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
            ADAM.DataBase.Account dr = db.Accounts.Single(a => a.Id == long.Parse(ddlAccount.SelectedValue));
            txtAccountCode.Text = dr.AccountCode.ToString();
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            ReportDocument myReportDocument = new ReportDocument();

            myReportDocument.Load(Server.MapPath("~/AccountReport/Report/rptAccountStatement.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;

            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@AccountId", long.Parse(ddlAccount.SelectedValue));
            myReportDocument.SetParameterValue("@BeginJournalDate", DateTime.Parse(txtbeginDate.Text));
            myReportDocument.SetParameterValue("@EndJournalDate", DateTime.Parse(txtEndDate.Text));
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }

        protected void btnAccount_Click(object sender, EventArgs e)
        {
            divAccount.Visible = true;
            divData.Visible = false;
        }

        protected void gvAcccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlAccount.SelectedValue = gvAcccount.SelectedDataKey.Value.ToString();
            divData.Visible = true;
            divAccount.Visible = false;
            ddlAccount_SelectedIndexChanged(sender, e);
        }
    }
}