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
    public partial class webrptSupplierFollowUp : System.Web.UI.Page
    {
        public int pageid = 113;

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
            myReportDocument.Load(Server.MapPath("~/CostsReport/Reports/rptSupplierFollowUp.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);

            myReportDocument.SetParameterValue("@SupplierDataId", long.Parse(ddlSupplierName.SelectedValue));

            if (string.IsNullOrEmpty(txtFPurchaseDate.Text))
                txtFPurchaseDate.Text = "2000-01-01";
            if (string.IsNullOrEmpty(txtEPurchaseDate.Text))
                txtEPurchaseDate.Text = "3000-01-01";

            myReportDocument.SetParameterValue("@FPurchaseDate", DateTime.Parse(txtFPurchaseDate.Text));
            myReportDocument.SetParameterValue("@EPurchaseDate", DateTime.Parse(txtEPurchaseDate.Text));

            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }

        protected void txtSupplierCode_TextChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.SupplierData dr = mdb.SupplierDatas.Single(a => a.Code == long.Parse(txtSupplierCode.Text));
            ddlSupplierName.SelectedValue = dr.Id.ToString();
        }

        protected void btnShowDiv_Click(object sender, EventArgs e)
        {
            divData.Visible = false;
            divSupplier.Visible = true;
        }

        protected void gvSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            divData.Visible = true;
            divSupplier.Visible = false;
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.SupplierData dr = mdb.SupplierDatas.Single(a => a.Id == long.Parse(gvSupplier.SelectedDataKey.Value.ToString()));
            ddlSupplierName.SelectedValue = dr.Id.ToString();
            txtSupplierCode.Text = dr.Code.ToString();
        }

        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.SupplierData dr = mdb.SupplierDatas.Single(a => a.Id == long.Parse(ddlSupplierName.SelectedValue));
            txtSupplierCode.Text = dr.Code.ToString();
        }
    }
}