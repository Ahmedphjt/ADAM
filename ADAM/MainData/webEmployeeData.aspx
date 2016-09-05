<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webEmployeeData.aspx.cs" Inherits="ADAM.MainData.webEmployeeData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="شاشة الموظفين" Font-Size="X-Large" CssClass="lblPageName" Font-Underline="true"></asp:Label></h1>

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
                <asp:Label ID="Label29" runat="server" Text="ادخل رقم الحساب" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtAccountCode" TextMode="Number" ToolTip="ادخل رقم الحساب ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="كود الموظف" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtCode" TextMode="Number" ToolTip="ادخل كود الموظف ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="الاسم الاول" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" ToolTip="ادخل اسم الاول" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="الاسم الثاني" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtLastName" runat="server" ToolTip="ادخل اسم الثاني" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="تاريخ الميلاد" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtBirthDate" runat="server" TextMode="Date" ToolTip="ادخل تاريخ الميلاد" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="رقم البطاقة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtIdNo" runat="server" TextMode="Number" ToolTip="ادخل رقم البطاقة" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="النوع" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlSex" runat="server" CssClass="ddl">
                    <asp:ListItem Text="---" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="ذكر" Value="1"></asp:ListItem>
                    <asp:ListItem Text="أنثي" Value="2"></asp:ListItem>
                    <asp:ListItem Text="اخري" Value="3"></asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:Label ID="Label7" runat="server" Text="العنوان" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server" ToolTip="ادخل العنوان" Width="250px" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="الهاتف الاول" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFirstPhone" runat="server" TextMode="Number" ToolTip="ادخل رقم الهاتف الاول" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="الهاتف الثاني" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtSecondPhone" runat="server" TextMode="Number" ToolTip="ادخل رقم الهاتف الثاني" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="الموبايل الاول" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFirstMobileNo" runat="server" TextMode="Number" ToolTip="ادخل رقم الموبايل الاول" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="الموبايل الثاني" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtSecondMobileNo" runat="server" TextMode="Number" ToolTip="ادخل رقم الموبايل الثاني" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="رقم الفاكس" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFaxNo" runat="server" TextMode="Number" ToolTip="ادخل رقم الفاكس" CssClass="txt"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label13" runat="server" Text="الايميل" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtEmail" runat="server" ToolTip="ادخل الايميل" Width="250px" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="الادارة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="ddl" DataSourceID="dbDepartment" DataTextField="Name" DataValueField="Id" AutoPostBack="True"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label15" runat="server" Text="القسم" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="ddl" DataSourceID="dbDivision" DataTextField="Name" DataValueField="Id"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Text="الوظيفة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlJob" runat="server" CssClass="ddl" DataSourceID="dbJobs" DataTextField="JobName" DataValueField="Id"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label17" runat="server" Text="تاريخ التوظيف" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:TextBox ID="txtStartJobDate" runat="server" TextMode="Date" ToolTip="ادخل تاريخ التوظيف" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" Text="الموقف من التجنيد" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlMilitaryStatus" CssClass="ddl" runat="server">
                    <asp:ListItem Value="0" Text="---"></asp:ListItem>
                    <asp:ListItem Value="1" Text="انهي الخدمة"></asp:ListItem>
                    <asp:ListItem Value="2" Text="معفي نهائي"></asp:ListItem>
                    <asp:ListItem Value="3" Text="معفي مؤقت"></asp:ListItem>
                    <asp:ListItem Value="4" Text="لم يصبه الدور"></asp:ListItem>
                    <asp:ListItem Value="5" Text="غير ذلك"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label19" runat="server" Text="الموقف التأميني" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlInsuranceStatus" CssClass="ddl" runat="server">
                    <asp:ListItem Value="0" Text="---"></asp:ListItem>
                    <asp:ListItem Value="1" Text="مؤمن عليه"></asp:ListItem>
                    <asp:ListItem Value="2" Text="غير مؤمن عليه"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Text="الحالة الاجتماعية" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlMaritalStatus" CssClass="ddl" runat="server">
                    <asp:ListItem Value="0" Text="---"></asp:ListItem>
                    <asp:ListItem Value="1" Text="اعزب"></asp:ListItem>
                    <asp:ListItem Value="2" Text="متزوج"></asp:ListItem>
                    <asp:ListItem Value="3" Text="مطلق"></asp:ListItem>
                    <asp:ListItem Value="4" Text="ارمل"></asp:ListItem>
                    <asp:ListItem Value="5" Text="غير ذلك"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label21" runat="server" Text="المؤهل الدراسي" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlQualification" CssClass="ddl" runat="server" Width="200px" DataSourceID="dbQualification" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label22" runat="server" Text="الديانه" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlReligion" CssClass="ddl" runat="server">
                    <asp:ListItem Value="0" Text="---"></asp:ListItem>
                    <asp:ListItem Value="1" Text="مسلم"></asp:ListItem>
                    <asp:ListItem Value="2" Text="مسيحي"></asp:ListItem>
                    <asp:ListItem Value="3" Text="غير ذلك"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label23" runat="server" Text="نوع التعاقد" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlContractType" CssClass="ddl" runat="server">
                    <asp:ListItem Value="0" Text="---"></asp:ListItem>
                    <asp:ListItem Value="1" Text="دائم"></asp:ListItem>
                    <asp:ListItem Value="2" Text="عقد سنوي"></asp:ListItem>
                    <asp:ListItem Value="3" Text="يومية"></asp:ListItem>
                    <asp:ListItem Value="4" Text="غير ذلك"></asp:ListItem>
                </asp:DropDownList>
            </td>
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
            <td><asp:Label ID="Label26" runat="server" Text="المدينة" CssClass="lbl"></asp:Label></td>
            <td><asp:DropDownList ID="ddlGovernorate" CssClass="ddl" runat="server" DataSourceID="dbGovernorate" DataTextField="GovernorateName" DataValueField="Id" AutoPostBack="True">
                </asp:DropDownList></td>
            <td><asp:Label ID="Label27" runat="server" Text="المنطقة" CssClass="lbl"></asp:Label></td>
            <td style="text-align: right"><asp:DropDownList ID="ddlArea" CssClass="ddl" runat="server" DataSourceID="dbArea" DataTextField="AreaName" DataValueField="Id">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label28" runat="server" Text="هل مندوب مبيعات" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:CheckBox ID="chkISSalesRepresentative" runat="server" />
            </td>
            <td></td>
            <td style="text-align: right"></td>
        </tr>
    </table>

    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM Department"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dbCountry" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM CountryData"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbCity" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '----' AS Name UNION SELECT Id, Name FROM CityData WHERE (CountryId = @CountryId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCountry" DefaultValue="0" Name="CountryId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="dbDivision" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM division WHERE (DepartmentId = @DepartmentId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlDepartment" DefaultValue="0" Name="DepartmentId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbJobs" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS JobName UNION SELECT Id, JobName FROM Jobs"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dbGovernorate" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS GovernorateName UNION SELECT Id, GovernorateName FROM GovernorateData WHERE (CityId = @CityId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCity" DefaultValue="0" Name="CityId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbQualification" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM QualificationsData"></asp:SqlDataSource>
                <asp:SqlDataSource ID="dbArea" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS AreaName UNION SELECT Id, AreaName FROM AreaData WHERE (GovernorateId = @GovernorateId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlGovernorate" DefaultValue="0" Name="GovernorateId" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
