using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webConvertBetweenMainQtyAndTester : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex != -1)
                {
                    ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                    long ItemId = long.Parse(gvItems.DataKeys[e.Row.RowIndex][0].ToString());
                    long ItemColorId = long.Parse(gvItems.DataKeys[e.Row.RowIndex][1].ToString());

                    var Rows = from a in mdb.ItemMovements where a.StoreId == 2 && a.ItemId == ItemId && a.ItemColorId == ItemColorId select a;
                    if (Rows.Count() > 0)
                    {
                        decimal MainQty = 0;
                        decimal Tester = 0;

                        foreach (ADAM.DataBase.ItemMovement movdr in Rows)
                        {
                            MainQty += movdr.MainQty;
                            Tester += movdr.AdditionalQty;
                        }

                        e.Row.Cells[7].Text = MainQty.ToString();
                        e.Row.Cells[8].Text = Tester.ToString();
                    }
                    else
                    {
                        e.Row.Cells[7].Text = "0";
                        e.Row.Cells[8].Text = "0";
                    }
                }
            }
            catch
            {
                //Response.Write("<script>alert('من فضلك اختر المخزن')</script>");
                return;
            }
        }

        protected void gvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                hfItemId.Value = gvItems.SelectedDataKey[0].ToString();
                hfItemColorId.Value = gvItems.SelectedDataKey[1].ToString();
                gvBalance.DataBind();
            }
            catch {
                return;
            }
        }

        protected void gvBalance_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                long MovementId = long.Parse(gvBalance.SelectedDataKey.Value.ToString());
                TextBox txtQty = gvBalance.SelectedRow.FindControl("txtQty") as TextBox;
                TextBox txtTester = gvBalance.SelectedRow.FindControl("txtTester") as TextBox;
                if (string.IsNullOrEmpty(txtQty.Text) && string.IsNullOrEmpty(txtTester.Text))
                {
                    return;
                }

                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemMovement movdr = mdb.ItemMovements.Single(a => a.Id == MovementId);

                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    ADAM.DataBase.ItemMovement newmovdr = new DataBase.ItemMovement();
                    newmovdr.AdditionalQty = 0;
                    newmovdr.AdditionalQtyOut = 0;
                    newmovdr.AuditDetailsId = movdr.AuditDetailsId;
                    newmovdr.DocmentId = 0;
                    newmovdr.IncommingOrderNo = movdr.IncommingOrderNo;
                    newmovdr.ItemColorId = movdr.ItemColorId;
                    newmovdr.ItemId = movdr.ItemId;
                    newmovdr.ItemUnitId = movdr.ItemUnitId;
                    newmovdr.LocatioId = movdr.LocatioId;
                    newmovdr.MainQty = -1 * decimal.Parse(txtQty.Text);
                    newmovdr.MainQtyOut = 0;
                    newmovdr.MovementDate = DateTime.Now;
                    newmovdr.MovmentnameId = 18;
                    newmovdr.RecDate = DateTime.Now;
                    newmovdr.StoreId = movdr.StoreId;
                    newmovdr.SupplyOrderDetailsId = movdr.SupplyOrderDetailsId;
                    newmovdr.ParentItemMoveMentId = movdr.Id;
                    mdb.ItemMovements.Add(newmovdr);

                    movdr.MainQtyOut += decimal.Parse(txtQty.Text);
                    movdr.AdditionalQty += decimal.Parse(txtQty.Text);

                    ADAM.DataBase.ItemMovement newmovddr = new DataBase.ItemMovement();
                    newmovddr.AdditionalQty = decimal.Parse(txtQty.Text);
                    newmovddr.AdditionalQtyOut = 0;
                    newmovddr.AuditDetailsId = movdr.AuditDetailsId;
                    newmovddr.DocmentId = 0;
                    newmovddr.IncommingOrderNo = movdr.IncommingOrderNo;
                    newmovddr.ItemColorId = movdr.ItemColorId;
                    newmovddr.ItemId = movdr.ItemId;
                    newmovddr.ItemUnitId = movdr.ItemUnitId;
                    newmovddr.LocatioId = movdr.LocatioId;
                    newmovddr.MainQty = 0;
                    newmovddr.MainQtyOut = 0;
                    newmovddr.MovementDate = DateTime.Now;
                    newmovddr.MovmentnameId = 15;
                    newmovddr.RecDate = DateTime.Now;
                    newmovddr.StoreId = movdr.StoreId;
                    newmovddr.SupplyOrderDetailsId = movdr.SupplyOrderDetailsId;
                    newmovddr.ParentItemMoveMentId = movdr.Id;
                    mdb.ItemMovements.Add(newmovddr);
                }

                if (!string.IsNullOrEmpty(txtTester.Text))
                {
                    ADAM.DataBase.ItemMovement newmovdr = new DataBase.ItemMovement();
                    newmovdr.AdditionalQty = -1 * decimal.Parse(txtTester.Text);
                    newmovdr.AdditionalQtyOut = 0;
                    newmovdr.AuditDetailsId = movdr.AuditDetailsId;
                    newmovdr.DocmentId = 0;
                    newmovdr.IncommingOrderNo = movdr.IncommingOrderNo;
                    newmovdr.ItemColorId = movdr.ItemColorId;
                    newmovdr.ItemId = movdr.ItemId;
                    newmovdr.ItemUnitId = movdr.ItemUnitId;
                    newmovdr.LocatioId = movdr.LocatioId;
                    newmovdr.MainQty = 0;
                    newmovdr.MainQtyOut = 0;
                    newmovdr.MovementDate = DateTime.Now;
                    newmovdr.MovmentnameId = 19;
                    newmovdr.RecDate = DateTime.Now;
                    newmovdr.StoreId = movdr.StoreId;
                    newmovdr.SupplyOrderDetailsId = movdr.SupplyOrderDetailsId;
                    newmovdr.ParentItemMoveMentId = movdr.Id;
                    mdb.ItemMovements.Add(newmovdr);

                    movdr.AdditionalQtyOut += decimal.Parse(txtTester.Text);
                    movdr.MainQty += decimal.Parse(txtTester.Text);

                    ADAM.DataBase.ItemMovement newmovddr = new DataBase.ItemMovement();
                    newmovddr.AdditionalQty = 0;
                    newmovddr.AdditionalQtyOut = 0;
                    newmovddr.AuditDetailsId = movdr.AuditDetailsId;
                    newmovddr.DocmentId = 0;
                    newmovddr.IncommingOrderNo = movdr.IncommingOrderNo;
                    newmovddr.ItemColorId = movdr.ItemColorId;
                    newmovddr.ItemId = movdr.ItemId;
                    newmovddr.ItemUnitId = movdr.ItemUnitId;
                    newmovddr.LocatioId = movdr.LocatioId;
                    newmovddr.MainQty = decimal.Parse(txtTester.Text);
                    newmovddr.MainQtyOut = 0;
                    newmovddr.MovementDate = DateTime.Now;
                    newmovddr.MovmentnameId = 14;
                    newmovddr.RecDate = DateTime.Now;
                    newmovddr.StoreId = movdr.StoreId;
                    newmovddr.SupplyOrderDetailsId = movdr.SupplyOrderDetailsId;
                    newmovddr.ParentItemMoveMentId = movdr.Id;
                    mdb.ItemMovements.Add(newmovddr);
                }

                mdb.SaveChanges();
                gvBalance.DataBind();
                gvItems.DataBind();
            }
            catch { }
        }
    }
}