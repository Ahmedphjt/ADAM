<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webAccountHelper.aspx.cs" Inherits="ADAM.Account.webAccountHelper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة حسابات أساسية" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" />
            </td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="ترحيل" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="divData">
        <table>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="حساب المبيعات" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtSalesAccount" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtSalesAccount_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSalesAccount" runat="server" Text="_" OnClick="btnSalesAccount_Click" />
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label2" runat="server" Text="اسم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSalesAccount" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbSalesAccount" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlSalesAccount_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbSalesAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Text="حساب تكلفة المبيعات" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtSalesCost" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtSalesCost_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSalesCost" runat="server" Text="_" OnClick="btnSalesCost_Click" />
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label4" runat="server" Text="اسم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSalesCost" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbSalesCost" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlSalesCost_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbSalesCost" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label5" runat="server" Text="حساب مردودات المبيعات" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtReturnSalesAccount" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtReturnSalesAccount_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnReturnSalesAccount" runat="server" Text="_" OnClick="btnReturnSalesAccount_Click" />
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label6" runat="server" Text="اسم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlReturnSalesAccount" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbReturnSalesAccount" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlReturnSalesAccount_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbReturnSalesAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label7" runat="server" Text="حساب الخصم المسموح به" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtAllowedDiscount" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtAllowedDiscount_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnAllowedDiscount" runat="server" Text="_" OnClick="btnAllowedDiscount_Click" />
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label8" runat="server" Text="اسم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlAllowedDiscount" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbAllowedDiscount" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlAllowedDiscount_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbAllowedDiscount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label9" runat="server" Text="حساب مردودات السنوات السابقة" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtReturnOldeYears" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtReturnOldeYears_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnReturnOldeYears" runat="server" Text="_" OnClick="btnReturnOldeYears_Click" />
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label10" runat="server" Text="اسم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlReturnOldeYears" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbReturnOldeYears" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlReturnOldeYears_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbReturnOldeYears" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label11" runat="server" Text="حساب تكلفة مردودات السنوات" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtRetuenYearsCost" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtRetuenYearsCost_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnRetuenYearsCost" runat="server" Text="_" OnClick="btnRetuenYearsCost_Click" />
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label12" runat="server" Text="اسم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlRetuenYearsCost" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbRetuenYearsCost" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlRetuenYearsCost_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbRetuenYearsCost" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label13" runat="server" Text="حساب مردودات المشتريات" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtReturnPurchase" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtReturnPurchase_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnReturnPurchase" runat="server" Text="_" OnClick="btnReturnPurchase_Click" />
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label14" runat="server" Text="اسم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlReturnPurchase" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbReturnPurchase" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlReturnPurchase_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbReturnPurchase" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label15" runat="server" Text="حساب الخصم المكتسب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtWinDiscount" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtWinDiscount_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnWinDiscount" runat="server" Text="_" OnClick="btnWinDiscount_Click" />
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label16" runat="server" Text="اسم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlWinDiscount" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbWinDiscount" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlWinDiscount_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbWinDiscount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label17" runat="server" Text="حساب الكميات المجانية" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtFreeQty" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtFreeQty_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnFreeQty" runat="server" Text="_" OnClick="btnFreeQty_Click" />
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label18" runat="server" Text="اسم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlFreeQty" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbFreeQty" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlFreeQty_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbFreeQty" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div runat="server" id="divAccount" visible="false">
        <asp:GridView ID="gvAcccount" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbgvAccount" DataKeyNames="Id" OnSelectedIndexChanged="gvAcccount_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="AccountCode" HeaderText="رقم الحساب" SortExpression="AccountCode" />
                <asp:BoundField DataField="AccountName" HeaderText="الحساب" SortExpression="AccountName" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <table>
            <tr>
                <td>
        <asp:SqlDataSource ID="dbgvAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT AccountCode, AccountName, Id FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>
        <asp:HiddenField ID="hfControlName" Value="0" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
