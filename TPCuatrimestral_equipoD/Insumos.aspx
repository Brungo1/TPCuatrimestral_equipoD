<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Insumos.aspx.cs" Inherits="TPCuatrimestral_equipoD.Insumos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>INSUMOS</h2>
        <a href="#" class="btn btn-primary">Agregar Insumo</a>
        </div>
    <asp:GridView ID="dgvInsumos" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false">
        <Columns>
           <asp:TemplateField Headertext ="ID">
               <ItemTemplate>
                   <%# Eval("id") %>
               </ItemTemplate>
           </asp:TemplateField>
             <asp:TemplateField HeaderText="Nombre">
            <ItemTemplate>
                <%# Eval("nombre") %>
            </ItemTemplate>
        </asp:TemplateField>
              <asp:TemplateField HeaderText="Descripcion">
            <ItemTemplate>
                <%# Eval("descripcion") %>
            </ItemTemplate>
        </asp:TemplateField>
              <asp:TemplateField HeaderText="Categoria">
            <ItemTemplate>
                <%# Eval("categoria.descripcion") %>
            </ItemTemplate>
        </asp:TemplateField>
               <asp:TemplateField HeaderText="Precio">
            <ItemTemplate>
                $ <%# Eval("precio") %>
            </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="Stock">
            <ItemTemplate>
                <%# Eval("stockactual") %>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <view
</asp:Content>
