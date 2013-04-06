<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VendasProduto.aspx.cs" Inherits="ServidorWeb.ML.Paginas.VendasProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Consultar" />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server">
    
    <Columns>
     <asp:TemplateField HeaderText="Cliente" InsertVisible="False" 
            SortExpression="cmpName">
       <ItemTemplate>
          <asp:HyperLink ID="id_buyer" runat="server" 
               NavigateUrl='<%# Eval("id_buyer", "cliente.aspx?code={0}") %>'
               Text='<%# Eval("id_buyer") %>'>
          </asp:HyperLink>
       </ItemTemplate>
     </asp:TemplateField>
  </Columns>
    
    </asp:GridView>
    
    

    <br />
    <br />
    <asp:Label ID="Label2" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="GridView2" runat="server">

        <Columns>
     <asp:TemplateField HeaderText="Cliente" InsertVisible="False" 
            SortExpression="cmpName">
       <ItemTemplate>
          <asp:HyperLink ID="id_buyer" runat="server" 
               NavigateUrl='<%# Eval("id_buyer", "cliente.aspx?code={0}") %>'
               Text='<%# Eval("id_buyer") %>'>
          </asp:HyperLink>
       </ItemTemplate>
     </asp:TemplateField>
  </Columns>

    </asp:GridView>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="GridView3" runat="server">
            <Columns>
     <asp:TemplateField HeaderText="Pagamento" InsertVisible="False" 
            SortExpression="cmpName">
       <ItemTemplate>
          <asp:HyperLink ID="id_buyer" runat="server" 
               NavigateUrl='<%# Eval("PagamentoID", "Pagamento.aspx?code={0}") %>'
               Text='<%# Eval("PagamentoID") %>'>
          </asp:HyperLink>
       </ItemTemplate>
     </asp:TemplateField>
  </Columns>
    </asp:GridView>
</asp:Content>
