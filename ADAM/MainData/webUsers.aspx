<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webUsers.aspx.cs" Inherits="ADAM.MainData.webUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="شاشة المستخدمين" Font-Size="X-Large" CssClass="lblPageName" Font-Underline="true"></asp:Label></h1>
 
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" /></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
            <td>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <table class="table">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="الموظف" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlEmployee" CssClass="ddl" runat="server" DataSourceID="dbEmployee" DataTextField="Name" DataValueField="Id"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="اسم الدخول" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtNickName" ToolTip="ادخل اسم الدخول" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="كلمة المرور" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtPassword" TextMode="Password" ToolTip="ادخل كلمة المرور" runat="server" CssClass="txt"></asp:TextBox>
                 <asp:CheckBox ID="chkShowPassword" AutoPostBack="true" runat="server" OnCheckedChanged="chkShowPassword_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="الحالة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlIsLock" CssClass="ddl" runat="server">
                    <asp:ListItem Text="---" Value="0"></asp:ListItem>
                    <asp:ListItem Text="فعال" Value="1"></asp:ListItem>
                    <asp:ListItem Text="غير فعال" Value="2"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>

                <asp:SqlDataSource ID="dbEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + ' ' + LastName AS Name FROM EmployeeData"></asp:SqlDataSource>

            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
