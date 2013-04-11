<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perguntas.aspx.cs" Inherits="ServidorWeb.ML.Paginas.Perguntas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        <br />
        <asp:GridView ID="GridView1" runat="server">
    
    <Columns>
     <asp:TemplateField HeaderText="ID_Pergunta" InsertVisible="False" 
            SortExpression="cmpName">
       <ItemTemplate>
          <asp:HyperLink ID="id" runat="server" 
               NavigateUrl='<%# Eval("id", "pergunta.aspx?code={0}") %>'
               Text='<%# Eval("id") %>'>
          </asp:HyperLink>
       </ItemTemplate>
     </asp:TemplateField>
  </Columns>
    
    </asp:GridView>
</asp:Content>
