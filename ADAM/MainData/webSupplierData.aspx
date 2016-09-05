<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webSupplierData.aspx.cs" Inherits="ADAM.MainData.webSupplierData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="شاشة الموردين" Font-Size="X-Large" CssClass="lblPageName" Font-Underline="true"></asp:Label></h1>

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
                <asp:Label ID="Label15" runat="server" Text="نوع المورد" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlSupplierType" CssClass="ddl" runat="server" DataSourceID="dbSupplierType" DataTextField="SupplierTypeName" DataValueField="Id" OnSelectedIndexChanged="ddlSupplierType_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Text="ادخل رقم الحساب" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtAccountCode" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="كود المورد" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtCode" TextMode="Number" ToolTip="ادخل كود العميل ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="الاسم الاول" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFirstName" ToolTip="ادخل الاسم الاول" runat="server" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="الاسم الثاني" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtLastName" runat="server" ToolTip="ادخل الاسم الثاني" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="النوع" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlSex" runat="server" CssClass="ddl">
                    <asp:ListItem Text="---" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="ذكر" Value="1"></asp:ListItem>
                    <asp:ListItem Text="أنثي" Value="2"></asp:ListItem>
                    <asp:ListItem Text="اخري" Value="3"></asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="رقم البطاقة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtIdNo" runat="server" TextMode="Number" ToolTip="ادخل رقم البطاقة" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="رقم الهاتف الاول" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFirstPhone" ToolTip="ادخل رقم الهاتف الاول" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="رقم الهاتف الثاني" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtSecondPhone" runat="server" ToolTip="ادخل رقم الهاتف الثاني" TextMode="Number" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="رقم الموبايل الاول" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFirstMobile" ToolTip="ادخل رقم الموبايل الاول" runat="server" TextMode="Number" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="رقم الموبايل الثاني" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtSecondMobile" runat="server" ToolTip="ادخل رقم الموبايل الثاني" TextMode="Number" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="رقم الفاكس" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFax" ToolTip="ادخل رقم الفاكس" runat="server" TextMode="Number" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="الايميل" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" ToolTip="ادخل الايميل" CssClass="txt" Width="250px"></asp:TextBox></td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="Label24" runat="server" Text="الدولة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlCountry" CssClass="ddl" runat="server" AutoPostBack="True" DataSourceID="dbCountry" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label25" runat="server" Text="المحافظة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlCity" CssClass="ddl" runat="server" DataSourceID="dbCity" DataTextField="Name" DataValueField="Id" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label26" runat="server" Text="المدينة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlGovernorate" CssClass="ddl" runat="server" DataSourceID="dbGovernorate" DataTextField="GovernorateName" DataValueField="Id" AutoPostBack="True">
                </asp:DropDownList></td>
            <td>
                <asp:Label ID="Label27" runat="server" Text="المنطقة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlArea" CssClass="ddl" runat="server" DataSourceID="dbArea" DataTextField="AreaName" DataValueField="Id">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="الوظيفة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlJob" runat="server" CssClass="ddl" DataSourceID="dbJobs" DataTextField="JobName" DataValueField="Id"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label13" runat="server" Text="العنوان" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" ToolTip="ادخل العنوان" CssClass="txt" Width="250px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="حالة المورد" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl">
                    <asp:ListItem Text="---" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="فعال" Value="1"></asp:ListItem>
                    <asp:ListItem Text="غير فعال" Value="2"></asp:ListItem>
                    <asp:ListItem Text="اخري" Value="3"></asp:ListItem>
                </asp:DropDownList></td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbJobs" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS JobName UNION SELECT Id, JobName FROM Jobs"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbGovernorate" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS GovernorateName UNION SELECT Id, GovernorateName FROM GovernorateData WHERE (CityId = @CityId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCity" DefaultValue="0" Name="CityId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="dbArea" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS AreaName UNION SELECT Id, AreaName FROM AreaData WHERE (GovernorateId = @GovernorateId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlGovernorate" DefaultValue="0" Name="GovernorateId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbCountry" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM CountryData"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dbSupplierType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS SupplierTypeName UNION SELECT Id, SupplierTypeName FROM SupplierType"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbCity" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS Name UNION SELECT Id, Name FROM CityData WHERE (CountryId = @CountryId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCountry" DefaultValue="0" Name="CountryId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
