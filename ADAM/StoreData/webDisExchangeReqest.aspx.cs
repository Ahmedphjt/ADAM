using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webDisExchangeReqest : System.Web.UI.Page
    {
        public int pageid = 74;

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
            Response.Redirect("~/StoreData/webDisExchangeReqest.aspx");
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 5;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");
        }

        protected void btnShowData_Click(object sender, ImageClickEventArgs e)
        {

            if (string.IsNullOrEmpty(txtExchangeRequestNo.Text))
            {
                Response.Write("<script>alert('من فضلك ادخل رقم طلب الصرف')</script>");
                return;
            }

            txtExchangeRequestNo.Enabled = false;

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var HRows = from a in mdb.ExchangeRequestHeaderDatas
                        where  a.OrderType == int.Parse(ddlExchangeRequestType.SelectedValue) && a.ExchangeRequestNo == int.Parse(txtExchangeRequestNo.Text)
                        select a;
            if (HRows.Count() > 0)
            {
                ADAM.DataBase.ExchangeRequestHeaderData Hdr = mdb.ExchangeRequestHeaderDatas.Single(a => a.ExchangeRequestNo == int.Parse(txtExchangeRequestNo.Text)
                    && a.OrderType == int.Parse(ddlExchangeRequestType.SelectedValue) );

                hfId.Value = Hdr.Id.ToString();
                ADAM.DataBase.ClientData client = mdb.ClientDatas.Single(a => a.Id == Hdr.ClientId);
                ADAM.DataBase.division Ddr = mdb.divisions.Single(a => a.Id == Hdr.DivisionId);
                ADAM.DataBase.Department Depdr = mdb.Departments.Single(a => a.Id == Ddr.DepartmentId);
                ddlDepartment.SelectedValue = Depdr.Id.ToString();
                ddlDivision.DataBind();
                ddlDivision.SelectedValue = Ddr.Id.ToString();
                ddlEmployee.DataBind();
                ddlEmployee.SelectedValue = Hdr.EmpId.ToString();
                txtDate.Text = Hdr.ExchangeRequestDate.ToString("yyyy-MM-dd");
                txtClientAddress.Text = client.Address;
                txtClientMob.Text = client.FirstMobile;
                txtClientName.Text = client.FirstName + " " + client.LastName;
                txtClientPhone.Text = client.FirstPhone;
                
                gvExchangeRequestData.DataBind();
            }
        }

        protected void gvExchangeRequestData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ExchangeRequestDetailsData Detailsdr = mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == long.Parse(gvExchangeRequestData.SelectedDataKey.Value.ToString()));
            hfDetailsId.Value = Detailsdr.Id.ToString();
            ADAM.DataBase.Item itmdr = mdb.Items.Single(a => a.Id == Detailsdr.ItemId);
            ItemId.Value = itmdr.Id.ToString();
            hfItemColorId.Value = Detailsdr.ItemColorId.ToString();            
            gvItemMovement.DataBind();
            gvExchangeRequestData.DataBind();
            gvExchangeRequestData.SelectedRow.BackColor = System.Drawing.Color.BurlyWood;
            gvExchangeRequestData.SelectedRow.Cells[8].BackColor = gvExchangeRequestData.SelectedRow.Cells[9].BackColor = System.Drawing.Color.PaleVioletRed;
        }

        protected void gvItemMovement_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ItemMovement movdr = Mdb.ItemMovements.Single(a => a.Id == long.Parse(gvItemMovement.SelectedDataKey.Value.ToString()));
            TextBox txtQtyOut = gvItemMovement.SelectedRow.FindControl("txtQtyOut") as TextBox;
            TextBox txtfreeQtyOut = gvItemMovement.SelectedRow.FindControl("txtfreeQtyOut") as TextBox;


            if ((movdr.MainQtyOut + decimal.Parse(txtQtyOut.Text) <= movdr.MainQty) && (movdr.AdditionalQtyOut + decimal.Parse(txtfreeQtyOut.Text) <= movdr.AdditionalQty))
            {
                movdr.MainQtyOut = movdr.MainQtyOut + decimal.Parse(txtQtyOut.Text);
                movdr.AdditionalQtyOut = movdr.AdditionalQtyOut + decimal.Parse(txtfreeQtyOut.Text);

                var ExchangeRequestOrderRows = from a in Mdb.ExchangeRequestDetailsDatas
                                               where a.ExchangeRequestHeaderData.OrderType == long.Parse(ddlExchangeRequestType.SelectedValue)
                                               && a.Status == 1
                                               orderby a.ExchangeRequestOrder descending
                                               select a;

                if (ExchangeRequestOrderRows.Count() <= 0)
                    hfExchangeRequestOrder.Value = "1";
                else
                {
                    if (hfExchangeRequestOrder.Value == "0")
                        hfExchangeRequestOrder.Value = (ExchangeRequestOrderRows.First().ExchangeRequestOrder + 1).ToString();
                }

                ADAM.DataBase.ExchangeRequestDetailsData Exdr = Mdb.ExchangeRequestDetailsDatas.Single(a => a.Id == long.Parse(hfDetailsId.Value));
                if ((decimal.Parse(txtQtyOut.Text)) > (Exdr.Qty + Exdr.Bounce))
                {
                    Response.Write("<script>alert('لا يمكن ان يكون الكمية المنصرفة اكبر من كمية طلب الصرف')</script>");
                    return;
                }
                if ((decimal.Parse(txtfreeQtyOut.Text)) > (Exdr.FreeQty))
                {
                    Response.Write("<script>alert('لا يمكن ان يكون الكمية Tester المنصرفة اكبر من كمية طلب الصرف')</script>");
                    return;
                }
                Exdr.ExchangeRequestOrder = long.Parse(hfExchangeRequestOrder.Value);
                Exdr.Status = 1;

                if (movdr.IncommingOrderNo != 0)
                {
                    ADAM.DataBase.IncommingOrderData incdr = Mdb.IncommingOrderDatas.Single(a => a.IncommingOrderNo == movdr.IncommingOrderNo && a.AuditDetailsId == movdr.AuditDetailsId);
                    Exdr.IncommingOrderId = incdr.Id;
                    Exdr.LocationId = movdr.LocatioId;
                }

                ADAM.DataBase.ItemMovement dr = new DataBase.ItemMovement();
                dr.AdditionalQty = decimal.Parse(txtfreeQtyOut.Text) * -1;
                dr.AdditionalQtyOut = 0;
                dr.AuditDetailsId = movdr.AuditDetailsId;
                dr.DocmentId = Exdr.Id;
                dr.IncommingOrderNo = movdr.IncommingOrderNo;
                dr.ItemColorId = movdr.ItemColorId;
                dr.ItemId = movdr.ItemId;
                dr.ItemUnitId = movdr.ItemUnitId;
                dr.LocatioId = movdr.LocatioId;
                dr.MainQty = decimal.Parse(txtQtyOut.Text) * -1;
                dr.MainQtyOut = 0;
                dr.MovementDate = DateTime.Now;
                dr.MovmentnameId = int.Parse(ddlExchangeRequestType.Text);
                dr.RecDate = DateTime.Now;
                dr.StoreId = movdr.StoreId;
                dr.SupplyOrderDetailsId = movdr.SupplyOrderDetailsId;
                dr.ParentItemMoveMentId = movdr.Id;
                Mdb.ItemMovements.Add(dr);

                Exdr.IncommingOrderNo = movdr.IncommingOrderNo;

                Mdb.SaveChanges();

                Exdr.MovementId = dr.Id;

                gvExchangeRequestData.DataBind();
            }
            else
            {
                Response.Write("<script>alert('لا يمكن ان يكون الكمية المنصرفة اكبر من كمية الرصيد الحالي')</script>");
                return;
            }
        }

        protected void btnGetExchangeNo_Click(object sender, EventArgs e)
        {
            InsertData.Visible = false;
            ExchangeRequestNo.Visible = true;
        }

        protected void gvExchangeRequestNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            InsertData.Visible = true;
            ExchangeRequestNo.Visible = false;
            txtExchangeRequestNo.Text = gvExchangeRequestNo.SelectedRow.Cells[0].Text;
        }
    }
}