<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webItemContentData.aspx.cs" Inherits="ADAM.Prodction.webItemContentData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة التركيبات" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
               <%-- <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" />--%></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="مخزن المنتج"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionItemType" CssClass="ddl" runat="server" DataSourceID="dbProductionItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="خط انتاج المنتج"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductProductionLine" CssClass="ddl" runat="server" AutoPostBack="True" DataSourceID="dbPrductProductionLine" DataTextField="productionLineName" DataValueField="Id"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" Text="المنتج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionItem" CssClass="ddl" runat="server" AutoPostBack="True" DataSourceID="dbProduct" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label6" runat="server" Text="لون المنتج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionItemcolor" CssClass="ddl" runat="server" DataSourceID="dbProductItemColor" DataTextField="ColorName" DataValueField="Id"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="مخزن الخامات"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemTpe" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="خط الانتاج للخامات"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionLine" CssClass="ddl" runat="server" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvItemContentData" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbItemContenData">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="Qty" HeaderText="الكمية" SortExpression="Qty" />
            <asp:BoundField DataField="ItemTypeName" HeaderText="المخزن" SortExpression="ItemTypeName" />
            <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:GridView ID="gvItemContent" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbItemContent" DataKeyNames="ItemsId,ItemColorId" OnSelectedIndexChanged="gvItemContent_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="ColorName" HeaderText="لون الصنف" SortExpression="ColorName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:TemplateField HeaderText="الكمية">
                <ItemTemplate>
                    <asp:TextBox ID="txtQty" CssClass="txt" Width="100px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField SelectText="ادخال" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbProductionItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbPrductProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProduct" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items WHERE (ItemTypeId = @ItemTypeId) AND (ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlProductionItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ColorName UNION SELECT ItemColor.Id, ItemColor.ColorName FROM ItemColor INNER JOIN ItemColorSelected ON ItemColor.Id = ItemColorSelected.ItemColorId WHERE (ItemColorSelected.ItemId = @ItemId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlProductionItem" DefaultValue="0" Name="ItemId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemContenData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemColor.ColorName, ItemStatus.ItemStatus, SexData.Sex, ItemUnit.Name AS ItemUnitName, ItemContentDetails.Qty, ItemContentDetails.ItemContentHeaderId, ProductionLine.productionLineName, ItemType.Name AS ItemTypeName FROM ItemContentDetails INNER JOIN Items INNER JOIN ItemColorSelected ON Items.Id = ItemColorSelected.ItemId INNER JOIN ItemColor ON ItemColorSelected.ItemColorId = ItemColor.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id ON ItemContentDetails.ItemId = Items.Id AND ItemContentDetails.ItemColorId = ItemColor.Id INNER JOIN ItemType ON Items.ItemTypeId = ItemType.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id WHERE (ItemContentDetails.ItemContentHeaderId = @ItemContentHeaderId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfItemContentHeaderId" DefaultValue="0" Name="ItemContentHeaderId" PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItemTpe" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemContent" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemColor.ColorName, ItemStatus.ItemStatus, SexData.Sex, ItemUnit.Name AS ItemUnitName, Items.ItemTypeId, Items.ProductionLineId, Items.Id AS ItemsId, ItemColor.Id AS ItemColorId FROM Items INNER JOIN ItemColorSelected ON Items.Id = ItemColorSelected.ItemId INNER JOIN ItemColor ON ItemColorSelected.ItemColorId = ItemColor.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id WHERE (Items.ItemTypeId = @ItemTypeId) AND (Items.ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:HiddenField ID="hfItemContentHeaderId" Value="0" runat="server" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
