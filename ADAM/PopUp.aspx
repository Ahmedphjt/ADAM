<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUp.aspx.cs" Inherits="ADAM.PopUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="FJScript.js"></script>
    <script src="SJScript.js" type="text/javascript"></script>
    <link href="JScript.css" rel="stylesheet" type="text/css" />  
    <script type="text/javascript">

        function btnDelete_onclick() {
           $("#popup").dialog({ title: "PopUp Message", width: 600, })
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="popup" style="display:none">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="return btnDelete_onclick()"/>

    </div>
       <input  id="btnDelete" type="button"  onclick="return btnDelete_onclick()" />
    </form>
    

</body>
</html>
