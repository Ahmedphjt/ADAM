using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webItemTypeProductionLine : System.Web.UI.Page
    {
        public int pageid = 109;

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
            Response.Redirect("~/MainData/webItemTypeProductionLine.aspx");
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

            SaveData();
        }

        private void SaveData()
        {
            try {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemTypeProdcutionLine dr = new DataBase.ItemTypeProdcutionLine();
                dr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                dr.ProdctionLineId = int.Parse(ddlProductionLine.SelectedValue);
                mdb.ItemTypeProdcutionLines.Add(dr);
                mdb.SaveChanges();
                gvItemTypeProductionLine.DataBind();
            }
            catch { }
        }

        protected void gvItemTypeProductionLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ItemTypeProdcutionLine dr = mdb.ItemTypeProdcutionLines.Single(a => a.Id == long.Parse(gvItemTypeProductionLine.SelectedDataKey.Value.ToString()));
            mdb.ItemTypeProdcutionLines.Remove(dr);
            mdb.SaveChanges();
        }        
    }
}