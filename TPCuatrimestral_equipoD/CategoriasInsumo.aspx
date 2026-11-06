<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoriasInsumo.aspx.cs" Inherits="TPCuatrimestral_equipoD.CategoriasInsumo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>CATEGORIAS DE INSUMOS</h2>
        <a href="Insumos.aspx" class="btn btn-outline-secondary">Volver a Insumos</a>
    </div>

    <div class="row g-2 align-items-end mb-4">
        <div class="col-md-5">
            <label class="form-label">Nueva Categoria</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Descripcion de la categoria..." />
        </div>
        <div class="col-md-3">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar Categoria" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
        </div>
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

    <asp:GridView ID="dgvCategorias" runat="server" CssClass="table table-striped table-hover align-middle" AutoGenerateColumns="false" OnRowCommand="dgvCategorias_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <%# Eval("id") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Descripcion">
                <ItemTemplate>
                    <%# Eval("descripcion") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <%# (bool)Eval("activo") ? "Activa" : "Inactiva" %>
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
                        OnClientClick="return confirm('Esta seguro que desea eliminar esta categoria?');"
                        CssClass="btn btn-sm btn-outline-danger">
                        Eliminar
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>

