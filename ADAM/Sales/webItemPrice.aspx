<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webItemPrice.aspx.cs" Inherits="ADAM.Sales.webItemPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="table">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label5" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="ddl" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" Text="خط الانتاج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionLine" runat="server" CssClass="ddl" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvItemPrice" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbItemPrice" OnSelectedIndexChanged="gvItemPrice_SelectedIndexChanged" DataKeyNames="ItemColorSelectedId" OnRowDataBound="gvItemPrice_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:TemplateField HeaderText="س.الجملة">
                <ItemTemplate>
                    <asp:TextBox ID="txtMainClausePrice" Width="63px" CssClass="txt" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="س.البيع">
                <ItemTemplate>
                    <asp:TextBox ID="txtMainSalesPrice" Width="63px" CssClass="txt" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="س.المعارض">
                <ItemTemplate>
                    <asp:TextBox ID="txtMainShowsPrice" Width="63px" CssClass="txt" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="س.الجملة T">
                <ItemTemplate>
                    <asp:TextBox ID="txtTesterClausePrice" Width="63px" CssClass="txt" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="س.البيع T">
                <ItemTemplate>
                    <asp:TextBox ID="txtTesterSalesPrice" Width="63px" CssClass="txt" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="س.المعارض T">
                <ItemTemplate>
                    <asp:TextBox ID="txtTesterShowsPrice" Width="63px" CssClass="txt" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField SelectText="حفظ" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItemPrice" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemColor.ColorName, ItemUnit.Name AS ItemUnitName, ItemStatus.ItemStatus, SexData.Sex, Items.ProductionLineId, ItemColorSelected.Id AS ItemColorSelectedId FROM ItemColorSelected INNER JOIN ItemColor ON ItemColorSelected.ItemColorId = ItemColor.Id INNER JOIN Items ON ItemColorSelected.ItemId = Items.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN SexData ON Items.Sex = SexData.Id WHERE (Items.ItemTypeId = @ItemTypeId) AND (Items.ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT ProductionLine.Id, ProductionLine.productionLineName FROM ProductionLine INNER JOIN ItemTypeProdcutionLine ON ProductionLine.Id = ItemTypeProdcutionLine.ProdctionLineId WHERE (ItemTypeProdcutionLine.ItemTypeId = @ItemTypeId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
