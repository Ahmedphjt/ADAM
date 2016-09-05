<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webJournal.aspx.cs" Inherits="ADAM.Account.webJournal" %>

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
        <table class="table" style="width:35%">
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="رقم القيد :" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtjournalCode" TextMode="Number" ToolTip="ادخل رقم القيد ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="تاريخ القيد :" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtjournalDate" TextMode="Date" ToolTip="ادخل تاريخ القيد ويجب ان يكون تاريخ" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4"><hr /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="نوع القيد :" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlJournaType" CssClass="ddl" runat="server" DataSourceID="dbJournalType" DataTextField="JournalTypeName" DataValueField="Id">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="ملاحظات :" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtJournalNote" TextMode="MultiLine" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
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
                    <asp:Label ID="Label9" runat="server" Text="مركز التكلفة" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
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
                    <asp:DropDownList ID="ddlCostCenter" CssClass="ddl" Width="200px" runat="server" DataSourceID="dbCostcenter" DataTextField="CostCenter" DataValueField="id">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetCostCenter" CssClass="btn" runat="server" Text="--" OnClick="btnGetCostCenter_Click" />
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
                <asp:BoundField DataField="CostCenterName" HeaderText="مركز التكلفة" SortExpression="CostCenterName" />
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
                    <asp:SqlDataSource ID="dbJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Account.AccountCode, Account.AccountName, JournalDetail.Debit, JournalDetail.Credit, JournalDetail.CostCenterId, JournalDetail.Notes, CostCenter.CostCenterName, JournalDetail.Id AS JournalDetailId FROM JournalDetail INNER JOIN Account ON JournalDetail.AccountId = Account.Id INNER JOIN CostCenter ON JournalDetail.CostCenterId = CostCenter.Id WHERE (JournalDetail.JournalId = @JournalId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfJournalHeaderId" DefaultValue="0" Name="JournalId" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbJournalType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS JournalTypeName UNION SELECT Id, JournalTypeName FROM JournalType WHERE (Id = 1) OR (Id = 2) OR (Id = 3) OR (Id = 4) OR (Id = 5)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbgvAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT AccountCode, AccountName, Id FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbgvCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Id, CostCenterCode, CostCenterName, CostCenterType FROM CostCenter WHERE (CostCenterType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbCostcenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS id, '---' AS CostCenter UNION SELECT Id,  CostCenterName AS CostCenter FROM CostCenter WHERE (CostCenterType = 1 Or Id = 1)"></asp:SqlDataSource>
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
    <br />
    <div runat="server" id="divCostCenter" visible="false">
        <asp:GridView ID="gvCostCenter" CssClass="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="dbgvCostCenter" OnSelectedIndexChanged="gvCostCenter_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="CostCenterCode" HeaderText="رقم مركز التكلفة" SortExpression="CostCenterCode" />
                <asp:BoundField DataField="CostCenterName" HeaderText="مركز التكلفة" SortExpression="CostCenterName" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
