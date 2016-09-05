<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webUpdateItemContent.aspx.cs" Inherits="ADAM.Prodction.webUpdateItemContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة تعديل التركيبات" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="مخزن المنتج"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionItemType" CssClass="ddl" runat="server" DataSourceID="dbProductionItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="خط انتاج المنتج"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductProductionLine" CssClass="ddl" runat="server" AutoPostBack="True" DataSourceID="dbPrductProductionLine" DataTextField="productionLineName" DataValueField="Id"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" Text="المنتج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionItem" CssClass="ddl" runat="server" AutoPostBack="True" DataSourceID="dbProduct" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label6" runat="server" Text="لون المنتج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionItemcolor" CssClass="ddl" runat="server" DataSourceID="dbProductItemColor" DataTextField="ColorName" DataValueField="Id"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="مخزن الخامات"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemTpe" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="خط الانتاج للخامات"></asp:Label></td>
            <td style="text-align: center">
                <asp:DropDownList ID="ddlProductionLine" CssClass="ddl" runat="server" DataSourceID="dbProductionLine" DataTextField="productionLineName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvItemContentData" runat="server" AutoGenerateColumns="False" CssClass="gv" DataSourceID="dbItemContenData" DataKeyNames="ItemContentDetailsId" OnSelectedIndexChanged="gvItemContentData_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="الصنف" SortExpression="Name" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="Qty" HeaderText="الكمية" SortExpression="Qty" />
            <asp:BoundField DataField="ItemTypeName" HeaderText="المخزن" SortExpression="ItemTypeName" />
            <asp:BoundField DataField="productionLineName" HeaderText="خط الانتاج" SortExpression="productionLineName" />
            <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <table class="gv" style="border: solid;">
        <tr style="border: solid;">
            <td style="">كود الصنف</td>
            <td style="">الصنف</td>
            <td style="">وحدة الصنف</td>
            <td style="">النوع</td>
            <td style="">لون الصنف</td>
            <td style="">نوع المنتج</td>
            <td style="">الكمية</td>
            <td style="">حفظ</td>
            <td style="">تعديل</td>
            <td style="">حذف</td>
        </tr>
        <tr>
            <td style="">
                <asp:TextBox ID="txtItemCode" Width="75px" CssClass="txt" runat="server"></asp:TextBox>
                <asp:Button ID="btnGetItem" runat="server" Text="!!" OnClick="btnGetItem_Click" />
            </td>
            <td style="">
                <asp:DropDownList ID="ddlItemName" Width="200px" runat="server" CssClass="ddl" DataSourceID="dbItem" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"></asp:DropDownList></td>
            <td style="">
                <asp:Label ID="lblItemUnit" runat="server"></asp:Label></td>
            <td style="">
                <asp:Label ID="lblSex" runat="server"></asp:Label></td>
            <td style="">
                <asp:DropDownList ID="ddlItemColor" Width="150px" CssClass="ddl" runat="server" DataSourceID="dbItemColor" DataTextField="ColorName" DataValueField="Id"></asp:DropDownList></td>
            <td style="">
                <asp:Label ID="lblItemstatus" runat="server"></asp:Label></td>
            <td style="">
                <asp:TextBox ID="txtQty" Width="100px" CssClass="txt" runat="server"></asp:TextBox></td>
            <td style="">
                <asp:ImageButton ID="btnSaveOrderItem" runat="server" ImageUrl="~/Image/Save.png" Width="30px" Height="30px" ToolTip="حفظ" OnClick="btnSaveOrderItem_Click" /></td>
            <td style="">
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/APPLICATION FOLDER.png" Width="30px" Height="30px" ToolTip="تعديل" OnClick="btnEdit_Click" /></td>
            <td style="">
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" Width="30px" Height="30px" ToolTip="حذف" OnClick="btnDelete_Click" /></td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbProductionItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbPrductProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProduct" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items WHERE (ItemTypeId = @ItemTypeId) AND (ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlProductionItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ColorName UNION SELECT ItemColor.Id, ItemColor.ColorName FROM ItemColor INNER JOIN ItemColorSelected ON ItemColor.Id = ItemColorSelected.ItemColorId WHERE (ItemColorSelected.ItemId = @ItemId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlProductionItem" DefaultValue="0" Name="ItemId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemContenData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name, ItemColor.ColorName, ItemStatus.ItemStatus, SexData.Sex, ItemUnit.Name AS ItemUnitName, ItemContentDetails.Qty, ProductionLine.productionLineName, ItemType.Name AS ItemTypeName, ItemContentHeader.ItemType, ItemContentDetails.Id AS ItemContentDetailsId, ItemContentDetails.ItemContentHeaderId FROM ItemContentDetails INNER JOIN Items INNER JOIN ItemColorSelected ON Items.Id = ItemColorSelected.ItemId INNER JOIN ItemColor ON ItemColorSelected.ItemColorId = ItemColor.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SexData ON Items.Sex = SexData.Id ON ItemContentDetails.ItemId = Items.Id AND ItemContentDetails.ItemColorId = ItemColor.Id INNER JOIN ItemType ON Items.ItemTypeId = ItemType.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN ItemContentHeader ON ItemContentDetails.ItemContentHeaderId = ItemContentHeader.Id WHERE (ItemContentDetails.ItemContentHeaderId = @ItemContentHeaderId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfItemContentHeaderId" DefaultValue="0" Name="ItemContentHeaderId" PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItemTpe" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductionLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItem" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Items WHERE (ItemTypeId = @ItemTypeId) AND (ProductionLineId = @ProductionLineId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductionLine" DefaultValue="0" Name="ProductionLineId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:HiddenField ID="hfItemContentHeaderId" Value="0" runat="server" />
                <asp:HiddenField ID="hfItemContentDetailsId" Value="0" runat="server" />
            </td>
               <td>
                <asp:SqlDataSource ID="dbItemColor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ColorName UNION SELECT ItemColor.Id, ItemColor.ColorName FROM ItemColor INNER JOIN ItemColorSelected ON ItemColor.Id = ItemColorSelected.ItemColorId WHERE (ItemColorSelected.ItemId = @ItemId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemName" DefaultValue="0" Name="ItemId" PropertyName="SelectedValue" />
                    </SelectParameters>
                   </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
