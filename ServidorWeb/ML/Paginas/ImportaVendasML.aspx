<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImportaVendasML.aspx.cs" Inherits="ServidorWeb.ML.Paginas.ImportaVendasML" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Aqui ele vai pegar todas as ordens e vai incluir no banco, caso o ID da ordem já 
    exista ele vai pular para a proxima.<br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <br />
    <br />
    Precisa fazer a lógica para atualizar uma venda, o sistema tem que pegar todas 
    as vendas não finalizadas e uma a uma pegar os dados, substituir no objeto e dar 
    o update.
</asp:Content>
