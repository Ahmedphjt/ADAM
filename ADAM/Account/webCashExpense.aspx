<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webCashExpense.aspx.cs" Inherits="ADAM.Account.webCashExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة صرف نقدية" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="divData">
        <table class="menu">
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="رقم سند الصرف" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDocNo" TextMode="Number" ToolTip="ادخل رقم سند الصرف ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="تاريخ سند الصرف" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDocDate" TextMode="Date" ToolTip="ادخل تاريخ سند الصرف" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="رقم الصندوق" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHeaderAccountNo" TextMode="Number" ToolTip="ادخل رقم الصندوق ويجب ان يكون رقماً" AutoPostBack="true" runat="server" CssClass="txt" Width="100px" OnTextChanged="txtHeaderAccountNo_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="الصندوق" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlHeaderAccountName" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbHeaderAccount" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlHeaderAccountName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetAllAccountforHeader" CssClass="btn" runat="server" Text="--" OnClick="btnGetAllAccountforHeader_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="المبلغ" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtMasterAccountQty" ToolTip="ادخل المبلغ ويجب ان يكون رقماً" runat="server" CssClass="txt">0</asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="مركز التكلفة" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlHeaderCostCenter" CssClass="ddl" Width="200px" runat="server" DataSourceID="dbHeaderCostCenter" DataTextField="CostCenter" DataValueField="id">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetHeaderCostCenter" CssClass="btn" runat="server" Text="--" OnClick="btnGetHeaderCostCenter_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="يصرف الي" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHeaderNote" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <br />
        <table class="gv" style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label7" runat="server" Text="رقم الحساب" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label8" runat="server" Text="الحساب" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label10" runat="server" Text="المبلغ" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label11" runat="server" Text="مركز التكلفة" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label12" runat="server" Text="البيان" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: center"></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:TextBox ID="txtDetailsAccountNo" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" AutoPostBack="true" runat="server" CssClass="txt" Width="100px" OnTextChanged="txtDetailsAccountNo_TextChanged"></asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlDetailsAccountName" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbDetailsAccount" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlDetailsAccountName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetAllAccountForDetails" CssClass="btn" runat="server" Text="--" OnClick="btnGetAllAccountForDetails_Click" />
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtDetailsAccountQty" runat="server" Width="100px" CssClass="txt">0</asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlDetailsCostCenter" CssClass="ddl" Width="200px" runat="server" DataSourceID="dbDetailsCostCenter" DataTextField="CostCenter" DataValueField="id">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetDetailsCostCenter" CssClass="btn" runat="server" Text="--" OnClick="btnGetDetailsCostCenter_Click" />
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtDetailsNote" TextMode="MultiLine" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvDocDetails" Width="100%" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbDocDetails">
            <Columns>
                <asp:BoundField DataField="AccountCode" HeaderText="رقم الحساب" SortExpression="AccountCode" />
                <asp:BoundField DataField="AccountName" HeaderText="الحساب" SortExpression="AccountName" />
                <asp:BoundField DataField="DetailsAccountQty" HeaderText="المبلغ" SortExpression="DetailsAccountQty" />
                <asp:BoundField DataField="CostCenterName" HeaderText="مركز التكلفة" SortExpression="CostCenterName" />
                <asp:BoundField DataField="Notes" HeaderText="ملاحظات او البيان" SortExpression="Notes" />
            </Columns>
        </asp:GridView>
        <br />
        <table>
            <tr>
                <td>
                    <asp:SqlDataSource ID="dbHeaderAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbHeaderCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS id, '---' AS CostCenter UNION SELECT Id,  CostCenterName AS CostCenter FROM CostCenter WHERE (CostCenterType = 1 Or Id = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbDocDetails" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT DocumentDetails.Id, DocumentDetails.DocHeaderId, DocumentDetails.AccountId, DocumentDetails.CostCenterId, DocumentDetails.DetailsAccountQty, DocumentDetails.Notes, CostCenter.CostCenterCode, CostCenter.CostCenterName, Account.AccountName, Account.AccountCode FROM DocumentDetails INNER JOIN Account ON DocumentDetails.AccountId = Account.Id INNER JOIN CostCenter ON DocumentDetails.CostCenterId = CostCenter.Id WHERE (DocumentDetails.DocHeaderId = @DocHeaderId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfDocHeaderId" DefaultValue="0" Name="DocHeaderId" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:HiddenField ID="hfDocHeaderId" Value="0" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="hfControlName" Value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="dbDetailsAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbDetailsCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS id, '---' AS CostCenter UNION SELECT Id,  CostCenterName AS CostCenter FROM CostCenter WHERE (CostCenterType = 1 Or Id = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbgvAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT AccountCode, AccountName, Id FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbgvCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Id, CostCenterCode, CostCenterName, CostCenterType FROM CostCenter WHERE (CostCenterType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:HiddenField ID="hfJournalId" Value="0" runat="server" />
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
