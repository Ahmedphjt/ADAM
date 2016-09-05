using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Prodction
{
    public partial class webDisProductionOrder : System.Web.UI.Page
    {
        public int pageid = 95;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 4;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Prodction/webDisProductionOrder.aspx");
        }

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 3;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            if (string.IsNullOrEmpty(txtOrderNo.Text))
            {
                Response.Write("<script>alert('من فضلك تأكد من ادخال رقم الطلب')</script>");
                return;
            }

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ProductionHeaderOrder hdr = mdb.ProductionHeaderOrders.Single(a => a.ProductionNo == long.Parse(txtOrderNo.Text));
            txtDate.Text = hdr.ProductionDate.ToString("yyyy-MM-dd");

            gvProducionOrder.DataBind();
        }

        protected void gvPurchaseDetailsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hfProductionNo.Value = gvProducionOrder.SelectedDataKey.Value.ToString();
                gvItemContent.DataBind();
            }
            catch
            {
                return;
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                for (int Row = 0; Row < gvItemContent.Rows.Count; Row++)
                {
                    CheckBox chkChoose = gvItemContent.Rows[Row].FindControl("chkChoose") as CheckBox;
                    if (chkChoose.Checked)
                    {

                        decimal Qty = decimal.Parse(gvItemContent.Rows[Row].Cells[8].Text);
                        long ItemMoveId = long.Parse(gvItemContent.DataKeys[Row][0].ToString());
                        long ProductionDetailsOrderId = long.Parse(gvItemContent.DataKeys[Row][1].ToString());
                        hfProductionDetailsOrderId.Value = ProductionDetailsOrderId.ToString();

                        ADAM.DataBase.ItemMovement itmMovementdr = mdb.ItemMovements.Single(a => a.Id == ItemMoveId);
                        itmMovementdr.MainQtyOut = itmMovementdr.MainQtyOut + Qty;

                        var Rows = from a in mdb.ItemMovements
                                   where a.MovmentnameId == 21 && a.ItemColorId == itmMovementdr.ItemColorId
                                       && a.ItemId == itmMovementdr.ItemId && a.DocmentId == ProductionDetailsOrderId
                                   select a;

                        if (Rows.Count() > 0)
                            continue;

                        ADAM.DataBase.ItemMovement Nmovedr = new DataBase.ItemMovement();
                        Nmovedr.AdditionalQty = 0;
                        Nmovedr.AdditionalQtyOut = 0;
                        Nmovedr.AuditDetailsId = 0;
                        Nmovedr.DocmentId = ProductionDetailsOrderId;
                        Nmovedr.IncommingOrderNo = itmMovementdr.IncommingOrderNo;
                        Nmovedr.ItemColorId = itmMovementdr.ItemColorId;
                        Nmovedr.ItemId = itmMovementdr.ItemId;
                        Nmovedr.ItemUnitId = itmMovementdr.ItemUnitId;
                        Nmovedr.LocatioId = itmMovementdr.LocatioId;
                        Nmovedr.MainQty = Qty * -1;
                        Nmovedr.MainQtyOut = 0;
                        Nmovedr.MovementDate = DateTime.Now;
                        Nmovedr.MovmentnameId = 21;
                        Nmovedr.RecDate = DateTime.Now;
                        Nmovedr.StoreId = itmMovementdr.StoreId;
                        Nmovedr.SupplyOrderDetailsId = 0;
                        Nmovedr.ParentItemMoveMentId = itmMovementdr.Id;

                        mdb.ItemMovements.Add(Nmovedr);
                        mdb.SaveChanges();
                    }
                }
                ADAM.DataBase.ProductionDetailsOrder Proddr = mdb.ProductionDetailsOrders.Single(a => a.Id == long.Parse(hfProductionDetailsOrderId.Value));
                Proddr.Status = 1;
                mdb.SaveChanges();
            }
            catch { }
        }

        protected void gvItemContent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}