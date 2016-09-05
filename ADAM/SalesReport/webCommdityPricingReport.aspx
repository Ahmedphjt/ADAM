<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webCommdityPricingReport.aspx.cs" Inherits="ADAM.SalesReport.webCommdityPricingReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير أذن صرف بضاعة مسعر" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="رقم طلب الصرف" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtExchangeOrder" CssClass="txt" runat="server"></asp:TextBox></td>
            <td></td>
            <td>
                <asp:DropDownList ID="ddlENo" Width="85px" CssClass="ddl" runat="server">
                    <asp:ListItem Text="---" Value="0"></asp:ListItem>
                    <asp:ListItem Text="بدون رقم" Value="1"></asp:ListItem>
                       <asp:ListItem Text="معارض" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="عرض التقرير" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            <td>
                
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvExchangePrice" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbExchangePrice" Width="97%" OnSelectedIndexChanged="gvExchangePrice_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="ExchangeRequestNo" HeaderText="رقم الطلب" SortExpression="ExchangeRequestNo" />
            <asp:BoundField DataField="ExchangeRequestDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="تاريخ الطلب" SortExpression="ExchangeRequestDate" />
            <asp:BoundField DataField="ItemName" HeaderText="الصنف" SortExpression="ItemName" />
            <asp:BoundField DataField="Qty" HeaderText="الكمية" SortExpression="Qty" />
            <asp:BoundField DataField="FreeQty" HeaderText="Tester" SortExpression="FreeQty" />
            <asp:BoundField DataField="Bounce" HeaderText="Bounce" SortExpression="Bounce" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ItemTypeName" HeaderText="المخزن" SortExpression="ItemTypeName" />
            <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemCode" HeaderText="كود الصنف" SortExpression="ItemCode" />
            <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dbExchangePrice" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT ExchangeRequestHeaderData.ExchangeRequestNo, ExchangeRequestHeaderData.ExchangeRequestDate, Items.Name AS ItemName, ExchangeRequestDetailsData.Qty, ExchangeRequestDetailsData.FreeQty, ExchangeRequestDetailsData.Bounce, ItemUnit.Name AS ItemUnitName, ItemColor.ColorName, ItemStatus.ItemStatus, ItemType.Name AS ItemTypeName, ProductionLine.productionLineName, SexData.Sex, division.Name AS divisionName, EmployeeData.Code AS EmpCode, EmployeeData.FirstName + ' ' + EmployeeData.LastName AS EmpName, ExchangeRequestHeaderData.OrderType, ExchangeRequestDetailsData.ExchangeRequestHeaderDataId, Items.Code AS ItemCode, ClientData.Code AS ClientCode, ClientData.FirstName + '  ' + ClientData.LastName AS ClientName, ClientData.FirstPhone, ClientData.FirstMobile, ClientData.Address FROM ExchangeRequestDetailsData INNER JOIN ExchangeRequestHeaderData ON ExchangeRequestDetailsData.ExchangeRequestHeaderDataId = ExchangeRequestHeaderData.Id INNER JOIN ExchangeRequestPricing ON ExchangeRequestDetailsData.Id = ExchangeRequestPricing.ExchangeRequestDetailsId INNER JOIN Items ON ExchangeRequestDetailsData.ItemId = Items.Id INNER JOIN ItemColor ON ExchangeRequestDetailsData.ItemColorId = ItemColor.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN ItemType ON Items.ItemTypeId = ItemType.Id AND ExchangeRequestDetailsData.ItemTypeId = ItemType.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN division ON ExchangeRequestHeaderData.DivisionId = division.Id INNER JOIN EmployeeData ON ExchangeRequestHeaderData.EmpId = EmployeeData.Id AND division.Id = EmployeeData.DivisionId INNER JOIN ClientData ON ExchangeRequestHeaderData.ClientId = ClientData.Id WHERE (ExchangeRequestHeaderData.OrderType = 9)"></asp:SqlDataSource>

    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>

