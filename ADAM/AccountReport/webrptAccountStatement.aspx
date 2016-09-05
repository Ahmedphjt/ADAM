<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webrptAccountStatement.aspx.cs" Inherits="ADAM.AccountReport.webrptAccountStatement" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير كشف حساب" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="divData">
        <table class="menu">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Text="رقم الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtAccountCode" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtAccountCode_TextChanged"></asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="الحساب" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlAccount" runat="server" CssClass="txt" Width="150px" DataSourceID="dbAccount" DataTextField="AccountName" DataValueField="Id" OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    <asp:Button ID="btnAccount" runat="server" Text="_" OnClick="btnAccount_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label2" runat="server" Text="من تاريخ" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtbeginDate" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label4" runat="server" Text="الي تاريخ" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtEndDate" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:SqlDataSource ID="dbAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS AccountName UNION SELECT Id, AccountName FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td style="text-align: center" colspan="3">
                    <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            </tr>
        </table>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
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
</asp:Content>
