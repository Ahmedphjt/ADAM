<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webItems.aspx.cs" Inherits="ADAM.MainData.webItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="شاشة الاصناف" Font-Size="X-Large" CssClass="lblPageName" Font-Underline="true"></asp:Label></h1>

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
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged"></asp:DropDownList></td>
            <td>
                &nbsp;</td>
            <td style="text-align: right">
                &nbsp;</td>
            <td rowspan="4">
                <asp:Image ID="ImgItem" runat="server" Height="150px" Width="150px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="كود الصنف" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtCode" TextMode="Number" ToolTip="ادخل كود الصنف ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="الصنف" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtName" runat="server" ToolTip="ادخل اسم الصنف" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="وحدة الصنف" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlItemUnit" CssClass="ddl" runat="server" DataSourceID="dbItemUnit" DataTextField="Name" DataValueField="Id"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="النوع" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlSex" runat="server" CssClass="ddl" DataSourceID="dbSex" DataTextField="Sex" DataValueField="Id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="حد الطلب" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtLimitQty" TextMode="Number" ToolTip="ادخل حد الطلب ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="خط انتاج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlProductionLine" runat="server" CssClass="ddl" DataSourceID="dbProductLine" DataTextField="productionLineName" DataValueField="Id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="نوع المنتج" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlItemStatus" CssClass="ddl" runat="server" DataSourceID="dbItemStatus" DataTextField="ItemStatus" DataValueField="Id">
                </asp:DropDownList></td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="مواصفات" CssClass="lbl"></asp:Label></td>
            <td colspan="2" style="text-align: right">
                <asp:TextBox ID="txtSpecification" runat="server" ToolTip="ادخل مواصفات الصنف" TextMode="MultiLine" Width="250px" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="صورة الصنف" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:FileUpload ID="fulImage" runat="server" Width="150px" /></td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="ملاحظات" CssClass="lbl"></asp:Label></td>
            <td colspan="2" style="text-align: right">
                <asp:TextBox ID="txtNote" TextMode="MultiLine" ToolTip="ادخل الملاحظات" Width="250px" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemUnit" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemUnit"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbProductLine" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS productionLineName UNION SELECT Id, productionLineName FROM ProductionLine"></asp:SqlDataSource>
            </td>
            <td>&nbsp;</td>

        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbSex" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Sex UNION SELECT Id, Sex FROM SexData"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ItemStatus UNION SELECT Id, ItemStatus FROM ItemStatus"></asp:SqlDataSource>
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <%--    <script type="text/javascript">
        function Enbtn() {
            document.getElementById("btnSave").Enable = false;
        }
    </script>--%>
</asp:Content>
