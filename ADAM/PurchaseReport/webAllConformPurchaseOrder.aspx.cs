using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.PurchaseReport
{
    public partial class webAllConformPurchaseOrder : System.Web.UI.Page
    {
        public int pageid = 43;

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

                myReportDocument.Load(Server.MapPath("~/PurchaseReport/Report/rptGetAllConformedPurchaseOrder.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                long Department = 0;
                long Division = 0;

                if (ddlDepartment.SelectedValue != "0")
                    Department = long.Parse(ddlDepartment.SelectedValue);
                if (ddlDivision.SelectedValue != "0")
                    Division = long.Parse(ddlDivision.SelectedValue);

                //myReportDocument.SetDatabaseLogon(csGetPermission.DBUser, csGetPermission.DBPassword, csGetPermission.DBServerName, csGetPermission.DBName);
                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@PurchaseOrderNo", 0);
                myReportDocument.SetParameterValue("@DepartmentID", Department);
                myReportDocument.SetParameterValue("@DivisionID", Division);
                myReportDocument.SetParameterValue("@Status", 2);
                myReportDocument.SetParameterValue("@SStatus", 3);

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }
    }
}