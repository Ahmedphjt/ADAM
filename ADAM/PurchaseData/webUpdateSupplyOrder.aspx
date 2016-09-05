<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webUpdateSupplyOrder.aspx.cs" Inherits="ADAM.PurchaseData.webUpdateSupplyOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة انشاء طلب التوريد" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
            <td></td>
            <td>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" Text="رقم امر التوريد" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtSupplyOrderNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox></td>
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
    <asp:GridView ID="gvSupplyOrder" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbSupplyOrder" Width="97%" DataKeyNames="Id" OnRowDataBound="gvSupplyOrder_RowDataBound" OnSelectedIndexChanged="gvSupplyOrder_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="PurchaseOrderNo" HeaderText="رقم طلب الشراء" SortExpression="PurchaseOrderNo" />
            <asp:BoundField DataField="PurchaseDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="تاريخ الطلب" SortExpression="PurchaseDate" />
            <asp:BoundField DataField="ItemsCode" HeaderText="كود الصنف" SortExpression="ItemsCode" />
            <asp:BoundField DataField="ItemsName" HeaderText="الصنف" SortExpression="ItemsName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex"/>
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus"/>
            <asp:BoundField DataField="ConformQty" HeaderText="الكمية المقبولة" SortExpression="ConformQty" />
            <asp:BoundField DataField="ItemTypeName" HeaderText="المخزن" SortExpression="ItemTypeName" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة " SortExpression="ItemUnitName" />
            <asp:BoundField DataField="PurchaseItemStatus" HeaderText="حالة الطلب" SortExpression="PurchaseItemStatus" />
            <asp:BoundField DataField="ColorName" HeaderText="لون الصنف" SortExpression="ColorName" />
            <asp:TemplateField HeaderText="تسعير الصنف">
                <ItemTemplate>
                    <asp:TextBox ID="txtItemPrice" runat="server" Width="75px" TextMode="Number"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField HeaderText="حذف" SelectImageUrl="~/Image/Delete.png" SelectText="حذف" ShowSelectButton="True">
                <ControlStyle ForeColor="#FF0066" />
                <FooterStyle ForeColor="#FF0066" />
                <ItemStyle ForeColor="#FF0066" />
            </asp:CommandField>
        </Columns>
    </asp:GridView>
    <br />
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbSupplyOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT PurchaseOrderHeader.PurchaseOrderNo, PurchaseOrderHeader.PurchaseDate, Items.Code AS ItemsCode, Items.Name AS ItemsName, PurchaseOredrDetails.ConformQty, ItemType.Name AS ItemTypeName, ItemUnit.Name AS ItemUnitName, PurchaseItemStatus.PurchaseItemStatus, SupplyOrderHeader.SupplyOrderNo, SupplyOrderDetails.Id, ItemColor.ColorName, ItemStatus.ItemStatus, SexData.Sex FROM PurchaseOrderHeader INNER JOIN PurchaseOredrDetails ON PurchaseOrderHeader.Id = PurchaseOredrDetails.PurchaseOredeHeaderId INNER JOIN Items ON PurchaseOredrDetails.ItemId = Items.Id INNER JOIN ItemType ON PurchaseOrderHeader.ItemTypeId = ItemType.Id AND Items.ItemTypeId = ItemType.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN PurchaseItemStatus ON PurchaseOredrDetails.Status = PurchaseItemStatus.Id INNER JOIN SupplyOrderDetails ON PurchaseOredrDetails.Id = SupplyOrderDetails.PurchaseOrderDetailsId INNER JOIN SupplyOrderHeader ON SupplyOrderDetails.SupplyOrderHeaderId = SupplyOrderHeader.Id INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN SexData ON Items.Sex = SexData.Id WHERE (SupplyOrderHeader.SupplyOrderNo = @SupplyOrderNo)">
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
</asp:Content>
