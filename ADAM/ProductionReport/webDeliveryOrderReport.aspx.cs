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
    public partial class webDeliveryOrderReport : System.Web.UI.Page
    {
        public int pageid = 85;

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
            txtDeliveryOrder.Text = gvDeliveryData.SelectedRow.Cells[0].Text;
            ShowReportData();
        }

        private void ShowReportData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDeliveryOrder.Text))
                {
                    Response.Write("<script>alert('من فضلك أدخل رقم طلب تسليم منتج تام')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in Mdb.DeliveryDataHeaders where a.DeliveryNo == long.Parse(txtDeliveryOrder.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.DeliveryDataHeader dr = Mdb.DeliveryDataHeaders.Single(a => a.DeliveryNo == long.Parse(txtDeliveryOrder.Text));
                    var DRows = from a in Mdb.DeliveryDataDetails where a.DeliveryDataHeaderId == dr.Id select a;
                   
                    foreach (ADAM.DataBase.DeliveryDataDetail ddr in DRows)
                    {
                        var AuditRows = from a in Mdb.DeliveryDataDetails where a.DeliveryDataHeaderId == dr.Id && a.Status == 0 select a;
                        if (AuditRows.Count() > 0)
                            ShowReport();
                        else
                            Response.Write("<script>alert('لا يوجد اصناف لم يتم تسليمها')</script>");
                    }   
                 
                }

                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم طلب تسليم منتج تام')</script>");
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

                myReportDocument.Load(Server.MapPath("~/ProductionReport/Report/rptDeliveryOrder.rpt"));
                myReportDocument.Refresh();               
                CrystalReportViewer1.ReportSource = myReportDocument;

                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@DeliveryNo", long.Parse(txtDeliveryOrder.Text));

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            ShowReportData();
        }
    }
}