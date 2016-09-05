<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webItemTypeProductionLine.aspx.cs" Inherits="ADAM.MainData.webItemTypeProductionLine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة ربط خط الانتاج بالمخزن" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td></td>
            <td></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <table class="table">
        <tr>
            <td style="text-align:center">
                <asp:Label ID="Label3" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="txt" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="Label1" runat="server" Text="خط الانتاج" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlProductionLine" runat="server" CssClass="txt" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" Width="150px"></asp:DropDownList></td>
        </tr>        
    </table>
    <br />
        <asp:GridView ID="gvItemTypeProductionLine" runat="server" CssClass="gv" AutoGenerateColumns="False" DataSourceID="dbItemTypPrductionLine" Width="97%" DataKeyNames="Id" OnSelectedIndexChanged="gvItemTypeProductionLine_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="المخزن" SortExpression="Name" />
            <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
            <asp:CommandField SelectText="حذف" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dbItemTypPrductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT ItemType.Name, ProductionLine.productionLineName, ItemType.Id AS ItemTypeId, ItemTypeProdcutionLine.Id FROM ItemType INNER JOIN ItemTypeProdcutionLine ON ItemType.Id = ItemTypeProdcutionLine.ItemTypeId INNER JOIN ProductionLine ON ItemTypeProdcutionLine.ProdctionLineId = ProductionLine.Id WHERE (ItemType.Id = @ItemTypeId)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <table>
        <tr>
            <td>                
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
