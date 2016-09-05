<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webArea.aspx.cs" Inherits="ADAM.MainData.webArea" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة المناطق" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
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
            <td style="text-align:center">
                <asp:Label ID="Label4" runat="server" Text="الدولة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txt" DataSourceID="dbCountry" DataTextField="Name" DataValueField="Id" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="Label3" runat="server" Text=" المحافظة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlCity" runat="server" CssClass="txt" DataSourceID="dbCity" DataTextField="Name" DataValueField="Id" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="Label5" runat="server" Text=" المدينة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlGovernorate" runat="server" CssClass="txt" DataSourceID="dbGovernorate" DataTextField="GovernorateName" DataValueField="Id" Width="150px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="Label1" runat="server" Text="كود المنطقة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtCode" TextMode="Number" ToolTip="ادخل كود المنطقة ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="Label2" runat="server" Text="المنطقة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ToolTip="ادخل اسم المنطقة" CssClass="txt"></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbCity" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM CityData WHERE (CountryId = @CountryId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCountry" DefaultValue="0" Name="CountryId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbCountry" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS Name UNION SELECT Id, Name FROM CountryData"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbGovernorate" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS GovernorateName UNION SELECT Id, GovernorateName FROM GovernorateData WHERE (CityId = @CityId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCity" DefaultValue="0" Name="CityId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
