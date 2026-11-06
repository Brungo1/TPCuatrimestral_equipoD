<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoriasInsumo.aspx.cs" Inherits="TPCuatrimestral_equipoD.CategoriasInsumo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
            <h2>Categorias de Insumos</h2>
            <p class="text-secondary">
                Administracion de categorias de platos y bebidas
            </p>
        </div>
        <a href="Insumos.aspx" class="btn btn-outline-secondary">Volver a Insumos</a>
    </div>

    <asp:Label
        ID="lblMensaje"
        runat="server"
        CssClass="alert alert-danger d-block"
        Visible="false">
    </asp:Label>

    <div class="card shadow-sm mb-4">
        <div class="card-body">
            <div class="row g-2 align-items-end">
                <div class="col-md-5">
                    <label class="form-label">Descripcion de la Categoria</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Ej: Entradas, Postres..." />
                </div>
                <div class="col-md-4 d-flex gap-2">
                    <asp:Button ID="btnAgregar" runat="server" Text="Guardar Categoria" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
                    <a href="CategoriasInsumo.aspx" class="btn btn-secondary">Cancelar</a>
                </div>
            </div>
        </div>
    </div>

    <div class="card shadow-sm">

        <div class="card-body">

            <div class="table-responsive">

                <asp:GridView
                    ID="dgvCategorias"
                    runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover align-middle"
                    OnRowCommand="dgvCategorias_RowCommand">

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
                                <%# (bool)Eval("activo")
                                    ? "<span class='badge bg-success'>Activa</span>"
                                    : "<span class='badge bg-danger'>Inactiva</span>" %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>

                                <a href='CategoriasInsumo.aspx?id=<%# Eval("id") %>'
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
                                    OnClientClick="return confirm('Seguro que queres eliminar esta categoria?');">
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
