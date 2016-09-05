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
    public partial class webPurchaseOrderFollowUp : System.Web.UI.Page
    {
        public int pageid = 115;

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
            myReportDocument.Load(Server.MapPath("~/CostsReport/Reports/rptPurchaseOrderFollowUp.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);

            if (string.IsNullOrEmpty(txtFPurchaseDate.Text))
                txtFPurchaseDate.Text = "2000-01-01";
            if (string.IsNullOrEmpty(txtEPurchaseDate.Text))
                txtEPurchaseDate.Text = "3000-01-01";

            if (DateTime.Parse(txtEPurchaseDate.Text) > DateTime.Now)
            {
                Response.Write("<script>alert('لا يمكن ان يكون تاريخ النهاية اكبر من تاريخ اليوم')</script>");
                return;
            }

            myReportDocument.SetParameterValue("@SupplierDataId", 0);
            myReportDocument.SetParameterValue("@FPurchaseDate", DateTime.Parse(txtFPurchaseDate.Text));
            myReportDocument.SetParameterValue("@EPurchaseDate", DateTime.Parse(txtEPurchaseDate.Text));

            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}