<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Mesas.aspx.cs" Inherits="TPCuatrimestral_equipoD.Mesas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
            <h2>Gestion de Mesas</h2>
            <p class="text-secondary">Estado actual y administracion de las mesas del restaurante</p>
        </div>
        <div id="divAccionesGerente" runat="server">
            <a href="AsignarMeseros.aspx" class="btn btn-outline-primary me-2">Asignar Meseros</a>
            <a href="FormularioMesa.aspx" class="btn btn-primary">Agregar Mesa</a>
        </div>
    </div>

    <asp:Label
        ID="lblMensaje"
        runat="server"
        CssClass="alert alert-danger d-block"
        Visible="false">
    </asp:Label>

    <asp:Panel
        ID="pnlSinMesas"
        runat="server"
        CssClass="alert alert-info"
        Visible="false">
        <asp:Label ID="lblSinMesas" runat="server" />
    </asp:Panel>

    <div class="row g-4">
        <asp:Repeater ID="repMesas" runat="server" OnItemCommand="repMesas_ItemCommand">
            <ItemTemplate>
                <div class="col-md-4">
                    <div class='<%# (bool)Eval("activa") ? "card shadow-sm h-100" : "card shadow-sm h-100 bg-light opacity-75" %>'>
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center mb-2">
                                <h4 class="card-title mb-0">Mesa <%# Eval("numeromesa") %></h4>
                                <%# !(bool)Eval("activa") ? "<span class='badge bg-dark'>Inactiva</span>" : "" %>
                            </div>

                            <p>Capacidad: <%# Eval("capacidad") %> personas</p>
                            <p>
                                Estado:
                                <span class='<%# ObtenerClaseBadge((Dominio.EstadoMesa)Eval("estado")) %>'>
                                    <%# Eval("estado") %>
                                </span>
                            </p>
                            <p class="text-secondary">
                                <%# Eval("meseroasignado") != null
                                    ? "Mesero: " + Eval("meseroasignado.nombre") + " " + Eval("meseroasignado.apellido")
                                    : "Sin mesero asignado" %>
                            </p>

                            <div class="d-flex flex-wrap gap-2">
                                <asp:HyperLink
                                    ID="hlPedido"
                                    runat="server"
                                    NavigateUrl='<%# "GestionDePedidos.aspx?idMesa=" + Eval("id") %>'
                                    Visible='<%# (bool)Eval("activa") && (Dominio.EstadoMesa)Eval("estado") != Dominio.EstadoMesa.Cerrada %>'
                                    CssClass='<%# (Dominio.EstadoMesa)Eval("estado") == Dominio.EstadoMesa.Ocupada ? "btn btn-sm btn-danger" : "btn btn-sm btn-success" %>'
                                    Text='<%# (Dominio.EstadoMesa)Eval("estado") == Dominio.EstadoMesa.Ocupada ? "Gestionar Pedido" : "Abrir Pedido" %>' />

                                <asp:LinkButton
                                    ID="btnEstadoMesa"
                                    runat="server"
                                    CommandName="CambiarEstadoMesa"
                                    CommandArgument='<%# Eval("id") + "," + (int)(Dominio.EstadoMesa)Eval("estado") %>'
                                    Visible='<%# EsGerente && (bool)Eval("activa") && (Dominio.EstadoMesa)Eval("estado") != Dominio.EstadoMesa.Ocupada %>'
                                    CssClass='<%# (Dominio.EstadoMesa)Eval("estado") == Dominio.EstadoMesa.Libre ? "btn btn-sm btn-outline-warning" : "btn btn-sm btn-outline-success" %>'>
                                    <%# (Dominio.EstadoMesa)Eval("estado") == Dominio.EstadoMesa.Libre ? "Cerrar Mesa" : "Abrir Mesa" %>
                                </asp:LinkButton>

                                <asp:HyperLink
                                    ID="hlEditar"
                                    runat="server"
                                    NavigateUrl='<%# "FormularioMesa.aspx?id=" + Eval("id") %>'
                                    Visible='<%# EsGerente %>'
                                    CssClass="btn btn-sm btn-outline-primary"
                                    Text="Editar" />

                                <asp:LinkButton
                                    ID="btnCambiarActiva"
                                    runat="server"
                                    CommandName="CambiarActiva"
                                    CommandArgument='<%# Eval("id") + "," + Eval("activa") %>'
                                    Visible='<%# EsGerente %>'
                                    CssClass="btn btn-sm btn-outline-secondary">
                                    <%# (bool)Eval("activa") ? "Dar de Baja" : "Reactivar" %>
                                </asp:LinkButton>

                                <asp:LinkButton
                                    ID="btnEliminar"
                                    runat="server"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("id") %>'
                                    Visible='<%# EsGerente %>'
                                    CssClass="btn btn-sm btn-outline-danger"
                                    OnClientClick="return confirm('Seguro que queres eliminar esta mesa?');">
                                    Eliminar
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
