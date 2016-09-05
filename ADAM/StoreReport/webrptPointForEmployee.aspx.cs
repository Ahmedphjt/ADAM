using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreReport
{
    public partial class webrptPointForEmployee : System.Web.UI.Page
    {
        public int pageid = 147;

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

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            try
            {
                ReportDocument myReportDocument = new ReportDocument();

                myReportDocument.Load(Server.MapPath("~/StoreReport/Report/rptPointForEmployee.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                DateTime BeforeExchangeRequestDate = DateTime.Parse(txtBeforeExchangeRequestDate.Text);
                DateTime AfterExchangeRequestDate = DateTime.Parse(txtAfterExchangeRequestDate.Text);

                DateTime NBeforeExchangeRequestDate = new DateTime(BeforeExchangeRequestDate.Year, BeforeExchangeRequestDate.Month, BeforeExchangeRequestDate.Day, 00, 00, 01);
                DateTime NAfterExchangeRequestDate = new DateTime(AfterExchangeRequestDate.Year, AfterExchangeRequestDate.Month, AfterExchangeRequestDate.Day, 23, 59, 59);
            

                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@EmpId", long.Parse(ddlEmployee.SelectedValue));
                myReportDocument.SetParameterValue("@BeforeExchangeRequestDate", NBeforeExchangeRequestDate);
                myReportDocument.SetParameterValue("@AfterExchangeRequestDate", NAfterExchangeRequestDate);

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }
    }
}