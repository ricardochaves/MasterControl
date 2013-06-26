<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfigEstoqueIncluir.aspx.cs" Inherits="ServidorWeb.PgBot.ConfigEstoqueIncluir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="IDItem"></asp:Label>
    <asp:TextBox ID="txtIdItem" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Qtd"></asp:Label>
    <asp:TextBox ID="txtQtd" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Text="NomePersonagem"></asp:Label>
    <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
</asp:Content>
