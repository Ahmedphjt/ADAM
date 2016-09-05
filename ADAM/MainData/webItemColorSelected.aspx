<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webItemColorSelected.aspx.cs" Inherits="ADAM.MainData.webItemColorSelected" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة تحديد الوان الاصناف" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <%-- <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" />--%></td>
            <td>
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" CssClass="Img" ToolTip="تعديل" OnClick="btnEdit_Click" /></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
            <td>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" /></td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label3" runat="server" Text=" المخزن" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="txt" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="خط الانتاج" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlProdctionLine" runat="server" CssClass="txt" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label2" runat="server" Text="الصنف" CssClass="lbl"></asp:Label></td>
            <td colspan="2">
                <asp:DropDownList ID="ddlItems" runat="server" CssClass="txt" DataSourceID="dbItem" DataTextField="Name" DataValueField="Id" Width="300px"></asp:DropDownList></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label1" runat="server" Text="لون الصنف" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemColor" runat="server" CssClass="txt" DataSourceID="dbItemColor" DataTextField="ColorName" DataValueField="Id" Width="150px"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="عدد النقاط" CssClass="lbl"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPoint" CssClass="txt" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="text-align: center">
                <asp:Label ID="Label6" runat="server" Text="مجموعة الاصناف" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="txt" DataSourceID="dbItemGroup" DataTextField="ItemGroupName" DataValueField="Id" Width="300px"></asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:SqlDataSource ID="dbItemGroup" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ItemGroupName UNION SELECT Id, ItemGroupName FROM ItemsGroup"></asp:SqlDataSource>
            </td>
        </tr>

    </table>
    <br />
    <asp:GridView ID="gvItemColorSelected" runat="server" CssClass="gv" AutoGenerateColumns="False" DataSourceID="dbItemColorSelected" Width="97%" DataKeyNames="ItemColorSelectedId" OnSelectedIndexChanged="gvItemColorSelected_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="ColorName" HeaderText="لون الصنف" SortExpression="ColorName" />
            <asp:BoundField DataField="Point" HeaderText="عدد النقاط" SortExpression="Point" />
            <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItem" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items WHERE (ItemTypeId = @ItemTypeId) AND (ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProdctionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ColorName UNION SELECT Id, ColorName FROM ItemColor"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemColorSelected" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemColor.ColorName, ItemColorSelected.Id AS ItemColorSelectedId, ItemColorSelected.Point FROM ItemColorSelected INNER JOIN ItemColor ON ItemColorSelected.ItemColorId = ItemColor.Id INNER JOIN Items ON ItemColorSelected.ItemId = Items.Id WHERE (Items.ItemTypeId = @ItemTypeId) AND (Items.ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProdctionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:HiddenField ID="hfItemColorSelectedId" Value="0" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
