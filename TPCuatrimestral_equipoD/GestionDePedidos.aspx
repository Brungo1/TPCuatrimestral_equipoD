<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="GestionDePedidos.aspx.cs"
    Inherits="TPCuatrimestral_equipoD.GestionDePedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
            <h2>Gestion de Pedido</h2>
            <p class="text-secondary">
                Apertura y administracion de pedidos
            </p>
        </div>

        <a href="Pedidos.aspx" class="btn btn-secondary">
            Volver
        </a>
    </div>


    <asp:Label
        ID="lblExito"
        runat="server"
        CssClass="alert alert-success d-block"
        Visible="false">
    </asp:Label>

    <asp:Label
        ID="lblError"
        runat="server"
        CssClass="alert alert-danger d-block"
        Visible="false">
    </asp:Label>


    <!-- NUEVO PEDIDO -->
    <asp:Panel ID="pnlNuevoPedido" runat="server">

        <div class="card shadow-sm">

            <div class="card-body">

                <h4 class="mb-4">Nuevo Pedido</h4>

                <div class="row g-3">

                    <div class="col-md-6">

                        <label class="form-label">
                            Mesa
                        </label>

                        <asp:DropDownList
                            ID="ddlMesa"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>

                    </div>


                    <div class="col-md-6">

                        <label class="form-label">
                            Mesero
                        </label>

                        <asp:DropDownList
                            ID="ddlMesero"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>

                    </div>

                </div>


                <div class="mt-4">

                    <asp:Button
                        ID="btnAbrirPedido"
                        runat="server"
                        Text="Abrir Pedido"
                        CssClass="btn btn-primary"
                        OnClick="btnAbrirPedido_Click" />

                </div>

            </div>

        </div>

    </asp:Panel>



    <!-- PEDIDO EXISTENTE -->
    <asp:Panel
        ID="pnlPedido"
        runat="server"
        Visible="false">


        <!-- DATOS DEL PEDIDO -->

        <div class="card shadow-sm mb-4">

            <div class="card-body">

                <div class="row">

                    <div class="col-md-3">

                        <strong>Pedido</strong>

                        <br />

                        <asp:Label
                            ID="lblNumeroPedido"
                            runat="server">
                        </asp:Label>

                    </div>


                    <div class="col-md-3">

                        <strong>Mesa</strong>

                        <br />

                        <asp:Label
                            ID="lblMesa"
                            runat="server">
                        </asp:Label>

                    </div>


                    <div class="col-md-3">

                        <strong>Mesero</strong>

                        <br />

                        <asp:Label
                            ID="lblMesero"
                            runat="server">
                        </asp:Label>

                    </div>


                    <div class="col-md-3">

                        <strong>Estado</strong>

                        <br />

                        <asp:Label
                            ID="lblEstado"
                            runat="server">
                        </asp:Label>

                    </div>

                </div>


                <hr />


                <div class="row">

                    <div class="col-md-6">

                        <strong>Apertura:</strong>

                        <asp:Label
                            ID="lblApertura"
                            runat="server">
                        </asp:Label>

                    </div>


                    <div class="col-md-6">

                        <strong>Cierre:</strong>

                        <asp:Label
                            ID="lblCierre"
                            runat="server">
                        </asp:Label>

                    </div>

                </div>

            </div>

        </div>



        <!-- AGREGAR INSUMO -->

        <asp:Panel
            ID="pnlAgregarInsumo"
            runat="server">

            <div class="card shadow-sm mb-4">

                <div class="card-body">

                    <h5 class="mb-3">
                        Agregar Insumo
                    </h5>

                    <div class="row g-3 align-items-end">

                        <div class="col-md-6">

                            <label class="form-label">
                                Insumo
                            </label>

                            <asp:DropDownList
                                ID="ddlInsumo"
                                runat="server"
                                CssClass="form-select">
                            </asp:DropDownList>

                        </div>


                        <div class="col-md-2">

                            <label class="form-label">
                                Cantidad
                            </label>

                            <asp:TextBox
                                ID="txtCantidad"
                                runat="server"
                                TextMode="Number"
                                CssClass="form-control"
                                Text="1">
                            </asp:TextBox>

                        </div>


                        <div class="col-md-4">

                            <asp:Button
                                ID="btnAgregarInsumo"
                                runat="server"
                                Text="Agregar al Pedido"
                                CssClass="btn btn-success"
                                OnClick="btnAgregarInsumo_Click" />

                        </div>

                    </div>

                </div>

            </div>

        </asp:Panel>



        <!-- DETALLE -->

        <div class="card shadow-sm mb-4">

            <div class="card-body">

                <h5 class="mb-3">
                    Detalle del Pedido
                </h5>


                <asp:GridView
                    ID="dgvDetalle"
                    runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover align-middle"
                    EmptyDataText="El pedido todavia no tiene insumos."
                    OnRowCommand="dgvDetalle_Accion">

                    <Columns>

                        <asp:TemplateField HeaderText="Insumo">

                            <ItemTemplate>

                                <%# Eval("insumo.nombre") %>

                            </ItemTemplate>

                        </asp:TemplateField>


                        <asp:BoundField
                            DataField="cantidad"
                            HeaderText="Cantidad" />


                        <asp:TemplateField HeaderText="Precio Unitario">

                            <ItemTemplate>

                                $ <%# ((decimal)Eval("preciounitario")).ToString("N2") %>

                            </ItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Subtotal">

                            <ItemTemplate>

                                $ <%# ((decimal)Eval("subtotal")).ToString("N2") %>

                            </ItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Acciones">

                            <ItemTemplate>

                                <asp:LinkButton
                                    ID="btnQuitar"
                                    runat="server"
                                    Text="Quitar"
                                    CommandName="Quitar"
                                    CommandArgument='<%# Eval("id") %>'
                                    CssClass="btn btn-sm btn-outline-danger"
                                    OnClientClick="return confirm('Seguro que queres quitar este insumo?');">
                                </asp:LinkButton>

                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>


                <div class="text-end mt-3">

                    <h4>

                        Total:

                        <asp:Label
                            ID="lblTotal"
                            runat="server"
                            CssClass="text-success">
                        </asp:Label>

                    </h4>

                </div>

            </div>

        </div>



        <!-- CERRAR / CANCELAR -->

        <asp:Panel
            ID="pnlAcciones"
            runat="server">

            <div class="d-flex gap-2">

                <asp:Button
                    ID="btnCerrarPedido"
                    runat="server"
                    Text="Cerrar Pedido"
                    CssClass="btn btn-success"
                    OnClick="btnCerrarPedido_Click"
                    OnClientClick="return confirm('Seguro que queres cerrar el pedido?');" />


                <asp:Button
                    ID="btnCancelarPedido"
                    runat="server"
                    Text="Cancelar Pedido"
                    CssClass="btn btn-danger"
                    OnClick="btnCancelarPedido_Click"
                    OnClientClick="return confirm('Seguro que queres cancelar el pedido?');" />

            </div>

        </asp:Panel>


    </asp:Panel>

</asp:Content>