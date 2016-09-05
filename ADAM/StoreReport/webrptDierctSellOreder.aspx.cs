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
    public partial class webrptDierctSellOreder : System.Web.UI.Page
    {
        public int pageid = 77;

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
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var HRows = from a in Mdb.DirectSellDatas where a.DirectSellNo == long.Parse(txtExchangeRequestNo.Text) select a;
                if (HRows.Count() <= 0)
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم طلب الصرف')</script>");
                    return;
                }

                ShowReport();

            }
            catch { Response.Write("<script>alert('خطأ أثناء تحميل التقرير')</script>"); }
        }

        private void ShowReport()
        {
            try
            {
                ReportDocument myReportDocument = new ReportDocument();

                myReportDocument.Load(Server.MapPath("~/StoreReport/Report/rptDierctSellOrder.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                long ExchangeRequestNo = 0;

                if (!string.IsNullOrEmpty(txtExchangeRequestNo.Text))
                    ExchangeRequestNo = long.Parse(txtExchangeRequestNo.Text);

                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@DirectSellNo", ExchangeRequestNo);

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }
    }
}