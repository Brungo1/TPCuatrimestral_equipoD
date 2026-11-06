<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Usuarios.aspx.cs" Inherits="TPCuatrimestral_equipoD.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">

        <div>
            <h2>Gestion de Usuarios</h2>

            <p class="text-secondary">
                Administracion de gerentes y meseros
           
            </p>
        </div>

        <a href="FormularioUsuario.aspx" class="btn btn-primary">Agregar Usuario
        </a>

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
                    ID="dgvUsuarios"
                    runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover align-middle"
                    OnRowCommand="dgvUsuarios_Accion">

                    <Columns>

                        <asp:BoundField
                            DataField="nombreusuario"
                            HeaderText="Usuario" />

                        <asp:BoundField
                            DataField="nombre"
                            HeaderText="Nombre" />

                        <asp:BoundField
                            DataField="apellido"
                            HeaderText="Apellido" />

                        <asp:BoundField
                            DataField="perfil"
                            HeaderText="Perfil" />

                        <asp:TemplateField HeaderText="Estado">

                            <ItemTemplate>

                                <%# (bool)Eval("activo")
                                    ? "<span class='badge bg-success'>Activo</span>"
                                    : "<span class='badge bg-danger'>Inactivo</span>" %>
                            </ItemTemplate>

                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="Acciones">

    <ItemTemplate>

        <a href='FormularioUsuario.aspx?id=<%# Eval("id") %>'
           class="btn btn-sm btn-outline-primary">
            Editar
        </a>

        <asp:LinkButton
            ID="btnEstado"
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
            OnClientClick="return confirm('¿Seguro que queres eliminar este usuario?');">

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
