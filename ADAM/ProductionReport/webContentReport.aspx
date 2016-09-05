<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webContentReport.aspx.cs" Inherits="ADAM.ProductionReport.webContentReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير التركيبات" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="مخزن المنتج"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionItemType" CssClass="ddl" runat="server" DataSourceID="dbProductionItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="خط انتاج المنتج"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductProductionLine" CssClass="ddl" runat="server" AutoPostBack="True" DataSourceID="dbPrductProductionLine" DataTextField="productionLineName" DataValueField="Id"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" Text="المنتج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionItem" CssClass="ddl" runat="server" AutoPostBack="True" DataSourceID="dbProduct" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label6" runat="server" Text="لون المنتج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionItemcolor" CssClass="ddl" runat="server" DataSourceID="dbProductItemColor" DataTextField="ColorName" DataValueField="Id"></asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: center" colspan="2">
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" />
            </td>
            <td style="text-align: center">&nbsp;</td>
        </tr>
    </table>
    <br />
     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbProductionItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbPrductProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProduct" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items WHERE (ItemTypeId = @ItemTypeId) AND (ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlProductionItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ColorName UNION SELECT ItemColor.Id, ItemColor.ColorName FROM ItemColor INNER JOIN ItemColorSelected ON ItemColor.Id = ItemColorSelected.ItemColorId WHERE (ItemColorSelected.ItemId = @ItemId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlProductionItem" DefaultValue="0" Name="ItemId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
