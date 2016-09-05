<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webPermissionReport.aspx.cs" Inherits="ADAM.MainReport.webPermissionReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير الصلاحيات" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table class="table">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Text=" الموظف" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="txt" DataSourceID="dbEmployee" DataTextField="Name" DataValueField="Id" Width="150px"></asp:DropDownList></td>
                <td>
                    <asp:SqlDataSource ID="dbEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + ' ' + LastName AS Name FROM EmployeeData"></asp:SqlDataSource>
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text=" الصلاحية" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlOperation" runat="server" CssClass="txt" DataSourceID="dbOperation" DataTextField="ArOperationName" DataValueField="Id" Width="150px"></asp:DropDownList></td>
                <td>
                    <asp:SqlDataSource ID="dbOperation" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ArOperationName UNION SELECT Id, ArOperationName FROM Operations"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            </tr>
        </table>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
    </div>
</asp:Content>
