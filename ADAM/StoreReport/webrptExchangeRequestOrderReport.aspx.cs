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
    public partial class webrptExchangeRequestOrderReport : System.Web.UI.Page
    {
        public int pageid = 75;

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
                var HRows = from a in Mdb.ExchangeRequestHeaderDatas where a.ExchangeRequestNo == long.Parse(txtExchangeRequestNo.Text)  select a;
                if (HRows.Count() <= 0)
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم طلب الصرف')</script>");
                    return;
                }
                var Rows = from a in Mdb.ExchangeRequestDetailsDatas
                           where a.ExchangeRequestHeaderData.ExchangeRequestNo == long.Parse(txtExchangeRequestNo.Text)
                                && a.Status == 1
                           select a;
                if (Rows.Count() > 0)
                    ShowReport();
                else
                    Response.Write("<script>alert('لا يوجد اصناف لم يتم صرفها')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء تحميل التقرير')</script>"); }
        }

        private void ShowReport()
        {
            try
            {
                ReportDocument myReportDocument = new ReportDocument();

                myReportDocument.Load(Server.MapPath("~/StoreReport/Report/rptExchangeRequestOrderReport.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                long ExchangeRequestNo = 0;

                if (!string.IsNullOrEmpty(txtExchangeRequestNo.Text))
                    ExchangeRequestNo = long.Parse(txtExchangeRequestNo.Text);

                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@ExchangeRequestNo", ExchangeRequestNo);
                myReportDocument.SetParameterValue("@OrderType", long.Parse(ddlExchangeRequest.SelectedValue));

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }

        protected void gvAudit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ExchangeRequestDetailsData ddr = mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == long.Parse(gvExcahnge.SelectedDataKey.Value.ToString()));
          
            ddlExchangeRequest.SelectedValue = ddr.ExchangeRequestHeaderData.OrderType.ToString();
            txtExchangeRequestNo.Text = ddr.ExchangeRequestHeaderData.ExchangeRequestNo.ToString();
            ShowReport();
        }
    }
}