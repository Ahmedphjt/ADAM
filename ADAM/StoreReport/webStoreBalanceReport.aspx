<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webStoreBalanceReport.aspx.cs" Inherits="ADAM.StoreReport.webStoreBalanceReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
        <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير أرصده المخازن" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table>
        <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="خط الانتاج" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProductionLine" CssClass="ddl" runat="server" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id"></asp:DropDownList>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label2" runat="server" Text="المخزن" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="عرض التقرير" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            <td><asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource></td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>