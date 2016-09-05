<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webProductionOrder.aspx.cs" Inherits="ADAM.Prodction.webProductionOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة انشاء أمر أنتاج" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" /></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
            <td>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
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
    <asp:GridView ID="gvProductionOrderData" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbProductionOrder" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="ProductionItemTypeName" HeaderText="المخزن" SortExpression="ProductionItemTypeName" />
            <asp:BoundField DataField="ProductProductionLineName" HeaderText="خط الانتاج" SortExpression="ProductProductionLineName" />
            <asp:BoundField DataField="ProductionItemsName" HeaderText="المستحضر" SortExpression="ProductionItemsName" />
            <asp:BoundField DataField="ProductionItemColorName" HeaderText="لون المستحضر" SortExpression="ProductionItemColorName" />
            <asp:TemplateField HeaderText="الكمية">
                <ItemTemplate>
                    <asp:TextBox ID="txtQty" CssClass="txt" Width="100px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HiddenField ID="hfHeaderId" Value="0" runat="server" />
    <asp:SqlDataSource ID="dbProductionOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT ItemContentHeader.ItemType, ProductionItemType.Name AS ProductionItemTypeName, ProductProductionLine.productionLineName AS ProductProductionLineName, ProductionItems.Name AS ProductionItemsName, ProductionItemColor.ColorName AS ProductionItemColorName, ItemContentHeader.Id FROM ItemColor AS ProductionItemColor INNER JOIN ItemContentHeader ON ProductionItemColor.Id = ItemContentHeader.ProductItemColor INNER JOIN Items AS ProductionItems INNER JOIN ItemType AS ProductionItemType ON ProductionItems.ItemTypeId = ProductionItemType.Id INNER JOIN ProductionLine AS ProductProductionLine ON ProductionItems.ProductionLineId = ProductProductionLine.Id ON ItemContentHeader.ItemType = ProductionItemType.Id AND ItemContentHeader.ProductItemId = ProductionItems.Id AND ItemContentHeader.ProductionLineId = ProductProductionLine.Id"></asp:SqlDataSource>
</asp:Content>
