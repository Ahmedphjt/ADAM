using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADAM.BasicData
{
    public class csJournal
    {
        ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();

        public long InsertIntoJournalHeader(long JournalCode, DateTime JournalDate, int JournalType, string Note, int Posted,long DocId)
        {
            if (JournalCode == 0)
            {
                var Rows = from a in db.JournalHeaders where a.JournalType == JournalType  orderby a.JournalCode descending select a;
                if (Rows.Count() > 0)
                {
                    JournalCode = Rows.First().JournalCode;
                    JournalCode += 1;
                }
                else
                    JournalCode = 1;
            }

            ADAM.DataBase.JournalHeader hdr = new DataBase.JournalHeader();
            hdr.JournalCode = JournalCode;
            hdr.JournalDate = JournalDate;
            hdr.JournalType = JournalType;
            hdr.Note = Note;            
            hdr.Posted = Posted;
            hdr.DocId = DocId;

            db.JournalHeaders.Add(hdr);
            db.SaveChanges();

            return hdr.Id;
        }

        public void InsertIntoJournalDetails(long AccountId, long CostCenterId, decimal Debit, decimal Credit, long JournalId,string Notes)
        {
            ADAM.DataBase.JournalDetail ddr = new DataBase.JournalDetail();
            ddr.AccountId = AccountId;
            ddr.CostCenterId = CostCenterId;
            ddr.Debit = Debit;
            ddr.Credit = Credit;
            ddr.JournalId = JournalId;
            ddr.Notes = Notes;

            db.JournalDetails.Add(ddr);
            db.SaveChanges();
        }
    }
}