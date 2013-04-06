<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InicioML.aspx.cs" Inherits="ServidorWeb.ML.Paginas.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="AccessToken: "></asp:Label>
<br />
    <asp:TextBox ID="TextBox1" runat="server" Width="292px"></asp:TextBox>
    <br />
    <asp:TextBox ID="TextBox2" runat="server" Width="289px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
        Text="TrataCallBack" />
    <br />
<br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="ImportaVendasML" />
    <br />
    <br />
    <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
        Text="Perguntas" />
    <br />
    <br />
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
        Text="RelatorioProdutos" />
    <br />
    <br />
    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
        Text="ResumoPagamentoItem" />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="Importar MercadoPago" />
    <br />
    <br />
    <br />
    <asp:Button ID="Button7" runat="server" onclick="Button7_Click" Text="Logar" />
    <br />
    <br />
    </asp:Content>
