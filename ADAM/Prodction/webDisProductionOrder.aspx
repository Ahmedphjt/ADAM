<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webDisProductionOrder.aspx.cs" Inherits="ADAM.Prodction.webDisProductionOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة صرف أمر أنتاج" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <%-- <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" />--%></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="تخصيم" OnClick="btnSave_Click"/></td>
            <td>
                <%--   <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" />--%></td>
            <td>
                <%--  <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" />--%></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" Text="رقم أمر الانتاج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtOrderNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox></td>
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" Text="تاريخ أمر الانتاج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
        <asp:GridView ID="gvItemContent" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbItemContentDetails" DataKeyNames="ItemMovementId,ProductionDetailsOrderId" OnSelectedIndexChanged="gvItemContent_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="ItemTypeName" HeaderText="المخزن" SortExpression="ItemTypeName" />
            <asp:BoundField DataField="ItemsCode" HeaderText="كود الخامه" SortExpression="ItemsCode" />
            <asp:BoundField DataField="ItemsName" HeaderText="الخامة" SortExpression="ItemsName" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="ItemContentDetailsQty" HeaderText="كمية الخامة" SortExpression="ItemContentDetailsQty" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="UnitName" HeaderText="الوحدة" SortExpression="UnitName" />
            <asp:BoundField DataField="AllQty" HeaderText="الكمية الفعلية المصروفة" SortExpression="AllQty" />
            <asp:BoundField DataField="RMainQty" HeaderText="الرصيد الفعلي" SortExpression="RMainQty" />
            <asp:BoundField DataField="tester" HeaderText="tester" SortExpression="tester" />
            <asp:BoundField DataField="IncommingOrderNo" HeaderText="أذن الوارد" SortExpression="IncommingOrderNo" />
            <asp:TemplateField HeaderText="أختيار">
                <ItemTemplate>
                    <asp:CheckBox ID="chkChoose" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:GridView ID="gvProducionOrder" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbProducionOrder" OnSelectedIndexChanged="gvPurchaseDetailsData_SelectedIndexChanged" DataKeyNames="ProductionNo">
        <Columns>
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
    <table>
        <tr>
            <td>

    <asp:HiddenField ID="hfProductionNo" Value="0" runat="server" />
                <asp:HiddenField ID="hfProductionDetailsOrderId" Value="0" runat="server" />

            </td>
            <td>
    <asp:SqlDataSource ID="dbProducionOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT ItemContentHeader.ItemType, ProductionItemType.Name AS ProductionItemTypeName, ProductProductionLine.productionLineName AS ProductProductionLineName, ProductionItems.Name AS ProductionItemsName, ProductionItemColor.ColorName AS ProductionItemColorName, ItemContentHeader.Id, ProductionHeaderOrder.ProductionNo, ProductionHeaderOrder.ProductionDate, ProductionDetailsOrder.Status, ProductionDetailsOrder.Qty, ProductionItems.Code, ItemUnit.Name AS UnitName, ItemStatus.ItemStatus, SexData.Sex, ProductionHeaderOrder.Id AS Expr1 FROM ProductionHeaderOrder INNER JOIN ProductionDetailsOrder ON ProductionHeaderOrder.Id = ProductionDetailsOrder.ProductionHeaderOrderId INNER JOIN ItemColor AS ProductionItemColor INNER JOIN ItemContentHeader ON ProductionItemColor.Id = ItemContentHeader.ProductItemColor INNER JOIN Items AS ProductionItems INNER JOIN ItemType AS ProductionItemType ON ProductionItems.ItemTypeId = ProductionItemType.Id INNER JOIN ProductionLine AS ProductProductionLine ON ProductionItems.ProductionLineId = ProductProductionLine.Id ON ItemContentHeader.ItemType = ProductionItemType.Id AND ItemContentHeader.ProductItemId = ProductionItems.Id AND ItemContentHeader.ProductionLineId = ProductProductionLine.Id ON ProductionDetailsOrder.ContentHeaderId = ItemContentHeader.Id INNER JOIN SexData ON ProductionItems.Sex = SexData.Id INNER JOIN ItemStatus ON ProductionItems.ItemStatus = ItemStatus.Id INNER JOIN ItemUnit ON ProductionItems.ItemunitId = ItemUnit.Id WHERE (ProductionHeaderOrder.ProductionNo = @ProductionNo)">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtOrderNo" DefaultValue="0" Name="ProductionNo" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

            </td>
            <td>
    <asp:SqlDataSource ID="dbItemContentDetails" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT ItemContentHeader.ItemType, ItemContentHeader.Id AS ItemContentHeaderId, ProductionDetailsOrder.Status AS ProductionDetailsOrderStatus, ProductionDetailsOrder.Qty, ItemType.Name AS ItemTypeName, Items.Code AS ItemsCode, Items.Name AS ItemsName, ItemColor.ColorName, ItemContentDetails.Qty AS ItemContentDetailsQty, ItemContentDetails.Id AS ItemContentDetailsId, ProductionHeaderOrder.ProductionNo, Items.ItemTypeId, ItemStatus.ItemStatus, SexData.Sex, ItemUnit.Name AS UnitName, ProductionDetailsOrder.Qty * ItemContentDetails.Qty AS AllQty, Items.Id AS ItemId, ItemMovement.MovementDate, ItemMovement.MainQty - ItemMovement.MainQtyOut AS RMainQty, ItemMovement.AdditionalQty - ItemMovement.AdditionalQtyOut AS tester, ItemMovement.MovmentnameId, ItemMovement.IncommingOrderNo, ItemMovement.Id AS ItemMovementId, ProductionDetailsOrder.Id AS ProductionDetailsOrderId FROM ProductionDetailsOrder INNER JOIN ItemContentHeader ON ProductionDetailsOrder.ContentHeaderId = ItemContentHeader.Id INNER JOIN ItemContentDetails ON ItemContentHeader.Id = ItemContentDetails.ItemContentHeaderId INNER JOIN ItemType ON ItemContentDetails.ItemTypeId = ItemType.Id INNER JOIN ProductionLine ON ItemContentDetails.ProductionLineId = ProductionLine.Id INNER JOIN Items ON ItemContentDetails.ItemId = Items.Id INNER JOIN ItemColor ON ItemContentDetails.ItemColorId = ItemColor.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ProductionHeaderOrder ON ProductionDetailsOrder.ProductionHeaderOrderId = ProductionHeaderOrder.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN ItemMovement ON Items.Id = ItemMovement.ItemId AND ItemColor.Id = ItemMovement.ItemColorId AND ItemUnit.Id = ItemMovement.ItemUnitId WHERE (ProductionHeaderOrder.ProductionNo = @ProductionNo) AND (ItemMovement.MovmentnameId = 3 OR ItemMovement.MovmentnameId = 6 OR ItemMovement.MovmentnameId = 14 OR ItemMovement.MovmentnameId = 16 OR ItemMovement.MovmentnameId = 20)">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfProductionNo" DefaultValue="0" Name="ProductionNo" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
