<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TratarCallBack.aspx.cs" Inherits="ServidorWeb.ML.Paginas.TratarCallBack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="TextBox1" runat="server" Width="292px"></asp:TextBox>
    <br />
    <asp:TextBox ID="TextBox2" runat="server" Width="289px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Executar" />
    <br />
    <br />
    <br />
</asp:Content>
