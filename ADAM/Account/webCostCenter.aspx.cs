using ADAM.BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.Account
{
    public partial class webCostCenter : System.Web.UI.Page
    {
        public int pageid = 117;

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
            Response.Redirect("~/Account/webCostCenter.aspx");
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
                    ADAM.DataBase.CostCenter dr = db.CostCenters.Single(a => a.Id == long.Parse(hfID.Value));
                    if (dr.ParentId == 0)
                    {
                        Response.Write("<script>alert('لا يمكن تعديل هذا المركز')</script>");
                        return;
                    }
                    dr.CostCenterCode = long.Parse(txtCostCenterCode.Text);
                    dr.CostCenterName = txtCostCenterName.Text;
                    dr.CostCenterType = int.Parse(ddlCostCenterType.SelectedValue);

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
                    ADAM.DataBase.CostCenter olddr = db.CostCenters.Single(a => a.Id == long.Parse(hfParentId.Value));

                    ADAM.DataBase.CostCenter dr = new DataBase.CostCenter();

                    dr.CostCenterCode = long.Parse(txtCostCenterCode.Text);
                    dr.CostCenterName = txtCostCenterName.Text;
                    dr.CostCenterType = int.Parse(ddlCostCenterType.SelectedValue);
                    dr.ParentId = olddr.Id;
                    dr.CostCenterLevel = olddr.CostCenterLevel + 1;

                    db.CostCenters.Add(dr);
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

            txtCostCenterCode.Text = txtCostCenterName.Text = "";
            ddlCostCenterType.SelectedValue = "-1";
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
                ADAM.DataBase.CostCenter dr = db.CostCenters.Single(a => a.Id == long.Parse(hfParentId.Value) && a.Id > 1);
                txtCostCenterCode.Text = dr.CostCenterCode.ToString();
                txtCostCenterName.Text = dr.CostCenterName;
                ddlCostCenterType.SelectedValue = dr.CostCenterType.ToString();
                
                hfID.Value = dr.Id.ToString();
            }
            catch { }
        }

        private void DrawTree()
        {
            tvCostCenter.Nodes.Clear();
            ADAM.DataBase.ADAMConnectionString Db = new DataBase.ADAMConnectionString();
            var Rows = from a in Db.CostCenters where a.ParentId == 0 && a.Id > 1 select a;
            long SerialNo = 0;
            if (Rows.Count() > 0)
            {
                int Node = 0;
                foreach (ADAM.DataBase.CostCenter dr in Rows)
                {
                    SerialNo += 1;
                    TreeNode AccName = new TreeNode();
                    AccName.Value = dr.Id.ToString();
                    AccName.Text = dr.CostCenterCode.ToString() + " " + dr.CostCenterName;
                    tvCostCenter.Nodes.Add(AccName);
                    dr.SerialNo = SerialNo;

                    #region Add First Child
                    var SRows = from a in Db.CostCenters where a.ParentId == dr.Id select a;
                    if (SRows.Count() > 0)
                    {
                        int SNote = 0;

                        foreach (ADAM.DataBase.CostCenter Sdr in SRows)
                        {
                            SerialNo += 1;
                            TreeNode SAccName = new TreeNode();
                            SAccName.Value = Sdr.Id.ToString();
                            SAccName.Text = Sdr.CostCenterCode.ToString() + " " + Sdr.CostCenterName;
                            tvCostCenter.Nodes[Node].ChildNodes.Add(SAccName);
                            Sdr.SerialNo = SerialNo;

                            #region Add Sec Child
                            var TRows = from a in Db.CostCenters where a.ParentId == Sdr.Id select a;
                            if (TRows.Count() > 0)
                            {
                                int FoNote = 0;
                                foreach (ADAM.DataBase.CostCenter Tdr in TRows)
                                {
                                    SerialNo += 1;
                                    TreeNode TAccName = new TreeNode();
                                    TAccName.Value = Tdr.Id.ToString();
                                    TAccName.Text = Tdr.CostCenterCode + " " + Tdr.CostCenterName;
                                    tvCostCenter.Nodes[Node].ChildNodes[SNote].ChildNodes.Add(TAccName);
                                    Tdr.SerialNo = SerialNo;

                                    #region Add Th Child
                                    var FoRows = from a in Db.CostCenters where a.ParentId == Tdr.Id select a;
                                    if (FoRows.Count() > 0)
                                    {
                                        int FiNode = 0;
                                        foreach (ADAM.DataBase.CostCenter Fodr in FoRows)
                                        {
                                            SerialNo += 1;
                                            TreeNode FoAccName = new TreeNode();
                                            FoAccName.Value = Fodr.Id.ToString();
                                            FoAccName.Text = Fodr.CostCenterCode + " " + Fodr.CostCenterName;
                                            tvCostCenter.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes.Add(FoAccName);
                                            Fodr.SerialNo = SerialNo;

                                            #region Add For Child
                                            var FiRows = from a in Db.CostCenters where a.ParentId == Fodr.Id select a;
                                            if (FiRows.Count() > 0)
                                            {
                                                int SiNode = 0;
                                                foreach (ADAM.DataBase.CostCenter Fidr in FiRows)
                                                {
                                                    SerialNo += 1;
                                                    TreeNode FiAccName = new TreeNode();
                                                    FiAccName.Value = Fidr.Id.ToString();
                                                    FiAccName.Text = Fidr.CostCenterCode + " " + Fidr.CostCenterName;
                                                    tvCostCenter.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes.Add(FiAccName);
                                                    Fidr.SerialNo = SerialNo;

                                                    #region Add For Child
                                                    var SiRows = from a in Db.CostCenters where a.ParentId == Fidr.Id select a;
                                                    if (SiRows.Count() > 0)
                                                    {
                                                        int SeNode = 0;
                                                        foreach (ADAM.DataBase.CostCenter Sidr in SiRows)
                                                        {
                                                            SerialNo += 1;
                                                            TreeNode SiAccName = new TreeNode();
                                                            SiAccName.Value = Sidr.Id.ToString();
                                                            SiAccName.Text = Sidr.CostCenterCode + " " + Sidr.CostCenterName;
                                                            tvCostCenter.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes.Add(SiAccName);
                                                            Sidr.SerialNo = SerialNo;

                                                            #region Add Sev Child
                                                            var SeRows = from a in Db.CostCenters where a.ParentId == Sidr.Id select a;
                                                            if (SeRows.Count() > 0)
                                                            {
                                                                int EiNode = 0;
                                                                foreach (ADAM.DataBase.CostCenter Sedr in SeRows)
                                                                {
                                                                    SerialNo += 1;
                                                                    TreeNode SeAccName = new TreeNode();
                                                                    SeAccName.Value = Sedr.Id.ToString();
                                                                    SeAccName.Text = Sedr.CostCenterCode + " " + Sedr.CostCenterName;
                                                                    tvCostCenter.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes[SeNode].ChildNodes.Add(SeAccName);
                                                                    Sedr.SerialNo = SerialNo;

                                                                    #region Add Ei Child
                                                                    var EiRows = from a in Db.CostCenters where a.ParentId == Sedr.Id select a;
                                                                    if (EiRows.Count() > 0)
                                                                    {
                                                                        int NiNode = 0;
                                                                        foreach (ADAM.DataBase.CostCenter Eidr in EiRows)
                                                                        {
                                                                            SerialNo += 1;
                                                                            TreeNode EiAccName = new TreeNode();
                                                                            EiAccName.Value = Eidr.Id.ToString();
                                                                            EiAccName.Text = Eidr.CostCenterCode + " " + Eidr.CostCenterName;
                                                                            tvCostCenter.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes[SeNode].ChildNodes[EiNode].ChildNodes.Add(EiAccName);
                                                                            Eidr.SerialNo = SerialNo;

                                                                            #region Add Ni Child
                                                                            var NiRows = from a in Db.CostCenters where a.ParentId == Eidr.Id select a;
                                                                            if (NiRows.Count() > 0)
                                                                            {
                                                                                int TENode = 0;
                                                                                foreach (ADAM.DataBase.CostCenter Nidr in NiRows)
                                                                                {
                                                                                    SerialNo += 1;
                                                                                    TreeNode NiAccName = new TreeNode();
                                                                                    NiAccName.Value = Nidr.Id.ToString();
                                                                                    NiAccName.Text = Nidr.CostCenterCode + " " + Nidr.CostCenterName;
                                                                                    tvCostCenter.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes[SeNode].ChildNodes[EiNode].ChildNodes[NiNode].ChildNodes.Add(NiAccName);
                                                                                    Nidr.SerialNo = SerialNo;

                                                                                    #region Add Te Child
                                                                                    var TERows = from a in Db.CostCenters where a.ParentId == Nidr.Id select a;
                                                                                    if (TERows.Count() > 0)
                                                                                    {
                                                                                        foreach (ADAM.DataBase.CostCenter TEdr in TERows)
                                                                                        {
                                                                                            SerialNo += 1;
                                                                                            TreeNode TEAccName = new TreeNode();
                                                                                            TEAccName.Value = TEdr.Id.ToString();
                                                                                            TEAccName.Text = TEdr.CostCenterCode + " " + TEdr.CostCenterName;
                                                                                            tvCostCenter.Nodes[Node].ChildNodes[SNote].ChildNodes[FoNote].ChildNodes[FiNode].ChildNodes[SiNode].ChildNodes[SeNode].ChildNodes[EiNode].ChildNodes[NiNode].ChildNodes[TENode].ChildNodes.Add(TEAccName);
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
            ADAM.DataBase.CostCenter dr = mdb.CostCenters.Single(a => a.Id == long.Parse(tvCostCenter.SelectedValue));
            lblCostCenterName.Text = dr.CostCenterName;
            lblCostCenterCode.Text = dr.CostCenterCode.ToString();
            hfParentId.Value = dr.Id.ToString();
        }
    }
}