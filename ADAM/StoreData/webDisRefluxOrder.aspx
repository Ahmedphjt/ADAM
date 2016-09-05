<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webDisRefluxOrder.aspx.cs" Inherits="ADAM.StoreData.webDisRefluxOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة أرتجاع اصناف" Font-Size="X-Large" Font-Underline="True"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label8" runat="server" Text="نوع الارتجاع" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlRefluxType" runat="server" CssClass="ddl">
                    <asp:ListItem Text="----" Value="0"></asp:ListItem>
                    <asp:ListItem Text="علي طلب شراء" Value="1"></asp:ListItem>
                    <asp:ListItem Text="علي طلب صرف" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label6" runat="server" Text="رقم طلب الارتجاع" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtRefluxNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvReflux" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbRefluxDetailsData" DataKeyNames="RefluxDetailsDataId" OnSelectedIndexChanged="gvReflux_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="RefluxQty" HeaderText="الكمية المرتجعة" SortExpression="RefluxQty" />
            <asp:BoundField DataField="RefluxFreeQty" HeaderText="Tester مرتجع" SortExpression="RefluxFreeQty" />
            <asp:BoundField DataField="Bounce" HeaderText="مرتجع Bounce" SortExpression="Bounce" />
            <asp:BoundField DataField="IncommingOrderNo" HeaderText="رقم أذن الوارد" SortExpression="IncommingOrderNo" />
            <asp:CommandField SelectText="اختر" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <table class="gv" style="border: solid;">
        <tr style="border: solid;">
            <td style="">كود الصنف</td>
            <td style="">الصنف</td>
            <td style="">وحدة الصنف</td>
            <td style="">لون الصنف</td>
            <td style="">الكمية</td>
            <td style="">Tester</td>
            <td style="">Bounce</td>
            <td style="">رقم اذن الوارد</td>
            <td style="">أرتجاع</td>
        </tr>
        <tr>
            <td style="">
                <asp:TextBox ID="txtItemCode" Width="75px" CssClass="txt" runat="server" Enabled="False"></asp:TextBox></td>
            <td style="">
                <asp:DropDownList ID="ddlItemName" Width="200px" runat="server" CssClass="ddl" DataSourceID="dbItem" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList></td>
            <td style="">
                <asp:Label ID="lblItemUnit" runat="server"></asp:Label></td>
            <td style="">
                <asp:DropDownList ID="ddlItemColor" Width="150px" CssClass="ddl"
                    runat="server" DataSourceID="dbItemColor" DataTextField="ColorName" DataValueField="Id" Enabled="False">
                </asp:DropDownList>
            </td>
            <td style="">
                <asp:TextBox ID="txtQty" Width="75px" CssClass="txt" runat="server">
                </asp:TextBox></td>
            <td style="">
                <asp:TextBox ID="txtFreeQty" Width="75px" CssClass="txt" runat="server">
                </asp:TextBox></td>
              <td style="">
                <asp:TextBox ID="txtBounce" Width="75px" CssClass="txt" runat="server">
                </asp:TextBox></td>
            <td style="">
                <asp:TextBox ID="txtIncommingOrder" Width="75px" CssClass="txt" runat="server">
                </asp:TextBox></td>
            <td style="">
                  <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" Width="30px" Height="30px" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItem" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items ">
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS ColorName UNION SELECT Id, ColorName FROM ItemColor"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbRefluxDetailsData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemColor.ColorName, RefluxDetailsData.RefluxQty, RefluxDetailsData.RefluxFreeQty, ItemUnit.Name AS ItemUnitName, RefluxDetailsData.RefluxHeaderId, RefluxDetailsData.Id AS RefluxDetailsDataId, RefluxDetailsData.Status, RefluxDetailsData.Bounce, RefluxDetailsData.IncommingOrderNo FROM RefluxDetailsData INNER JOIN Items ON RefluxDetailsData.ItemId = Items.Id INNER JOIN ItemColor ON RefluxDetailsData.ItemColorId = ItemColor.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id WHERE (RefluxDetailsData.RefluxHeaderId = @RefluxHeaderId) AND (RefluxDetailsData.Status = 0)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="RefluxHeaderId" DefaultValue="0" Name="RefluxHeaderId" PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:HiddenField ID="RefluxHeaderId" Value="0" runat="server" />
                <asp:HiddenField ID="RefluxDetailsId" Value="0" runat="server" />
            </td>
        </tr>
    </table>


</asp:Content>
