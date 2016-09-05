<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="wemPointOfSales.aspx.cs" Inherits="ADAM.MainData.wemPointOfSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="شاشة نقاط البيع" Font-Size="X-Large" CssClass="lblPageName" Font-Underline="true"></asp:Label></h1>
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
    <table class="menu">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="كود نقطة البيع" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtCode" TextMode="Number" ToolTip="ادخل كود نقطة البيع ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="نقطة البيع" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtName" runat="server" ToolTip="ادخل اسم نقطة البيع" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label24" runat="server" Text="المدينة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlCountry" CssClass="ddl" runat="server" AutoPostBack="True" DataSourceID="dbCountry" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label25" runat="server" Text="المحافظة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlCity" CssClass="ddl" runat="server" DataSourceID="dbCity" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="هاتف نقطة البيع" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtPhone" TextMode="Number" ToolTip="ادخل هاتف نقطة البيع" runat="server" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="العنوان" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtAdress" Width="250px" TextMode="MultiLine" runat="server" ToolTip="ادخل العنوان" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="ملاحظات" CssClass="lbl"></asp:Label></td>
            <td colspan="3" style="text-align: right">
                <asp:TextBox ID="txtNote" TextMode="MultiLine" Width="250" ToolTip="ادخل الملاحظات" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbCountry" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM CountryData"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbCity" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS Name UNION SELECT Id, Name FROM CityData WHERE (CountryId = @CountryId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCountry" DefaultValue="0" Name="CountryId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
