﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webAccountData.aspx.cs" Inherits="ADAM.Account.webAccountData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة أدخال الحساب" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" /></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>

            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu" style="width: 70%">
        <tr>
            <td style="width: 70%; vertical-align: top">
                <table class="menu" style="width: 100%">
                    <tr>
                        <td colspan="4">
                            <br />
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="Label6" runat="server" Text="الحساب :" ForeColor="Black" Font-Bold="true" Font-Underline="true" CssClass="lbl"></asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblAccountName" runat="server" Text="---" ForeColor="Black" Font-Bold="true" CssClass="lbl" Font-Underline="true"></asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label2" runat="server" ForeColor="Black" Font-Underline="true" Font-Bold="true" Text="رقم الحساب :" CssClass="lbl"></asp:Label></td>
                        <td style="text-align: center">
                            <asp:Label ID="lblAccountCode" ForeColor="Black" Font-Bold="true" Font-Underline="true" runat="server" Text="---" CssClass="lbl"></asp:Label>
                        </td>
                        
                    </tr>

                    <tr>
                        <td colspan="4">                           
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="Label4" runat="server" Text="رقم الحساب" CssClass="lbl"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtAccountCode" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="Label3" runat="server" Text="الحساب" CssClass="lbl"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtAccountName" ToolTip="ادخل اسم الحساب" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="Label5" runat="server" Text="نوع الحساب" CssClass="lbl"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="txt" Width="150px">
                                <asp:ListItem Value="-1">-----</asp:ListItem>
                                <asp:ListItem Value="0">رئيسي</asp:ListItem>
                                <asp:ListItem Value="1">فرعي</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td><asp:Label ID="Label7" runat="server" Text="عملة الحساب" CssClass="lbl"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="txt" Width="150px" DataSourceID="dbCurrency" DataTextField="CurrencyName" DataValueField="Id">
                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    </table>
            </td>
            <td style="width: 30%; vertical-align: top;">
                <table class="menu" style="width: 100%">
                    <tr>
                        <td colspan="5">
                            <br />
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <table>
                                <tr>
                                    <td></td>
                                    <td>

                                        <asp:TreeView ID="tvAccount" runat="server" ImageSet="Simple" ExpandDepth="0" OnSelectedNodeChanged="tvAccount_SelectedNodeChanged">
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                            <NodeStyle Font-Names="Tahoma" Font-Size="Medium" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="1px" VerticalPadding="5px" Height="1px" />
                                            <ParentNodeStyle Font-Bold="False" />
                                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#5555DD" />
                                        </asp:TreeView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
    </table>

    <br />
    <table>
        <tr>
            <td>
                <asp:HiddenField ID="hfParentId" Value="0" runat="server" />
            </td>
            <td>
                <asp:SqlDataSource ID="dbAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Id, AccountName, AccountCode, ParentId FROM Account WHERE (ParentId = 0)"></asp:SqlDataSource>
            </td>
            <td>
                <asp:HiddenField ID="hfID" Value="0" runat="server" />
            </td>
            <td>
                            <asp:SqlDataSource ID="dbCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS CurrencyName UNION SELECT Id, CurrencyName FROM CurrencyData"></asp:SqlDataSource>
                        </td>
        </tr>
    </table>
</asp:Content>
