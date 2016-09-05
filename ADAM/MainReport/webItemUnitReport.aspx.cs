using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainReport
{
    public partial class webItemUnitReport : System.Web.UI.Page
    {
        public int pageid = 17;

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
            ReportDocument myReportDocument = new ReportDocument();

            myReportDocument.Load(Server.MapPath("~/MainReport/Reports/rptItemUnit.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;
            //myReportDocument.SetDatabaseLogon(csGetPermission.DBUser, csGetPermission.DBPassword, csGetPermission.DBServerName, csGetPermission.DBName);
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}