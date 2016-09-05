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
    public partial class webrptRefluxHeaderData : System.Web.UI.Page
    {
        public int pageid = 81;

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
                var HRows = from a in Mdb.RefluxHeaderDatas where a.RefluxNo == long.Parse(txtExchangeRequestNo.Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue) select a;
                if (HRows.Count() <= 0)
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم طلب الارتجاع')</script>");
                    return;
                }
                //var Rows = from a in Mdb.RefluxDetailsDatas
                //           where a.RefluxHeaderData.RefluxNo == long.Parse(txtExchangeRequestNo.Text)
                //               && a.RefluxHeaderData.ItemTypeId == long.Parse(ddlItemType.SelectedValue)
                //           select a;
                //if (Rows.Count() > 0)
                    ShowReport();
                //else
                //    Response.Write("<script>alert('لا يوجد اصناف لم يتم أرتجاعها')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء تحميل التقرير')</script>"); }
        }

        private void ShowReport()
        {
            try
            {
                ReportDocument myReportDocument = new ReportDocument();

                myReportDocument.Load(Server.MapPath("~/StoreReport/Report/rptRefluxHeaderData.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                long ExchangeRequestNo = 0;

                if (!string.IsNullOrEmpty(txtExchangeRequestNo.Text))
                    ExchangeRequestNo = long.Parse(txtExchangeRequestNo.Text);

                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@RefluxNo", ExchangeRequestNo);
                myReportDocument.SetParameterValue("@ItemTypeId", long.Parse(ddlItemType.SelectedValue));

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }
    }
}