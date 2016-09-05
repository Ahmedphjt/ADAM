<%@ Page Title="" Language="C#" MasterPageFile="~/MyMater.Master" AutoEventWireup="true" CodeBehind="webConformAudit.aspx.cs" Inherits="ADAM.StoreData.webConformAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageAddress" runat="server">
    <h1>
        <asp:Label ID="lblPageName" CssClass="lblPageName" runat="server" Text="شاشة اعتماد أخطار فحص" Font-Size="X-Large" Font-Underline="true"></asp:Label></h1>
    <br />
    <table class="menu">
        <tr>
            <td>
                <asp:ImageButton ID="btnNew" runat="server" ImageUrl="~/Image/Cryo64 Genesis 3G (80).png" CssClass="Img" ToolTip="جديد" OnClick="btnNew_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowData" runat="server" ImageUrl="~/Image/View.png" CssClass="Img" ToolTip="عرض" OnClick="btnShowData_Click" /></td>
            <td>
                <asp:ImageButton ID="btnConform" runat="server" ImageUrl="~/Image/Save.png" CssClass="Img" ToolTip="اعتماد" OnClick="btnConform_Click" /></td>
            <td>
                <asp:ImageButton ID="btnShowReport" runat="server" ImageUrl="~/Image/Report.png" CssClass="Img" ToolTip="طباعة" OnClick="btnShowReport_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageData" runat="server">
    <table class="menu">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="المخزن" CssClass="lbl"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlItemType" CssClass="ddl" runat="server" DataSourceID="dbItemType" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Label ID="Label3" runat="server" Text="رقم محضر الاستلام" CssClass="lbl"></asp:Label></td>
            <td style="text-align: center">
                <asp:TextBox ID="txtAuditNo" TextMode="Number" runat="server" CssClass="txt"></asp:TextBox></td>

        </tr>
    </table>
    <br />
    <asp:GridView ID="gvAuditData" CssClass="gv" Visible="False" runat="server" AutoGenerateColumns="False" DataSourceID="dbAuditData" Width="97%" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="كود الصنف" SortExpression="Code" />
            <asp:BoundField DataField="ItemName" HeaderText="الصنف" SortExpression="ItemName" />
            <asp:BoundField DataField="QtyReceived" HeaderText="ك.المستلمة" SortExpression="QtyReceived" />
            <asp:BoundField DataField="ItemUnitName" HeaderText="الوحدة" SortExpression="ItemUnitName" />
            <asp:BoundField DataField="ColorName" HeaderText="اللون" SortExpression="ColorName" />
            <asp:BoundField DataField="Sex" HeaderText="النوع" SortExpression="Sex" />
            <asp:BoundField DataField="ItemStatus" HeaderText="نوع المنتج" SortExpression="ItemStatus" />
            <asp:TemplateField HeaderText="القائم بالفحص">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="ddl" DataSourceID="dbEmployee" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="dbEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, FirstName + '  ' + LastName AS Name FROM EmployeeData"></asp:SqlDataSource>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ك.المقبولة">
                <ItemTemplate>
                    <asp:TextBox ID="txtAcceptQty" runat="server" Width="75px" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Acc. Tester Qty">
                <ItemTemplate>
                    <asp:TextBox ID="txtFreeAcceptQty" runat="server" Width="75px" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Loc">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ddl" Width="75px" DataSourceID="dbLocation" DataTextField="LocationName" DataValueField="Id">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="dbLocation" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS LocationName UNION SELECT Id, LocationName FROM ItemLocation WHERE (ItemTypeId = @ItemTypeId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ك.المرفوضة">
                <ItemTemplate>
                    <asp:TextBox ID="txtRefused" runat="server" Width="75px" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ref. Tester Qty">
                <ItemTemplate>
                    <asp:TextBox ID="txtfreeRefusedQty" runat="server" Width="75px" CssClass="txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ملاحظات">
                <ItemTemplate>
                    <asp:TextBox ID="txtNote" runat="server" CssClass="txt" TextMode="MultiLine"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="productionLineName" HeaderText="خط انتاج" SortExpression="productionLineName" />
            <asp:TemplateField HeaderText="أختيار">
                <ItemTemplate>
                    <asp:CheckBox ID="chkChoose" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <table>
        <tr>
            <td>
                <asp:SqlDataSource ID="dbAuditData" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT RecordReceiptHeader.RecordReceiptNo, RecordReceiptHeader.RecordReceiptDate, Items.Code, Items.Name AS ItemName, RecordReceiptDetails.QtyReceived, RecordReceiptDetails.Indoor, RecordReceiptDetails.Note, ItemType.Name AS ItemTypeName, ItemUnit.Name AS ItemUnitName, Department.Name AS DepName, AuditHeader.AuditNo, AuditHeader.AuditDate, PurchaseOrderHeader.PurchaseOrderNo, AuditDetails.Id, ItemColor.ColorName, SexData.Sex, ItemStatus.ItemStatus, ProductionLine.productionLineName, ItemType.Id AS ItemTypeId FROM RecordReceiptDetails INNER JOIN RecordReceiptHeader ON RecordReceiptDetails.RecordReceiptHeaderId = RecordReceiptHeader.Id INNER JOIN Items ON RecordReceiptDetails.ItemId = Items.Id INNER JOIN ItemType ON Items.ItemTypeId = ItemType.Id INNER JOIN ItemUnit ON Items.ItemunitId = ItemUnit.Id INNER JOIN SupplyOrderDetails ON RecordReceiptDetails.SupplyOrderDetailsId = SupplyOrderDetails.Id INNER JOIN PurchaseOredrDetails ON Items.Id = PurchaseOredrDetails.ItemId AND SupplyOrderDetails.PurchaseOrderDetailsId = PurchaseOredrDetails.Id INNER JOIN PurchaseOrderHeader ON ItemType.Id = PurchaseOrderHeader.ItemTypeId AND PurchaseOredrDetails.PurchaseOredeHeaderId = PurchaseOrderHeader.Id INNER JOIN Department ON PurchaseOrderHeader.DepartmentId = Department.Id INNER JOIN AuditDetails ON RecordReceiptDetails.Id = AuditDetails.RecordReceiptDetailsId INNER JOIN AuditHeader ON AuditDetails.AuditHeaderId = AuditHeader.Id INNER JOIN ItemColor ON PurchaseOredrDetails.ItemColorId = ItemColor.Id INNER JOIN ItemStatus ON Items.ItemStatus = ItemStatus.Id INNER JOIN ProductionLine ON Items.ProductionLineId = ProductionLine.Id INNER JOIN SexData ON Items.Sex = SexData.Id WHERE (ItemType.Id = @ItemTypeId) AND (RecordReceiptHeader.RecordReceiptNo = @RecordReceiptNo)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlItemType" DefaultValue="0" Name="ItemTypeId" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="txtAuditNo" DefaultValue="0" Name="RecordReceiptNo" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:SqlDataSource ID="dbItemType" runat="server" ConnectionString="<%$ ConnectionStrings:ADAMConnectionString %>" SelectCommand="SELECT 0 AS Id, '---' AS Name UNION SELECT Id, Name FROM ItemType"></asp:SqlDataSource>
 
            </td>
        </tr>
    </table>
</asp:Content>
