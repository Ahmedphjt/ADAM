<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webFollowUpPurchaseOrder.aspx.cs" Inherits="ADAM.PurchaseData.webFollowUpPurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="شاشة متابعة طلبات الشراء" Font-Size="X-Large" CssClass="lblPageName" Font-Underline="true"></asp:Label></h1>
    <table class="menu" style="width: 90%">
        <tr>
            <td colspan="5" style="text-align: center">
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="النوع" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlSex" runat="server" CssClass="ddl" DataSourceID="dbSex" DataTextField="Sex" DataValueField="Id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="نوع المنتج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlItemStatus" CssClass="ddl" runat="server" DataSourceID="dbItemStatus" DataTextField="ItemStatus" DataValueField="Id">
                </asp:DropDownList>

            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="خط أنتاج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlProdctionLine" CssClass="ddl" runat="server" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
             <td>
                <asp:Label ID="Label2" runat="server" Text="الادارة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlDepartment" CssClass="ddl" runat="server" DataSourceID="dbDepartment" DataTextField="Name" DataValueField="Id" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                </td>
            <td style="text-align: right">
               </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvBrowsePurchaseOrder" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbBrowsePurchaseOrder" OnRowDataBound="gvBrowsePurchaseOrder_RowDataBound" Visible="False" Width="97%">
        <Columns>
            <asp:BoundField DataField="PurchaseOrderNo" HeaderText="رقم طلب الشراء" SortExpression="PurchaseOrderNo" />
            <asp:BoundField DataField="PurchaseDate" HeaderText="تاريخ الطلب" SortExpression="PurchaseDate" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="DepartmentName" HeaderText="الادارة" SortExpression="DepartmentName" />
            <asp:BoundField DataField="DivisionName" HeaderText="القسم" SortExpression="DivisionName" />
            <asp:BoundField DataField="EmployeeName" HeaderText="الموظف" ReadOnly="True" SortExpression="EmployeeName" />
            <asp:BoundField DataField="ItemsCode" HeaderText="كود الصنف" SortExpression="ItemsCode" />
            <asp:BoundField DataField="ItemsName" HeaderText="الصنف" SortExpression="ItemsName" />
            <asp:BoundField DataField="Qty" HeaderText="كمية طلب الشراء" SortExpression="Qty" />
            <asp:BoundField DataField="ConformQty" HeaderText="الكمية المعتمدة" SortExpression="ConformQty" />
            <asp:BoundField DataField="ItemTypeName" HeaderText="نوع الصنف" SortExpression="ItemTypeName" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="SexName" HeaderText="النوع" SortExpression="SexName" />
            <asp:BoundField DataField="ItemStatusName" HeaderText="نوع المنتج" SortExpression="ItemStatusName" />
            <asp:BoundField DataField="PurchaseItemStatus" HeaderText="حالة الطلب" SortExpression="PurchaseItemStatus" />
            <asp:BoundField DataField="IsChecked" HeaderText="حالة التوريد" SortExpression="IsChecked" />
        </Columns>
    </asp:GridView>
    <br />
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbSex" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS Sex UNION SELECT Id, Sex FROM SexData"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS ItemStatus UNION SELECT Id, ItemStatus FROM ItemStatus"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbBrowsePurchaseOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="FollowUpPurchaseOrder" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" Type="Int64" />
                        <asp:ControlParameter ControlID="ddlSex" DefaultValue="0" Name="Sex" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlItemStatus" DefaultValue="0" Name="ItemStatus" PropertyName="SelectedValue" Type="Int64" />
                        <asp:ControlParameter ControlID="ddlProdctionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" Type="Int64" />
                        <asp:ControlParameter ControlID="ddlDepartment" DefaultValue="0" Name="DepartmentId" PropertyName="SelectedValue" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS Name UNION SELECT Id, Name FROM Department"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

