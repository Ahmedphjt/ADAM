﻿using ADAM.BasicData;
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
    public partial class webCheckAuditReport : System.Web.UI.Page
    {
        public int pageid = 52;

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
                if (string.IsNullOrEmpty(txtRecordReceiptNo.Text))
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم محضر الاستلام')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in Mdb.RecordReceiptHeaders where a.RecordReceiptNo == long.Parse(txtRecordReceiptNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.RecordReceiptHeader dr = Mdb.RecordReceiptHeaders.Single(a => a.RecordReceiptNo == long.Parse(txtRecordReceiptNo.Text));
                    var DRows = from a in Mdb.RecordReceiptDetails where a.RecordReceiptHeaderId == dr.Id select a;
                    foreach (ADAM.DataBase.RecordReceiptDetail ddr in DRows)
                    {
                        var AuditRows = from a in Mdb.AuditDetails where a.RecordReceiptDetailsId == ddr.Id && (a.AcceptQty == 0 || a.RefusedQty == 0) select a;
                        if (AuditRows.Count() <= 0)
                            Response.Write("<script>alert('لا يوجد اصناف تحت الفحص لطباعتها')</script>");
                        else
                            ShowReport();
                    }
                    Response.Write("<script>alert('لا يوجد اصناف تحت الفحص لطباعتها')</script>");
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم محضر الاستلام')</script>");
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

                myReportDocument.Load(Server.MapPath("~/StoreReport/Report/rptCheckAudit.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                long RecordReceiptNo = 0;
                long AuditNo = 0;

                if (!string.IsNullOrEmpty(txtRecordReceiptNo.Text))
                    RecordReceiptNo = long.Parse(txtRecordReceiptNo.Text);

                if (!string.IsNullOrEmpty(txtAuditNo.Text))
                    AuditNo = long.Parse(txtAuditNo.Text);

                //myReportDocument.SetDatabaseLogon(csGetPermission.DBUser, csGetPermission.DBPassword, csGetPermission.DBServerName, csGetPermission.DBName);
                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@RecordReceiptNo", RecordReceiptNo);
                myReportDocument.SetParameterValue("@AuditNo", AuditNo);

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }

        protected void gvAudit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ReportDocument myReportDocument = new ReportDocument();

                myReportDocument.Load(Server.MapPath("~/StoreReport/Report/rptCheckAudit.rpt"));
                myReportDocument.Refresh();
                CrystalReportViewer1.ReportSource = myReportDocument;

                long RecordReceiptNo = long.Parse(gvAudit.SelectedDataKey.Values[0].ToString());
                long AuditNo = long.Parse(gvAudit.SelectedDataKey.Values[1].ToString()); ;

                //myReportDocument.SetDatabaseLogon(csGetPermission.DBUser, csGetPermission.DBPassword, csGetPermission.DBServerName, csGetPermission.DBName);
                myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
                myReportDocument.SetParameterValue("@RecordReceiptNo", RecordReceiptNo);
                myReportDocument.SetParameterValue("@AuditNo", AuditNo);

                myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            catch { }
        }
    }
}