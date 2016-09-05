<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webrptExchangeRequestOrderReport.aspx.cs" Inherits="ADAM.StoreReport.webrptExchangeRequestOrderReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير أذن الصرف" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label4" runat="server" Text="نوع طلب الصرف" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlExchangeRequest" CssClass="ddl" runat="server" DataSourceID="dbExchangeRequest" DataTextField="MovementName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="رقم طلب الصرف" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExchangeRequestNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Text="عرض التقرير" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            </tr>
        </table>
        <asp:GridView ID="gvExcahnge" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbExcahnge" Width="97%" OnSelectedIndexChanged="gvAudit_SelectedIndexChanged" DataKeyNames="ExchangeRequestDetailsDataId">
            <Columns>
                <asp:BoundField DataField="ExchangeRequestNo" HeaderText="رقم الطلب" SortExpression="ExchangeRequestNo" />
                <asp:BoundField DataField="ExchangeRequestDate" HeaderText="تاريخ الطلب" SortExpression="ExchangeRequestDate" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
                <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
                <asp:BoundField DataField="Qty" HeaderText="الكمية" SortExpression="Qty" />
                <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
                <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
                <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
                <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
                <asp:BoundField DataField="FreeQty" HeaderText="Tester" SortExpression="FreeQty" />
                <asp:BoundField DataField="Bounce" HeaderText="Bounce" SortExpression="Bounce" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
        <table>
            <tr>
                <td>
                    <asp:SqlDataSource ID="dbExcahnge" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT ExchangeRequestHeaderData.ExchangeRequestNo, ExchangeRequestHeaderData.ExchangeRequestDate, Items.Code, Items.Name, ItemType.Name AS ItemTypeName, division.Name AS divisionName, EmployeeData.FirstName + ' ' + EmployeeData.LastName AS EmpName, ExchangeRequestDetailsData.Qty, SexData.Sex, ItemUnit.Name AS ItemUnitName, Items.LimitQty, ItemStatus.ItemStatus, Items.Specification, ProductionLine.productionLineName, Items.ItemTypeId, ItemColor.ColorName, ExchangeRequestHeaderData.OrderType, ExchangeRequestDetailsData.FreeQty, ExchangeRequestDetailsData.Bounce, ExchangeRequestDetailsData.Id AS ExchangeRequestDetailsDataId, ExchangeRequestDetailsData.Status FROM ExchangeRequestDetailsData INNER JOIN ExchangeRequestHeaderData ON ExchangeRequestDetailsData.ExchangeRequestHeaderDataId = ExchangeRequestHeaderData.Id INNER JOIN Items ON ExchangeRequestDetailsData.ItemId = Items.Id INNER JOIN ItemType ON Items.ItemTypeId = ItemType.Id AND ExchangeRequestDetailsData.ItemTypeId = ItemType.Id INNER JOIN division ON ExchangeRequestHeaderData.DivisionId = division.Id INNER JOIN EmployeeData ON ExchangeRequestHeaderData.EmpId = EmployeeData.Id AND division.Id = EmployeeData.DivisionId INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN ItemColor ON ExchangeRequestDetailsData.ItemColorId = ItemColor.Id WHERE (ExchangeRequestHeaderData.OrderType = @OrderType) AND (ExchangeRequestDetailsData.Status = 1) ORDER BY ExchangeRequestHeaderData.ExchangeRequestNo DESC">
                        <SelectParameters>
                            
                            <asp:ControlParameter ControlID="ddlExchangeRequest" DefaultValue="0" Name="OrderType" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:SqlDataSource ID="dbExchangeRequest" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS MovementName UNION SELECT Id, MovementName FROM MovmentName WHERE (Id &gt; 7) AND (Id &lt; 13) or (id=17)"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>