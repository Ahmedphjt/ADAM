using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.ProductionReport
{
    public partial class webContentReport : System.Web.UI.Page
    {
        public int pageid = 92;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 4;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in mdb.ItemContentHeaders
                           where a.ItemType == long.Parse(ddlProductionItemType.SelectedValue) ||
                               a.ProductionLineId == long.Parse(ddlProductProductionLine.SelectedValue) ||
                               a.ProductItemColor == long.Parse(ddlProductionItemcolor.SelectedValue) || a.ProductItemId == long.Parse(ddlProductionItem.SelectedValue)
                           select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.ItemContentHeader header = Rows.Single(a => a.ProductItemId == long.Parse(ddlProductionItem.SelectedValue));
                    ReportDocument myReportDocument = new ReportDocument();
                    myReportDocument.Load(Server.MapPath("~/ProductionReport/Report/rptContentItem.rpt"));
                    myReportDocument.Refresh();
                    CrystalReportViewer1.ReportSource = myReportDocument;

                    myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                    myReportDocument.SetParameterValue("@ItemContentHeaderId", header.Id);

                    myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
                }
            }
            catch { }
        }
    }
}