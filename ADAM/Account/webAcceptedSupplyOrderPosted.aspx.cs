using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Account
{
    public partial class webAcceptedSupplyOrderPosted : System.Web.UI.Page
    {
        public int pageid = 139;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                    Response.Redirect("~/BasicData/webLogIn.aspx");
                int userid = int.Parse(Session["UserID"].ToString());
                int operationid = 4;

                csGetPermission Per = new csGetPermission();
                if (!Per.getPermission(userid, pageid, operationid))
                    Response.Redirect("~/BasicData/webHomePage.aspx");
            }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Account/webAcceptedSupplyOrderPosted.aspx");
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 1;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                ADAM.DataBase.JournalHeader dr = db.JournalHeaders.Single(a => a.Id == long.Parse(gvJorunal.SelectedDataKey.Value.ToString()));
                dr.Posted = 1;
                db.SaveChanges();
                gvJorunal.DataBind();
                hfJournalHeaderId.Value = "0";
                gvJorunalDetails.DataBind();
            }
            catch { }
        }

        protected void gvJorunal_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfJournalHeaderId.Value = gvJorunal.SelectedDataKey.Value.ToString();
        }

        protected void ddlJournaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfJournalHeaderId.Value = "0";
            gvJorunalDetails.DataBind();
        }
    }
}