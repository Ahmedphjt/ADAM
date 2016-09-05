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
    public partial class webItemReport : System.Web.UI.Page
    {
        public int pageid = 34;
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
            if (rdoNormalItem.Checked)
                myReportDocument.Load(Server.MapPath("~/MainReport/Reports/rptNormalItems.rpt"));
            else if (rdoSpecItem.Checked)
                myReportDocument.Load(Server.MapPath("~/MainReport/Reports/rptSpecItems.rpt"));

            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;

            long ItemTypeId = 0;
            int sex = 0;
            long ItemStatus = 0;
            long ItemId = 0;
            string ItemCode = "0";

            if (ddlItemType.SelectedValue != "")
                ItemTypeId = long.Parse(ddlItemType.SelectedValue);
            if (ddlSex.SelectedValue != "")
                sex = int.Parse(ddlSex.SelectedValue);
            if (ddlItemStatus.SelectedValue != "")
                ItemStatus = long.Parse(ddlItemStatus.SelectedValue);
            if (ddlItems.SelectedValue != "")
                ItemId = long.Parse(ddlItems.SelectedValue);
            if (!string.IsNullOrEmpty(txtCode.Text))
                ItemCode = txtCode.Text;

            //myReportDocument.SetDatabaseLogon(csGetPermission.DBUser, csGetPermission.DBPassword, csGetPermission.DBServerName, csGetPermission.DBName);
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@ItemTypeId", ItemTypeId);
            myReportDocument.SetParameterValue("@Sex", sex);
            myReportDocument.SetParameterValue("@ItemStatus", ItemStatus);
            myReportDocument.SetParameterValue("@ItemId", ItemId);
            myReportDocument.SetParameterValue("@ItemCode", ItemCode);

            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}