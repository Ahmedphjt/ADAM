<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webItemReport.aspx.cs" Inherits="ADAM.MainReport.webItemReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="تقرير الاصناف" CssClass="lblPageName" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="position: relative; padding-right: 50px">
        <table class="menu">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="كود الصنف" CssClass="lbl"></asp:Label></td>
                <td colspan="3">
                    <asp:TextBox ID="txtCode" ToolTip="ادخل كود الصنف" runat="server" CssClass="txt"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="نوع الصنف" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id"></asp:DropDownList></td>
                <td style="text-align:left">
                    <asp:Label ID="Label5" runat="server" Text="النوع" CssClass="lbl"></asp:Label></td>
                <td style="text-align:center">
                    <asp:DropDownList ID="ddlSex" runat="server" CssClass="ddl" DataSourceID="dbSexData" DataTextField="Sex" DataValueField="Id">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="نوع المنتج" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlItemStatus" CssClass="ddl" runat="server" DataSourceID="dbItemStatus" DataTextField="ItemStatus" DataValueField="Id">
                    </asp:DropDownList></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label2" runat="server" Text="الصنف" CssClass="lbl"></asp:Label></td>
                <td colspan="3" class="auto-style1">
                    <asp:DropDownList ID="ddlItems" CssClass="ddl" runat="server" DataSourceID="dbItems" DataTextField="Name" DataValueField="Id" Width="250px">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="نوع التقرير" CssClass="lbl"></asp:Label></td>
                <td>
                    <asp:RadioButton ID="rdoNormalItem" runat="server" Text="تقرير عام عن الاصناف" Checked="true" ForeColor="White" GroupName="A" /></td>
                <td>
                    <asp:RadioButton ID="rdoSpecItem" runat="server" Text="تقرير مفصل عن الاصناف" ForeColor="White" GroupName="A" /></td>
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
                    <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbItems" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="ShowItem" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" Type="Int64" />
                            <asp:ControlParameter ControlID="ddlSex" DefaultValue="0" Name="Sex" PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlItemStatus" DefaultValue="0" Name="ItemStatus" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbSexData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Sex UNION SELECT Id, Sex FROM SexData"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbItemStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ItemStatus UNION SELECT Id, ItemStatus FROM ItemStatus"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
