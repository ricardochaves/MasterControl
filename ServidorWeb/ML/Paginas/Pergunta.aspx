﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pergunta.aspx.cs" Inherits="ServidorWeb.ML.Paginas.Pergunta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="txtPergunta" runat="server" Height="82px" TextMode="MultiLine" 
        Width="554px"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:TextBox ID="txtResposta" runat="server" Height="31px" Width="548px"></asp:TextBox>
    <br />
    <br />
    <asp:TextBox ID="TextBox2" runat="server" Height="43px" Width="511px"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Responder" 
        onclick="Button1_Click" />
    <br />
</asp:Content>