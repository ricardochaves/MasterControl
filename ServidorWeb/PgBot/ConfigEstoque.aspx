<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfigEstoque.aspx.cs" Inherits="ServidorWeb.PgBot.ConfigEstoque" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Button" />
    <br />
    <asp:GridView ID="GridView1" runat="server">

        <Columns>
             <asp:TemplateField HeaderText="Item" InsertVisible="False" 
                    SortExpression="cmpName">
               <ItemTemplate>
                  <asp:HyperLink ID="ItemID" runat="server" 
                       NavigateUrl='<%# Eval("ItemID", "http://www.wowhead.com/item={0}") %>'
                       Text='<%# Eval("Item") %>'>
                  </asp:HyperLink>
                  <a rel='<%# Eval("ItemID","item={0}") %>'></a>
               </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="Chave" InsertVisible="False" 
                    SortExpression="cmp2">
               <ItemTemplate>
                  <asp:HyperLink ID="Chave" runat="server" 
                       NavigateUrl='<%# Eval("Chave", "ConfigEstoqueAltera.aspx?code={0}") %>'
                       Text='<%# Eval("Chave") %>'>
                  </asp:HyperLink>
                  
               </ItemTemplate>
             </asp:TemplateField>

        </Columns>

    </asp:GridView>
    <br />
    </asp:Content>
