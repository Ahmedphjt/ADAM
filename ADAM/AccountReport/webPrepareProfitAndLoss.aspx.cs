using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.AccountReport
{
    public partial class webPrepareProfitAndLoss : System.Web.UI.Page
    {
        public int pageid = 145;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 5;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            ReportDocument myReportDocument = new ReportDocument();

            myReportDocument.Load(Server.MapPath("~/AccountReport/Report/rptProfitAndLoss.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;

            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@AccountLevel", int.Parse(ddlAccountLevel.SelectedValue));
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ADAM.DataBase.ADAMConnectionString Db = new DataBase.ADAMConnectionString();

                var DelRows = from a in Db.ProfitAndLosses select a;
                foreach (ADAM.DataBase.ProfitAndLoss Deldr in DelRows)
                    Db.ProfitAndLosses.Remove(Deldr);
                Db.SaveChanges();

                DateTime FromDate = DateTime.Parse(txtFromDate.Text);
                DateTime ToDate = DateTime.Parse(txtToDate.Text);
                
                var Rows = from a in Db.Accounts where a.ParentId == 0 && (a.MasterCode == 3 || a.MasterCode == 4) select a;

                if (Rows.Count() > 0)
                {
                    foreach (ADAM.DataBase.Account dr in Rows)
                    {
                        decimal flevelDebit = 0; decimal flevelCrdeit = 0;

                        ADAM.DataBase.ProfitAndLoss flevelProf = new DataBase.ProfitAndLoss();
                        flevelProf.AccountId = dr.Id;
                        flevelProf.AccountLevel = dr.AccountLevel;

                        var flevelRows = from a in Db.JournalDetails where a.AccountId == dr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1) select a;

                        #region Calc FJournal
                        if (flevelRows.Count() > 0)
                        {
                            foreach (ADAM.DataBase.JournalDetail fleveldr in flevelRows)
                            {
                                flevelDebit += fleveldr.Debit;
                                flevelCrdeit += fleveldr.Credit;
                            }

                            flevelProf.Credit = flevelCrdeit;
                            flevelProf.Debit = flevelDebit;
                        }
                        #endregion

                        else
                        {
                            #region Add First Child
                            var SRows = from a in Db.Accounts where a.ParentId == dr.Id select a;
                            if (SRows.Count() > 0)
                            {
                                foreach (ADAM.DataBase.Account Sdr in SRows)
                                {
                                    decimal SlevelDebit = 0; decimal SlevelCrdeit = 0;

                                    ADAM.DataBase.ProfitAndLoss SlevelProf = new DataBase.ProfitAndLoss();
                                    SlevelProf.AccountId = Sdr.Id;
                                    SlevelProf.AccountLevel = Sdr.AccountLevel;

                                    var SlevelRows = from a in Db.JournalDetails where a.AccountId == Sdr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1) select a;
                                    #region Calc Slevel
                                    if (SlevelRows.Count() > 0)
                                    {
                                        foreach (ADAM.DataBase.JournalDetail Sleveldr in SlevelRows)
                                        {
                                            SlevelDebit += Sleveldr.Debit;
                                            SlevelCrdeit += Sleveldr.Credit;
                                        }

                                        SlevelProf.Credit = SlevelCrdeit;
                                        SlevelProf.Debit = SlevelDebit;
                                    }
                                    #endregion

                                    #region Add Sec Child
                                    var TRows = from a in Db.Accounts where a.ParentId == Sdr.Id select a;
                                    if (TRows.Count() > 0)
                                    {
                                        foreach (ADAM.DataBase.Account Tdr in TRows)
                                        {
                                            decimal TlevelDebit = 0; decimal TlevelCrdeit = 0;

                                            ADAM.DataBase.ProfitAndLoss TlevelProf = new DataBase.ProfitAndLoss();
                                            TlevelProf.AccountId = Tdr.Id;
                                            TlevelProf.AccountLevel = Tdr.AccountLevel;

                                            var TlevelRows = from a in Db.JournalDetails where a.AccountId == Tdr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1) select a;
                                            #region Calc Slevel
                                            if (TlevelRows.Count() > 0)
                                            {
                                                foreach (ADAM.DataBase.JournalDetail Tleveldr in TlevelRows)
                                                {
                                                    TlevelDebit += Tleveldr.Debit;
                                                    TlevelCrdeit += Tleveldr.Credit;
                                                }
                                            }
                                            #endregion
                                            TlevelProf.Credit = TlevelCrdeit;
                                            TlevelProf.Debit = TlevelDebit;

                                            #region Add Th Child
                                            var FoRows = from a in Db.Accounts where a.ParentId == Tdr.Id select a;
                                            if (FoRows.Count() > 0)
                                            {
                                                foreach (ADAM.DataBase.Account Fodr in FoRows)
                                                {
                                                    decimal FolevelDebit = 0; decimal FolevelCrdeit = 0;

                                                    ADAM.DataBase.ProfitAndLoss FolevelProf = new DataBase.ProfitAndLoss();
                                                    FolevelProf.AccountId = Fodr.Id;
                                                    FolevelProf.AccountLevel = Fodr.AccountLevel;

                                                    var FolevelRows = from a in Db.JournalDetails where a.AccountId == Fodr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1)  select a;
                                                    #region Calc Folevel
                                                    if (FolevelRows.Count() > 0)
                                                    {
                                                        foreach (ADAM.DataBase.JournalDetail Foleveldr in FolevelRows)
                                                        {
                                                            FolevelDebit += Foleveldr.Debit;
                                                            FolevelCrdeit += Foleveldr.Credit;
                                                        }
                                                    }
                                                    #endregion

                                                    FolevelProf.Credit = FolevelCrdeit;
                                                    FolevelProf.Debit = FolevelDebit;

                                                    #region Add For Child
                                                    var FiRows = from a in Db.Accounts where a.ParentId == Fodr.Id select a;
                                                    if (FiRows.Count() > 0)
                                                    {
                                                        foreach (ADAM.DataBase.Account Fidr in FiRows)
                                                        {
                                                            decimal FilevelDebit = 0; decimal FilevelCrdeit = 0;

                                                            ADAM.DataBase.ProfitAndLoss FilevelProf = new DataBase.ProfitAndLoss();
                                                            FilevelProf.AccountId = Fidr.Id;
                                                            FilevelProf.AccountLevel = Fidr.AccountLevel;

                                                            var FilevelRows = from a in Db.JournalDetails where a.AccountId == Fidr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1) select a;
                                                            #region Calc Filevel
                                                            if (FilevelRows.Count() > 0)
                                                            {
                                                                foreach (ADAM.DataBase.JournalDetail Fileveldr in FilevelRows)
                                                                {
                                                                    FilevelDebit += Fileveldr.Debit;
                                                                    FilevelCrdeit += Fileveldr.Credit;
                                                                }
                                                            }
                                                            #endregion

                                                            FilevelProf.Credit = FilevelCrdeit;
                                                            FilevelProf.Debit = FilevelDebit;

                                                            #region Add For Child
                                                            var SiRows = from a in Db.Accounts where a.ParentId == Fidr.Id select a;
                                                            if (SiRows.Count() > 0)
                                                            {
                                                                foreach (ADAM.DataBase.Account Sidr in SiRows)
                                                                {
                                                                    decimal SilevelDebit = 0; decimal SilevelCrdeit = 0;

                                                                    ADAM.DataBase.ProfitAndLoss SilevelProf = new DataBase.ProfitAndLoss();
                                                                    SilevelProf.AccountId = Sidr.Id;
                                                                    SilevelProf.AccountLevel = Sidr.AccountLevel;

                                                                    var SilevelRows = from a in Db.JournalDetails where a.AccountId == Sidr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1) select a;
                                                                    #region Calc Silevel
                                                                    if (SilevelRows.Count() > 0)
                                                                    {
                                                                        foreach (ADAM.DataBase.JournalDetail Sileveldr in SilevelRows)
                                                                        {
                                                                            SilevelDebit += Sileveldr.Debit;
                                                                            SilevelCrdeit += Sileveldr.Credit;
                                                                        }
                                                                    }
                                                                    #endregion

                                                                    SilevelProf.Credit = SilevelCrdeit;
                                                                    SilevelProf.Debit = SilevelDebit;

                                                                    #region Add Sev Child
                                                                    var SeRows = from a in Db.Accounts where a.ParentId == Sidr.Id select a;
                                                                    if (SeRows.Count() > 0)
                                                                    {
                                                                        foreach (ADAM.DataBase.Account Sedr in SeRows)
                                                                        {
                                                                            decimal SelevelDebit = 0; decimal SelevelCrdeit = 0;

                                                                            ADAM.DataBase.ProfitAndLoss SelevelProf = new DataBase.ProfitAndLoss();
                                                                            SelevelProf.AccountId = Sedr.Id;
                                                                            SelevelProf.AccountLevel = Sedr.AccountLevel;

                                                                            var SelevelRows = from a in Db.JournalDetails where a.AccountId == Sedr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1) select a;
                                                                            #region Calc Selevel
                                                                            if (SelevelRows.Count() > 0)
                                                                            {
                                                                                foreach (ADAM.DataBase.JournalDetail Seleveldr in SelevelRows)
                                                                                {
                                                                                    SelevelDebit += Seleveldr.Debit;
                                                                                    SelevelCrdeit += Seleveldr.Credit;
                                                                                }
                                                                            }
                                                                            #endregion

                                                                            SelevelProf.Credit = SelevelCrdeit;
                                                                            SelevelProf.Debit = SelevelDebit;

                                                                            #region Add Ei Child
                                                                            var EiRows = from a in Db.Accounts where a.ParentId == Sedr.Id select a;
                                                                            if (EiRows.Count() > 0)
                                                                            {
                                                                                foreach (ADAM.DataBase.Account Eidr in EiRows)
                                                                                {
                                                                                    decimal EilevelDebit = 0; decimal EilevelCrdeit = 0;

                                                                                    ADAM.DataBase.ProfitAndLoss EilevelProf = new DataBase.ProfitAndLoss();
                                                                                    EilevelProf.AccountId = Eidr.Id;
                                                                                    EilevelProf.AccountLevel = Eidr.AccountLevel;

                                                                                    var EilevelRows = from a in Db.JournalDetails where a.AccountId == Eidr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1) select a;
                                                                                    #region Calc Eilevel
                                                                                    if (EilevelRows.Count() > 0)
                                                                                    {
                                                                                        foreach (ADAM.DataBase.JournalDetail Eileveldr in EilevelRows)
                                                                                        {
                                                                                            EilevelDebit += Eileveldr.Debit;
                                                                                            EilevelCrdeit += Eileveldr.Credit;
                                                                                        }
                                                                                    }
                                                                                    #endregion

                                                                                    EilevelProf.Credit = EilevelCrdeit;
                                                                                    EilevelProf.Debit = EilevelDebit;

                                                                                    #region Add Ni Child
                                                                                    var NiRows = from a in Db.Accounts where a.ParentId == Eidr.Id select a;
                                                                                    if (NiRows.Count() > 0)
                                                                                    {
                                                                                        foreach (ADAM.DataBase.Account Nidr in NiRows)
                                                                                        {
                                                                                            decimal NilevelDebit = 0; decimal NilevelCrdeit = 0;

                                                                                            ADAM.DataBase.ProfitAndLoss NilevelProf = new DataBase.ProfitAndLoss();
                                                                                            NilevelProf.AccountId = Nidr.Id;
                                                                                            NilevelProf.AccountLevel = Nidr.AccountLevel;

                                                                                            var NilevelRows = from a in Db.JournalDetails where a.AccountId == Nidr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1) select a;
                                                                                            #region Calc Eilevel
                                                                                            if (NilevelRows.Count() > 0)
                                                                                            {
                                                                                                foreach (ADAM.DataBase.JournalDetail Nileveldr in NilevelRows)
                                                                                                {
                                                                                                    NilevelDebit += Nileveldr.Debit;
                                                                                                    NilevelCrdeit += Nileveldr.Credit;
                                                                                                }
                                                                                            }
                                                                                            #endregion

                                                                                            NilevelProf.Credit = NilevelCrdeit;
                                                                                            NilevelProf.Debit = NilevelDebit;

                                                                                            #region Add Te Child
                                                                                            var TERows = from a in Db.Accounts where a.ParentId == Nidr.Id select a;
                                                                                            if (TERows.Count() > 0)
                                                                                            {
                                                                                                foreach (ADAM.DataBase.Account TEdr in TERows)
                                                                                                {
                                                                                                    decimal TElevelDebit = 0; decimal TElevelCrdeit = 0;

                                                                                                    ADAM.DataBase.ProfitAndLoss TElevelProf = new DataBase.ProfitAndLoss();
                                                                                                    TElevelProf.AccountId = TEdr.Id;
                                                                                                    TElevelProf.AccountLevel = TEdr.AccountLevel;

                                                                                                    var TElevelRows = from a in Db.JournalDetails where a.AccountId == TEdr.Id && a.JournalHeader.Posted == 1 && a.JournalHeader.JournalDate > FromDate.AddDays(-1) && a.JournalHeader.JournalDate < ToDate.AddDays(1) select a;
                                                                                                    #region Calc TElevel
                                                                                                    if (TElevelRows.Count() > 0)
                                                                                                    {
                                                                                                        foreach (ADAM.DataBase.JournalDetail TEleveldr in TElevelRows)
                                                                                                        {
                                                                                                            TElevelDebit += TEleveldr.Debit;
                                                                                                            TElevelCrdeit += TEleveldr.Credit;
                                                                                                        }
                                                                                                    }
                                                                                                    #endregion

                                                                                                    TElevelProf.Credit = TElevelCrdeit;
                                                                                                    TElevelProf.Debit = TElevelDebit;

                                                                                                    TElevelProf.TotalDebit = TElevelProf.TotalCredit = 0;
                                                                                                    if (TElevelDebit > TElevelCrdeit)
                                                                                                    {
                                                                                                        TElevelProf.TotalDebit = TElevelDebit - TElevelCrdeit;
                                                                                                        TElevelProf.TotalCredit = 0;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        TElevelProf.TotalDebit = 0;
                                                                                                        TElevelProf.TotalCredit = TElevelCrdeit - TElevelDebit;
                                                                                                    }

                                                                                                    Db.ProfitAndLosses.Add(TElevelProf);

                                                                                                    NilevelProf.Credit = NilevelCrdeit = NilevelProf.Credit + TElevelProf.Credit;
                                                                                                    NilevelProf.Debit = NilevelDebit = NilevelProf.Debit + TElevelProf.Debit;
                                                                                                }
                                                                                            }
                                                                                            #endregion

                                                                                            NilevelProf.TotalDebit = NilevelProf.TotalCredit = 0;
                                                                                            if (NilevelDebit > NilevelCrdeit)
                                                                                            {
                                                                                                NilevelProf.TotalDebit = NilevelDebit - NilevelCrdeit;
                                                                                                NilevelProf.TotalCredit = 0;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                NilevelProf.TotalDebit = 0;
                                                                                                NilevelProf.TotalCredit = NilevelCrdeit - NilevelDebit;
                                                                                            }

                                                                                            Db.ProfitAndLosses.Add(NilevelProf);

                                                                                            EilevelProf.Credit = EilevelCrdeit = EilevelProf.Credit + NilevelProf.Credit;
                                                                                            EilevelProf.Debit = EilevelDebit = EilevelProf.Debit + NilevelProf.Debit;
                                                                                        }
                                                                                    }
                                                                                    #endregion

                                                                                    EilevelProf.TotalDebit = EilevelProf.TotalCredit = 0;
                                                                                    if (EilevelDebit > EilevelCrdeit)
                                                                                    {
                                                                                        EilevelProf.TotalDebit = EilevelDebit - EilevelCrdeit;
                                                                                        EilevelProf.TotalCredit = 0;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        EilevelProf.TotalDebit = 0;
                                                                                        EilevelProf.TotalCredit = EilevelCrdeit - EilevelDebit;
                                                                                    }

                                                                                    Db.ProfitAndLosses.Add(EilevelProf);

                                                                                    SelevelProf.Credit = SelevelCrdeit = SelevelProf.Credit + EilevelProf.Credit;
                                                                                    SelevelProf.Debit = SelevelDebit = SelevelProf.Debit + EilevelProf.Debit;
                                                                                 
                                                                                }
                                                                            }
                                                                            #endregion

                                                                            SelevelProf.TotalDebit = SelevelProf.TotalCredit = 0;
                                                                            if (SelevelDebit > SelevelCrdeit)
                                                                            {
                                                                                SelevelProf.TotalDebit = SelevelDebit - SelevelCrdeit;
                                                                                SelevelProf.TotalCredit = 0;
                                                                            }
                                                                            else
                                                                            {
                                                                                SelevelProf.TotalDebit = 0;
                                                                                SelevelProf.TotalCredit = SelevelCrdeit - SelevelDebit;
                                                                            }

                                                                            Db.ProfitAndLosses.Add(SelevelProf);

                                                                            SilevelProf.Credit = SilevelCrdeit = SilevelProf.Credit + SelevelProf.Credit;
                                                                            SilevelProf.Debit = SilevelDebit = SilevelProf.Debit + SelevelProf.Debit;
                                                                            
                                                                        }
                                                                    }
                                                                    #endregion

                                                                    SilevelProf.TotalDebit = SilevelProf.TotalCredit = 0;
                                                                    if (SilevelDebit > SilevelCrdeit)
                                                                    {
                                                                        SilevelProf.TotalDebit = SilevelDebit - SilevelCrdeit;
                                                                        SilevelProf.TotalCredit = 0;
                                                                    }
                                                                    else
                                                                    {
                                                                        SilevelProf.TotalDebit = 0;
                                                                        SilevelProf.TotalCredit = SilevelCrdeit - SilevelDebit;
                                                                    }

                                                                    Db.ProfitAndLosses.Add(SilevelProf);

                                                                    FilevelProf.Credit = FilevelCrdeit = FilevelProf.Credit + SilevelProf.Credit;
                                                                    FilevelProf.Debit = FilevelDebit = FilevelProf.Debit + SilevelProf.Debit;
                                                                    
                                                                }
                                                            }
                                                            #endregion

                                                            FilevelProf.TotalDebit = FilevelProf.TotalCredit = 0;
                                                            if (FilevelDebit > FilevelCrdeit)
                                                            {
                                                                FilevelProf.TotalDebit = FilevelDebit - FilevelCrdeit;
                                                                FilevelProf.TotalCredit = 0;
                                                            }
                                                            else
                                                            {
                                                                FilevelProf.TotalDebit = 0;
                                                                FilevelProf.TotalCredit = FilevelCrdeit - FilevelDebit;
                                                            }

                                                            Db.ProfitAndLosses.Add(FilevelProf);

                                                            FolevelProf.Credit = FolevelCrdeit = FolevelProf.Credit + FilevelProf.Credit;
                                                            FolevelProf.Debit = FolevelDebit = FolevelProf.Debit + FilevelProf.Debit;
                                                        }
                                                    }
                                                    #endregion

                                                    FolevelProf.TotalDebit = FolevelProf.TotalCredit = 0;
                                                    if (FolevelDebit > FolevelCrdeit)
                                                    {
                                                        FolevelProf.TotalDebit = FolevelDebit - FolevelCrdeit;
                                                        FolevelProf.TotalCredit = 0;
                                                    }
                                                    else
                                                    {
                                                        FolevelProf.TotalDebit = 0;
                                                        FolevelProf.TotalCredit = FolevelCrdeit - FolevelDebit;
                                                    }

                                                    Db.ProfitAndLosses.Add(FolevelProf);

                                                    TlevelProf.Credit = TlevelCrdeit = TlevelProf.Credit + FolevelProf.Credit;
                                                    TlevelProf.Debit = TlevelDebit = TlevelProf.Debit + FolevelProf.Debit;

                                                }
                                            }
                                            #endregion

                                            TlevelProf.TotalDebit = TlevelProf.TotalCredit = 0;
                                            if (TlevelDebit > TlevelCrdeit)
                                            {
                                                TlevelProf.TotalDebit = TlevelDebit - TlevelCrdeit;
                                                TlevelProf.TotalCredit = 0;
                                            }
                                            else
                                            {
                                                TlevelProf.TotalDebit = 0;
                                                TlevelProf.TotalCredit = TlevelCrdeit - TlevelDebit;
                                            }

                                            Db.ProfitAndLosses.Add(TlevelProf);

                                            SlevelProf.Credit = SlevelCrdeit = SlevelProf.Credit + TlevelProf.Credit;
                                            SlevelProf.Debit = SlevelDebit = SlevelProf.Debit + TlevelProf.Debit;
                                        }
                                    }
                                    #endregion

                                    SlevelProf.TotalDebit = SlevelProf.TotalCredit = 0;
                                    if (SlevelDebit > SlevelCrdeit)
                                    {
                                        SlevelProf.TotalDebit = SlevelDebit - SlevelCrdeit;
                                        SlevelProf.TotalCredit = 0;
                                    }
                                    else
                                    {
                                        SlevelProf.TotalDebit = 0;
                                        SlevelProf.TotalCredit = SlevelCrdeit - SlevelDebit;
                                    }

                                    Db.ProfitAndLosses.Add(SlevelProf);

                                    flevelProf.Credit = flevelCrdeit = flevelProf.Credit + SlevelProf.Credit;
                                    flevelProf.Debit = flevelDebit = flevelProf.Debit + SlevelProf.Debit;
                                }
                            }
                            #endregion
                        }

                        flevelProf.TotalDebit = flevelProf.TotalCredit = 0;
                        if (flevelDebit > flevelCrdeit)
                        {
                            flevelProf.TotalDebit = flevelDebit - flevelCrdeit;
                            flevelProf.TotalCredit = 0;
                        }
                        else
                        {
                            flevelProf.TotalDebit = 0;
                            flevelProf.TotalCredit = flevelCrdeit - flevelDebit;
                        }

                        Db.ProfitAndLosses.Add(flevelProf);
                    }
                }
                Db.SaveChanges();

                Response.Write("<script>alert('تمت عملية الاعداد بنجاح')</script>");
            }
            catch { }
        }
    }
}