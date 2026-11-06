<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Insumos.aspx.cs" Inherits="TPCuatrimestral_equipoD.Insumos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>INSUMOS</h2>
        <div>
            <a href="CategoriasInsumo.aspx" class="btn btn-outline-secondary me-2">Categorias</a>
            <a href="FormularioInsumo.aspx" class="btn btn-primary">Agregar Insumo</a>
        </div>
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

    <asp:GridView ID="dgvInsumos" runat="server" CssClass="table table-striped table-hover align-middle" AutoGenerateColumns="false" OnRowCommand="dgvInsumos_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="ID">
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

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <%# (bool)Eval("activo") ? "Activo" : "Inactivo" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="btnCambiarEstado" runat="server"
                        CommandName="CambiarEstado"
                        CommandArgument='<%# Eval("id") + "," + Eval("activo") %>'
                        CssClass='<%# (bool)Eval("activo") ? "btn btn-sm btn-outline-warning me-1" : "btn btn-sm btn-outline-success me-1" %>'>
                        <%# (bool)Eval("activo") ? "Dar de Baja" : "Activar" %>
                    </asp:LinkButton>

                    <asp:LinkButton ID="btnEliminar" runat="server"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("id") %>'
                        OnClientClick="return confirm('Esta seguro que desea eliminar este insumo?');"
                        CssClass="btn btn-sm btn-outline-danger">
                        Eliminar
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
