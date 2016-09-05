<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webProductionLine.aspx.cs" Inherits="ADAM.MainData.webProductionLine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="شاشة خطوط الانتاج" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
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
                <asp:Label ID="Label3" runat="server" Text="ادخل رقم الحساب" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtAccountCode" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="كود خط الانتاج" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtCode" TextMode="Number" ToolTip="ادخل كود الدولة ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="خط الانتاج" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ToolTip="ادخل اسم الدولة" CssClass="txt"></asp:TextBox></td>
        </tr>
    </table>
</asp:Content>

