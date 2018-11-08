<%@ Page Language="C#" Trace="false" Debug="true"
   CodeFile="~/Modification.aspx.cs"
   Inherits="Modification"%>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
    <title>Modification de DVD</title>   
   <style type="text/css">
      
   </style>
</head>

<body>
    <form id="form1" runat="server">   
       <asp:Label runat="server" CssClass="sCenter">Modification de DVD</asp:Label>
       <br />
       <br />
       <br />
       <br />

       <asp:DropDownList ID="ddl" runat="server">
          <asp:ListItem Text="C'est l'apocalypse" Value="1" Selected="True" />
       </asp:DropDownList>
       
       <br />
       <br />
       
       <asp:PlaceHolder id="phDynamique" runat="server" />
    </form>
</body>
</html>
