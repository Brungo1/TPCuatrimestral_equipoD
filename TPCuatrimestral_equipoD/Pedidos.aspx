<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Pedidos.aspx.cs" Inherits="TPCuatrimestral_equipoD.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
            <h2>Gestion de Pedidos</h2>
            <p class="text-secondary">Administracion de pedidos del restaurante</p>
        </div>
        <a href="GestionPedido.aspx" class="btn btn-primary">Nuevo Pedido</a>
    </div>

    <asp:Label ID="lblExito" runat="server" CssClass="alert alert-success d-block" Visible="false"></asp:Label>
    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block" Visible="false"></asp:Label>

    <div class="card shadow-sm">
        <div class="card-body">

            <div class="row mb-3">
                <div class="col-md-3">
                    <label class="form-label">Filtrar por estado</label>
                    <asp:DropDownList ID="ddlFiltroEstado" runat="server" CssClass="form-select form-select-sm"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="0" />
                        <asp:ListItem Text="Abiertos" Value="1" />
                        <asp:ListItem Text="Cerrados" Value="2" />
                        <asp:ListItem Text="Cancelados" Value="3" />
                    </asp:DropDownList>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView
                    ID="dgvPedidos"
                    runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover align-middle"
                    EmptyDataText="No hay pedidos registrados para el filtro seleccionado."
                    OnRowCommand="dgvPedidos_RowCommand">

                    <Columns>

                        <asp:TemplateField HeaderText="Pedido">
                            <ItemTemplate>
                                #<%# Eval("id") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mesa">
                            <ItemTemplate>
                                Mesa <%# Eval("mesa.numeromesa") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mesero">
                            <ItemTemplate>
                                <%# Eval("mesero.nombre") + " " + Eval("mesero.apellido") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Apertura">
                            <ItemTemplate>
                                <%# ((DateTime)Eval("fechahoraapertura")).ToString("dd/MM/yyyy HH:mm") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='<%# ObtenerClaseBadge((Dominio.EstadoPedido)Eval("estado")) %>'>
                                    <%# Eval("estado") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                $ <%# ((decimal)Eval("total")).ToString("N2") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="d-flex gap-2">
                                    <a href='GestionPedido.aspx?id=<%# Eval("id") %>' class="btn btn-sm btn-outline-primary">
                                        <%# (Dominio.EstadoPedido)Eval("estado") == Dominio.EstadoPedido.Abierto ? "Gestionar" : "Ver" %>
                                    </a>

                                    <asp:LinkButton
                                        ID="btnCerrar"
                                        runat="server"
                                        CommandName="Cerrar"
                                        CommandArgument='<%# Eval("id") %>'
                                        Visible='<%# (Dominio.EstadoPedido)Eval("estado") == Dominio.EstadoPedido.Abierto %>'
                                        CssClass="btn btn-sm btn-outline-danger"
                                        OnClientClick="return confirm('Seguro que queres cerrar este pedido y liberar la mesa?');">
                                        Cerrar
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>

        </div>
    </div>

</asp:Content>