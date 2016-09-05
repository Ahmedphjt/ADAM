<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webProductionOrderReport.aspx.cs" Inherits="ADAM.ProductionReport.webProductionOrderReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير طلب أمر انتاج" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label2" runat="server" Text="رقم الطلب" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDeliveryOrder" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label3" runat="server" Text="عرض التقرير" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvProducionOrder" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbProducionOrder" OnSelectedIndexChanged="gvPurchaseDetailsData_SelectedIndexChanged" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="ProductionNo" HeaderText="رقم امر الانتاج" SortExpression="ProductionNo" />
            <asp:BoundField DataField="ProductionDate" HeaderText="تاريخ امر الانتاج" SortExpression="ProductionDate" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Code" HeaderText="كود المنتج" SortExpression="Code" />
            <asp:BoundField DataField="ProductionItemsName" HeaderText="اسم المنتج" SortExpression="ProductionItemsName" />
            <asp:BoundField DataField="UnitName" HeaderText="الوحدة" SortExpression="UnitName" />
            <asp:BoundField DataField="ProductProductionLineName" HeaderText="خط الانتاج" SortExpression="ProductProductionLineName" />
            <asp:BoundField DataField="ProductionItemColorName" HeaderText="لون المنتج" SortExpression="ProductionItemColorName" />
            <asp:BoundField DataField="Qty" HeaderText="الكمية" SortExpression="Qty" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dbProducionOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT ItemContentHeader.ItemType, ProductionItemType.Name AS ProductionItemTypeName, ProductProductionLine.productionLineName AS ProductProductionLineName, ProductionItems.Name AS ProductionItemsName, ProductionItemColor.ColorName AS ProductionItemColorName, ItemContentHeader.Id, ProductionHeaderOrder.ProductionNo, ProductionHeaderOrder.ProductionDate, ProductionDetailsOrder.Status, ProductionDetailsOrder.Qty, ProductionItems.Code, ItemUnit.Name AS UnitName, ItemStatus.ItemStatus, SexData.Sex FROM ProductionHeaderOrder INNER JOIN ProductionDetailsOrder ON ProductionHeaderOrder.Id = ProductionDetailsOrder.ProductionHeaderOrderId INNER JOIN ItemColor AS ProductionItemColor INNER JOIN ItemContentHeader ON ProductionItemColor.Id = ItemContentHeader.ProductItemColor INNER JOIN Items AS ProductionItems INNER JOIN ItemType AS ProductionItemType ON ProductionItems.ItemTypeId = ProductionItemType.Id INNER JOIN ProductionLine AS ProductProductionLine ON ProductionItems.ProductionLineId = ProductProductionLine.Id ON ItemContentHeader.ItemType = ProductionItemType.Id AND ItemContentHeader.ProductItemId = ProductionItems.Id AND ItemContentHeader.ProductionLineId = ProductProductionLine.Id ON ProductionDetailsOrder.ContentHeaderId = ItemContentHeader.Id INNER JOIN SexData ON ProductionItems.Sex = SexData.Id INNER JOIN ItemStatus ON ProductionItems.ItemStatus = ItemStatus.Id INNER JOIN ItemUnit ON ProductionItems.ItemunitId = ItemUnit.Id"></asp:SqlDataSource>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
</asp:Content>
