<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webGiftsPricing.aspx.cs" Inherits="ADAM.Sales.webGiftsPricing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة تسعير صرف هدايا" Font-Size="X-Large" Font-Underline="True"></asp:Label></h1>
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
                <asp:Label ID="Label3" runat="server" Text="رقم طلب الصرف" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtExchangeRequestNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td style="text-align: left">
                &nbsp;</td>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>


        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label14" runat="server" Text="الادارة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="ddl" DataSourceID="dbDepartment" DataTextField="Name" DataValueField="Id" AutoPostBack="True" Enabled="False"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label15" runat="server" Text="القسم" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="ddl" DataSourceID="dbDivision" DataTextField="Name" DataValueField="Id" AutoPostBack="True" Enabled="False"></asp:DropDownList></td>
        </tr>
        <tr>

            <td style="text-align: left">
                <asp:Label ID="Label4" runat="server" Text="مندوب الادارة الطالبة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="ddl" DataSourceID="dbEmployee" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" Text="تاريخ طلب الصرف" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="txt" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label16" runat="server" Text="العميل" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtClientName" runat="server" CssClass="txt" Enabled="False"></asp:TextBox></td>
            <td style="text-align: left">
                <asp:Label ID="Label17" runat="server" Text="رقم الهاتف" CssClass="lbl"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:TextBox ID="txtClientPhone" runat="server" CssClass="txt" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label18" runat="server" Text="رقم الموبايل" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtClientMob" runat="server" CssClass="txt" Enabled="False"></asp:TextBox></td>
            <td style="text-align: left">
                <asp:Label ID="Label19" runat="server" Text="العنوان" CssClass="lbl"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:TextBox ID="txtClientAddress" runat="server" CssClass="txt" Enabled="False"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
        <asp:GridView ID="gvExchangeRequestData" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbExchangeRequestsData" DataKeyNames="ExchangeRequestDetailsDataId,ItemPriceId" OnSelectedIndexChanged="gvExchangeRequestData_SelectedIndexChanged" OnRowDataBound="gvExchangeRequestData_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="UnitName" HeaderText="الوحدة" SortExpression="UnitName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="Qty" HeaderText="ك.ط.الصرف" SortExpression="Qty" />
            <asp:BoundField DataField="FreeQty" HeaderText="Tester" SortExpression="FreeQty" />
            <asp:BoundField DataField="productionLineName" HeaderText="خط انتاج" SortExpression="productionLineName" />
            <asp:BoundField DataField="MainClausePrice" HeaderText="س.الجملة" SortExpression="MainClausePrice" />
            <asp:BoundField DataField="MainSalesPrice" HeaderText="س.البيع" SortExpression="MainSalesPrice" />
            <asp:BoundField DataField="TesterClausePrice" HeaderText="T س.جملة" SortExpression="TesterClausePrice" />
            <asp:BoundField DataField="TesterSalesPrice" HeaderText="T س.بيع" SortExpression="TesterSalesPrice" />
            <asp:TemplateField HeaderText="نوع السعر">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlSalsType" runat="server" CssClass="ddl" Width="75px">
                        <asp:ListItem Value="0">---</asp:ListItem>
                        <asp:ListItem Value="1">بيع</asp:ListItem>
                        <asp:ListItem Value="2">جملة</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField SelectText="تسعير" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '  ' + LastName AS Name FROM EmployeeData WHERE (DivisionId = @DivisionId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlDivision" DefaultValue="0" Name="DivisionId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
        <asp:SqlDataSource ID="dbExchangeRequestsData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS UnitName, SexData.Sex, ItemStatus.ItemStatus, ItemColor.ColorName, ExchangeRequestDetailsData.Qty, ExchangeRequestDetailsData.Note, ExchangeRequestDetailsData.ExchangeRequestHeaderDataId, ExchangeRequestDetailsData.Id AS ExchangeRequestDetailsDataId, ExchangeRequestDetailsData.Status, ExchangeRequestDetailsData.FreeQty, ProductionLine.productionLineName, ExchangeRequestDetailsData.Bounce, ExchangeRequestHeaderData.OrderType, ItemPrice.MainClausePrice, ItemPrice.MainSalesPrice, ItemPrice.MainShowsPrice, ItemPrice.TesterClausePrice, ItemPrice.TesterSalesPrice, ItemPrice.TesterShowsPrice, ItemPrice.Id AS ItemPriceId FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ExchangeRequestDetailsData ON Items.Id = ExchangeRequestDetailsData.ItemId INNER JOIN ItemColor ON ExchangeRequestDetailsData.ItemColorId = ItemColor.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN ExchangeRequestHeaderData ON ExchangeRequestDetailsData.ExchangeRequestHeaderDataId = ExchangeRequestHeaderData.Id INNER JOIN ItemPrice ON Items.Id = ItemPrice.ItemId AND ItemColor.Id = ItemPrice.ItemColorId WHERE (ExchangeRequestDetailsData.ExchangeRequestHeaderDataId = @ExchangeRequestHeaderDataId) AND (ExchangeRequestDetailsData.Status = 1) AND (ExchangeRequestHeaderData.OrderType = @OrderType)">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfId" DefaultValue="0" Name="ExchangeRequestHeaderDataId" PropertyName="Value" />
            <asp:ControlParameter ControlID="hfOrderValue" DefaultValue="0" Name="OrderType" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>
            </td>
            <td>
                <asp:HiddenField ID="hfId" Value="0" runat="server"></asp:HiddenField>
                 <asp:HiddenField ID="hfOrderValue" Value="11" runat="server"></asp:HiddenField>
            </td>
            <td><asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource></td>
            <td><asp:SqlDataSource ID="dbDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Department"></asp:SqlDataSource></td>
            <td>
                <asp:SqlDataSource ID="dbDivision" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM division WHERE (DepartmentId = @DepartmentId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlDepartment" DefaultValue="0" Name="DepartmentId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
  
</asp:Content>