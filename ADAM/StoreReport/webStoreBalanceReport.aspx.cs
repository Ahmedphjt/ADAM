﻿using ADAM.BasicData;
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
    public partial class webStoreBalanceReport : System.Web.UI.Page
    {
        public int pageid = 103;

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
            ReportDocument myReportDocument = new ReportDocument();
            myReportDocument.Load(Server.MapPath("~/StoreReport/Report/rptStoreBalance.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@ItemTpeId", long.Parse(ddlItemType.SelectedValue));
            myReportDocument.SetParameterValue("@ProductionLineId", long.Parse(ddlProductionLine.SelectedValue));
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}