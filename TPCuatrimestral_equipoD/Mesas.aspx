<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Mesas.aspx.cs" Inherits="TPCuatrimestral_equipoD.Mesas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
            <h2>Mesas</h2>
            <p class="text-secondary">Estado actual de las mesas del restaurante</p>
        </div>
        <a href="FormularioMesa.aspx" class="btn btn-primary">Agregar Mesa</a>
    </div>

    <div class="row g-4">
        <asp:Repeater ID="repMesas" runat="server">
            <ItemTemplate>
                <div class="col-md-4">
                    <div class="card shadow-sm h-100">
                        <div class="card-body">
                            <h4 class="card-title">Mesa <%# Eval("numeromesa") %></h4>
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
                            <div class="d-flex gap-2">
                                <a href='GestionPedido.aspx?idMesa=<%# Eval("id") %>' class="btn btn-primary btn-sm">
                                    Gestionar
                                </a>
                                <a href='FormularioMesa.aspx?id=<%# Eval("id") %>' class="btn btn-outline-secondary btn-sm">
                                    Editar
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
