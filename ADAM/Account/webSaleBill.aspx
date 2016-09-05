<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webSaleBill.aspx.cs" Inherits="ADAM.Account.webSaleBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة فاتورة بيع" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="divData">
        <table class="menu">
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="نوع طلب الصرف" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlExchangeRequestType" CssClass="ddl" runat="server">
                        <asp:ListItem Text="---" Value="0"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف خامات" Value="10"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف بضاعة" Value="11"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف تالف" Value="12"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف هدايا" Value="13"></asp:ListItem>
                        <asp:ListItem Text="طلب صرف عهدة" Value="14"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <asp:HiddenField ID="hfExchangeRequestType" Value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="رقم طلب الصرف" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExchangeRequestNo" TextMode="Number" ToolTip="ادخل رقم طلب الصرف ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="تاريخ طلب الصرف" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExchangeRequestDate" TextMode="Date" Enabled="false" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="كود العميل" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtClientCode" Enabled="false" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="العميل" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlClient" CssClass="ddl" Width="250px" runat="server" DataSourceID="dbClient" DataTextField="Name" DataValueField="Id" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="رقم حساب الصندوق" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBoxNo" TextMode="Number" ToolTip="ادخل رقم حساب الصندوق ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtBoxNo_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="اسم الصندوق" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBoxName" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbBoxAccount" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlBoxName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetAllAccountforBox" CssClass="btn" runat="server" Text="--" OnClick="btnGetAllAccountforBox_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="رقم حساب مركز التكلفة" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCostCenter" TextMode="Number" ToolTip="ادخل رقم حساب مركز التكلفة ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtCostCenter_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="اسم المركز" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCostCenterName" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbCostCenter" DataTextField="CostCenter" DataValueField="id" OnSelectedIndexChanged="ddlCostCenterName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetAllAccountforCostCenter" CssClass="btn" runat="server" Text="--" OnClick="btnGetAllAccountforCostCenter_Click" />
                </td>
            </tr>
        </table>
                <br />
        <asp:GridView ID="gvExchangeData" Width="100%" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbgvExchangeData" DataKeyNames="IncommingOrderId">
            <Columns>
                <asp:BoundField DataField="ItemCode" HeaderText="كود الصنف" SortExpression="ItemCode" />
                <asp:BoundField DataField="ItemName" HeaderText="الصنف" SortExpression="ItemName" />
                <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
                <asp:BoundField DataField="Qty" HeaderText="الكمية" SortExpression="Qty" />
                <asp:BoundField DataField="InvoicePrice" HeaderText="سعر الوحدة" SortExpression="InvoicePrice" />
                <asp:BoundField DataField="FreeQty" HeaderText="Tester" SortExpression="FreeQty" />
                <asp:BoundField DataField="TInvoicePrice" HeaderText="Tester سعر" SortExpression="TInvoicePrice" />
            </Columns>
        </asp:GridView>
        <br />
        <table>
            <tr>
                <td colspan="4"><hr /></td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:Label ID="Label10" runat="server" Text="أجمالي الفاتورة" CssClass="lbl" Font-Bold="True" ForeColor="#FF3300"></asp:Label></td>
                <td>
                    <asp:Label ID="lblBillPrice" runat="server" Text="0" CssClass="lbl" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="dbClient" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '   ' + LastName AS Name FROM ClientData"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbBoxAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS id, '---' AS CostCenter UNION SELECT Id,  CostCenterName AS CostCenter FROM CostCenter WHERE (CostCenterType = 1 Or Id = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:HiddenField ID="hfExchangeheaderId" Value="0" runat="server" />
                </td>
                <td>
                    <asp:SqlDataSource ID="dbgvExchangeData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Name AS ItemName, ExchangeRequestDetailsData.Qty, ExchangeRequestDetailsData.FreeQty, ItemColor.ColorName, ExchangeRequestDetailsData.ExchangeRequestHeaderDataId, Items.Code AS ItemCode, ExchangeRequestPricing.TInvoicePrice, ExchangeRequestPricing.InvoicePrice, ExchangeRequestDetailsData.IncommingOrderId FROM ExchangeRequestDetailsData INNER JOIN ExchangeRequestPricing ON ExchangeRequestDetailsData.Id = ExchangeRequestPricing.ExchangeRequestDetailsId INNER JOIN Items ON ExchangeRequestDetailsData.ItemId = Items.Id INNER JOIN ItemColor ON ExchangeRequestDetailsData.ItemColorId = ItemColor.Id WHERE (ExchangeRequestDetailsData.ExchangeRequestHeaderDataId = @ExchangeRequestHeaderDataId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfExchangeheaderId" DefaultValue="0" Name="ExchangeRequestHeaderDataId" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
     <div runat="server" id="divAccount" visible="false">
        <asp:GridView ID="gvAcccount" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbgvAccount" DataKeyNames="Id" OnSelectedIndexChanged="gvAcccount_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="AccountCode" HeaderText="رقم الحساب" SortExpression="AccountCode" />
                <asp:BoundField DataField="AccountName" HeaderText="الحساب" SortExpression="AccountName" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
         <asp:SqlDataSource ID="dbgvAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT AccountCode, AccountName, Id FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
    </div>
    <br />
    <div runat="server" id="divCostCenter" visible="false">
        <asp:GridView ID="gvCostCenter" CssClass="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="dbgvCostCenter" OnSelectedIndexChanged="gvCostCenter_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="CostCenterCode" HeaderText="رقم مركز التكلفة" SortExpression="CostCenterCode" />
                <asp:BoundField DataField="CostCenterName" HeaderText="مركز التكلفة" SortExpression="CostCenterName" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbgvCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Id, CostCenterCode, CostCenterName, CostCenterType FROM CostCenter WHERE (CostCenterType = 1)"></asp:SqlDataSource>
    </div>
</asp:Content>
