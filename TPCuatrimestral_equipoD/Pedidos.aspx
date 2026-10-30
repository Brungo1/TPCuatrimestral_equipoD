<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Pedidos.aspx.cs" Inherits="TPCuatrimestral_equipoD.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">

        <div>
            <h2>Gestion de Pedidos</h2>
            <p class="text-secondary">
                Administracion de pedidos del restaurante
            </p>
        </div>

        <button type="button" class="btn btn-primary">
            Nuevo Pedido
        </button>

    </div>

    <div class="card shadow-sm">

        <div class="card-body">

            <div class="table-responsive">

                <table class="table table-hover align-middle">

                    <thead>
                        <tr>
                            <th>Pedido</th>
                            <th>Mesa</th>
                            <th>Mesero</th>
                            <th>Apertura</th>
                            <th>Estado</th>
                            <th>Total</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>

                    <tbody>

                        <tr>
                            <td>#1</td>
                            <td>Mesa 2</td>
                            <td>Juan Perez</td>
                            <td>20:30</td>

                            <td>
                                <span class="badge bg-success">
                                    Abierto
                                </span>
                            </td>

                            <td>$ 25.000</td>

                            <td>
                                <button type="button" class="btn btn-sm btn-outline-primary">
                                    Ver
                                </button>

                                <button type="button" class="btn btn-sm btn-outline-danger">
                                    Cerrar
                                </button>
                            </td>
                        </tr>

                        <tr>
                            <td>#2</td>
                            <td>Mesa 4</td>
                            <td>Pedro Lopez</td>
                            <td>19:45</td>

                            <td>
                                <span class="badge bg-secondary">
                                    Cerrado
                                </span>
                            </td>

                            <td>$ 38.500</td>

                            <td>
                                <button type="button" class="btn btn-sm btn-outline-primary">
                                    Ver
                                </button>
                            </td>
                        </tr>

                    </tbody>

                </table>

            </div>

        </div>

    </div>

</asp:Content>