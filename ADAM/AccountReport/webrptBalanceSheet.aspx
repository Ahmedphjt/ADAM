<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webrptBalanceSheet.aspx.cs" Inherits="ADAM.AccountReport.webrptBalanceSheet" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير الميزانية العمومية" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="table">
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="من تاريخ" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFromDate" CssClass="txt" TextMode="Date" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="الي تاريخ" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtToDate" CssClass="txt" TextMode="Date" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="أعداد تقرير الميزانية العمومية:" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="أختر المستوي" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAccountLevel" runat="server">
                    <asp:ListItem Text="---" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="المستوي الاول" Value="0"></asp:ListItem>
                    <asp:ListItem Text="المستوي الثاني" Value="1"></asp:ListItem>
                    <asp:ListItem Text="المستوي الثالث" Value="2"></asp:ListItem>
                    <asp:ListItem Text="المستوي الرابع" Value="3"></asp:ListItem>
                    <asp:ListItem Text="المستوي الخامس" Value="4"></asp:ListItem>
                    <asp:ListItem Text="المستوي السادس" Value="5"></asp:ListItem>
                    <asp:ListItem Text="المستوي السابع" Value="6"></asp:ListItem>
                    <asp:ListItem Text="المستوي الثامن" Value="7"></asp:ListItem>
                    <asp:ListItem Text="المستوي التاسع" Value="8"></asp:ListItem>
                    <asp:ListItem Text="المستوي العاشر" Value="9"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>
