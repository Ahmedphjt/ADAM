<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webDisExchangeReqest.aspx.cs" Inherits="ADAM.StoreData.webDisExchangeReqest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة تخصيم طلب صرف" Font-Size="X-Large" Font-Underline="True"></asp:Label></h1>
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
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="InsertData">
        <table class="menu">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Text="نوع طلب الصرف" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlExchangeRequestType" runat="server" CssClass="ddl" AutoPostBack="True">
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
                <td style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Text="رقم طلب الصرف" CssClass="lbl"></asp:Label></td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtExchangeRequestNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:Button ID="btnGetExchangeNo" runat="server" Text="!!" OnClick="btnGetExchangeNo_Click" /></td>
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
                <td style="text-align: left">
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
                <td style="text-align: left">
                    <asp:TextBox ID="txtClientAddress" runat="server" CssClass="txt" Enabled="False"></asp:TextBox>
                </td>
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
                <asp:BoundField DataField="Qty" HeaderText="كمية طلب الصرف" SortExpression="Qty" />
                <asp:BoundField DataField="FreeQty" HeaderText="Tester" SortExpression="FreeQty" />
                <asp:BoundField DataField="Bounce" HeaderText="Bounce" SortExpression="Bounce" />
                <asp:BoundField DataField="productionLineName" HeaderText="خط انتاج" SortExpression="productionLineName" />
                <asp:BoundField DataField="Note" HeaderText="ملاحظات" SortExpression="Note" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbExchangeRequestsData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemUnit.Name AS UnitName, Items.LimitQty, SexData.Sex, ItemStatus.ItemStatus, ItemColor.ColorName, Items.Specification, ExchangeRequestDetailsData.Qty, ExchangeRequestDetailsData.Note, ExchangeRequestDetailsData.ExchangeRequestHeaderDataId, ExchangeRequestDetailsData.Id AS ExchangeRequestDetailsDataId, ExchangeRequestDetailsData.Status, ExchangeRequestDetailsData.FreeQty, ProductionLine.productionLineName, ExchangeRequestDetailsData.Bounce FROM Items INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ExchangeRequestDetailsData ON Items.Id = ExchangeRequestDetailsData.ItemId INNER JOIN ItemColor ON ExchangeRequestDetailsData.ItemColorId = ItemColor.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id WHERE (ExchangeRequestDetailsData.ExchangeRequestHeaderDataId = @ExchangeRequestHeaderDataId) AND (ExchangeRequestDetailsData.Status = 0)">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfId" DefaultValue="0" Name="ExchangeRequestHeaderDataId" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:GridView ID="gvItemMovement" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbItemMovement" DataKeyNames="ItemMovementId" OnSelectedIndexChanged="gvItemMovement_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
                <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
                <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
                <asp:BoundField DataField="CurrentBalance" HeaderText="الرصيد الحالي" SortExpression="CurrentBalance" ReadOnly="True" />
                <asp:BoundField DataField="CurrentAdditionalQty" HeaderText="Tester Balance" SortExpression="CurrentAdditionalQty" ReadOnly="True" />
                <asp:BoundField DataField="IncommingOrderNo" HeaderText="رقم اذن الوارد" SortExpression="IncommingOrderNo" />
                <asp:TemplateField HeaderText="الكمية المصروفة">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQtyOut" CssClass="txt" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tester Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtfreeQtyOut" CssClass="txt" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField SelectText="صرف" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbItemMovement" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Id, Items.Code, Items.Name, ItemMovement.MainQty, ItemMovement.MainQtyOut, ItemMovement.MainQty - ItemMovement.MainQtyOut AS CurrentBalance, ItemMovement.AdditionalQty, ItemMovement.AdditionalQtyOut, ItemMovement.AdditionalQty - ItemMovement.AdditionalQtyOut AS CurrentAdditionalQty, ItemMovement.IncommingOrderNo, ItemMovement.MovmentnameId, ItemMovement.Id AS ItemMovementId, ItemColor.ColorName, ItemColor.Id AS ItemColorId, ItemLocation.LocationName FROM Items INNER JOIN ItemMovement ON Items.Id = ItemMovement.ItemId INNER JOIN ItemColor ON ItemMovement.ItemColorId = ItemColor.Id INNER JOIN ItemLocation ON ItemMovement.LocatioId = ItemLocation.Id WHERE (ItemMovement.MovmentnameId = 6 OR ItemMovement.MovmentnameId = 20 OR ItemMovement.MovmentnameId = 14 OR ItemMovement.MovmentnameId = 16 OR ItemMovement.MovmentnameId = 3) AND (Items.Id = @ItemId) AND (ItemMovement.MainQtyOut &lt; ItemMovement.MainQty) AND (ItemColor.Id = @ItemColorId) OR (ItemMovement.MovmentnameId = 6 OR ItemMovement.MovmentnameId = 20 OR ItemMovement.MovmentnameId = 16 OR ItemMovement.MovmentnameId = 3) AND (Items.Id = @ItemId) AND (ItemColor.Id = @ItemColorId) AND (ItemMovement.AdditionalQty &gt; ItemMovement.AdditionalQtyOut)">
            <SelectParameters>
                <asp:ControlParameter ControlID="ItemId" DefaultValue="0" Name="ItemId" PropertyName="Value" />
                <asp:ControlParameter ControlID="hfItemColorId" DefaultValue="0" Name="ItemColorId" PropertyName="Value" />
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
                    <asp:HiddenField ID="ItemId" Value="0" runat="server"></asp:HiddenField>
                </td>
                <td>
                    <asp:HiddenField ID="hfExchangeRequestOrder" Value="0" runat="server" />
                   
                </td>
                <td>
                    <asp:SqlDataSource ID="dbItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS ColorName UNION SELECT Id, ColorName FROM ItemColor"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:HiddenField ID="hfId" Value="0" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfDetailsId" Value="0" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfItemColorId" Value="0" runat="server"></asp:HiddenField>
                </td>
            </tr>
        </table>
    </div>
    <div runat="server" id="ExchangeRequestNo" visible="false">
        <asp:GridView ID="gvExchangeRequestNo" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbExchangeRequestNo" OnSelectedIndexChanged="gvExchangeRequestNo_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="ExchangeRequestNo" HeaderText="رقم الطلب" SortExpression="ExchangeRequestNo" />
                <asp:BoundField DataField="ExchangeRequestDate" HeaderText="تاريخ الطلب" SortExpression="ExchangeRequestDate" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="Name" HeaderText="اسم الموظف" SortExpression="Name" ReadOnly="True" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbExchangeRequestNo" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT ExchangeRequestHeaderData.ExchangeRequestNo, ExchangeRequestHeaderData.ExchangeRequestDate, EmployeeData.FirstName + ' ' + EmployeeData.LastName AS Name, ExchangeRequestHeaderData.OrderType FROM ExchangeRequestHeaderData INNER JOIN EmployeeData ON ExchangeRequestHeaderData.EmpId = EmployeeData.Id WHERE (ExchangeRequestHeaderData.OrderType = @OrderType) ORDER BY ExchangeRequestHeaderData.ExchangeRequestNo DESC">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlExchangeRequestType" DefaultValue="0" Name="OrderType" PropertyName="SelectedValue" />
                
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>


