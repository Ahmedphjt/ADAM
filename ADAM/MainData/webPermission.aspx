<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webPermission.aspx.cs" Inherits="ADAM.MainData.webPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" runat="server" Text="شاشة الصلاحيات" Font-Size="X-Large" CssClass="lblPageName" Font-Underline="true"></asp:Label></h1>

    <table class="menu">
        <tr>
            <%--<td>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" /></td>--%>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
            <%--<td>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/Delete.png" CssClass="Img" ToolTip="حذف" OnClick="btnDelete_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" /></td>--%>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="المستخدم" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" CssClass="ddl" DataSourceID="dbUser" DataTextField="NickName" DataValueField="Id" AutoPostBack="True"></asp:DropDownList>
                
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="الشاشة" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlPages" runat="server" CssClass="ddl" Width="275px" DataSourceID="dbPages" DataTextField="ArPagename" DataValueField="Id" AutoPostBack="True"></asp:DropDownList></td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="الصلاحية" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlOperation" runat="server" CssClass="ddl" DataSourceID="dbOperation" DataTextField="ArOperationName" DataValueField="Id"></asp:DropDownList></td>
        </tr>
    </table>
    <br />

    <asp:GridView ID="gvUserPermission" runat="server" AllowPaging="True" CssClass="gv" AutoGenerateColumns="False" DataSourceID="dbgvUserPermission" PageSize="25" Width="95%" DataKeyNames="Id" OnSelectedIndexChanged="gvUserPermission_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="FirstName"  HeaderText="الاسم الاول" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="الاسم الثاني" SortExpression="LastName" />
            <asp:BoundField DataField="NickName" HeaderText="اسم الدخول" SortExpression="NickName" />
            <asp:BoundField DataField="ArPagename" HeaderText="الشاشة" SortExpression="ArPagename" />
            <asp:BoundField DataField="ArOperationName" HeaderText="الصلاحية" SortExpression="ArOperationName" />
            <asp:CommandField SelectText="حذف" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="dbgvUserPermission" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT EmployeeData.FirstName, EmployeeData.LastName, UserData.NickName, Operations.ArOperationName, PagesName.ArPagename, Permission.Id FROM Permission INNER JOIN PagesName ON Permission.PageId = PagesName.Id INNER JOIN Operations ON Permission.OperationId = Operations.Id INNER JOIN UserData ON Permission.UserId = UserData.Id INNER JOIN EmployeeData ON UserData.UserID = EmployeeData.Id WHERE (UserData.Id = @userId) AND (PagesName.Id = @PageId) ORDER BY PagesName.Id">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlUser" DefaultValue="0" Name="userId" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="ddlPages" DefaultValue="0" Name="PageId" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <table>
        <tr>
            <td><asp:SqlDataSource ID="dbUser" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS NickName UNION SELECT Id, NickName FROM UserData"></asp:SqlDataSource></td>
            <td>
                <asp:SqlDataSource ID="dbPages" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ArPagename UNION SELECT Id, ArPagename FROM PagesName"></asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbOperation" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS ArOperationName UNION SELECT Id, ArOperationName FROM Operations"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
