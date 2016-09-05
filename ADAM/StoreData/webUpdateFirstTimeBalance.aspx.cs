using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webUpdateFirstTimeBalance : System.Web.UI.Page
    {
        public int pageid = 58;

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

        protected void gvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ItemMovement dr = Mdb.ItemMovements.Single(a => a.Id == long.Parse(gvItems.SelectedDataKey.Value.ToString()));
            
            TextBox txtMainQty = gvItems.SelectedRow.FindControl("txtMainQty") as TextBox;
            TextBox txtAdditionalQty = gvItems.SelectedRow.FindControl("txtAdditionalQty") as TextBox;
            DropDownList ddlLocation = gvItems.SelectedRow.FindControl("ddlLocation") as DropDownList;
            DropDownList ddlItemColor = gvItems.SelectedRow.FindControl("ddlItemColor") as DropDownList;

            dr.MainQty = decimal.Parse(txtMainQty.Text);
            dr.AdditionalQty = decimal.Parse(txtAdditionalQty.Text);
            dr.LocatioId = long.Parse(ddlLocation.SelectedValue);
            dr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);

            Mdb.SaveChanges();
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();

                ADAM.DataBase.ItemMovement dr = Mdb.ItemMovements.Single(a => a.Id == long.Parse(gvItems.DataKeys[e.Row.RowIndex].Value.ToString()));
                TextBox txtMainQty = e.Row.FindControl("txtMainQty") as TextBox;
                txtMainQty.Text = dr.MainQty.ToString();
                TextBox txtAdditionalQty = e.Row.FindControl("txtAdditionalQty") as TextBox;
                txtAdditionalQty.Text = dr.AdditionalQty.ToString();
                DropDownList ddlLocation = e.Row.FindControl("ddlLocation") as DropDownList;
                ddlLocation.SelectedValue = dr.LocatioId.ToString();
                DropDownList ddlItemColor = e.Row.FindControl("ddlItemColor") as DropDownList;
                ddlItemColor.SelectedValue = dr.ItemColorId.ToString();
            }
        }
    }
}