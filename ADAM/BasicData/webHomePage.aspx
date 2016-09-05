<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webHomePage.aspx.cs" Inherits="ADAM.BasicData.webHomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div style="text-align: left">
        <asp:ImageButton ID="LogMeIn" runat="server" ImageUrl="~/Image/LogOff.png" AlternateText="خروج"  Height="63px" Width="63px" OnClick="LogMeIn_Click" />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
