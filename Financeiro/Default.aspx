<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Financeiro._Default" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<asp:Label ID="Label1" runat="server" Text="Nome: "></asp:Label>
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
<asp:Button ID="Button1" runat="server" Text="Inclui" onclick="Button1_Click" />
<asp:GridView ID="GridView1" runat="server">
</asp:GridView>
<asp:Button ID="Button2" runat="server" Text="Tudao" onclick="Button2_Click" />
   <br />
<asp:DropDownList ID="DropDownList1" runat="server" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
        AutoPostBack="True">
<asp:ListItem Value = "1">Mercadolivre</asp:ListItem>
<asp:ListItem Value = "2">BomNegocio</asp:ListItem>
<asp:ListItem Value = "3">OLX</asp:ListItem>
<asp:ListItem Value = "4">TodOferta</asp:ListItem>
<asp:ListItem Value = "5">Miguel</asp:ListItem>
</asp:DropDownList>

<asp:DropDownList ID="DropDownList2" runat="server">
</asp:DropDownList>
   
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Vender" />
   
</asp:Content>


