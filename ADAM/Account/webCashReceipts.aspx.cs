using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Account
{
    public partial class webCashReceipts : System.Web.UI.Page
    {
        public int pageid = 122;

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

                GetNum();
            }
        }

        private void GetNum()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in mdb.DocumentHeaders where a.DocType == 1 orderby a.Id descending select a;
                if (Rows.Count() == 0)
                    txtDocNo.Text = "1";
                else
                {
                    ADAM.DataBase.DocumentHeader dr = Rows.First();
                    txtDocNo.Text = (dr.DocNo + 1).ToString();
                }
            }
            catch { }
        }

        protected void txtHeaderAccountNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                var Rows = from a in db.Accounts where a.AccountCode == long.Parse(txtHeaderAccountNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.Account accdr = db.Accounts.Single(a => a.AccountCode == long.Parse(txtHeaderAccountNo.Text));
                    ddlHeaderAccountName.SelectedValue = accdr.Id.ToString();
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                    return;
                }
            }
            catch { }
        }

        protected void ddlHeaderAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Account accdr = db.Accounts.Single(a => a.Id == long.Parse(ddlHeaderAccountName.SelectedValue));
                txtHeaderAccountNo.Text = accdr.AccountCode.ToString();
            }
            catch { }
        }

        protected void txtDetailsAccountNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                var Rows = from a in db.Accounts where a.AccountCode == long.Parse(txtDetailsAccountNo.Text) select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.Account accdr = db.Accounts.Single(a => a.AccountCode == long.Parse(txtDetailsAccountNo.Text));
                    ddlDetailsAccountName.SelectedValue = accdr.Id.ToString();
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من رقم الحساب')</script>");
                    return;
                }
            }
            catch { }
        }

        protected void ddlDetailsAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Account accdr = db.Accounts.Single(a => a.Id == long.Parse(ddlDetailsAccountName.SelectedValue));
                txtDetailsAccountNo.Text = accdr.AccountCode.ToString();
            }
            catch { }
        }

        protected void btnGetAllAccountforHeader_Click(object sender, EventArgs e)
        {
            GetAccountName("ddlHeaderAccountName");
        }

        private void GetAccountName(string ControlName)
        {
            divCostCenter.Visible = divData.Visible = false;
            divAccount.Visible = true;
            hfControlName.Value = ControlName;
        }

        protected void btnGetAllAccountForDetails_Click(object sender, EventArgs e)
        {
            GetAccountName("ddlDetailsAccountName");
        }

        protected void gvAcccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
            ADAM.DataBase.Account dr = db.Accounts.Single(a => a.Id == long.Parse(gvAcccount.SelectedDataKey.Value.ToString()));

            if (hfControlName.Value == "ddlHeaderAccountName")
            {
                ddlHeaderAccountName.SelectedValue = dr.Id.ToString();
                txtHeaderAccountNo.Text = dr.AccountCode.ToString();
            }

            if (hfControlName.Value == "ddlDetailsAccountName")
            {
                ddlDetailsAccountName.SelectedValue = dr.Id.ToString();
                txtDetailsAccountNo.Text = dr.AccountCode.ToString();
            }

            divAccount.Visible = divCostCenter.Visible = false;
            divData.Visible = true;
        }

        protected void gvCostCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hfControlName.Value == "ddlHeaderCostCenter")
                ddlHeaderCostCenter.SelectedValue = gvCostCenter.SelectedDataKey.Value.ToString();

            if (hfControlName.Value == "ddlDetailsCostCenter")
                ddlDetailsCostCenter.SelectedValue = gvCostCenter.SelectedDataKey.Value.ToString();

            divAccount.Visible = divCostCenter.Visible = false;
            divData.Visible = true;
        }

        protected void btnGetHeaderCostCenter_Click(object sender, EventArgs e)
        {
            GetCostCenter("ddlHeaderCostCenter");
        }

        private void GetCostCenter(string ControlName)
        {
            divAccount.Visible = divData.Visible = false;
            divCostCenter.Visible = true;
            hfControlName.Value = ControlName;
        }

        protected void btnGetDetailsCostCenter_Click(object sender, EventArgs e)
        {
            GetCostCenter("ddlDetailsCostCenter");
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Account/webCashReceipts.aspx");
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GetNum();

                #region Validation

                if (string.IsNullOrEmpty(txtMasterAccountQty.Text) || txtMasterAccountQty.Text == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال المبالغ بشكل صحيح')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtDetailsAccountQty.Text) || txtDetailsAccountQty.Text == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال المبالغ بشكل صحيح')</script>");
                    return;
                }

                if (ddlHeaderAccountName.SelectedValue == "0" || ddlDetailsAccountName.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من اختيار الحساب بشكل صحيح')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtDocNo.Text) || string.IsNullOrEmpty(txtDocDate.Text))
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال كافة البيانات بشكل صحيح')</script>");
                    return;
                }

                if (ddlDetailsCostCenter.SelectedValue == "0" || ddlHeaderCostCenter.SelectedValue == "0")
                {
                    Response.Write("<script>alert('من فضلك تأكد من ادخال مراكز التكلفة بشكل صحيح')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(txtDetailsNote.Text))
                    txtDetailsNote.Text = "-";
                if (string.IsNullOrEmpty(txtHeaderNote.Text))
                    txtHeaderNote.Text = "-";
                #endregion

                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                csJournal Journal = new csJournal();
                if (hfDocHeaderId.Value == "0")
                {
                    ADAM.DataBase.DocumentHeader Docdr = new DataBase.DocumentHeader();
                    Docdr.AccountId = long.Parse(ddlHeaderAccountName.SelectedValue);
                    Docdr.CostCenterId = long.Parse(ddlHeaderCostCenter.SelectedValue);
                    Docdr.DocDate = DateTime.Parse(txtDocDate.Text);
                    Docdr.DocNo = long.Parse(txtDocNo.Text);
                    Docdr.DocPaymentType = 1; // نقدا
                    Docdr.DocType = 2; // نوع المستند قبض
                    Docdr.MasterAccountQty = decimal.Parse(txtMasterAccountQty.Text);
                    Docdr.Notes = txtHeaderNote.Text;
                    db.DocumentHeaders.Add(Docdr);
                    db.SaveChanges();
                    hfDocHeaderId.Value = Docdr.Id.ToString();

                    hfJournalId.Value = (Journal.InsertIntoJournalHeader(0, Docdr.DocDate, 7, "سند قبض", 0 , Docdr.Id)).ToString();
                    Journal.InsertIntoJournalDetails(Docdr.AccountId, Docdr.CostCenterId, decimal.Parse(txtMasterAccountQty.Text),0, long.Parse(hfJournalId.Value), (" من حـ / :" + db.Accounts.Single(a => a.Id == long.Parse(ddlHeaderAccountName.SelectedValue)).AccountName));
                }

                ADAM.DataBase.DocumentDetail docddr = new DataBase.DocumentDetail();
                docddr.AccountId = long.Parse(ddlDetailsAccountName.SelectedValue);
                docddr.CostCenterId = long.Parse(ddlDetailsCostCenter.SelectedValue);
                docddr.DetailsAccountQty = decimal.Parse(txtDetailsAccountQty.Text);
                docddr.DocHeaderId = long.Parse(hfDocHeaderId.Value);
                docddr.Notes = txtDetailsNote.Text;
                db.DocumentDetails.Add(docddr);
                db.SaveChanges();

                Journal.InsertIntoJournalDetails(docddr.AccountId, docddr.CostCenterId,0 , docddr.DetailsAccountQty, long.Parse(hfJournalId.Value), ("الي حـ / :" + db.Accounts.Single(a => a.Id == long.Parse(ddlDetailsAccountName.SelectedValue)).AccountName));

                gvDocDetails.DataBind();
            }
            catch
            {

            }
        }
    }
}