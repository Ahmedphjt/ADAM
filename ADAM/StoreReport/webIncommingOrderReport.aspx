<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webIncommingOrderReport.aspx.cs" Inherits="ADAM.StoreReport.webIncommingOrderReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير اذن الوارد" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="رقم محضر الاستلام" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRecordReceiptNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label2" runat="server" Text="رقم أخطار الفحص" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAuditNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label4" runat="server" Text="رقم أذن الوارد" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIncommingOrderNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
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

        <asp:GridView ID="gvIncomingOrder" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbAudit" Width="97%" DataKeyNames="RecordReceiptNo,AuditNo,IncommingOrderNo" OnSelectedIndexChanged="gvAudit_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="RecordReceiptNo" HeaderText="م الاستلام" SortExpression="RecordReceiptNo" />
                <asp:BoundField DataField="RecordReceiptDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="ت م الاستلام" SortExpression="RecordReceiptDate" />
                <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
                <asp:BoundField DataField="ItemName" HeaderText="الصنف" SortExpression="ItemName" />
                <asp:BoundField DataField="ItemTypeName" HeaderText="المخزن" SortExpression="ItemTypeName" />
                <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
                <asp:BoundField DataField="AuditNo" HeaderText="رقم الاخطار" SortExpression="AuditNo" />
                <asp:BoundField DataField="AuditDate" HeaderText="ت أخطار الفحص" SortExpression="AuditDate" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
                <asp:BoundField DataField="IncommingOrderNo" HeaderText="رقم اذن الوارد" SortExpression="IncommingOrderNo" />
                <asp:BoundField DataField="LocationName" HeaderText="Location" SortExpression="LocationName" />
                <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbAudit" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT RecordReceiptHeader.RecordReceiptNo, RecordReceiptHeader.RecordReceiptDate, Items.Code, Items.Name AS ItemName, RecordReceiptDetails.QtyReceived, RecordReceiptDetails.Indoor, RecordReceiptDetails.Note, ItemType.Name AS ItemTypeName, ItemUnit.Name AS ItemUnitName, Department.Name AS DepName, AuditHeader.AuditNo, AuditHeader.AuditDate, PurchaseOrderHeader.PurchaseOrderNo, ItemStatus.ItemStatus, ItemColor.ColorName, SexData.Sex, RecordReceiptDetails.FreeQty, AuditDetails.EmployeeId, AuditDetails.AcceptQty, AuditDetails.RefusedQty, AuditDetails.AcceptfreeQty, AuditDetails.RefusedFreeQty, AuditDetails.AuditDetailsDate, EmployeeData.FirstName + ' ' + EmployeeData.LastName AS EmpName, IncommingOrderData.IncommingOrderNo, IncommingOrderData.ItemPrice, IncommingOrderData.FreeItemPrice, IncommingOrderData.LocationId, ItemLocation.LocationName, ProductionLine.productionLineName FROM RecordReceiptDetails INNER JOIN RecordReceiptHeader ON RecordReceiptDetails.RecordReceiptHeaderId = RecordReceiptHeader.Id INNER JOIN Items ON RecordReceiptDetails.ItemId = Items.Id INNER JOIN ItemType ON Items.ItemTypeId = ItemType.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SupplyOrderDetails ON RecordReceiptDetails.SupplyOrderDetailsId = SupplyOrderDetails.Id INNER JOIN PurchaseOredrDetails ON Items.Id = PurchaseOredrDetails.ItemId AND SupplyOrderDetails.PurchaseOrderDetailsId = PurchaseOredrDetails.Id INNER JOIN PurchaseOrderHeader ON ItemType.Id = PurchaseOrderHeader.ItemTypeId AND PurchaseOredrDetails.PurchaseOredeHeaderId = PurchaseOrderHeader.Id INNER JOIN Department ON PurchaseOrderHeader.DepartmentId = Department.Id INNER JOIN AuditDetails ON RecordReceiptDetails.Id = AuditDetails.RecordReceiptDetailsId INNER JOIN AuditHeader ON AuditDetails.AuditHeaderId = AuditHeader.Id INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN EmployeeData ON Department.Id = EmployeeData.DepartmentId INNER JOIN IncommingOrderData ON RecordReceiptDetails.Id = IncommingOrderData.RecordReceiptDetailsId AND ItemType.Id = IncommingOrderData.ItemTypeId AND AuditDetails.Id = IncommingOrderData.AuditDetailsId INNER JOIN ItemLocation ON ItemType.Id = ItemLocation.ItemTypeId AND IncommingOrderData.LocationId = ItemLocation.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id ORDER BY IncommingOrderData.IncommingOrderNo DESC"></asp:SqlDataSource>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
    </div>
</asp:Content>
