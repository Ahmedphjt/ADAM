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
    public partial class webProductionOrderReport : System.Web.UI.Page
    {
        public int pageid = 94;

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

        protected void gvPurchaseDetailsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDeliveryOrder.Text = gvProducionOrder.SelectedRow.Cells[0].Text;
            ShowReport(txtDeliveryOrder.Text);
        }

        private void ShowReport(string OrderNo)
        {
            try
            {
                ReportDocument myReportDocument = new ReportDocument();

                myReportDocument.Load(Server.MapPath("~/ProductionReport/Report/rptProductioOrder.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@ProductionNo", long.Parse(txtDeliveryOrder.Text));

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDeliveryOrder.Text))
            {
                Response.Write("<script>alert('من فضلك تأكد من ادخال أمر الانتاج')</script>");
                return;
            }
            ShowReport(txtDeliveryOrder.Text);
        }
    }
}