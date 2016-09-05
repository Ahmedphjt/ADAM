using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.PurchaseData
{
    public partial class webConformPurchaseOrder : System.Web.UI.Page
    {
        public int pageid = 36;

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
            Response.Redirect("~/PurchaseData/webConformPurchaseOrder.aspx");
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

            txtPurchaseOrderNo.Enabled = false;
            ShowData();
        }

        protected void btnConform_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 7;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");

            ConformPurchaseOrder();
        }

        private void ConformPurchaseOrder()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                for (int Row = 0; Row < gvPurchaseData.Rows.Count; Row++)
                {
                    TextBox txtConformQty = gvPurchaseData.Rows[Row].FindControl("txtConformQty") as TextBox;
                    DropDownList ddlConformtype = gvPurchaseData.Rows[Row].FindControl("ddlConformtype") as DropDownList;

                    if (ddlConformtype.SelectedValue == "4")
                    {
                        if (string.IsNullOrEmpty(txtConformQty.Text) || decimal.Parse(txtConformQty.Text) != 0)
                        {
                            Response.Write("<script>alert('يجب ان تكون كمية الاعتماد تساوي صفر في حالة الرفض')</script>");
                            return;
                        }
                    }

                    if (!string.IsNullOrEmpty("txtConformQty") && txtConformQty.Text != "" && ddlConformtype.SelectedValue != "0")
                    {
                        decimal Qty = decimal.Parse(gvPurchaseData.Rows[Row].Cells[7].Text);
                        long Id = long.Parse(gvPurchaseData.DataKeys[Row].Value.ToString());
                        ADAM.DataBase.PurchaseOredrDetail dr = mdb.PurchaseOredrDetails.Single(a => a.Id == Id);
                        if (ddlConformtype.SelectedValue == "2")
                        {
                            if (decimal.Parse(txtConformQty.Text) != Qty)
                            {
                                Response.Write("<script>alert('يجب ان تكون كمية الاعتماد مساوية لكمية طلب الشراء في حالة الاعتماد الكلي')</script>");
                                return;
                            }
                        }

                        if (ddlConformtype.SelectedValue == "3")
                        {
                            if (decimal.Parse(txtConformQty.Text) >= Qty)
                            {
                                Response.Write("<script>alert('يجب ان تكون كمية الاعتماد اقل من كمية طلب الشراء في حالة الاعتماد الجزئي')</script>");
                                return;
                            }
                        }

                        dr.Status = int.Parse(ddlConformtype.SelectedValue);
                        dr.ConformQty = decimal.Parse(txtConformQty.Text);
                    }
                }
                mdb.SaveChanges();
                Response.Write("<script>alert('تمت عملية الاعتماد بنجاح')</script>");
            }
            catch { Response.Write("<script>alert('خطأ أثناء عملية الاعتماد')</script>"); }
        }

        private void ShowData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in Mdb.PurchaseOrderHeaders where a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text) select a;

                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.PurchaseOrderHeader dr = Mdb.PurchaseOrderHeaders.Single(a => a.PurchaseOrderNo == long.Parse(txtPurchaseOrderNo.Text));
                    var Drows = from a in Mdb.PurchaseOredrDetails where a.Status == 1 && a.PurchaseOredeHeaderId == dr.Id select a;
                    if (Drows.Count() > 0)
                    {
                        txtPurchaseOrderNo.Text = dr.PurchaseOrderNo.ToString();
                        txtDate.Text = dr.PurchaseDate.ToString("yyyy-MM-dd");
                        ddlDepartment.SelectedValue = dr.DepartmentId.ToString();
                        dbDivision.DataBind();
                        ddlDivision.DataBind();
                        ddlDivision.SelectedValue = dr.DivisionId.ToString();
                        ddlEmployee.SelectedValue = dr.EmployeeId.ToString();
                        ddlSupplierName.SelectedValue = dr.SupplierId.ToString();
                        ddlItemType.SelectedValue = dr.ItemTypeId.ToString();
                        txtNote.Text = dr.Note;
                        gvPurchaseData.DataBind();
                    }
                    else
                    {
                        txtPurchaseOrderNo.Text = "";
                        Response.Write("<script>alert('لقد تم اعتماد كافه اصناف هذا الطلب')</script>");
                    }
                }
                else { Response.Write("<script>alert('من فضلك تأكد من رقم طلب الشراء')</script>"); }
            }
            catch { Response.Write("<script>alert('خطأ أثناء عرض البيانات من فضلك تأكد من الكود او من الاتصال بقاعدة البيانات')</script>"); }
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

            Response.Redirect("~/PurchaseReport/webConformPurchaseOrderReport.aspx");
        }

        protected void ShowgvPurchaseNo_Click(object sender, EventArgs e)
        {
            Data.Visible = false;
            PurchaseNumber.Visible = true;
        }

        protected void gvPurchaseNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Visible = true; ;
            PurchaseNumber.Visible = false;
            txtPurchaseOrderNo.Text = gvPurchaseNo.SelectedRow.Cells[0].Text;
        }
    }
}