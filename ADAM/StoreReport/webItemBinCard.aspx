<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webItemBinCard.aspx.cs" Inherits="ADAM.StoreReport.webItemBinCard" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير كارت الصنف" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="المخزن"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="خط الانتاج"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlProductionLine" CssClass="ddl" runat="server" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="الصنف"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemName" CssClass="ddl" runat="server" DataSourceID="dbItemName" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="اللون"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemColor" CssClass="ddl" runat="server" DataSourceID="dbItemColor" DataTextField="ColorName" DataValueField="Id"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
             <td >
                    <asp:Label ID="Label5" runat="server" Text="عرض التقرير" CssClass="lbl"></asp:Label></td>
            <td colspan="3"style="text-align: center"> <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemName" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Items.Id, Items.Name + '_' + ItemStatus.ItemStatus AS Name FROM Items INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id WHERE (Items.ItemTypeId = @ItemTypeId) AND (Items.ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ColorName UNION SELECT ItemColor.Id, ItemColor.ColorName FROM ItemColor INNER JOIN ItemColorSelected ON ItemColor.Id = ItemColorSelected.ItemColorId WHERE (ItemColorSelected.ItemId = @ItemId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemName" DefaultValue="0" Name="ItemId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td><asp:SqlDataSource ID="dbItemStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ItemStatus UNION SELECT Id, ItemStatus FROM ItemStatus"></asp:SqlDataSource></td>
        </tr>
    </table>
</asp:Content>
