﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webrptClientFollowUp.aspx.cs" Inherits="ADAM.CostsReport.webrptClientFollowUp" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير متابعة العملاء" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="Data">
        <table>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="كود العميل" CssClass="lbl"></asp:Label></td>
                <td colspan="2">
                    <asp:TextBox ID="txtClienCode" runat="server" CssClass="txt" AutoPostBack="True" OnTextChanged="txtClientName_TextChanged"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnShowDiv" runat="server" OnClick="btnShowDiv_Click" Text="!!" />
                </td>
            </tr>

            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label4" runat="server" Text="العميل" CssClass="lbl"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlClientName" CssClass="ddl" runat="server" DataSourceID="dbClient" DataTextField="Name" DataValueField="Id" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbClient" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + ' ' + LastName AS Name FROM ClientData"></asp:SqlDataSource>
                </td>
            </tr>

            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label5" runat="server" Text="تاريخ البداية" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFExchangeRequestDate" runat="server" TextMode="Date" ToolTip="ادخل تاريخ البداية" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label6" runat="server" Text="تاريخ النهاية" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEExchangeRequestDate" runat="server" TextMode="Date" ToolTip="ادخل تاريخ النهاية" CssClass="txt"></asp:TextBox>
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
    </div>
    <div runat="server" id="Clients" visible="false">
        <asp:GridView ID="gvClient" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbgvClient" OnSelectedIndexChanged="gvClient_SelectedIndexChanged" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="كود العميل" SortExpression="Code" />
                <asp:BoundField DataField="Name" HeaderText="العميل" ReadOnly="True" SortExpression="Name" />
                <asp:BoundField DataField="FirstPhone" HeaderText="رقم التليفون" SortExpression="FirstPhone" />
                <asp:BoundField DataField="FirstMobile" HeaderText="الموبايل" SortExpression="FirstMobile" />
                <asp:BoundField DataField="Address" HeaderText="العنوان" SortExpression="Address" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbgvClient" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Code, FirstName + ' ' + LastName AS Name, FirstPhone, FirstMobile, Address, Id FROM ClientData"></asp:SqlDataSource>
    </div>
</asp:Content>
