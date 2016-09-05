<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webRefluxOrder.aspx.cs" Inherits="ADAM.StoreData.webRefluxOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة انشاء طلب أرتجاع" Font-Size="X-Large" Font-Underline="True"></asp:Label></h1>
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
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="dvInsertData">
        <table class="menu">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label8" runat="server" Text="نوع الارتجاع" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlRefluxType" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlRefluxType_SelectedIndexChanged">
                        <asp:ListItem Text="----" Value="0"></asp:ListItem>
                       <%-- <asp:ListItem Text="علي طلب شراء" Value="1"></asp:ListItem>--%>
                        <asp:ListItem Text="علي طلب صرف" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label6" runat="server" Text="رقم طلب الارتجاع" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtRefluxNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblItemType" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlItemType" runat="server" CssClass="ddl" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblExchangeOrderType" runat="server" Text="نوع طلب الصرف" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlExchangeRequestType" runat="server" CssClass="ddl">
                        <asp:ListItem Text="----" Value="0"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف خامات" Value="8"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف بضاعة" Value="9"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف تالف" Value="10"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف هدايا" Value="11"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف عهدة" Value="12"></asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblOrderName" runat="server" Text="رقم أذن الصرف" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtPurchaseOrExchangeOrderNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:Button ID="btnGetExchageNo" runat="server" Text="!!" OnClick="btnGetExchageNo_Click" />
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblSupplierName" Visible="false" runat="server" Text="المورد" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlSupplierName" Visible="False" runat="server" CssClass="ddl" DataSourceID="dbSupplier" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label14" runat="server" Text="الادارة" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="ddl" DataSourceID="dbDepartment" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label15" runat="server" Text="القسم" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="ddl" DataSourceID="dbDivision" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList></td>
            </tr>
            <tr>

                <td style="text-align: left">
                    <asp:Label ID="Label4" runat="server" Text="مندوب الادارة الطالبة" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="ddl" DataSourceID="dbEmployee" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblDate" runat="server" Text="تاريخ طلب الصرف" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtDate" TextMode="Date" runat="server" CssClass="txt" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>

                <td style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Text="سبب الارتجاع" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtReason" TextMode="MultiLine" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Text="تاريخ الارتجاع" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtRefluxDate" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvExchangeRequestData" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbExchangeRequestsData" DataKeyNames="ExchangeRequestDetailsDataId" OnSelectedIndexChanged="gvExchangeRequestData_SelectedIndexChanged">
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
                <asp:BoundField DataField="Note" HeaderText="ملاحظات" SortExpression="Note" />
                <asp:BoundField DataField="IncommingOrderNo" HeaderText="رقم أذن الوارد" SortExpression="IncommingOrderNo" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
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
                <td style="">الكمية</td>
                <td style="">ك. المرتجعة</td>
                <td style="">Tester</td>
                <td style="">مرتجع Tester</td>
                <td style="">مرتجع Bounce</td>
                <td style="">حفظ</td>

            </tr>
            <tr>
                <td style="">
                    <asp:TextBox ID="txtItemCode" Width="75px" CssClass="txt" runat="server"></asp:TextBox></td>
                <td style="">
                    <asp:DropDownList ID="ddlItemName" Width="200" runat="server" CssClass="ddl" DataSourceID="dbItem" DataTextField="Name" DataValueField="Id" Enabled="False"></asp:DropDownList></td>
                <td style="">
                    <asp:Label ID="lblItemUnit" runat="server"></asp:Label></td>
                <td style="">
                    <asp:Label ID="lblLimitQty" runat="server"></asp:Label></td>
                <td style="">
                    <asp:Label ID="lblSex" runat="server"></asp:Label></td>
                <td style="">
                    <asp:DropDownList ID="ddlItemColor" Width="150px" CssClass="ddl" runat="server" DataSourceID="dbItemColor" DataTextField="ColorName" DataValueField="Id" Enabled="False"></asp:DropDownList></td>
                <td style="">
                    <asp:Label ID="lblItemstatus" runat="server"></asp:Label></td>
                <td style="">
                    <asp:TextBox ID="txtQty" Width="100px" CssClass="txt" runat="server" Enabled="False"></asp:TextBox></td>
                <td style="">
                    <asp:TextBox ID="txtRefluxQty" Width="100px" CssClass="txt" runat="server"></asp:TextBox></td>
                <td style="">
                    <asp:TextBox ID="txtFreeQty" Width="100px" CssClass="txt" runat="server" Enabled="False"></asp:TextBox></td>
                <td style="">
                    <asp:TextBox ID="txtFreeRefluxQty" Width="100px" CssClass="txt" runat="server"></asp:TextBox></td>
                <td style="">
                    <asp:TextBox ID="txtBouncefluxQty" Width="100px" CssClass="txt" runat="server"></asp:TextBox></td>
                <td style="">
                    <asp:ImageButton ID="btnSaveOrderItem" runat="server" ImageUrl="~/Image/Save.png" Width="30px" Height="30px" ToolTip="حفظ" OnClick="btnSaveOrderItem_Click" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvReflux" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbRefluxDetailsData">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
                <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
                <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
                <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
                <asp:BoundField DataField="RefluxQty" HeaderText="الكمية المرتجعة" SortExpression="RefluxQty" />
                <asp:BoundField DataField="RefluxFreeQty" HeaderText="الكمية المجانية المرتجعة" SortExpression="RefluxFreeQty" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbRefluxDetailsData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemColor.ColorName, RefluxDetailsData.RefluxQty, RefluxDetailsData.RefluxFreeQty, ItemUnit.Name AS ItemUnitName, RefluxDetailsData.RefluxHeaderId FROM RefluxDetailsData INNER JOIN Items ON RefluxDetailsData.ItemId = Items.Id INNER JOIN ItemColor ON RefluxDetailsData.ItemColorId = ItemColor.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id WHERE (RefluxDetailsData.RefluxHeaderId = @RefluxHeaderId)">
            <SelectParameters>
                <asp:ControlParameter ControlID="RefluxHeaderId" DefaultValue="0" Name="RefluxHeaderId" PropertyName="Value" />
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
                    <asp:SqlDataSource ID="dbPurchaseDetailsData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT ExchangeRequestHeaderData.ExchangeRequestNo, ExchangeRequestHeaderData.ExchangeRequestDate, Items.Name, ExchangeRequestDetailsData.ExchangeRequestOrder, ExchangeRequestHeaderData.OrderType, ExchangeRequestHeaderData.ItemTypeId FROM ExchangeRequestDetailsData INNER JOIN ExchangeRequestHeaderData ON ExchangeRequestDetailsData.ExchangeRequestHeaderDataId = ExchangeRequestHeaderData.Id INNER JOIN Items ON ExchangeRequestDetailsData.ItemId = Items.Id WHERE (ExchangeRequestHeaderData.OrderType = @OrderType) AND (ExchangeRequestHeaderData.ItemTypeId = @ItemTypeId) ORDER BY ExchangeRequestDetailsData.ExchangeRequestOrder DESC">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlExchangeRequestType" DefaultValue="0" Name="OrderType" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="hfPurchaseHeaderId" Value="0" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfPurchaseDetailsId" Value="0" runat="server"></asp:HiddenField>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS ColorName UNION SELECT Id, ColorName FROM ItemColor"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbExchangeRequestsData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS UnitName, Items.LimitQty, SexData.Sex, ItemStatus.ItemStatus, ItemColor.ColorName, Items.Specification, ExchangeRequestDetailsData.Qty, ExchangeRequestDetailsData.Note, ExchangeRequestDetailsData.ExchangeRequestHeaderDataId, ExchangeRequestDetailsData.Id AS ExchangeRequestDetailsDataId, ExchangeRequestDetailsData.FreeQty, ExchangeRequestDetailsData.Bounce, ExchangeRequestDetailsData.IncommingOrderId, ExchangeRequestDetailsData.IncommingOrderNo FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ExchangeRequestDetailsData ON Items.Id = ExchangeRequestDetailsData.ItemId INNER JOIN ItemColor ON ExchangeRequestDetailsData.ItemColorId = ItemColor.Id WHERE (ExchangeRequestDetailsData.ExchangeRequestHeaderDataId = @ExchangeRequestHeaderDataId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfExchangeRequestHeaderId" DefaultValue="0" Name="ExchangeRequestHeaderDataId" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="hfExchangeRequestHeaderId" Value="0" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfExchangeRequestDetailsId" Value="0" runat="server"></asp:HiddenField>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbItem" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items WHERE (ItemTypeId = @ItemTypeId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbSupplier" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '   ' + LastName AS Name FROM SupplierData"></asp:SqlDataSource>
                    <asp:HiddenField ID="RefluxHeaderId" Value="0" runat="server"></asp:HiddenField>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvExchangeRequestOrder" runat="server" Visible="False">
        <asp:GridView ID="gvExchangeRequestOrder" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbPurchaseDetailsData" OnSelectedIndexChanged="gvExchangeRequestOrder_SelectedIndexChanged" >
            <Columns>
                <asp:BoundField DataField="ExchangeRequestOrder" HeaderText="رقم اذن الصرف" SortExpression="ExchangeRequestOrder" />
                <asp:BoundField DataField="ExchangeRequestNo" HeaderText="رقم الطلب" SortExpression="ExchangeRequestNo" />
                <asp:BoundField DataField="ExchangeRequestDate" HeaderText="تاريخ الطلب" SortExpression="ExchangeRequestDate" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
                <asp:CommandField SelectText="اختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </div>
     <asp:HiddenField ID="hfIncommingOrder" Value="0" runat="server"></asp:HiddenField>
</asp:Content>
