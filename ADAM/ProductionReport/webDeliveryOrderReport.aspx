<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webDeliveryOrderReport.aspx.cs" Inherits="ADAM.ProductionReport.webDeliveryOrderReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير طلب تسليم منتج تام" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
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
    <asp:GridView ID="gvDeliveryData" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbDeliveryData" OnSelectedIndexChanged="gvPurchaseDetailsData_SelectedIndexChanged" DataKeyNames="DeliveryDataDetailsId">
        <Columns>
            <asp:BoundField DataField="DeliveryNo" HeaderText="رقم الطلب" SortExpression="DeliveryNo" />
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="UnitName" HeaderText="الوحدة" SortExpression="UnitName" />
            <asp:BoundField DataField="LimitQty" HeaderText="الحد الاقصي" SortExpression="LimitQty" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="Specification" HeaderText="الوصف" SortExpression="Specification" />
            <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
            <asp:BoundField DataField="Qty" HeaderText="الكمية" SortExpression="Qty" />
            <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dbDeliveryData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS UnitName, Items.LimitQty, SexData.Sex, ItemStatus.ItemStatus, ItemColor.ColorName, Items.Specification, ProductionLine.productionLineName, DeliveryDataDetails.Qty, DeliveryDataDetails.Id AS DeliveryDataDetailsId, DeliveryDataDetails.DeliveryDataHeaderId, DeliveryDataDetails.Status, DeliveryDataHeader.DeliveryNo FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN DeliveryDataDetails ON Items.Id = DeliveryDataDetails.ItemId INNER JOIN ItemColor ON DeliveryDataDetails.ItemColorId = ItemColor.Id INNER JOIN DeliveryDataHeader ON ProductionLine.Id = DeliveryDataHeader.ProductionLineId AND DeliveryDataDetails.DeliveryDataHeaderId = DeliveryDataHeader.Id WHERE (DeliveryDataDetails.Status = 0) ORDER BY DeliveryDataHeader.DeliveryNo DESC"></asp:SqlDataSource>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
</asp:Content>
