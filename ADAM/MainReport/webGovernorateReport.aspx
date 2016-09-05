<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webGovernorateReport.aspx.cs" Inherits="ADAM.MainReport.webGovernorateReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير المدن" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table class="table">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Text="الدولة" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txt" DataSourceID="dbCountry" DataTextField="Name" DataValueField="Id" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
                <td>
                    <asp:SqlDataSource ID="dbCountry" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM CountryData"></asp:SqlDataSource>
                </td>

                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="المحافظة" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="txt" DataSourceID="dbCity" DataTextField="Name" DataValueField="Id" Width="150px"></asp:DropDownList></td>
                <td>

                    <asp:SqlDataSource ID="dbCity" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM CityData WHERE (CountryId = @CountryId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCountry" DefaultValue="0" Name="CountryId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </td>

                <td>
                    <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/WMP.png" CssClass="Img" ToolTip="عرض التقرير" OnClick="btnShowReport_Click" /></td>
            </tr>
        </table>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
    </div>
</asp:Content>
