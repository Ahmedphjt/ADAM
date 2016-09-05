<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webrptJournal.aspx.cs" Inherits="ADAM.AccountReport.webrptJournal" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير القيد اليومي" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table class="table">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Text="نوع القيد" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlJournalType" runat="server" CssClass="txt" Width="150px" DataSourceID="dbJournalType" DataTextField="JournalTypeName" DataValueField="Id"></asp:DropDownList></td>
                <td></td>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="رقم القيد" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtJournalCode" TextMode="Number" CssClass="txt" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="5">
                    <hr /></td>
            </tr>
            <tr>
                <td style="text-align: center"></td>
                <td>
                    <asp:SqlDataSource ID="dbJournalType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS JournalTypeName UNION SELECT Id, JournalTypeName FROM JournalType WHERE (Id = 1) OR (Id = 2) OR (Id = 3) OR (Id = 4) OR (Id = 5)"></asp:SqlDataSource>
                </td>
                <td></td>
                <td>
                    <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
                <td></td>
            </tr>
        </table>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
    </div>
</asp:Content>