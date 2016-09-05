using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ADAM.BasicData
{
    public class csGetPermission
    {
        public bool getPermission(int UserId, int PageId, int OperationId)
        {
            //if (DateTime.Now >= DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["DBNewLevel"].ToString()))
            //    return false;

            if (UserId == -11 || UserId == -12 || UserId == -13)
                return true;

            ADAM.DataBase.ADAMConnectionString mdb = new ADAM.DataBase.ADAMConnectionString();
            var Rows = from a in mdb.Permissions
                       where a.OperationId == OperationId && a.PageId == PageId && a.UserId == UserId
                       select a;
            if (Rows.Count() == 0)
                return false;
            return true;
        }

        #region ReportData

        public static string DBUser = System.Configuration.ConfigurationManager.AppSettings["DBUser"].ToString();
        public static string DBPassword = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();
        public static string DBServerName = Dns.GetHostName().ToString();// + "\\SQLEXPRESS";
        public static string DBName = System.Configuration.ConfigurationManager.AppSettings["DBName"].ToString();

        #endregion
    }
}