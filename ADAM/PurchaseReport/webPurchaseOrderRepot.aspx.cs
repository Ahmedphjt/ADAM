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
    public partial class webPurchaseOrderRepot : System.Web.UI.Page
    {
        public int pageid = 40;

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
                if (string.IsNullOrEmpty(txtPurchaseOrderNo.Text))
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم طلب الشراء')</script>");
                    return;
                }
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in Mdb.PurchaseOrderHeaders where a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.PurchaseOrderHeader dr = Mdb.PurchaseOrderHeaders.Single(a => a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text));
                    var DRows = from a in Mdb.PurchaseOredrDetails where a.PurchaseOredeHeaderId == dr.Id select a;
                    foreach (ADAM.DataBase.PurchaseOredrDetail ddr in DRows)
                    {
                        if (ddr.Status != 1)
                            continue;
                        else
                            ShowReport();
                    }
                    Response.Write("<script>alert('لا يوجد اصناف غير معتمدة لطباعتها')</script>");
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم طلب الشراء')</script>");
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

                myReportDocument.Load(Server.MapPath("~/PurchaseReport/Report/rptGetPurchaseOrderData.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                long PurcahseOrder = 0;
                if (!string.IsNullOrEmpty(txtPurchaseOrderNo.Text))
                    PurcahseOrder = long.Parse(txtPurchaseOrderNo.Text);

                //myReportDocument.SetDatabaseLogon(csGetPermission.DBUser, csGetPermission.DBPassword, csGetPermission.DBServerName, csGetPermission.DBName);
                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@PurchaseOrderNo", PurcahseOrder);
                myReportDocument.SetParameterValue("@DepartmentID", 0);
                myReportDocument.SetParameterValue("@DivisionID", 0);
                myReportDocument.SetParameterValue("@Status", 1);
                myReportDocument.SetParameterValue("@SStatus", 1);

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }

        protected void gvPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ReportDocument myReportDocument = new ReportDocument();

                myReportDocument.Load(Server.MapPath("~/PurchaseReport/Report/rptGetPurchaseOrderData.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                long PurcahseOrder = long.Parse(gvPurchaseOrder.SelectedDataKey.Value.ToString());

                txtPurchaseOrderNo.Text = PurcahseOrder.ToString();

                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@PurchaseOrderNo", PurcahseOrder);
                myReportDocument.SetParameterValue("@DepartmentID", 0);
                myReportDocument.SetParameterValue("@DivisionID", 0);
                myReportDocument.SetParameterValue("@Status", 1);
                myReportDocument.SetParameterValue("@SStatus", 1);

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }
    }
}