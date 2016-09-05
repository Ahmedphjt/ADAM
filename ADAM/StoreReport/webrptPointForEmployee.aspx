<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webrptPointForEmployee.aspx.cs" Inherits="ADAM.StoreReport.webrptPointForEmployee" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير النقاط" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label1" runat="server" Text="الموظف" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlEmployee" CssClass="ddl" runat="server" DataSourceID="dbEmployee" DataTextField="EmpName" DataValueField="Id" Width="200px"></asp:DropDownList>
            </td>
            <td>
                <asp:SqlDataSource ID="dbEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS EmpName UNION SELECT Id, FirstName + '  ' + LastName AS EmpName FROM EmployeeData"></asp:SqlDataSource>
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label2" runat="server" Text="من تاريخ" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBeforeExchangeRequestDate" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="الي تاريخ" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAfterExchangeRequestDate" TextMode="Date" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="4">
                <hr /></td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label3" runat="server" Text="عرض التقرير" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <br />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
</asp:Content>
