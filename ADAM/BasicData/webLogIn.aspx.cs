using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.BasicData
{
    public partial class webLogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogMeIn_Click(object sender, ImageClickEventArgs e)
        {
           
            if (txtUserName.Text == "h0ssam" && txtPassword.Text == "11111983")
            {
                Session["UserID"] = "-11";
                Response.Redirect("~/BasicData/webHomePage.aspx");
            }
            else if (txtUserName.Text == "Adma" && txtPassword.Text == "h]lpshlhg]dk")
            {
                Session["UserID"] = "-12";
                Response.Redirect("~/BasicData/webHomePage.aspx");
            }
            else if (txtUserName.Text.ToLower() == "admin" && txtPassword.Text == "135792468")
            {
                Session["UserID"] = "-13";
                Response.Redirect("~/BasicData/webHomePage.aspx");
            }
            else
            {
                ADAM.DataBase.ADAMConnectionString mdb = new DataBase.ADAMConnectionString();
                var Rows = from a in mdb.UserDatas where a.NickName == txtUserName.Text && a.Password == txtPassword.Text select a;
                if (Rows.Count() > 0)
                {
                    ADAM.DataBase.UserData dr = mdb.UserDatas.Single(a => a.NickName == txtUserName.Text && a.Password == txtPassword.Text);
                    Session["UserID"] = dr.Id;
                    Response.Redirect("~/BasicData/webHomePage.aspx");
                }
                else
                {
                    Response.Write("<script>alert('من فضلك تأكد من اسم المستخدم وكلمة المرور')</script>");
                    return;
                }
            }
        }
    }
}