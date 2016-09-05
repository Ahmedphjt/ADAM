<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webInsertFirstBalance.aspx.cs" Inherits="ADAM.Account.webInsertFirstBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة القيود اليومية" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="divData">
        <table class="gv" style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label4" runat="server" Text="رقم الحساب" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label5" runat="server" Text="الحساب" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label7" runat="server" Text="مدين" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label8" runat="server" Text="دائن" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label10" runat="server" Text="البيان" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center"></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:TextBox ID="txtAccountNo" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" AutoPostBack="true" runat="server" CssClass="txt" Width="100px" OnTextChanged="txtAccountNo_TextChanged"></asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlAccountName" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbAccount" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlAccountName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetAllAccount" CssClass="btn" runat="server" Text="--" OnClick="btnGetAllAccount_Click" />
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtDebit" runat="server" Width="100px" CssClass="txt">0</asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtCredit" runat="server" Width="100px" CssClass="txt">0</asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvJournalDetails" Width="100%" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbJournal" DataKeyNames="JournalDetailId">
            <Columns>
                <asp:BoundField DataField="AccountCode" HeaderText="رقم الحساب" SortExpression="AccountCode" />
                <asp:BoundField DataField="AccountName" HeaderText="الحساب" SortExpression="AccountName" />
                <asp:BoundField DataField="Debit" HeaderText="مدين" SortExpression="Debit" />
                <asp:BoundField DataField="Credit" HeaderText="دائن" SortExpression="Credit" />
                <asp:BoundField DataField="Notes" HeaderText="ملاحظات او البيان" SortExpression="Notes" />
            </Columns>
        </asp:GridView>
        <br />
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="hfJournalHeaderId" Value="0" runat="server" />
                </td>
                <td>
                    <asp:SqlDataSource ID="dbJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Account.AccountCode, Account.AccountName, JournalDetail.Debit, JournalDetail.Credit, JournalDetail.CostCenterId, JournalDetail.Notes, JournalDetail.Id AS JournalDetailId FROM JournalDetail INNER JOIN Account ON JournalDetail.AccountId = Account.Id INNER JOIN JournalHeader ON JournalDetail.JournalId = JournalHeader.Id WHERE (JournalHeader.JournalType = 8)">
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbJournalType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS JournalTypeName UNION SELECT Id, JournalTypeName FROM JournalType WHERE (Id = 1) OR (Id = 2) OR (Id = 3) OR (Id = 4) OR (Id = 5)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbgvAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT AccountCode, AccountName, Id FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>                    
                </td>
                <td></td>
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
    </div>
</asp:Content>