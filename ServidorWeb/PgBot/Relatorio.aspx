<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Relatorio.aspx.cs" Inherits="ServidorWeb.Relatorio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <div>
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        <asp:Calendar ID="Calendar2" runat="server" 
                onselectionchanged="Calendar2_SelectionChanged"></asp:Calendar>
        </div>
        <br />
        <asp:ListBox ID="ListBox1" runat="server" Height="222px" 
            onselectedindexchanged="ListBox1_SelectedIndexChanged" Width="553px">
        </asp:ListBox>
        <br />
        <br />
        <asp:ListBox ID="ListBox2" runat="server" Height="155px" Width="470px">
        </asp:ListBox>
        <br />
        <asp:ListBox ID="ListBox3" runat="server" Height="142px" Width="469px">
        </asp:ListBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    
      </div>

    </form>
</body>
</html>
