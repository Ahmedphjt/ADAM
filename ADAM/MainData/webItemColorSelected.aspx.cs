using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainData
{
    public partial class webItemColorSelected : System.Web.UI.Page
    {
        public int pageid = 82;

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

        #region btnFunction
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/MainData/webItemColorSelected.aspx");
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

                if (ddlItemColor.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر لون الصنف')</script>");
                    return;
                }

                if (ddlProdctionLine.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك أختر خط الانتاج')</script>");
                    return;
                }

                if (ddlItems.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك أختر الصنف')</script>");
                    return;
                }

                if (ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك أختر المخزن')</script>");
                    return;
                }

                EditData();
            }
            catch { }

        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
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

                if (ddlItemColor.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك اختر لون الصنف')</script>");
                    return;
                }

                if (ddlProdctionLine.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك أختر خط الانتاج')</script>");
                    return;
                }

                if (ddlItems.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك أختر الصنف')</script>");
                    return;
                }

                if (ddlItemType.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك أختر المخزن')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                var RepCode = from a in Mdb.ItemColorSelecteds where a.ItemId == long.Parse(ddlItems.SelectedValue) && a.ItemColorId == long.Parse(ddlItemColor.SelectedValue) select a;

                if (RepCode.Count() > 0)
                {
                    Response.Write("<script>alert('لقد تم ادخال هذا اللون لهذا الصنف من قبل')</script>");
                    return;
                }

                SaveData();
            }
            catch { }
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

            DeleteData();
        }

        #endregion

        #region Function

        private void EditData()
        {
            try
            {
                if (ddlItemType.SelectedValue == "0" || ddlProdctionLine.SelectedValue == "0" || ddlItems.SelectedValue == "0" || ddlItemColor.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كل البيانات بشكل صحيح')</script>");
                    return;
                }

                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemColorSelected dr = Mdb.ItemColorSelecteds.Single(a => a.Id == long.Parse(hfItemColorSelectedId.Value));
                dr.ItemId = long.Parse(ddlItems.SelectedValue);
                dr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
                dr.Point = decimal.Parse(txtPoint.Text);
                dr.ItemsGroupId = long.Parse(ddlItemGroup.SelectedValue);
                Mdb.SaveChanges();
                Response.Write("<script>alert('تمت عملية التعديل بنجاح')</script>");
                gvItemColorSelected.DataBind();
            }
            catch { Response.Write("<script>alert('خطأ أثناء التعديل من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات ')</script>"); }
        }

        private void SaveData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemColorSelected dr = new DataBase.ItemColorSelected();
                dr.ItemColorId = int.Parse(ddlItemColor.SelectedValue);
                dr.ItemId = long.Parse(ddlItems.SelectedValue);
                dr.Point = decimal.Parse(txtPoint.Text);
                dr.ItemsGroupId = long.Parse(ddlItemGroup.SelectedValue);
                Mdb.ItemColorSelecteds.Add(dr);                
                Mdb.SaveChanges();
                Response.Write("<script>alert('تمت عملية الحفظ بنجاح')</script>");
                gvItemColorSelected.DataBind();
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحفظ من فضلك تأكد من ادخال البيانات بشكل صحيح او الاتصال بقاعدة البيانات')</script>"); }
        }

        private void DeleteData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
                ADAM.DataBase.ItemColorSelected dr = Mdb.ItemColorSelecteds.Single(a => a.Id == long.Parse(hfItemColorSelectedId.Value));
                Mdb.ItemColorSelecteds.Remove(dr);
                Mdb.SaveChanges();
                EmptyData();
                Response.Write("<script>alert('تمت عملية الحذف نجاح')</script>");
                gvItemColorSelected.DataBind();
            }
            catch { Response.Write("<script>alert('خطأ أثناء الحذف من فضلك تأكد من ادخال البيانات بشكل صحيح او من الاتصال بقاعدة البيانات')</script>"); }
        }

        private void EmptyData()
        {
            ddlItems.SelectedValue = ddlItemColor.SelectedValue = ddlProdctionLine.SelectedValue = ddlItemType.SelectedValue = "0";
        }
        #endregion

        protected void gvItemColorSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfItemColorSelectedId.Value = gvItemColorSelected.SelectedDataKey.Value.ToString();
            ADAM.DataBase.ADAMConnectionString Mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.ItemColorSelected dr = Mdb.ItemColorSelecteds.Single(a => a.Id == long.Parse(hfItemColorSelectedId.Value));
            ADAM.DataBase.Item itmdr = Mdb.Items.Single(a => a.Id == dr.ItemId);
            ADAM.DataBase.ProductionLine prdr = Mdb.ProductionLines.Single(a => a.Id == itmdr.ProductionLineId);
            ADAM.DataBase.ItemType itmtyprdr = Mdb.ItemTypes.Single(a => a.Id == itmdr.ItemTypeId);
            ddlItemType.SelectedValue = itmtyprdr.Id.ToString();
            ddlProdctionLine.SelectedValue = prdr.Id.ToString();
            ddlItemGroup.SelectedValue = dr.ItemsGroupId.ToString();
            ddlItems.DataBind();
            ddlItems.SelectedValue = itmdr.Id.ToString();
            ddlItemColor.SelectedValue = dr.ItemColorId.ToString();
            txtPoint.Text = dr.Point.ToString();
        }
    }
}