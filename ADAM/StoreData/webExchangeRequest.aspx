<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webExchangeRequest.aspx.cs" Inherits="ADAM.StoreData.webExchangeRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة انشاء طلب صرف" Font-Size="X-Large" Font-Underline="True"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>

            <td>
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" /></td>

            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="data">
        <table class="menu">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Text="نوع طلب الصرف" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlExchangeRequestType" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlExchangeRequestType_SelectedIndexChanged">
                        <asp:ListItem Text="----" Value="0"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف خامات" Value="8"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف بضاعة" Value="9"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف تالف" Value="10"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف هدايا" Value="11"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف عهدة" Value="12"></asp:ListItem>
                        <asp:ListItem Text="طلب منتج تام" Value="17"></asp:ListItem>
                        <asp:ListItem Text="صرف لاسباب اخري" Value="22"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left"></td>
                <td style="text-align: center"></td>
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
                    <asp:DropDownList ID="ddlItemType" runat="server" CssClass="ddl" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Text="رقم طلب الصرف" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtExchangeRequestNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox></td>
                <td style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Text="تاريخ طلب الصرف" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label6" runat="server" Text="خط أنتاج" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlProdctionLine" runat="server" CssClass="ddl" DataSourceID="dbProdctionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList></td>
                <td style="text-align: left">
                    <asp:Label ID="Label7" runat="server" Text="العميل" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlClient" runat="server" CssClass="ddl" DataSourceID="dbClient" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
                    <asp:Button ID="btnClient" runat="server" Text="!!" OnClick="btnClient_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvExchangeRequestData" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbExchangeRequestsData">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
                <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
                <asp:BoundField DataField="UnitName" HeaderText="الوحدة" SortExpression="UnitName" />
                <asp:BoundField DataField="LimitQty" HeaderText="الحد الادني" SortExpression="LimitQty" />
                <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
                <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
                <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
                <asp:BoundField DataField="Specification" HeaderText="الوصف" SortExpression="Specification" />
                <asp:BoundField DataField="Qty" HeaderText="الكمية المصروفة" SortExpression="Qty" />
                <asp:BoundField DataField="FreeQty" HeaderText="Tester" SortExpression="FreeQty" />
                <asp:BoundField DataField="Bounce" HeaderText="Bounce" SortExpression="Bounce" />
                <asp:BoundField DataField="productionLineName" HeaderText="خط انتاج" SortExpression="productionLineName" />
                <asp:BoundField DataField="Note" HeaderText="ملاحظات" SortExpression="Note" />
            </Columns>
        </asp:GridView>
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
                <td style="">الرصيد المتاح</td>
                <td style="">Tester</td>
                <td style="">الكمية</td>
                <td style="">Tester</td>
                <td style="">Bounce</td>
                <td style="">ملاحظات</td>
                <td style="">أضافة صنف</td>
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
                    <asp:DropDownList ID="ddlItemColor" CssClass="ddl" Width="100px"
                        runat="server" DataSourceID="dbItemColor" DataTextField="ColorName" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="ddlItemColor_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="">
                    <asp:Label ID="lblItemstatus" runat="server"></asp:Label></td>
                <td style="">
                    <asp:Label ID="lblCurrentBalance" runat="server"></asp:Label></td>
                <td style="">
                    <asp:Label ID="lblFreeQty" runat="server"></asp:Label></td>
                <td style="">
                    <asp:TextBox ID="txtQty" Width="75px" CssClass="txt" runat="server"></asp:TextBox>
                </td>
                <td style="">
                    <asp:TextBox ID="txtFreeQty" Width="75px" CssClass="txt" runat="server"></asp:TextBox>
                    <asp:CheckBox ID="chkPrice" runat="server" />
                </td>
                <td style="">
                    <asp:TextBox ID="txtBounce" Width="75px" CssClass="txt" runat="server"></asp:TextBox>
                </td>
                <td style="">
                    <asp:TextBox ID="txtExchangeRequestNote" TextMode="MultiLine" CssClass="txt" runat="server"></asp:TextBox></td>
                <td style="">
                    <asp:ImageButton ID="btnSaveOrderItem" runat="server" ImageUrl="~/Image/Save.png" Width="30px" Height="30px" ToolTip="حفظ" OnClick="btnSaveExchangeRequerstItem_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <asp:SqlDataSource ID="dbExchangeRequestsData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS UnitName, Items.LimitQty, SexData.Sex, ItemStatus.ItemStatus, ItemColor.ColorName, Items.Specification, ExchangeRequestDetailsData.Qty, ExchangeRequestDetailsData.Note, ExchangeRequestDetailsData.ExchangeRequestHeaderDataId, ExchangeRequestDetailsData.FreeQty, ProductionLine.Id AS ProductionLineId, ProductionLine.productionLineName, ExchangeRequestDetailsData.Bounce FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ExchangeRequestDetailsData ON Items.Id = ExchangeRequestDetailsData.ItemId INNER JOIN ItemColor ON ExchangeRequestDetailsData.ItemColorId = ItemColor.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id WHERE (ExchangeRequestDetailsData.ExchangeRequestHeaderDataId = @ExchangeRequestHeaderDataId) ">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfId" DefaultValue="0" Name="ExchangeRequestHeaderDataId" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dbDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Department"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbProdctionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
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
                    <asp:SqlDataSource ID="dbClient" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '  ' + LastName AS Name FROM ClientData"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbItem" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items WHERE (ItemTypeId = @ItemTypeId) AND (ProductionLineId = @ProductionLineId) ORDER BY Name">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="ddlProdctionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS ColorName UNION SELECT ItemColor.Id, ItemColor.ColorName FROM ItemColor INNER JOIN ItemColorSelected ON ItemColor.Id = ItemColorSelected.ItemColorId WHERE (ItemColorSelected.ItemId = @ItemId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlItemName" DefaultValue="0" Name="ItemId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:HiddenField ID="hfId" Value="0" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfPurchaseDetailsId" Value="0" runat="server"></asp:HiddenField>
                </td>
            </tr>
        </table>
    </div>
    <div runat="server" id="Clients" visible="false">
        <asp:GridView ID="gvClient" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbgvClient" OnSelectedIndexChanged="gvClient_SelectedIndexChanged" DataKeyNames="Id" OnRowDataBound="gvClient_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="كود الموظف" SortExpression="Code" />
                <asp:BoundField DataField="Name" HeaderText="الموظف" ReadOnly="True" SortExpression="Name" />
                <asp:BoundField DataField="FirstPhone" HeaderText="رقم التليفون" SortExpression="FirstPhone" />
                <asp:BoundField DataField="FirstMobile" HeaderText="الموبايل" SortExpression="FirstMobile" />
                <asp:BoundField DataField="Address" HeaderText="العنوان" SortExpression="Address" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbgvClient" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Code, FirstName + ' ' + LastName AS Name, FirstPhone, FirstMobile, Address, Id FROM ClientData"></asp:SqlDataSource>
    </div>
</asp:Content>
