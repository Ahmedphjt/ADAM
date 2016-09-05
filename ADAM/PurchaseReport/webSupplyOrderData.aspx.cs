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
    public partial class webSupplyOrderData : System.Web.UI.Page
    {
        public int pageid = 45;

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
            try
            {
                if (string.IsNullOrEmpty(txtSupplyOrderNo.Text))
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم طلب امر التوريد')</script>");
                    return;
                }
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in Mdb.SupplyOrderHeaders where a.SupplyOrderNo == long.Parse(txtSupplyOrderNo.Text) select a;
                if (Rows.Count() > 0)
                    ShowReport();
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم امر التوريد')</script>");
                    return;
                }
            }
            catch { Response.Write("<script>alert('خطأ أثناء تحميل التقرير')</script>"); }
        }

        private void ShowReport()
        {
            try
            {
                ReportDocument myReportDocument = new ReportDocument();

                myReportDocument.Load(Server.MapPath("~/PurchaseReport/Report/rptGetSupplyOrderData.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                long SupplyOrderNo = 0;
                if (!string.IsNullOrEmpty(txtSupplyOrderNo.Text))
                    SupplyOrderNo = long.Parse(txtSupplyOrderNo.Text);

                //myReportDocument.SetDatabaseLogon(csGetPermission.DBUser, csGetPermission.DBPassword, csGetPermission.DBServerName, csGetPermission.DBName);
                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@SupplyOrderNo", SupplyOrderNo);

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }
    }
}