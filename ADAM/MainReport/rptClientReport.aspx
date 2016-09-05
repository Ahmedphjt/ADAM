<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="rptClientReport.aspx.cs" Inherits="ADAM.MainReport.rptClientReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير العملاء" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table class="menu">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=" كود العميل" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCode" TextMode="Number" ToolTip="ادخل كود العميل ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text=" النوع" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSex" runat="server" CssClass="ddl">
                        <asp:ListItem Text="---" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="ذكر" Value="1"></asp:ListItem>
                        <asp:ListItem Text="أنثي" Value="2"></asp:ListItem>
                        <asp:ListItem Text="اخري" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text=" الوظيفة" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlJob" runat="server" CssClass="ddl" DataSourceID="dbJobs" DataTextField="JobName" DataValueField="Id"></asp:DropDownList></td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text=" رقم البطاقة" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtIdNo" runat="server" TextMode="Number" ToolTip="ادخل رقم البطاقة" CssClass="txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text=" المدينة" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txt" DataSourceID="dbCountry" DataTextField="Name" DataValueField="Id" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text=" المحافظة" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCity" CssClass="ddl" runat="server" DataSourceID="dbCity" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text=" الاسم الاول" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" ToolTip="ادخل الاسم الاول" CssClass="txt"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text=" الاسم الثاني" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" ToolTip="ادخل الاسم الثاني" CssClass="txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="نوع التقرير" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:RadioButton ID="rdoNormalClient" runat="server" Text="تقرير عام عن العملاء" Checked="true" ForeColor="White" GroupName="A" /></td>
                <td>
                    <asp:RadioButton ID="rdoSpecClient" runat="server" Text="تقرير مفصل عن العملاء" ForeColor="White" GroupName="A" /></td>
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
                    <asp:SqlDataSource ID="dbJobs" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS JobName UNION SELECT Id, JobName FROM Jobs"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbCity" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS Name UNION SELECT Id, Name FROM CityData WHERE (CountryId = @CountryId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCountry" DefaultValue="0" Name="CountryId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbCountry" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM CountryData"></asp:SqlDataSource>
                </td>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
