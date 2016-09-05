<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webPurchaseBill.aspx.cs" Inherits="ADAM.Account.webPurchaseBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة فاتورة شراء" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="حفظ" OnClick="btnSave_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <div runat="server" id="divData">
        <table class="menu">
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="رقم أمر التوريد" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSupplyOrderNo" TextMode="Number" ToolTip="ادخل رقم أمر التوريد ويجب ان يكون رقماً" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="تاريخ أمر التوريد" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSupplyOrderDate" TextMode="Date" Enabled="false" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="كود المورد" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVendorCode" Enabled="false" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="المورد" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlVendorName" CssClass="ddl" Width="250px" runat="server" DataSourceID="dbVendor" DataTextField="Name" DataValueField="Id" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="رقم حساب الصندوق" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBoxNo" TextMode="Number" ToolTip="ادخل رقم حساب الصندوق ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtBoxNo_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="اسم الصندوق" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBoxName" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbBoxAccount" DataTextField="Account" DataValueField="Id" OnSelectedIndexChanged="ddlBoxName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetAllAccountforBox" CssClass="btn" runat="server" Text="--" OnClick="btnGetAllAccountforBox_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="رقم حساب مركز التكلفة" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCostCenter" TextMode="Number" ToolTip="ادخل رقم حساب مركز التكلفة ويجب ان يكون رقماً" runat="server" CssClass="txt" OnTextChanged="txtCostCenter_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="اسم المركز" CssClass="lbl"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCostCenterName" CssClass="ddl" Width="250px" runat="server" AutoPostBack="True" DataSourceID="dbCostCenter" DataTextField="CostCenter" DataValueField="id" OnSelectedIndexChanged="ddlCostCenterName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnGetAllAccountforCostCenter" CssClass="btn" runat="server" Text="--" OnClick="btnGetAllAccountforCostCenter_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvSupplyData" Width="100%" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbgvSupplyData">
            <Columns>
                <asp:BoundField DataField="PurchaseOrderNo" HeaderText="رقم طلب الشراء" SortExpression="PurchaseOrderNo" />
                <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
                <asp:BoundField DataField="ItemName" HeaderText="الصنف" SortExpression="ItemName" />
                <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
                <asp:BoundField DataField="RealQty" HeaderText="الكمية الاجمالية المقبولة" ReadOnly="True" SortExpression="RealQty" />
                <asp:BoundField DataField="ItemPrice" HeaderText="سعر الوحدة" SortExpression="ItemPrice" />
            </Columns>
        </asp:GridView>
        <br />
        <table>
            <tr>
                <td colspan="4"><hr /></td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:Label ID="Label9" runat="server" Text="أجمالي الفاتورة" CssClass="lbl" Font-Bold="True" ForeColor="#FF3300"></asp:Label></td>
                <td>
                    <asp:Label ID="lblBillPrice" runat="server" Text="0" CssClass="lbl" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="dbVendor" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '  ' + LastName AS Name FROM SupplierData"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbBoxAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Account UNION SELECT Id,  AccountName AS Account FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="dbCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS id, '---' AS CostCenter UNION SELECT Id,  CostCenterName AS CostCenter FROM CostCenter WHERE (CostCenterType = 1 Or Id = 1)"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:HiddenField ID="hfSupplyheaderId" Value="0" runat="server" />
                </td>
                <td>
                    <asp:SqlDataSource ID="dbgvSupplyData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Items.Code, Items.Name AS ItemName, PurchaseOrderHeader.PurchaseOrderNo, ItemColor.ColorName, AuditDetails.AcceptQty + AuditDetails.AcceptfreeQty AS RealQty, SupplyOrderDetails.SupplyOrderHeaderId, SupplyOrderDetails.ItemPrice FROM RecordReceiptDetails INNER JOIN Items ON RecordReceiptDetails.ItemId = Items.Id INNER JOIN SupplyOrderDetails ON RecordReceiptDetails.SupplyOrderDetailsId = SupplyOrderDetails.Id INNER JOIN PurchaseOredrDetails ON Items.Id = PurchaseOredrDetails.ItemId AND SupplyOrderDetails.PurchaseOrderDetailsId = PurchaseOredrDetails.Id INNER JOIN PurchaseOrderHeader ON PurchaseOredrDetails.PurchaseOredeHeaderId = PurchaseOrderHeader.Id INNER JOIN AuditDetails ON RecordReceiptDetails.Id = AuditDetails.RecordReceiptDetailsId INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id WHERE (SupplyOrderDetails.SupplyOrderHeaderId = @SupplyOrderHeaderId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfSupplyheaderId" DefaultValue="0" Name="SupplyOrderHeaderId" PropertyName="Value" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
     <div runat="server" id="divAccount" visible="false">
        <asp:GridView ID="gvAcccount" CssClass="gv" runat="server" AutoGenerateColumns="False" DataSourceID="dbgvAccount" DataKeyNames="Id" OnSelectedIndexChanged="gvAcccount_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="AccountCode" HeaderText="رقم الحساب" SortExpression="AccountCode" />
                <asp:BoundField DataField="AccountName" HeaderText="الحساب" SortExpression="AccountName" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
         <asp:SqlDataSource ID="dbgvAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT AccountCode, AccountName, Id FROM Account WHERE (AccountType = 1)"></asp:SqlDataSource>
    </div>
    <br />
    <div runat="server" id="divCostCenter" visible="false">
        <asp:GridView ID="gvCostCenter" CssClass="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="dbgvCostCenter" OnSelectedIndexChanged="gvCostCenter_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="CostCenterCode" HeaderText="رقم مركز التكلفة" SortExpression="CostCenterCode" />
                <asp:BoundField DataField="CostCenterName" HeaderText="مركز التكلفة" SortExpression="CostCenterName" />
                <asp:CommandField SelectText="أختيار" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dbgvCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT Id, CostCenterCode, CostCenterName, CostCenterType FROM CostCenter WHERE (CostCenterType = 1)"></asp:SqlDataSource>
    </div>
</asp:Content>
