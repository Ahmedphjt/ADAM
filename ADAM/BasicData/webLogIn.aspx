<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webLogIn.aspx.cs" Inherits="ADAM.BasicData.webLogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../MyStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10% 0 0 40%; text-align: center; width: auto">

            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="btn"></asp:TextBox></td>
                    <td>
                        <h2 style="color: white">اسم المستخدم</h2>
                    </td>
                    <td>
                        <h2>
                            <img src="../Image/ho0027-64.png" class="Img" /></h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="btn"></asp:TextBox></td>
                    <td>
                        <h2 style="color: white">كلمة المرور</h2>
                    </td>
                    <td>
                        <h2>
                            <img src="../Image/Key.png" class="Img" /></h2>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:ImageButton ID="LogMeIn" runat="server" ImageUrl="~/Image/Enter.png" Height="75px" Width="75px" AlternateText="دخول" OnClick="LogMeIn_Click" /></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
