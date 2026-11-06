<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Insumos.aspx.cs" Inherits="TPCuatrimestral_equipoD.Insumos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">

        <div>
            <h2>Gestion de Insumos</h2>

            <p class="text-secondary">
                Administracion de platos, bebidas, precios y stock
            </p>
        </div>

        <div>
            <a href="CategoriasInsumo.aspx" class="btn btn-outline-secondary me-2">Categorias</a>
            <a href="FormularioInsumo.aspx" class="btn btn-primary">Agregar Insumo</a>
        </div>

    </div>

    <asp:Label
        ID="lblMensaje"
        runat="server"
        CssClass="alert alert-danger d-block"
        Visible="false">
    </asp:Label>

    <div class="card shadow-sm">

        <div class="card-body">

            <div class="table-responsive">

                <asp:GridView
                    ID="dgvInsumos"
                    runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover align-middle"
                    OnRowCommand="dgvInsumos_RowCommand">

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
                                <%# (bool)Eval("activo")
                                    ? "<span class='badge bg-success'>Activo</span>"
                                    : "<span class='badge bg-danger'>Inactivo</span>" %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>

                                <a href='FormularioInsumo.aspx?id=<%# Eval("id") %>'
                                   class="btn btn-sm btn-outline-primary">
                                    Editar
                                </a>

                                <asp:LinkButton
                                    ID="btnCambiarEstado"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-secondary"
                                    CommandName="CambiarEstado"
                                    CommandArgument='<%# Eval("id") + "," + Eval("activo") %>'>
                                    <%# (bool)Eval("activo") ? "Dar de Baja" : "Reactivar" %>
                                </asp:LinkButton>

                                <asp:LinkButton
                                    ID="btnEliminar"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("id") %>'
                                    OnClientClick="return confirm('Seguro que queres eliminar este insumo?');">
                                    Eliminar
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>

    </div>

</asp:Content>
