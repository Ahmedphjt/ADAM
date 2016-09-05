<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webUpdateFirstTimeBalance.aspx.cs" Inherits="ADAM.StoreData.webUpdateFirstTimeBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة تعديل رصيد اول مدة" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="نوع الصنف"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="خط انتاج"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlProdctionLine" CssClass="ddl" runat="server" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="نوع الاضافة"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemMovementName" CssClass="ddl" runat="server" AutoPostBack="True">
                    <asp:ListItem Text="---" Value="0"></asp:ListItem>
                    <asp:ListItem Text="ادخال رصيد اول مدة" Value="6"></asp:ListItem>
                    <asp:ListItem Text="أضافة لأاسباب أخري" Value="20"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvItems" CssClass="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemMovementId" DataSourceID="dbgvItems" OnRowDataBound="gvItems_RowDataBound" OnSelectedIndexChanged="gvItems_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="وحدة الصنف" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="LimitQty" HeaderText="حد الطلب" SortExpression="LimitQty" />
            <asp:BoundField DataField="SexName" HeaderText="النوع" SortExpression="SexName" />
            <asp:BoundField DataField="ItemStatusName" HeaderText="نوع المنتج" SortExpression="ItemStatusName" />
            <asp:BoundField DataField="Specification" HeaderText="الوصف" SortExpression="Specification" />
            <asp:BoundField DataField="Note" HeaderText="ملاحظات" SortExpression="Note" />
            <asp:TemplateField HeaderText="لون الصنف">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlItemColor" runat="server" Width="105px" CssClass="ddl" DataSourceID="dbItemColor" DataTextField="ColorName" DataValueField="Id">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="الكمية">
                <ItemTemplate>
                    <asp:TextBox ID="txtMainQty" runat="server" Width="75px" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tester">
                <ItemTemplate>
                    <asp:TextBox ID="txtAdditionalQty" Width="75px" CssClass="txt" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ddl" Width="105px" DataSourceID="dbLocation" DataTextField="LocationName" DataValueField="Id">
                    </asp:DropDownList>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField SelectText="حفظ" ShowSelectButton="True">
                <ControlStyle Font-Size="Large" ForeColor="#CCCC00" />
            </asp:CommandField>
        </Columns>
    </asp:GridView>
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbgvItems" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS ItemUnitName, Items.LimitQty, Items.Sex, Items.ItemStatus, Items.Specification, Items.Note, Items.ItemTypeId, ItemMovement.MovmentnameId, ItemColor.ColorName, ItemMovement.Id AS ItemMovementId, SexData.Sex AS SexName, ItemStatus.ItemStatus AS ItemStatusName, ProductionLine.Id AS ProductionLineId, ProductionLine.productionLineName FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN ItemMovement ON Items.Id = ItemMovement.ItemId AND ItemUnit.Id = ItemMovement.ItemUnitId INNER JOIN ItemColor ON ItemMovement.ItemColorId = ItemColor.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id WHERE (Items.ItemTypeId = @ItemTypeId) AND (ItemMovement.MovmentnameId = @MovmentnameId) AND (ProductionLine.Id = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlItemMovementName" DefaultValue="0" Name="MovmentnameId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProdctionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>

                <asp:SqlDataSource ID="dbItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ColorName UNION SELECT Id, ColorName FROM ItemColor"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbLocation" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS LocationName UNION SELECT Id, LocationName FROM ItemLocation WHERE (ItemTypeId = @ItemTypeId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
