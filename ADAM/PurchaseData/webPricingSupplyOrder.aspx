<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webPricingSupplyOrder.aspx.cs" Inherits="ADAM.PurchaseData.webPricingSupplyOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة تسعير أمر توريد" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
     <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click"  /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click"  /></td>                            
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="أمر التوريد"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSupplyOrder" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:HiddenField ID="No" Value="0" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvSupplyOrder" CssClass="gv"  runat="server" AutoGenerateColumns="False" DataSourceID="dbSupplyOrder" Width="97%" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="SupplyOrderNo" HeaderText="رقم أمر التوريد" SortExpression="SupplyOrderNo" />
            <asp:BoundField DataField="SupplyOrderDate" HeaderText="تاريخ أمر التوريد" SortExpression="SupplyOrderDate" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Name" HeaderText="أسم المادة" SortExpression="Name" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="ItemPrice" HeaderText="السعر الحالي" SortExpression="ItemPrice" />
            <asp:TemplateField HeaderText="السعر الجديد">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
<asp:SqlDataSource ID="dbSupplyOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT SupplyOrderHeader.SupplyOrderNo, SupplyOrderHeader.SupplyOrderDate, Items.Name, ItemColor.ColorName, SupplyOrderDetails.ItemPrice, SupplyOrderDetails.Id FROM SupplyOrderDetails INNER JOIN SupplyOrderHeader ON SupplyOrderDetails.SupplyOrderHeaderId = SupplyOrderHeader.Id INNER JOIN PurchaseOredrDetails ON SupplyOrderDetails.PurchaseOrderDetailsId = PurchaseOredrDetails.Id INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id INNER JOIN Items ON PurchaseOredrDetails.ItemId = Items.Id WHERE (SupplyOrderHeader.SupplyOrderNo = @SupplyOrderNo) AND (PurchaseOredrDetails.IsChecked &lt; 7)">
    <SelectParameters>
        <asp:ControlParameter ControlID="No" DefaultValue="0" Name="SupplyOrderNo" PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
</asp:Content>
