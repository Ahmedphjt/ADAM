using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ADAM.BasicData
{
    public class csGetPermission
    {
        public bool getPermission(int UserId,int PageId,int OperationId)
        {
            ADAM.DataBase.AdamDataBataDataContext mdb = new ADAM.DataBase.AdamDataBataDataContext();
            var Rows = from a in mdb.Permissions
                       where a.OperationId == OperationId && a.PageId == PageId && a.UserId == UserId
                       select a;
            if (Rows.Count() == 0)
                return false;
            return true;
        }

        #region ReportData

        public static string DBUser = "sa";
        public static string DBPassword = "P@ssw0rd";
        public static string DBServerName = Dns.GetHostName().ToString();// + "\\SQLEXPRESS";
        public static string DBName = "ADAM";

        #endregion
    }
}