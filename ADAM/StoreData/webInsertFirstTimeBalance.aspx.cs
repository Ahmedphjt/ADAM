using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webInsertFirstTimeBalance : System.Web.UI.Page
    {
        public int pageid = 57;

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

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                TextBox txtMainQty = gvItems.SelectedRow.FindControl("txtMainQty") as TextBox;
                TextBox txtAdditionalQty = gvItems.SelectedRow.FindControl("txtAdditionalQty") as TextBox;
                int ItemColorId = int.Parse(gvItems.SelectedDataKey[1].ToString());
                DropDownList ddlLocation = gvItems.SelectedRow.FindControl("ddlLocation") as DropDownList;

                ADAM.DataBase.Item itmdr = mdb.Items.Single(a => a.Code == long.Parse(gvItems.SelectedRow.Cells[0].Text) && a.ItemTypeId == long.Parse(ddlItemType.SelectedValue));

                var CountRows = from a in mdb.ItemMovements
                                where a.StoreId == 2 && a.ItemColorId == ItemColorId && a.ItemId == itmdr.Id
                                    && a.MovmentnameId == long.Parse(ddlItemMovementName.SelectedValue)
                                select a;

                if (CountRows.Count() > 0 && ddlItemMovementName.SelectedValue == "6")
                {
                    Response.Write("<script>alert('لقد تم ادخل رصيد مدة لهذا الصنف مع هذا اللون')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtMainQty.Text) || string.IsNullOrEmpty(txtAdditionalQty.Text) || ddlLocation.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال الكميات بشكل صحيح وفي حالة عدم الرغبه في ادخال كمية ادخل صفر')</script>");
                    return;
                }

                ADAM.DataBase.ItemMovement dr = new DataBase.ItemMovement();
                dr.AdditionalQty = decimal.Parse(txtAdditionalQty.Text);
                dr.DocmentId = 0;
                dr.ItemColorId = ItemColorId;
                dr.ItemId = itmdr.Id;
                dr.ItemUnitId = itmdr.ItemunitId;
                dr.LocatioId = int.Parse(ddlLocation.SelectedValue);
                dr.MainQty = decimal.Parse(txtMainQty.Text);
                dr.MovementDate = DateTime.Now;
                dr.MovmentnameId = long.Parse(ddlItemMovementName.SelectedValue);
                dr.RecDate = DateTime.Now;
                dr.StoreId = 2;
                dr.SupplyOrderDetailsId = 0;
                dr.ParentItemMoveMentId = 0;

                mdb.ItemMovements.Add(dr);
                mdb.SaveChanges();
            }
            catch { }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/StoreData/webInsertFirstTimeBalance.aspx");
        }
    }
}