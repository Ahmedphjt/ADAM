using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Prodction
{
    public partial class webUpdateItemContent : System.Web.UI.Page
    {
        public int pageid = 91;
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

        protected void btnGetItem_Click(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in mdb.Items where a.Code == long.Parse(txtItemCode.Text) && a.ItemTypeId == long.Parse(ddlItemColor.SelectedValue)
                           && a.ProductionLineId == long.Parse(ddlProductionLine.SelectedValue) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.Item itm = Rows.Single(a => a.Code == long.Parse(txtItemCode.Text));
                    ddlItemName.SelectedValue = itm.Id.ToString();
                    GetItemData(itm.Id);
                }
            }
            catch {
                Response.Write("<script>alert('من فضلك تأكد من الكود')</script>");
                return; }
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Item itm = mdb.Items.Single(a => a.Id == long.Parse(ddlItemName.SelectedValue));
                txtItemCode.Text = itm.Code.ToString();
                GetItemData(itm.Id);
            }
            catch { }
        }

        private void GetItemData(long ItemId)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.Item itm = mdb.Items.Single(a => a.Id == ItemId);
            ddlItemName.SelectedValue = itm.Id.ToString();
            txtItemCode.Text = itm.Code.ToString();
            ADAM.DataBase.ItemUnit unit = mdb.ItemUnits.Single(a => a.Id == itm.ItemunitId);
            lblItemUnit.Text = unit.Name;
            ADAM.DataBase.SexData sex = mdb.SexDatas.Single(a => a.Id == itm.Sex);
            lblSex.Text = sex.Sex;
            ADAM.DataBase.ItemStatus itmstatus = mdb.ItemStatus.Single(a => a.Id == itm.ItemStatus);
            lblItemstatus.Text = itmstatus.Status;
        }

        protected void gvItemContentData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ItemContentDetail details = mdb.ItemContentDetails.Single(a => a.Id == long.Parse(gvItemContentData.SelectedDataKey.Value.ToString()));
            ddlItemType.SelectedValue = details.ItemTypeId.ToString();
            ddlProductionLine.SelectedValue = details.ProductionLineId.ToString();
            ddlItemName.DataBind();
            ddlItemName.SelectedValue = details.ItemId.ToString();
            GetItemData(details.ItemId);
            ddlItemColor.DataBind();
            ddlItemColor.SelectedValue = details.ItemColorId.ToString();
            txtQty.Text = details.Qty.ToString();
            hfItemContentDetailsId.Value = details.Id.ToString();
        }

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlProductionItemType.SelectedValue == "0" || ddlProductProductionLine.SelectedValue == "0"
                || ddlProductionItem.SelectedValue == "0" || ddlProductionItemcolor.SelectedValue == "0")
            {
                Response.Write("<script>alert('من فضلك تأكد من أدخال بيانات المستحضر بالكامل')</script>");
                return;
            }
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in mdb.ItemContentHeaders
                       where a.ItemType == long.Parse(ddlProductionItemType.SelectedValue) ||
                           a.ProductionLineId == long.Parse(ddlProductProductionLine.SelectedValue) ||
                           a.ProductItemColor == long.Parse(ddlProductionItemcolor.SelectedValue) || a.ProductItemId == long.Parse(ddlProductionItem.SelectedValue)
                       select a;
            if (Rows.Count() > 0)
            {
                ADAM.DataBase.ItemContentHeader header = Rows.Single(a => a.ProductItemId == long.Parse(ddlProductionItem.SelectedValue));
                hfItemContentHeaderId.Value = header.Id.ToString();
            }
            else
            {
                Response.Write("<script>alert('لا يوجد اي تركيبات لهذا المستحضر')</script>");
                return;
            }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Prodction/webUpdateItemContent.aspx");
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnSaveOrderItem_Click(object sender, ImageClickEventArgs e)
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
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemContentDetail details = new DataBase.ItemContentDetail();

                var Rows = from a in mdb.ItemContentDetails where a.ItemId == long.Parse(ddlItemName.SelectedValue) &&
                               a.ItemColorId == long.Parse(ddlItemColor.SelectedValue) select a;
                if (Rows.Count() > 0)
                {
                    Response.Write("<script>alert('لا يمكن ادخال نفس الصنف واللون اكثر من مرة في التركيبة الواحدة')</script>");
                    return;
                }


                details.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
                details.ItemContentHeaderId = long.Parse(hfItemContentHeaderId.Value);
                details.ItemId = long.Parse(ddlItemName.SelectedValue);
                details.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                details.ProductionLineId = long.Parse(ddlProductionLine.SelectedValue);
                details.Qty = decimal.Parse(txtQty.Text);

                mdb.ItemContentDetails.Add(details);
                mdb.SaveChanges();
                gvItemContentData.DataBind();
            }
            catch
            {

            }
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 2;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemContentDetail details = mdb.ItemContentDetails.Single(a => a.Id == long.Parse(hfItemContentDetailsId.Value));

                details.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
                details.ItemContentHeaderId = long.Parse(hfItemContentHeaderId.Value);
                details.ItemId = long.Parse(ddlItemName.SelectedValue);
                details.ItemTypeId = long.Parse(ddlItemType.SelectedValue);
                details.ProductionLineId = long.Parse(ddlProductionLine.SelectedValue);
                details.Qty = decimal.Parse(txtQty.Text);

                mdb.SaveChanges();
                gvItemContentData.DataBind();
            }
            catch
            {

            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 6;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemContentDetail details = mdb.ItemContentDetails.Single(a => a.Id == long.Parse(hfItemContentDetailsId.Value));
                mdb.ItemContentDetails.Remove(details);
                mdb.SaveChanges();
                var Rows = from a in mdb.ItemContentDetails where a.ItemContentHeaderId == long.Parse(hfItemContentHeaderId.Value) select a;
                if (Rows.Count() <= 0)
                {
                    ADAM.DataBase.ItemContentHeader header = mdb.ItemContentHeaders.Single(a => a.Id == long.Parse(hfItemContentHeaderId.Value));
                    mdb.ItemContentHeaders.Remove(header);
                    mdb.SaveChanges();
                    btnNew_Click(sender, e);
                }
                gvItemContentData.DataBind();
            }
            catch
            {
                Response.Write("<script>alert('لا يمكن الحذف لأرتباطها بطلبات صرف')</script>");
                return;
            }
        }
    }
}