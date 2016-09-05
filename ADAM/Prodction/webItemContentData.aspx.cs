using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Prodction
{
    public partial class webItemContentData : System.Web.UI.Page
    {
        public int pageid = 90;
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

        protected void gvItemContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 1;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            long ItemId = long.Parse(gvItemContent.SelectedDataKey[0].ToString());
            int ItemColorId = int.Parse(gvItemContent.SelectedDataKey[1].ToString());

            TextBox txtQty = gvItemContent.SelectedRow.FindControl("txtQty") as TextBox;
            SaveData(txtQty.Text,ItemId,ItemColorId);
        }

        private void SaveData(string Qty,long ItemId,int ItemColorId)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 1;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            if (hfItemContentHeaderId.Value == "0")
            {
                ADAM.DataBase.ItemContentHeader Hdr = new DataBase.ItemContentHeader();
                
                Hdr.ProductionLineId = long.Parse(ddlProductProductionLine.SelectedValue);
                Hdr.ProductItemColor = int.Parse(ddlProductionItemcolor.SelectedValue);
                Hdr.ProductItemId = long.Parse(ddlProductionItem.SelectedValue);
                Hdr.ItemType = int.Parse(ddlProductionItemType.SelectedValue);

                mdb.ItemContentHeaders.Add(Hdr);
                mdb.SaveChanges();
                hfItemContentHeaderId.Value = Hdr.Id.ToString();
            }

            var Rows = from a in mdb.ItemContentDetails where a.ItemId == ItemId && a.ItemColorId == ItemColorId select a;
            if (Rows.Count() > 0)
            {
                Response.Write("<script>alert('لا يمكن ادخال نفس الصنف واللون اكثر من مرة في التركيبة الواحدة')</script>");
                return;
            }

            ADAM.DataBase.ItemContentDetail ddr = new DataBase.ItemContentDetail();
            ddr.ItemColorId = ItemColorId;
            ddr.ItemContentHeaderId = long.Parse(hfItemContentHeaderId.Value);
            ddr.ItemId = ItemId;
            ddr.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
            ddr.ProductionLineId = long.Parse(ddlProductionLine.SelectedValue);
            ddr.Qty = decimal.Parse(Qty);

            mdb.ItemContentDetails.Add(ddr);
            mdb.SaveChanges();
            gvItemContentData.DataBind();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Prodction/webItemContentData.aspx");
        }
    }
}