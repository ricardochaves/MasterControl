<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResumoPagamentoItem.aspx.cs" Inherits="ServidorWeb.ML.Paginas.ResumoPagamentoItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Todas as vendas que nos qualificamos positivo, o valor liquido é o bruto 
    multiplicado por 0,925.<br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    
</asp:Content>
