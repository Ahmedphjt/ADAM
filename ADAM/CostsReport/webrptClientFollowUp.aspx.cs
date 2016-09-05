using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.CostsReport
{
    public partial class webrptClientFollowUp : System.Web.UI.Page
    {
        public int pageid = 113;

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

        protected void txtClientName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ClientData dr = mdb.ClientDatas.Single(a => a.Code == long.Parse(txtClienCode.Text));
                ddlClientName.SelectedValue = dr.Id.ToString();
            }
            catch
            {
                Response.Write("<script>alert('من فضلك تأكد من كود العميل')</script>");
                return;
            }
        }

        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ClientData dr = mdb.ClientDatas.Single(a => a.Id == long.Parse(ddlClientName.SelectedValue));
                txtClienCode.Text = dr.Code.ToString();
            }
            catch
            {
                return;
            }
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            ReportDocument myReportDocument = new ReportDocument();
            myReportDocument.Load(Server.MapPath("~/CostsReport/Reports/rptClientFollowUp.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);

            myReportDocument.SetParameterValue("@ClientDataId", long.Parse(ddlClientName.SelectedValue));
            
            if (string.IsNullOrEmpty(txtFExchangeRequestDate.Text))
                txtFExchangeRequestDate.Text = "2000-01-01";
            if (string.IsNullOrEmpty(txtEExchangeRequestDate.Text))
                txtEExchangeRequestDate.Text = "3000-01-01";

            myReportDocument.SetParameterValue("@FExchangeRequestDate", DateTime.Parse(txtFExchangeRequestDate.Text));
            myReportDocument.SetParameterValue("@EExchangeRequestDate", DateTime.Parse(txtEExchangeRequestDate.Text));

            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }

        protected void btnShowDiv_Click(object sender, EventArgs e)
        {
            Data.Visible = false;
            Clients.Visible = true;
        }

        protected void gvClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClientName.SelectedValue = gvClient.SelectedDataKey.Value.ToString();
            ddlClientName_SelectedIndexChanged(sender, e);
            Data.Visible = true;
            Clients.Visible = false;
        }
    }
}