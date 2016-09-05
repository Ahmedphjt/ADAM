<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webrptItemFollowUp.aspx.cs" Inherits="ADAM.CostsReport.webrptItemFollowUp" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير متابعة الاصناف" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label1" runat="server" Text="خط الانتاج" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlProductionLine" CssClass="ddl" runat="server" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label2" runat="server" Text="المخزن" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label4" runat="server" Text="الصنف" CssClass="lbl"></asp:Label>
                </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlItemName" CssClass="ddl" runat="server" DataSourceID="dbItems" DataTextField="Name" DataValueField="Id" Width="300px"></asp:DropDownList>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItems" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items WHERE (ItemTypeId = @ItemTypeId) AND (ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>

        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label5" runat="server" Text="تاريخ البداية" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFMovementDate" runat="server" TextMode="Date" ToolTip="ادخل تاريخ البداية" CssClass="txt"></asp:TextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label6" runat="server" Text="تاريخ النهاية" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEMovementDate" runat="server" TextMode="Date" ToolTip="ادخل تاريخ النهاية" CssClass="txt"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="عرض التقرير" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>
