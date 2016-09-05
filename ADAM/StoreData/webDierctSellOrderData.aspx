<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webDierctSellOrderData.aspx.cs" Inherits="ADAM.StoreData.webDierctSellOrderData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة انشاء طلب صرف بضاعة بيع مباشر" Font-Size="X-Large" Font-Underline="True"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" /></td>
            <td>
                <%--<asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" />--%></td>
            <td>
                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" />--%></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" Text="رقم طلب الصرف" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtDirectSellOrderNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox></td>
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" Text="تاريخ طلب الصرف" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label14" runat="server" Text="الادارة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="ddl" DataSourceID="dbDepartment" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label15" runat="server" Text="القسم" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="ddl" DataSourceID="dbDivision" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList></td>
        </tr>
        <tr>

            <td style="text-align: left">
                <asp:Label ID="Label4" runat="server" Text="مندوب الادارة الطالبة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="ddl" DataSourceID="dbEmployee" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label5" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="ddl" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>
        <tr>

            <td style="text-align: left"> <asp:Label ID="Label2" runat="server" Text="خط أنتاج" CssClass="lbl"></asp:Label>
              </td>
            <td style="text-align: center">
                 <asp:DropDownList ID="ddlProductionLine" runat="server" CssClass="ddl" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList> </td>
            <td style="text-align: left" colspan="2">
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
                </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvPurchaseDetailsData" runat="server" AutoGenerateColumns="False" CssClass="gv" DataKeyNames="Id" DataSourceID="dbPurchaseDetailsData" OnSelectedIndexChanged="gvPurchaseDetailsData_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="UnitName" HeaderText="الوحدة" SortExpression="UnitName" />
            <asp:BoundField DataField="LimitQty" HeaderText="الحد الادني" SortExpression="LimitQty" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ColorName" HeaderText="لون المنتج" SortExpression="ColorName" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="Specification" HeaderText="الوصف" SortExpression="Specification" />
            <asp:BoundField DataField="Qty" HeaderText="الكمية" SortExpression="Qty" />
            <asp:BoundField DataField="Tester" HeaderText="Tester" SortExpression="Tester" />
            <asp:BoundField DataField="Note" HeaderText="ملاحظات" SortExpression="Note" />
            <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dbPurchaseDetailsData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS UnitName, Items.LimitQty, SexData.Sex, ItemStatus.ItemStatus, ItemColor.ColorName, Items.Specification, DierctSellDetails.Qty, DierctSellDetails.Note, DierctSellDetails.Id, DierctSellDetails.Tester, ProductionLine.productionLineName, ProductionLine.Id AS ProductionLineId FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN DierctSellDetails ON Items.Id = DierctSellDetails.ItemId INNER JOIN ItemColor ON DierctSellDetails.ItemColorId = ItemColor.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id WHERE (DierctSellDetails.DirectSellHeaderId = @DirectSellHeaderId) AND (ProductionLine.Id = @ProductionLineId)">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfId" DefaultValue="0" Name="DirectSellHeaderId" PropertyName="Value" />
            <asp:ControlParameter ControlID="ddlProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
        </SelectParameters>
</asp:SqlDataSource>
    <br />
    <table class="gv" style="border: solid;">
        <tr style="border: solid;">
            <td style="">كود الصنف</td>
            <td style="">الصنف</td>
            <td style="">وحدة الصنف</td>
            <td style="">الحد الادني</td>
            <td style="">النوع</td>
            <td style="">لون الصنف</td>
            <td style="">نوع المنتج</td>
            <td style="">الوصف</td>
            <td style="">رصيد المخزن</td>
            <td style="">الكمية</td>
            <td style="">Tester</td>
            <td style="">ملاحظات</td>
            <td style="">أضافة صنف</td>
            <td style="">تعديل</td>
            <td style="">حذف</td>
        </tr>
        <tr>
            <td style="">
                <asp:TextBox ID="txtItemCode" Width="75px" CssClass="txt" runat="server">
                </asp:TextBox><asp:Button ID="btnGetItemData" runat="server" Text="!" OnClick="btnGetItemData_Click" /></td>
            <td style="">
                <asp:DropDownList ID="ddlItemName" Width="200" runat="server" AutoPostBack="True" CssClass="ddl" DataSourceID="dbItem" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"></asp:DropDownList></td>
            <td style="">
                <asp:Label ID="lblItemUnit" runat="server"></asp:Label></td>
            <td style="">
                <asp:Label ID="lblLimitQty" runat="server"></asp:Label></td>
            <td style="">
                <asp:Label ID="lblSex" runat="server"></asp:Label></td>
            <td style="">
                <asp:DropDownList ID="ddlItemColor" CssClass="ddl" runat="server" DataSourceID="dbItemColor"
                    DataTextField="ColorName" DataValueField="Id" OnSelectedIndexChanged="ddlItemColor_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td style="">
                <asp:Label ID="lblItemstatus" runat="server"></asp:Label></td>
            <td style="">
                <asp:Label ID="lblSpecification" runat="server"></asp:Label></td>
            <td style="">
                <asp:Label ID="lblCurrentBalance" runat="server"></asp:Label></td>            
            <td style="">
                <asp:TextBox ID="txtQty" Width="75px" CssClass="txt" runat="server"></asp:TextBox></td>
            <td style="">
                <asp:TextBox ID="txtTester" Width="75px" CssClass="txt" runat="server"></asp:TextBox></td>
            <td style="">
                <asp:TextBox ID="txtPurchaseNote" TextMode="MultiLine" CssClass="txt" runat="server"></asp:TextBox></td>
            <td style="">
                <asp:ImageButton ID="btnSavePurchaseItem" runat="server" ImageUrl="~/Image/Save.png" Width="30px" Height="30px" ToolTip="حفظ" OnClick="btnSavePurchaseItem_Click" />
            </td>
            <td style="">
                <asp:ImageButton ID="btnEditPurchaseItem" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" Width="30px" Height="30px" ToolTip="تعديل" OnClick="btnEditPurchaseItem_Click" /></td>
            <td style="">
                <asp:ImageButton ID="btndeletePurchaseItem" runat="server" ImageUrl="~/Image/Delete.png" ToolTip="حذف" Width="30px" Height="30px" OnClick="btndeletePurchaseItem_Click" />
            </td>
        </tr>
    </table>
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
                <asp:SqlDataSource ID="dbEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '  ' + LastName AS Name FROM EmployeeData WHERE (DivisionId = @DivisionId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlDivision" DefaultValue="0" Name="DivisionId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItem" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items WHERE (ItemTypeId = @ItemTypeId) AND (ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS ColorName UNION SELECT Id, ColorName FROM ItemColor"></asp:SqlDataSource>
            </td>
            <td>
                <asp:HiddenField ID="hfId" Value="0" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfPurchaseDetailsId" Value="0" runat="server"></asp:HiddenField>
            </td>
        </tr>
    </table>
</asp:Content>
