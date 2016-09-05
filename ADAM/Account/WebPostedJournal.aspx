<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="WebPostedJournal.aspx.cs" Inherits="ADAM.Account.WebPostedJournal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة ترحيل القيود اليومية" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
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
    <table>
        <tr>
            <td> <asp:Label ID="Label2" runat="server" Text="نوع القيد :" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label></td>
            <td><asp:DropDownList ID="ddlJournaType" CssClass="ddl" runat="server" DataSourceID="dbJournalType" DataTextField="JournalTypeName" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="ddlJournaType_SelectedIndexChanged">
                    </asp:DropDownList></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvJorunalDetails" Width="100%" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbgvJornalDetails">
        <AlternatingRowStyle BackColor="#00CCFF" />
        <Columns>
            <asp:BoundField DataField="AccountName" HeaderText="الحساب" SortExpression="AccountName" />
            <asp:BoundField DataField="Debit" HeaderText="مدين" SortExpression="Debit" />
            <asp:BoundField DataField="Credit" HeaderText="دائن" SortExpression="Credit" />
            <asp:BoundField DataField="Notes" HeaderText="ملاحظات" SortExpression="Notes" />
            <asp:BoundField DataField="CostCenterName" HeaderText="مركز التكلفة" SortExpression="CostCenterName" />
        </Columns>
        <RowStyle BackColor="#33CCCC" Font-Bold="True" Font-Overline="False" Font-Underline="False" />
    </asp:GridView>

    <br />
    <hr />
    <br />

    <asp:GridView ID="gvJorunal" Width="100%" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbgvJournal" DataKeyNames="JournalHeaderId" OnSelectedIndexChanged="gvJorunal_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="JournalCode" HeaderText="رقم القيد" SortExpression="JournalCode" />
            <asp:BoundField DataField="JournalDate" HeaderText="تاريخ القيد" SortExpression="JournalDate" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="Note" HeaderText="ملاحظات" SortExpression="Note" />
            <asp:CommandField SelectText="عرض القيد" ShowSelectButton="True" />
        </Columns>
        <SelectedRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <br />
    <table>
        <tr>
            <td>

                <asp:SqlDataSource ID="dbgvJornalDetails" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT JournalDetail.Id AS JournalDetailId, JournalDetail.JournalId, JournalDetail.AccountId, JournalDetail.Debit, JournalDetail.Credit, JournalDetail.CostCenterId, JournalDetail.Notes, JournalHeader.Id AS JournalHeaderId, JournalHeader.JournalCode, JournalHeader.JournalDate, JournalHeader.JournalType, JournalHeader.Note, JournalHeader.Posted, JournalHeader.DocId, JournalType.Id AS JournalTypeId, JournalType.JournalTypeName, Account.AccountName, CostCenter.CostCenterName FROM JournalDetail INNER JOIN JournalHeader ON JournalDetail.JournalId = JournalHeader.Id INNER JOIN JournalType ON JournalHeader.JournalType = JournalType.Id INNER JOIN Account ON JournalDetail.AccountId = Account.Id INNER JOIN CostCenter ON JournalDetail.CostCenterId = CostCenter.Id WHERE (JournalHeader.Id = @JournalHeaderId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfJournalHeaderId" DefaultValue="0" Name="JournalHeaderId" PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </td>
            <td>
                <asp:SqlDataSource ID="dbgvJournal" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT JournalHeader.Id AS JournalHeaderId, JournalHeader.JournalCode, JournalHeader.JournalDate, JournalHeader.JournalType, JournalHeader.Note, JournalHeader.Posted, JournalHeader.DocId, JournalType.Id AS JournalTypeId, JournalType.JournalTypeName FROM JournalHeader INNER JOIN JournalType ON JournalHeader.JournalType = JournalType.Id WHERE (JournalType.Id = @JournalType) AND (JournalHeader.Posted = 0)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlJournaType" DefaultValue="0" Name="JournalType" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:HiddenField ID="hfJournalHeaderId" Value="0" runat="server" />
            </td>
            <td><asp:SqlDataSource ID="dbJournalType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS JournalTypeName UNION SELECT Id, JournalTypeName FROM JournalType WHERE (Id = 1) OR (Id = 2) OR (Id = 3) OR (Id = 4) OR (Id = 5)"></asp:SqlDataSource></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
