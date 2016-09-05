<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webRecordReceiptData.aspx.cs" Inherits="ADAM.StoreData.webRecordReceiptData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة محضر الاستلام" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" /></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label3" runat="server" Text="رقم محضر الاستلام" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtRecordReceiptNo" TextMode="Number" ToolTip="ادخل رقم محضر الاستلام ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
            <td style="text-align: center">
                <asp:Label ID="Label1" runat="server" Text="تاريخ محضر الاستلام" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtRecordReceiptDate" TextMode="Date" ToolTip="ادخل تاريخ محضر الاستلام" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label2" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList></td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvRecordReceipt" CssClass="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="dbRecordReceipt" Width="99%">
        <Columns>
            <asp:BoundField DataField="PurchaseOrderNo" HeaderText="ط.الشراء" SortExpression="PurchaseOrderNo" />
            <asp:BoundField DataField="SupplyOrderNo" HeaderText="رقم امر التوريد" SortExpression="SupplyOrderNo" />
            <asp:BoundField DataField="SupplyOrderDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="ت. امر التوريد" SortExpression="SupplyOrderDate" />
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ItemPrice" HeaderText="سعر الوحدة" SortExpression="ItemPrice" />
            <asp:BoundField DataField="ConformQty" HeaderText="الكمية" SortExpression="ConformQty" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="ColorName" HeaderText="لون الصنف" SortExpression="ColorName" />
            <asp:BoundField DataField="productionLineName" HeaderText="خط انتاج" SortExpression="productionLineName" />
            <asp:TemplateField HeaderText="اختيار">
                <ItemTemplate>
                    <asp:CheckBox ID="chkChoose" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ك.المستلمة">
                <ItemTemplate>
                    <asp:TextBox ID="txtQtyReceived" Width="75px" CssClass="txt" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tester">
                <ItemTemplate>
                    <asp:TextBox ID="txtFreeQty" Width="75px" CssClass="txt" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="اذن البوابة">
                <ItemTemplate>
                    <asp:TextBox ID="txtIndoor" runat="server" Width="75px" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ملاحظات">
                <ItemTemplate>
                    <asp:TextBox ID="txtNote" runat="server" CssClass="txt" TextMode="MultiLine"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbRecordReceipt" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT SupplyOrderHeader.SupplyOrderNo, SupplyOrderHeader.SupplyOrderDate, SupplyOrderDetails.ItemPrice, PurchaseOredrDetails.ConformQty, Items.Code, Items.Name, Items.ItemTypeId, SupplyOrderDetails.Id, PurchaseOredrDetails.IsChecked, PurchaseOrderHeader.PurchaseOrderNo, ItemUnit.Name AS ItemUnitName, PurchaseOredrDetails.IsClosed, ItemColor.ColorName, SexData.Sex, ItemStatus.ItemStatus, ProductionLine.productionLineName FROM SupplyOrderDetails INNER JOIN SupplyOrderHeader ON SupplyOrderDetails.SupplyOrderHeaderId = SupplyOrderHeader.Id INNER JOIN PurchaseOredrDetails ON SupplyOrderDetails.PurchaseOrderDetailsId = PurchaseOredrDetails.Id INNER JOIN Items ON PurchaseOredrDetails.ItemId = Items.Id INNER JOIN PurchaseOrderHeader ON PurchaseOredrDetails.PurchaseOredeHeaderId = PurchaseOrderHeader.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN SexData ON Items.Sex = SexData.Id WHERE (Items.ItemTypeId = @ItemTypeId) AND (PurchaseOredrDetails.IsChecked &gt;= 5) AND (PurchaseOredrDetails.IsClosed = 0)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="hfAuditHeaderId" Value="0" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
