<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webAcceptedDeliveryOrder.aspx.cs" Inherits="ADAM.Prodction.webAcceptedDeliveryOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة اعتماد طلب تسليم منتج تام" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <%--  <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" /--%>></td>
            <td>
                <%--<asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" />--%></td>
            <td>
                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" />--%></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" Text="رقم طلب التسليم" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtDeliveryOrderNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox></td>
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" Text="تاريخ طلب التسليم" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="txt" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label5" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="ddl" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label6" runat="server" Text="خط أنتاج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionLine" runat="server" CssClass="ddl" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" Enabled="False"></asp:DropDownList></td>

        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label2" runat="server" Text="الادارة" CssClass="lbl"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="ddl" DataSourceID="dbDepartment" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label4" runat="server" Text="القسم" CssClass="lbl"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="ddl" DataSourceID="dbDivision" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label7" runat="server" Text="المندوب" CssClass="lbl"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="ddl" DataSourceID="dbEmployee" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList>
            </td>
            <td style="text-align: left"></td>
            <td style="text-align: center"></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvDeliveryData" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbDeliveryData" OnSelectedIndexChanged="gvPurchaseDetailsData_SelectedIndexChanged" DataKeyNames="DeliveryDataDetailsId">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="UnitName" HeaderText="الوحدة" SortExpression="UnitName" />
            <asp:BoundField DataField="LimitQty" HeaderText="الحد الاقصي" SortExpression="LimitQty" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="Specification" HeaderText="الوصف" SortExpression="Specification" />
            <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
            <asp:BoundField DataField="Qty" HeaderText="الكمية" SortExpression="Qty" />
            <asp:BoundField DataField="Tester" HeaderText="Tester" SortExpression="Tester" />
            <asp:TemplateField HeaderText="الكمية المستلمة">
                <ItemTemplate>
                    <asp:TextBox ID="txtRealQty" runat="server" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ك Tester مستلمة">
                <ItemTemplate>
                    <asp:TextBox ID="txtRealTester" runat="server" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ddl" DataSourceID="dbLocation" DataTextField="LocationName" DataValueField="Id">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField SelectText="حفظ" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <asp:HiddenField ID="hfId" Value="0" runat="server"></asp:HiddenField>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbDeliveryData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS UnitName, Items.LimitQty, SexData.Sex, ItemStatus.ItemStatus, ItemColor.ColorName, Items.Specification, ProductionLine.productionLineName, DeliveryDataDetails.Qty, DeliveryDataDetails.Id AS DeliveryDataDetailsId, DeliveryDataDetails.DeliveryDataHeaderId, DeliveryDataDetails.Status, DeliveryDataDetails.Tester FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN DeliveryDataDetails ON Items.Id = DeliveryDataDetails.ItemId INNER JOIN ItemColor ON DeliveryDataDetails.ItemColorId = ItemColor.Id WHERE (DeliveryDataDetails.DeliveryDataHeaderId = @DeliveryDataHeaderId) AND (DeliveryDataDetails.Status = 0)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfId" DefaultValue="0" Name="DeliveryDataHeaderId" PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbLocation" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS LocationName UNION SELECT Id, LocationName FROM ItemLocation WHERE (ItemTypeId = @ItemTypeId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>

                <asp:SqlDataSource ID="dbEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '' + LastName AS Name FROM EmployeeData WHERE (DivisionId = @DivisionId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlDivision" DefaultValue="0" Name="DivisionId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </td>
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
                <asp:HiddenField ID="hfPurchaseDetailsId" Value="0" runat="server"></asp:HiddenField>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
