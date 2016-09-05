<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webCurrencyData.aspx.cs" Inherits="ADAM.Account.webCurrencyData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة العملات" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
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
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="table">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label2" runat="server" Text="العملة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ToolTip="ادخل اسم العملة" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label3" runat="server" Text="رمز العمة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtStyle" ToolTip="ادخل رمز العملة" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label1" runat="server" Text="نوع العملة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="txt" Width="150px">
                    <asp:ListItem Value="0">-----</asp:ListItem>
                    <asp:ListItem Value="1">محلية</asp:ListItem>
                    <asp:ListItem Value="2">أجنبية</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label4" runat="server" Text="سعر التحويل" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtPrice" ToolTip="ادخل سعر التحويل ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvCurrency" CssClass="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="dbCurrency" OnRowDataBound="gvCurrency_RowDataBound" OnSelectedIndexChanged="gvCurrency_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="CurrencyName" HeaderText="اسم العملة" SortExpression="CurrencyName" />
            <asp:BoundField DataField="CurrencyStyle" HeaderText="رمز العملة" SortExpression="CurrencyStyle" />
            <asp:BoundField DataField="CurrencyType" HeaderText="نوع العملة" SortExpression="CurrencyType" />
            <asp:BoundField DataField="CurrencyPrice" HeaderText="سعر التحويل" SortExpression="CurrencyPrice" />
            <asp:CommandField SelectText="اختيار" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <table>
        <tr>
            <td>

                <asp:SqlDataSource ID="dbCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Id, CurrencyName, CurrencyStyle, CurrencyType, CurrencyPrice FROM CurrencyData"></asp:SqlDataSource>

            </td>
            <td>
                <asp:HiddenField ID="hfCurrenyId" Value="0" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
