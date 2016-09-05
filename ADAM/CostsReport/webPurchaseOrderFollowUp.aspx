<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webPurchaseOrderFollowUp.aspx.cs" Inherits="ADAM.CostsReport.webPurchaseOrderFollowUp" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير متابعة طلبات الشراء" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label5" runat="server" Text="تاريخ البداية" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFPurchaseDate" runat="server" TextMode="Date" ToolTip="ادخل تاريخ البداية" CssClass="txt"></asp:TextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label6" runat="server" Text="تاريخ النهاية" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEPurchaseDate" runat="server" TextMode="Date" ToolTip="ادخل تاريخ النهاية" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="عرض التقرير" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>