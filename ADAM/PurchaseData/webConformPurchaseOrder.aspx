<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webConformPurchaseOrder.aspx.cs" Inherits="ADAM.PurchaseData.webConformPurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة اعتماد طلب الشراء" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <%--<asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="اعتماد" />--%></td>
            <td>
                <asp:ImageButton ID="btnConform" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="اعتماد" OnClick="btnConform_Click" /></td>
            <td>
                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="رفض"/>--%></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="PurchaseNumber" visible="false">
        <asp:GridView ID="gvPurchaseNo" runat="server" CssClass="gv" AutoGenerateColumns="False" DataSourceID="dbPurchaseNo" Width="97%" OnSelectedIndexChanged="gvPurchaseNo_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="PurchaseOrderNo" HeaderText="رقم طلب الشراء" SortExpression="PurchaseOrderNo" />
                <asp:BoundField DataField="PurchaseDate" HeaderText="تاريخ الطلب" SortExpression="PurchaseDate" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="DepartmentName" HeaderText="الادارة" SortExpression="DepartmentName" />
                <asp:BoundField DataField="divisionName" HeaderText="القسم" SortExpression="divisionName" />
                <asp:BoundField DataField="EmpName" HeaderText="الموظف" ReadOnly="True" SortExpression="EmpName" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbPurchaseNo" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT PurchaseOrderHeader.PurchaseOrderNo, PurchaseOrderHeader.PurchaseDate, Department.Name AS DepartmentName, division.Name AS divisionName, EmployeeData.FirstName + '' + EmployeeData.LastName AS EmpName FROM PurchaseOrderHeader INNER JOIN Department ON PurchaseOrderHeader.DepartmentId = Department.Id INNER JOIN division ON PurchaseOrderHeader.DivisionId = division.Id AND Department.Id = division.DepartmentId INNER JOIN EmployeeData ON PurchaseOrderHeader.EmployeeId = EmployeeData.Id ORDER BY PurchaseOrderHeader.PurchaseOrderNo DESC"></asp:SqlDataSource>
    </div>
    <div runat="server" id="Data">
        <table class="menu">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Text="رقم طلب الشراء" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtPurchaseOrderNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:Button ID="ShowgvPurchaseNo" runat="server" Text="!!" OnClick="ShowgvPurchaseNo_Click" />
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Text="تاريخ طلب الشراء" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtDate" Enabled="false" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label14" runat="server" Text="الادارة" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlDepartment" Enabled="false" runat="server" CssClass="ddl" DataSourceID="dbDepartment" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label15" runat="server" Text="القسم" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlDivision" Enabled="false" runat="server" CssClass="ddl" DataSourceID="dbDivision" DataTextField="Name" DataValueField="Id"></asp:DropDownList></td>
            </tr>
            <tr>

                <td style="text-align: left">
                    <asp:Label ID="Label4" runat="server" Text="مندوب الادارة الطالبة" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlEmployee" runat="server" Enabled="false" CssClass="ddl" DataSourceID="dbEmployee" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label5" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlItemType" runat="server" Enabled="false" CssClass="ddl" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>

                <td style="text-align: left">
                    <asp:Label ID="Label16" runat="server" Text="المورد" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="ddl" DataSourceID="dbSupplier" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                </td>
                <td style="text-align: left"></td>
                <td style="text-align: center"></td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Text="ملاحظات" CssClass="lbl"></asp:Label></td>
                <td colspan="3">
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" Enabled="false" runat="server" CssClass="txt" Width="250px"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvPurchaseData" runat="server" CssClass="gv" AutoGenerateColumns="False" DataSourceID="dbPurchaseData" Width="97%" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
                <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
                <asp:BoundField DataField="ItemUnitName" HeaderText="وحدة الصنف" SortExpression="ItemUnitName" />
                <asp:BoundField DataField="SexName" HeaderText="النوع" SortExpression="SexName" />
                <asp:BoundField DataField="ItemStatusName" HeaderText="نوع المنتج" SortExpression="ItemStatusName" />
                <asp:BoundField DataField="productionLineName" HeaderText="خط أنتاج" SortExpression="productionLineName" />
                <asp:BoundField DataField="Specification" HeaderText="الوصف" SortExpression="Specification" />
                <asp:BoundField DataField="Qty" HeaderText="كمية طلب الشراء" SortExpression="Qty" />
                <asp:BoundField DataField="ColorName" HeaderText="لون الصنف" SortExpression="ColorName" />
                <asp:TemplateField HeaderText="نوع الاعتماد">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlConformtype" Width="100px" runat="server">
                            <asp:ListItem Text="---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="اعتماد كلي" Value="2"></asp:ListItem>
                            <asp:ListItem Text="اعتماد جزئي" Value="3"></asp:ListItem>
                            <asp:ListItem Text="مرفوض" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الكمية المعتمدة">
                    <ItemTemplate>
                        <asp:TextBox ID="txtConformQty" runat="server" CssClass="txt" Width="75px" TextMode="Number" ToolTip="من فضلك ادخل الكمية المعتمدة ويجب ان لا تزيد عن كمية طلب الشراء"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbPurchaseData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS ItemUnitName, PurchaseOredrDetails.Qty, ItemType.Name AS ItemTypeName, Items.Sex, Items.ItemStatus, Items.Specification, PurchaseOredrDetails.Id, ItemColor.ColorName, SexData.Sex AS SexName, ItemStatus.ItemStatus AS ItemStatusName, ProductionLine.productionLineName FROM PurchaseOrderHeader INNER JOIN PurchaseOredrDetails ON PurchaseOrderHeader.Id = PurchaseOredrDetails.PurchaseOredeHeaderId INNER JOIN Items ON PurchaseOredrDetails.ItemId = Items.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN ItemType ON PurchaseOrderHeader.ItemTypeId = ItemType.Id AND Items.ItemTypeId = ItemType.Id INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id WHERE (PurchaseOrderHeader.PurchaseOrderNo = @PurchaseOrderNo) AND (PurchaseOredrDetails.Status = 1)">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtPurchaseOrderNo" DefaultValue="0" Name="PurchaseOrderNo" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <table>
            <tr>
                <td>
                    <asp:SqlDataSource ID="dbDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Department"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbDivision" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM division WHERE (DepartmentId = @DepartmentId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlDepartment" DefaultValue="0" Name="DepartmentId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '  ' + LastName AS Name FROM EmployeeData"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbSupplier" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '   ' + LastName AS Name FROM SupplierData"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
