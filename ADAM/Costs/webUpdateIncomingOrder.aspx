<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webUpdateIncomingOrder.aspx.cs" Inherits="ADAM.Costs.webUpdateIncomingOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة تسعير أوامر التوريد" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <%--<asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" />--%></td>
            <td></td>
            <td>
                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" />--%></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div id="divData" runat="server">
        <table style="width: 70%">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Text="رقم امر التوريد" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtSupplyOrderNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:Button ID="btnShowDiv" runat="server" Text="!!" OnClick="btnShowDiv_Click" />
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Text="تاريخ امر التوريد" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="txt" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label14" runat="server" Text="كود المورد" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtSupplierCode" TextMode="Number" runat="server" CssClass="txt" Enabled="False"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label15" runat="server" Text="المورد" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlSupplier" CssClass="ddl" runat="server" DataSourceID="dbSupplier" DataTextField="Name" DataValueField="Id" Width="200px" Enabled="False"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvSupplyOrder" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbSupplyOrder" Width="97%" DataKeyNames="IncommingOrderDataId" OnSelectedIndexChanged="gvSupplyOrder_SelectedIndexChanged" Visible="False">
            <Columns>
                <asp:BoundField DataField="PurchaseOrderNo" HeaderText="رقم طلب الشراء" SortExpression="PurchaseOrderNo" />
                <asp:BoundField DataField="PurchaseDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="تاريخ طلب الشراء" SortExpression="PurchaseDate" />
                <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
                <asp:BoundField DataField="ItemTypeName" HeaderText="المخزن" SortExpression="ItemTypeName" />
                <asp:BoundField DataField="ItemsCode" HeaderText="كود الصنف" SortExpression="ItemsCode" />
                <asp:BoundField DataField="ItemsName" HeaderText="الصنف" SortExpression="ItemsName" />
                <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
                <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
                <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
                <asp:BoundField DataField="AcceptQty" HeaderText="الكمية" SortExpression="AcceptQty" />
                <asp:BoundField DataField="AcceptfreeQty" HeaderText="Tester" SortExpression="AcceptfreeQty" />
                <asp:TemplateField HeaderText="سعر الصنف">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQtyPrice" runat="server" CssClass="txt" Width="75px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="سعر Tester">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFQtyPrice" runat="server" CssClass="txt" Width="75px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField SelectText="حفظ" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <br />
        <table>
            <tr>
                <td>
                    <asp:SqlDataSource ID="dbSupplyOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT AuditDetails.Id AS AuditDetailsId, AuditDetails.AcceptQty, AuditDetails.RefusedQty, AuditDetails.AcceptfreeQty, AuditDetails.RefusedFreeQty, AuditDetails.AuditDetailsDate, AuditDetails.RecDate, AuditDetails.Note AS AuditDetailsNote, RecordReceiptDetails.Id AS RecordReceiptDetailsId, RecordReceiptDetails.FreeQty, RecordReceiptDetails.Indoor, RecordReceiptDetails.Note AS RecordReceiptDetailsNote, SupplyOrderDetails.Id AS SupplyOrderDetailsId, SupplyOrderDetails.ItemPrice, SupplyOrderDetails.Note AS SupplyOrderDetailsNote, PurchaseOredrDetails.Id AS PurchaseOredrDetailsId, PurchaseOredrDetails.Note AS PurchaseOredrDetailsNote, PurchaseOrderHeader.Id AS PurchaseOrderHeaderId, PurchaseOrderHeader.PurchaseOrderNo, PurchaseOrderHeader.Note AS PurchaseOrderHeaderNote, PurchaseOrderHeader.RecoredDate, Items.Id AS ItemsId, Items.Code AS ItemsCode, Items.Name AS ItemsName, RecordReceiptDetails.QtyReceived, PurchaseOredrDetails.Qty, PurchaseOredrDetails.ConformQty, PurchaseOrderHeader.PurchaseDate, ProductionLine.Id AS ProductionLineId, ProductionLine.productionLineName, ItemType.Id AS ItemTypeId, ItemType.Name AS ItemTypeName, ItemStatus.ItemStatus, SexData.Sex, ItemColor.ColorName, IncommingOrderData.IncommingOrderNo, IncommingOrderData.ItemPrice AS IncommingOrderDataItemPrice, IncommingOrderData.FreeItemPrice AS IncommingOrderDataFreeItemPrice, IncommingOrderData.Id AS IncommingOrderDataId, SupplyOrderHeader.SupplyOrderNo FROM AuditDetails INNER JOIN RecordReceiptDetails ON AuditDetails.RecordReceiptDetailsId = RecordReceiptDetails.Id INNER JOIN SupplyOrderDetails ON RecordReceiptDetails.SupplyOrderDetailsId = SupplyOrderDetails.Id INNER JOIN PurchaseOredrDetails ON SupplyOrderDetails.PurchaseOrderDetailsId = PurchaseOredrDetails.Id INNER JOIN PurchaseOrderHeader ON PurchaseOredrDetails.PurchaseOredeHeaderId = PurchaseOrderHeader.Id INNER JOIN Items ON RecordReceiptDetails.ItemId = Items.Id AND PurchaseOredrDetails.ItemId = Items.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id INNER JOIN ItemColorSelected ON Items.Id = ItemColorSelected.ItemId AND ItemColor.Id = ItemColorSelected.ItemColorId INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ItemType ON PurchaseOrderHeader.ItemTypeId = ItemType.Id AND Items.ItemTypeId = ItemType.Id INNER JOIN ItemTypeProdcutionLine ON ProductionLine.Id = ItemTypeProdcutionLine.ProdctionLineId AND ItemType.Id = ItemTypeProdcutionLine.ItemTypeId INNER JOIN IncommingOrderData ON AuditDetails.Id = IncommingOrderData.AuditDetailsId AND RecordReceiptDetails.Id = IncommingOrderData.RecordReceiptDetailsId AND ItemType.Id = IncommingOrderData.ItemTypeId INNER JOIN SupplyOrderHeader ON SupplyOrderDetails.SupplyOrderHeaderId = SupplyOrderHeader.Id WHERE (SupplyOrderHeader.SupplyOrderNo = @SupplyOrderNo)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtSupplyOrderNo" DefaultValue="0" Name="SupplyOrderNo" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbSupplier" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '   ' + LastName AS Name FROM SupplierData"></asp:SqlDataSource>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
    <div id="divPurchseOrder" visible="false" runat="server">
        <asp:GridView ID="gvPurchseOrder" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbPurchseOrder" Width="97%" OnSelectedIndexChanged="gvPurchseOrder_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="PurchaseOrderNo" HeaderText="رقم طلب الشراء" SortExpression="PurchaseOrderNo" />
                <asp:BoundField DataField="PurchaseDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="تاريخ طلب الشراء" SortExpression="PurchaseDate" />
                <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
                <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
                <asp:BoundField DataField="ConformQty" HeaderText="الكمية المعتمدة" SortExpression="ConformQty" />
                <asp:BoundField DataField="SupplyOrderNo" HeaderText="رقم امر التوريد" SortExpression="SupplyOrderNo" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbPurchseOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT PurchaseOrderHeader.PurchaseOrderNo, PurchaseOrderHeader.PurchaseDate, Items.Name, ItemColor.ColorName, SupplyOrderHeader.SupplyOrderNo, PurchaseOredrDetails.ConformQty FROM PurchaseOrderHeader INNER JOIN PurchaseOredrDetails ON PurchaseOrderHeader.Id = PurchaseOredrDetails.PurchaseOredeHeaderId INNER JOIN SupplyOrderDetails ON PurchaseOredrDetails.Id = SupplyOrderDetails.PurchaseOrderDetailsId INNER JOIN SupplyOrderHeader ON SupplyOrderDetails.SupplyOrderHeaderId = SupplyOrderHeader.Id INNER JOIN Items ON PurchaseOredrDetails.ItemId = Items.Id INNER JOIN ItemColorSelected ON Items.Id = ItemColorSelected.ItemId INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id AND ItemColorSelected.ItemColorId = ItemColor.Id ORDER BY PurchaseOrderHeader.Id DESC"></asp:SqlDataSource>
    </div>
</asp:Content>
