<%@ Page Language="C#" Trace="false" Debug="true"
   CodeFile="~/Ajout.aspx.cs"
   Inherits="Ajout"%>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
    <title>Ajout de DVD</title>   
   <style type="text/css">
      .sCenter  {vertical-align: center;}
   </style>
</head>

<body>
    <form id="form1" runat="server">   
       <asp:Label runat="server" CssClass="sCenter">Ajout de DVD</asp:Label>
       <br />
       <br />

       <asp:PlaceHolder id="phDynamique" runat="server" />
    </form>
</body>
</html>
