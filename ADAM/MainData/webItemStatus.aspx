<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webItemStatus.aspx.cs" Inherits="ADAM.MainData.webItemStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="شاشة نوع المنتج" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
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
                <%--<asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" />--%></td>
        </tr>
    </table></asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
        <table class="table">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="نوع المنتج" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" ToolTip="ادخل نوع المنتج" CssClass="txt"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvItemStatus" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbItemStatus" DataKeyNames="Id" OnSelectedIndexChanged="gvItemStatus_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbItemStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Id, ItemStatus FROM ItemStatus"></asp:SqlDataSource>
        <asp:HiddenField ID="hfId" Value="0" runat="server" />
    </asp:Content>

