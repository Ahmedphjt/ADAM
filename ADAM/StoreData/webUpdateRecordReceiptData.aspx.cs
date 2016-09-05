using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.StoreData
{
    public partial class webUpdateRecordReceiptData : System.Web.UI.Page
    {
        public int pageid = 56;

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
            Response.Redirect("~/StoreData/webUpdateRecordReceiptData.aspx");
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

            if (string.IsNullOrEmpty(txtRecordReceiptNo.Text))
            {
                Response.Write("<script>alert('من فضلك تأكد من ادخال رقم محضر الاستلام')</script>");
                return;
            }

            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            var Rows = from a in mdb.RecordReceiptHeaders where a.RecordReceiptNo == long.Parse(txtRecordReceiptNo.Text) select a;
            if (Rows.Count() > 0)
            {
                gvRecordReceiptData.Visible = true;
                gvRecordReceiptData.DataBind();
                txtRecordReceiptNo.Enabled = false;
            }
            else
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم محضر الاستلام')</script>");
                return;
            }
        }

        protected void gvRecordReceiptData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex != -1)
                {
                    ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                    long RecordReceiptDetailsId = long.Parse(gvRecordReceiptData.DataKeys[e.Row.RowIndex].Value.ToString());
                    ADAM.DataBase.RecordReceiptDetail dr = mdb.RecordReceiptDetails.Single(a => a.Id == RecordReceiptDetailsId);
                    
                    TextBox txtQtyRec = e.Row.FindControl("txtQtyRec") as TextBox;
                    txtQtyRec.Text = dr.QtyReceived.ToString();

                    TextBox txtFreeQty = e.Row.FindControl("txtFreeQty") as TextBox;
                    txtFreeQty.Text = dr.FreeQty.ToString();
                }
            }
            catch { }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                for (int Row = 0; Row < gvRecordReceiptData.Rows.Count; Row++)
                {
                    CheckBox chkChoose = gvRecordReceiptData.Rows[Row].FindControl("chkChoose") as CheckBox;
                    if (chkChoose.Checked)
                    {
                        TextBox txtQtyRec = gvRecordReceiptData.Rows[Row].FindControl("txtQtyRec") as TextBox;
                        TextBox txtFreeQty = gvRecordReceiptData.Rows[Row].FindControl("txtFreeQty") as TextBox;

                        long RecordReceiptDetailsId = long.Parse(gvRecordReceiptData.DataKeys[Row].Value.ToString());
                        ADAM.DataBase.RecordReceiptDetail dr = mdb.RecordReceiptDetails.Single(a => a.Id == RecordReceiptDetailsId);
                        ADAM.DataBase.AuditDetail auddr = mdb.AuditDetails.Single(a => a.RecordReceiptDetailsId == dr.Id);

                        if (auddr.AcceptQty > 0 || auddr.RefusedQty > 0)
                        {
                            Response.Write("<script>alert('لا يمكن التعديل لانه قد تم الفحص')</script>");
                            gvRecordReceiptData.Rows[Row].BackColor = System.Drawing.Color.Red;
                            return;
                        }
                        
                        dr.QtyReceived = decimal.Parse(txtQtyRec.Text);
                        dr.FreeQty = decimal.Parse(txtFreeQty.Text);

                        ADAM.DataBase.ItemMovement movdr = mdb.ItemMovements.Single(a => a.DocmentId == dr.Id);
                        movdr.MainQty = dr.QtyReceived;
                        movdr.AdditionalQty = dr.FreeQty;

                        mdb.SaveChanges();
                    }
                }
            }
            catch { }
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

            Response.Redirect("~/StoreReport/webRecordReceiptReport.aspx");
        }
    }
}