<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webSupplyOrderData.aspx.cs" Inherits="ADAM.PurchaseData.webSupplyOrderData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة انشاء طلب التوريد" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td></td>
            <td>&nbsp;</td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
            <td></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" Text="رقم امر التوريد" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtSupplyOrderNo" TextMode="Number" runat="server" CssClass="txt" Enabled="False"></asp:TextBox></td>
            <td style="text-align: center">
                <asp:Label ID="Label1" runat="server" Text="تاريخ امر التوريد" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label2" runat="server" Text="رقم طلب الشراء" CssClass="lbl"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:TextBox ID="txtPurchaseOrderNo" TextMode="Number" runat="server" CssClass="txt" OnTextChanged="txtPurchaseOrderNo_TextChanged" AutoPostBack="True" ToolTip="ادخل رقم طلب الشراء"></asp:TextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label4" runat="server" Text="تاريخ طلب الشراء" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPurchaseOrderDate" Enabled="false" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label14" runat="server" Text="كود المورد" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtSupplierCode" TextMode="Number" runat="server" CssClass="txt" AutoPostBack="True" OnTextChanged="txtSupplierCode_TextChanged"></asp:TextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label15" runat="server" Text="المورد" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlSupplier" CssClass="ddl" runat="server" DataSourceID="dbSupplier" DataTextField="Name" DataValueField="Id" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label5" runat="server" Text="مركز التكلفة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlHeaderCostCenter" CssClass="ddl" runat="server" DataSourceID="dbHeaderCostCenter" DataTextField="CostCenter" DataValueField="id">
                </asp:DropDownList>
            </td>
            <td style="text-align: left" colspan="2">
                <asp:SqlDataSource ID="dbHeaderCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS id, '---' AS CostCenter UNION SELECT Id, CostCenterName AS CostCenter FROM CostCenter WHERE (CostCenterType = 1 or Id = 1)"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvSupplyOrder" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbSupplyOrder" Width="97%" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="PurchaseOrderNo" HeaderText="رقم طلب الشراء" SortExpression="PurchaseOrderNo" />
            <asp:BoundField DataField="PurchaseDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="تاريخ الطلب" SortExpression="PurchaseDate" />
            <asp:BoundField DataField="ItemsCode" HeaderText="كود الصنف" SortExpression="ItemsCode" />
            <asp:BoundField DataField="ItemsName" HeaderText="الصنف" SortExpression="ItemsName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ConformQty" HeaderText="الكمية" SortExpression="ConformQty" />
            <asp:BoundField DataField="ItemTypeName" HeaderText="المخزن" SortExpression="ItemTypeName" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="PurchaseItemStatus" HeaderText="حالة الطلب" SortExpression="PurchaseItemStatus" />
            <asp:BoundField DataField="ColorName" HeaderText="لون الصنف" SortExpression="ColorName" />
            <asp:TemplateField HeaderText="ملاحظات">
                <ItemTemplate>
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="تسعير الصنف">
                <ItemTemplate>
                    <asp:TextBox ID="txtItemPrice" runat="server" Width="75px" TextMode="Number"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="أختيار">
                <ItemTemplate>
                    <asp:CheckBox ID="chkChoose" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbSupplyOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT PurchaseOrderHeader.PurchaseOrderNo, PurchaseOrderHeader.PurchaseDate, Items.Code AS ItemsCode, Items.Name AS ItemsName, PurchaseOredrDetails.ConformQty, ItemType.Name AS ItemTypeName, ItemUnit.Name AS ItemUnitName, PurchaseItemStatus.PurchaseItemStatus, PurchaseOredrDetails.Id, ItemColor.ColorName, SexData.Sex, ItemStatus.ItemStatus FROM PurchaseOrderHeader INNER JOIN PurchaseOredrDetails ON PurchaseOrderHeader.Id = PurchaseOredrDetails.PurchaseOredeHeaderId INNER JOIN Items ON PurchaseOredrDetails.ItemId = Items.Id INNER JOIN ItemType ON PurchaseOrderHeader.ItemTypeId = ItemType.Id AND Items.ItemTypeId = ItemType.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN PurchaseItemStatus ON PurchaseOredrDetails.Status = PurchaseItemStatus.Id INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id WHERE (PurchaseOredrDetails.Status = 2 OR PurchaseOredrDetails.Status = 3) AND (PurchaseOredrDetails.IsChecked = 0) AND (PurchaseOrderHeader.PurchaseOrderNo = @PurchaseOrderNo)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtPurchaseOrderNo" DefaultValue="0" Name="PurchaseOrderNo" PropertyName="Text" />
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
