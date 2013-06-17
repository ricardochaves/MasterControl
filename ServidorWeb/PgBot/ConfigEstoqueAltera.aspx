<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfigEstoqueAltera.aspx.cs" Inherits="ServidorWeb.PgBot.ConfigEstoqueAltera" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Qtd"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" Width="74px"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
</asp:Content>
