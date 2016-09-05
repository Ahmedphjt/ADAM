using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Account
{
    public partial class webAccountData : System.Web.UI.Page
    {
        public int pageid = 116;

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

                DrawTree();
            }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Account/webAccountData.aspx");
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

            EditData();
        }

        private void EditData()
        {
            try
            {
                if (hfID.Value != "0")
                {
                    ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                    ADAM.DataBase.Account dr = db.Accounts.Single(a => a.Id == long.Parse(hfID.Value));
                    if (dr.ParentId == 0)
                    {
                        Response.Write("<script>alert('لا يمكن تعديل هذا الحساب')</script>");
                        return;
                    }
                   
                    dr.AccountName = txtAccountName.Text;
                    dr.AccountType = int.Parse(ddlAccountType.SelectedValue);
                    dr.AccountCurrency = long.Parse(ddlCurrency.SelectedValue);
                    dr.AccountCode = long.Parse(txtAccountCode.Text);

                    db.SaveChanges();
                    gvDataBind();
                }
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

            SaveData();
        }

        private void SaveData()
        {
            try
            {
                if (hfID.Value == "0")
                {
                    ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                    ADAM.DataBase.Account olddr = db.Accounts.Single(a => a.Id == long.Parse(hfParentId.Value));

                    ADAM.DataBase.Account dr = new DataBase.Account();

                    dr.AccountCode = long.Parse(txtAccountCode.Text);
                    dr.AccountName = txtAccountName.Text;
                    dr.AccountType = int.Parse(ddlAccountType.SelectedValue);
                    dr.AccountLevel = olddr.AccountLevel + 1;
                    dr.ParentId = olddr.Id;
                    dr.AccountCurrency = long.Parse(ddlCurrency.SelectedValue);
                    dr.MasterCode = olddr.MasterCode;

                    dr.Mezania3momia = 0;
                    dr.Reb7and5sara = 0;
                    dr.H7sabElMotagra = 0;

                    db.Accounts.Add(dr);
                    db.SaveChanges();
                    gvDataBind();
                }
            }
            catch { }
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {

        }
                
        private void gvDataBind()
        {
            DrawTree();           

            txtAccountCode.Text = txtAccountName.Text = "";
            ddlAccountType.SelectedValue = "-1";
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

            ShowData();
        }

        private void ShowData()
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
                ADAM.DataBase.Account dr = db.Accounts.Single(a => a.Id == long.Parse(hfParentId.Value));
                txtAccountCode.Text = dr.AccountCode.ToString();
                txtAccountName.Text = dr.AccountName;
                ddlAccountType.SelectedValue = dr.AccountType.ToString();
                ddlCurrency.SelectedValue = dr.AccountCurrency.ToString();
                hfID.Value = dr.Id.ToString();
            }
            catch { }
        }

        private void DrawTree()
        {
            tvAccount.Nodes.Clear();
            ADAM.DataBase.ADAMConnectionString Db = new DataBase.ADAMConnectionString();
            var Rows = from a in Db.Accounts where a.ParentId == 0 select a;
            long SerialNo = 0;
            if (Rows.Count() > 0)
            {
                int Node = 0;
                foreach (ADAM.DataBase.Account dr in Rows)
                {   
                    SerialNo += 1;
                    TreeNode AccName = new TreeNode();
                    AccName.Value = dr.Id.ToString();
                    AccName.Text = dr.AccountCode.ToString() + " "+ dr.AccountName;
                    tvAccount.Nodes.Add(AccName);
                    dr.SerialNo = SerialNo;

                    #region Add First Child
                    var SRows = from a in Db.Accounts where a.ParentId == dr.Id select a;
                    if (SRows.Count() > 0)
                    {
                        int SNote = 0;

                        foreach (ADAM.DataBase.Account Sdr in SRows)
                        {
                            SerialNo += 1;
                            TreeNode SAccName = new TreeNode();
                            SAccName.Value = Sdr.Id.ToString();
                            SAccName.Text = Sdr.AccountCode.ToString() + " " + Sdr.AccountName;
                            tvAccount.Nodes[Node].ChildNodes.Add(SAccName);
                            Sdr.SerialNo = SerialNo;

                            #region Add Sec Child
                            var TRows = from a in Db.Accounts where a.ParentId == Sdr.Id select a;
                            if (TRows.Count() > 0)
                            {
                                int FoNote = 0;
                                foreach (ADAM.DataBase.Account Tdr in TRows)
                                {
                                    SerialNo += 1;
                                    TreeNode TAccName = new TreeNode();
                                    TAccName.Value = Tdr.Id.ToString();
                                    TAccName.Text = Tdr.AccountCode + " " + Tdr.AccountName;
                                    tvAccount.Nodes[Node].ChildNodes[SNote].ChildNodes.Add(TAccName);
                                    Tdr.SerialNo = SerialNo;

                                    #region Add Th Child
                                    var FoRows = from a in Db.Accounts where a.ParentId == Tdr.Id select a;
                                    if (FoRows.Count() > 0)
                                    {
                                        int FiNode = 0;
                                        foreach (ADAM.DataBase.Account Fodr in FoRows)
                                        {
                                            SerialNo += 1;
                                            TreeNode FoAccName = new TreeNode();
                                            FoAccName.Value = Fodr.Id.ToString();
                                            FoAccName.Text = Fodr.AccountCode + " " + Fodr.AccountName;
                                            tvAccount.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes.Add(FoAccName);
                                            Fodr.SerialNo = SerialNo;

                                            #region Add For Child
                                            var FiRows = from a in Db.Accounts where a.ParentId == Fodr.Id select a;
                                            if (FiRows.Count() > 0)
                                            {
                                                int SiNode = 0;
                                                foreach (ADAM.DataBase.Account Fidr in FiRows)
                                                {
                                                    SerialNo += 1;
                                                    TreeNode FiAccName = new TreeNode();
                                                    FiAccName.Value = Fidr.Id.ToString();
                                                    FiAccName.Text = Fidr.AccountCode + " " + Fidr.AccountName;
                                                    tvAccount.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes.Add(FiAccName);
                                                    Fidr.SerialNo = SerialNo;

                                                    #region Add For Child
                                                    var SiRows = from a in Db.Accounts where a.ParentId == Fidr.Id select a;
                                                    if (SiRows.Count() > 0)
                                                    {
                                                        int SeNode = 0;
                                                        foreach (ADAM.DataBase.Account Sidr in SiRows)
                                                        {
                                                            SerialNo += 1;
                                                            TreeNode SiAccName = new TreeNode();
                                                            SiAccName.Value = Sidr.Id.ToString();
                                                            SiAccName.Text = Sidr.AccountCode + " " + Sidr.AccountName;
                                                            tvAccount.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes.Add(SiAccName);
                                                            Sidr.SerialNo = SerialNo;

                                                            #region Add Sev Child
                                                            var SeRows = from a in Db.Accounts where a.ParentId == Sidr.Id select a;
                                                            if (SeRows.Count() > 0)
                                                            {
                                                                int EiNode = 0;
                                                                foreach (ADAM.DataBase.Account Sedr in SeRows)
                                                                {
                                                                    SerialNo += 1;
                                                                    TreeNode SeAccName = new TreeNode();
                                                                    SeAccName.Value = Sedr.Id.ToString();
                                                                    SeAccName.Text = Sedr.AccountCode+" " +Sedr.AccountName;
                                                                    tvAccount.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes[SeNode].ChildNodes.Add(SeAccName);
                                                                    Sedr.SerialNo = SerialNo;

                                                                    #region Add Ei Child
                                                                    var EiRows = from a in Db.Accounts where a.ParentId == Sedr.Id select a;
                                                                    if (EiRows.Count() > 0)
                                                                    {
                                                                        int NiNode = 0;
                                                                        foreach (ADAM.DataBase.Account Eidr in EiRows)
                                                                        {
                                                                            SerialNo += 1;
                                                                            TreeNode EiAccName = new TreeNode();
                                                                            EiAccName.Value = Eidr.Id.ToString();
                                                                            EiAccName.Text = Eidr.AccountCode+" " + Eidr.AccountName;
                                                                            tvAccount.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes[SeNode].ChildNodes[EiNode].ChildNodes.Add(EiAccName);
                                                                            Eidr.SerialNo = SerialNo;

                                                                            #region Add Ni Child
                                                                            var NiRows = from a in Db.Accounts where a.ParentId == Eidr.Id select a;
                                                                            if (NiRows.Count() > 0)
                                                                            {
                                                                                SerialNo += 1;
                                                                                int TENode = 0;
                                                                                foreach (ADAM.DataBase.Account Nidr in NiRows)
                                                                                {
                                                                                    TreeNode NiAccName = new TreeNode();
                                                                                    NiAccName.Value = Nidr.Id.ToString();
                                                                                    NiAccName.Text = Nidr.AccountCode + " " + Nidr.AccountName;
                                                                                    tvAccount.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes[SeNode].ChildNodes[EiNode].ChildNodes[NiNode].ChildNodes.Add(NiAccName);
                                                                                    Nidr.SerialNo = SerialNo;

                                                                                    #region Add Te Child
                                                                                    var TERows = from a in Db.Accounts where a.ParentId == Nidr.Id select a;
                                                                                    if (TERows.Count() > 0)
                                                                                    {
                                                                                        foreach (ADAM.DataBase.Account TEdr in TERows)
                                                                                        {
                                                                                            SerialNo += 1;
                                                                                            TreeNode TEAccName = new TreeNode();
                                                                                            TEAccName.Value = TEdr.Id.ToString();
                                                                                            TEAccName.Text = TEdr.AccountCode + " " + TEdr.AccountName;
                                                                                            tvAccount.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes[SeNode].ChildNodes[EiNode].ChildNodes[NiNode].ChildNodes[TENode].ChildNodes.Add(TEAccName);
                                                                                            TEdr.SerialNo = SerialNo;
                                                                                        }
                                                                                    }
                                                                                    #endregion
                                                                                    TENode += 1;
                                                                                }
                                                                            }
                                                                            #endregion
                                                                            NiNode += 1;
                                                                        }
                                                                    }
                                                                    #endregion
                                                                    EiNode += 1;
                                                                }
                                                            }
                                                            #endregion
                                                            SeNode += 1;
                                                        }
                                                    }
                                                    #endregion
                                                    SiNode += 1;
                                                }
                                            }
                                            #endregion
                                            FiNode += 1;
                                        }
                                    }
                                    #endregion
                                    FoNote += 1;
                                }
                            }
                            #endregion
                            SNote += 1;
                        }
                    }
                    #endregion
                    Node += 1;
                }
            }
            Db.SaveChanges();
        }

        protected void tvAccount_SelectedNodeChanged(object sender, EventArgs e)
        {
            ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
            ADAM.DataBase.Account dr = mdb.Accounts.Single(a => a.Id == long.Parse(tvAccount.SelectedValue));
            lblAccountName.Text = dr.AccountName;
            lblAccountCode.Text = dr.AccountCode.ToString();
            hfParentId.Value = dr.Id.ToString();
        }
    }
}