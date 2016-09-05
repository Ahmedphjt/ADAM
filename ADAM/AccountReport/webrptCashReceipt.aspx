<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webrptCashReceipt.aspx.cs" Inherits="ADAM.AccountReport.webrptCashReceipt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير سندات القبض" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table class="table">
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td></td>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="رقم سند القبض" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDocumentCode" TextMode="Number" CssClass="txt" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="5">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="text-align: center"></td>
                <td>
                    
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
