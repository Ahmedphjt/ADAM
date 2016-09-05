<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webPurchaseOrderRepot.aspx.cs" Inherits="ADAM.PurchaseReport.webPurchaseOrderRepot" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير طلب شراء غير معتمد" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="رقم طلب الشراء" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPurchaseOrderNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
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
        <asp:GridView ID="gvPurchaseOrder" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbPurcahseOrder" Width="97%" DataKeyNames="PurchaseOrderNo" OnSelectedIndexChanged="gvPurchaseOrder_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="PurchaseOrderNo" HeaderText="رقم طلب الشراء" SortExpression="PurchaseOrderNo" />
            <asp:BoundField DataField="PurchaseDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="تاريخ الطلب" SortExpression="PurchaseDate" />
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="Qty" HeaderText="الكمية " SortExpression="Qty"/>
            <asp:BoundField DataField="ConformQty" HeaderText="ك معتمدة" SortExpression="ConformQty"/>
            <asp:BoundField DataField="ItemTypeName" HeaderText="المخزن" SortExpression="ItemTypeName" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="EmpName" HeaderText="مندوب الادارة" SortExpression="EmpName" ReadOnly="True" />
            <asp:BoundField DataField="SexName" HeaderText="النوع" SortExpression="SexName" />
            <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
            <asp:BoundField DataField="ItemStatusName" HeaderText="نوع المنتج" SortExpression="ItemStatusName" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
        <asp:SqlDataSource ID="dbPurcahseOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT PurchaseOrderHeader.PurchaseOrderNo, PurchaseOrderHeader.PurchaseDate, PurchaseOrderHeader.Note, PurchaseOredrDetails.Qty, PurchaseOredrDetails.ConformQty, Items.Code, Items.Name, ItemType.Name AS ItemTypeName, ItemUnit.Name AS ItemUnitName, Department.Name AS DepartmentName, EmployeeData.Code AS EmployeeDataCode, EmployeeData.FirstName + '  ' + EmployeeData.LastName AS EmpName, division.Name AS divisionName, PurchaseOredrDetails.Note AS PurchaseOredrDetailsNote, Items.Sex, Items.ItemStatus, PurchaseOredrDetails.Status, Department.Id AS DepartmentID, division.Id AS DivisionID, PurchaseOredrDetails.IsChecked, SexData.Sex AS SexName, ProductionLine.productionLineName, ItemStatus.ItemStatus AS ItemStatusName, ItemColor.ColorName FROM PurchaseOrderHeader INNER JOIN PurchaseOredrDetails ON PurchaseOrderHeader.Id = PurchaseOredrDetails.PurchaseOredeHeaderId INNER JOIN Items ON PurchaseOredrDetails.ItemId = Items.Id INNER JOIN ItemType ON PurchaseOrderHeader.ItemTypeId = ItemType.Id AND Items.ItemTypeId = ItemType.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN EmployeeData ON PurchaseOrderHeader.EmployeeId = EmployeeData.Id INNER JOIN Department ON PurchaseOrderHeader.DepartmentId = Department.Id INNER JOIN division ON PurchaseOrderHeader.DivisionId = division.Id AND Department.Id = division.DepartmentId INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id WHERE (PurchaseOredrDetails.Status = 1) ORDER BY PurchaseOrderHeader.PurchaseOrderNo DESC">
        </asp:SqlDataSource>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
    </div>
</asp:Content>
