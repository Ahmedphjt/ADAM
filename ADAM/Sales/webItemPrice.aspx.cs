using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Sales
{
    public partial class webItemPrice : System.Web.UI.Page
    {
        public int pageid = 110;

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

        protected void gvItemPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 1;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            long ItemColorSelectedId = long.Parse(gvItemPrice.SelectedDataKey.Value.ToString());

            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemColorSelected itmcolordr = mdb.ItemColorSelecteds.Single(a => a.Id == ItemColorSelectedId);
                var Rows = from a in mdb.ItemPrices where a.ItemColorId == itmcolordr.ItemColorId && a.ItemId == itmcolordr.ItemId select a;
                ADAM.DataBase.ItemPrice itmpricedr = new DataBase.ItemPrice();

                if (Rows.Count() > 0)
                    itmpricedr = mdb.ItemPrices.Single(a => a.ItemId == itmcolordr.ItemId && a.ItemColorId == itmcolordr.ItemColorId);

                TextBox txtMainClausePrice = gvItemPrice.SelectedRow.FindControl("txtMainClausePrice") as TextBox;
                TextBox txtMainSalesPrice = gvItemPrice.SelectedRow.FindControl("txtMainSalesPrice") as TextBox;
                TextBox txtMainShowsPrice = gvItemPrice.SelectedRow.FindControl("txtMainShowsPrice") as TextBox;
                TextBox txtTesterClausePrice = gvItemPrice.SelectedRow.FindControl("txtTesterClausePrice") as TextBox;
                TextBox txtTesterSalesPrice = gvItemPrice.SelectedRow.FindControl("txtTesterSalesPrice") as TextBox;
                TextBox txtTesterShowsPrice = gvItemPrice.SelectedRow.FindControl("txtTesterShowsPrice") as TextBox;

                if (string.IsNullOrEmpty(txtMainClausePrice.Text)) txtMainClausePrice.Text = "0";
                if (string.IsNullOrEmpty(txtMainSalesPrice.Text)) txtMainSalesPrice.Text = "0";
                if (string.IsNullOrEmpty(txtMainShowsPrice.Text)) txtMainShowsPrice.Text = "0";
                if (string.IsNullOrEmpty(txtTesterClausePrice.Text)) txtTesterClausePrice.Text = "0";
                if (string.IsNullOrEmpty(txtTesterSalesPrice.Text)) txtTesterSalesPrice.Text = "0";
                if (string.IsNullOrEmpty(txtTesterShowsPrice.Text)) txtTesterShowsPrice.Text = "0";

                itmpricedr.ItemColorId = itmcolordr.ItemColorId;
                itmpricedr.ItemId = itmcolordr.ItemId;
                itmpricedr.MainClausePrice = decimal.Parse(txtMainClausePrice.Text);
                itmpricedr.MainSalesPrice = decimal.Parse(txtMainSalesPrice.Text);
                itmpricedr.MainShowsPrice = decimal.Parse(txtMainShowsPrice.Text);
                itmpricedr.TesterClausePrice = decimal.Parse(txtTesterClausePrice.Text);
                itmpricedr.TesterSalesPrice = decimal.Parse(txtTesterSalesPrice.Text);
                itmpricedr.TesterShowsPrice = decimal.Parse(txtTesterShowsPrice.Text);

                if (Rows.Count() <= 0)
                    mdb.ItemPrices.Add(itmpricedr);

                mdb.SaveChanges();
            }
            catch { }
        }

        protected void gvItemPrice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex != -1)
                {
                    TextBox txtMainClausePrice = e.Row.FindControl("txtMainClausePrice") as TextBox;
                    TextBox txtMainSalesPrice = e.Row.FindControl("txtMainSalesPrice") as TextBox;
                    TextBox txtMainShowsPrice = e.Row.FindControl("txtMainShowsPrice") as TextBox;
                    TextBox txtTesterClausePrice = e.Row.FindControl("txtTesterClausePrice") as TextBox;
                    TextBox txtTesterSalesPrice = e.Row.FindControl("txtTesterSalesPrice") as TextBox;
                    TextBox txtTesterShowsPrice = e.Row.FindControl("txtTesterShowsPrice") as TextBox;

                    ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                    long ItemColorSelectedId = long.Parse(gvItemPrice.DataKeys[e.Row.RowIndex].Value.ToString());
                    ADAM.DataBase.ItemColorSelected itmcolordr = mdb.ItemColorSelecteds.Single(a => a.Id == ItemColorSelectedId);
                    var Rows = from a in mdb.ItemPrices where a.ItemColorId == itmcolordr.ItemColorId && a.ItemId == itmcolordr.ItemId select a;
                    if (Rows.Count() > 0)
                    {
                        ADAM.DataBase.ItemPrice itmpricedr = mdb.ItemPrices.Single(a => a.ItemId == itmcolordr.ItemId && a.ItemColorId == itmcolordr.ItemColorId);

                        txtMainClausePrice.Text = itmpricedr.MainClausePrice.ToString();
                        txtMainSalesPrice.Text = itmpricedr.MainSalesPrice.ToString();
                        txtMainShowsPrice.Text = itmpricedr.MainShowsPrice.ToString();
                        txtTesterClausePrice.Text = itmpricedr.TesterClausePrice.ToString();
                        txtTesterSalesPrice.Text = itmpricedr.TesterSalesPrice.ToString();
                        txtTesterShowsPrice.Text = itmpricedr.TesterShowsPrice.ToString();
                    }
                    else
                    {
                        txtMainClausePrice.Text = txtMainSalesPrice.Text = txtMainShowsPrice.Text = "0";
                        txtTesterClausePrice.Text = txtTesterSalesPrice.Text = txtTesterShowsPrice.Text = "0";
                    }
                }
            }
            catch { }
        }
    }
}