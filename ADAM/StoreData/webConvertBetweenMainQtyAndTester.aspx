<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webConvertBetweenMainQtyAndTester.aspx.cs" Inherits="ADAM.StoreData.webConvertBetweenMainQtyAndTester" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة تحويل الارصدة" Font-Size="X-Large" Font-Underline="True"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label5" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="ddl" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label6" runat="server" Text="خط أنتاج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProdctionLine" runat="server" CssClass="ddl" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvBalance" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbBalance" OnSelectedIndexChanged="gvBalance_SelectedIndexChanged" DataKeyNames="ItemMovementId">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="CurrentQty" HeaderText="الرصيد الحالي" SortExpression="CurrentQty" ReadOnly="True" />
            <asp:BoundField DataField="Tester" HeaderText="Tester" ReadOnly="True" SortExpression="Tester" />
            <asp:BoundField DataField="IncommingOrderNo" HeaderText="رقم الوارد" SortExpression="IncommingOrderNo" />
            <asp:TemplateField HeaderText="الفعلي المحول">
                <ItemTemplate>
                    <asp:TextBox ID="txtQty" runat="server" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tester محول">
                <ItemTemplate>
                    <asp:TextBox ID="txtTester" runat="server" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField SelectText="تحويل" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dbBalance" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS ItemUnitName, Items.LimitQty, SexData.Sex, ItemStatus.ItemStatus, ItemColor.ColorName, Items.Id AS ItemsId, ItemColor.Id AS ItemColorId, ItemMovement.MovmentnameId, ItemMovement.MainQtyOut, ItemMovement.AdditionalQtyOut, ItemMovement.MainQty - ItemMovement.MainQtyOut AS CurrentQty, ItemMovement.AdditionalQty - ItemMovement.AdditionalQtyOut AS Tester, ItemMovement.IncommingOrderNo, ItemMovement.Id AS ItemMovementId FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ItemColorSelected ON Items.Id = ItemColorSelected.ItemId INNER JOIN ItemColor ON ItemColorSelected.ItemColorId = ItemColor.Id INNER JOIN ItemMovement ON Items.Id = ItemMovement.ItemId AND ItemUnit.Id = ItemMovement.ItemUnitId AND ItemColor.Id = ItemMovement.ItemColorId WHERE (ItemMovement.MovmentnameId = 3 OR  ItemMovement.MovmentnameId = 20 OR ItemMovement.MovmentnameId = 6 OR ItemMovement.MovmentnameId = 16) AND (Items.Id = @ItemsId) AND (ItemColor.Id = @ItemColorId)">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfItemId" DefaultValue="0" Name="ItemsId" PropertyName="Value" />
            <asp:ControlParameter ControlID="hfItemColorId" DefaultValue="0" Name="ItemColorId" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbItems" DataKeyNames="ItemsId,ItemColorId" OnRowDataBound="gvItems_RowDataBound" OnSelectedIndexChanged="gvItems_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="LimitQty" HeaderText="الحد الادني" SortExpression="LimitQty" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ColorName" HeaderText="لون المنتج" SortExpression="ColorName" />
            <asp:TemplateField HeaderText="الرصيد الحالي"></asp:TemplateField>
            <asp:TemplateField HeaderText="Tester"></asp:TemplateField>
            <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <br />

    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItems" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS ItemUnitName, Items.LimitQty, SexData.Sex, ItemStatus.ItemStatus, ItemColor.ColorName, Items.Id AS ItemsId, ItemColor.Id AS ItemColorId FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ItemColorSelected ON Items.Id = ItemColorSelected.ItemId INNER JOIN ItemColor ON ItemColorSelected.ItemColorId = ItemColor.Id WHERE (Items.ItemTypeId = @ItemTypeId) AND (Items.ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProdctionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:HiddenField ID="hfItemId" Value="0" runat="server" />
            </td>
            <td>
                <asp:HiddenField ID="hfItemColorId" Value="0" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
