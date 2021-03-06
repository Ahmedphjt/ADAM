﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webEmployeeClientReport.aspx.cs" Inherits="ADAM.MainReport.webEmployeeClientReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير مندوبي العملاء" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table class="menu">
            <tr>
                <td>
                    <asp:Label ID="Label1" CssClass="lbl" runat="server" Text="اسم المندوب"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="ddl" DataSourceID="dbEmployee" DataTextField="Name" DataValueField="Id"></asp:DropDownList></td>
                <td></td>
                <td style="text-align: center">
                    <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
        </table>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
        <table>
            <tr>
                <td>
                    <asp:SqlDataSource ID="dbEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + ' ' + LastName AS Name FROM EmployeeData"></asp:SqlDataSource>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
