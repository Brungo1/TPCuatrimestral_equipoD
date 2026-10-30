<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TPCuatrimestral_equipoD.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="text-center mb-5">
        <h1 class="display-4 fw-bold">Resto WEB</h1>

        <p class="lead text-secondary">
            Sistema de Gestion de Mesas y Pedidos
        </p>
    </div>


    <div class="row g-4 justify-content-center">

        <div class="col-md-4">
            <div class="card shadow-sm h-100">
                <div class="card-body text-center">

                    <h3>Mesas</h3>

                    <p class="text-secondary">
                        Gestion de mesas y estados.
                    </p>

                    <a href="Mesas.aspx" class="btn btn-primary">
                        Ver Mesas
                    </a>

                </div>
            </div>
        </div>


        <div class="col-md-4">
            <div class="card shadow-sm h-100">
                <div class="card-body text-center">

                    <h3>Insumos</h3>

                    <p class="text-secondary">
                        Consulta de platos bebidas precios y stock.
                    </p>

                    <a href="Insumos.aspx" class="btn btn-success">
                        Ver Insumos
                    </a>

                </div>
            </div>
        </div>


        <div class="col-md-4">
            <div class="card shadow-sm h-100">
                <div class="card-body text-center">

                    <h3>Pedidos</h3>

                    <p class="text-secondary">
                        Gestion de pedidos del restaurante.
                    </p>

                    <a href="Pedidos.aspx" class="btn btn-warning">
                        Ver Pedidos
                    </a>

                </div>
            </div>
        </div>


        <div class="col-md-4">
            <div class="card shadow-sm h-100">
                <div class="card-body text-center">

                    <h3>Usuarios</h3>

                    <p class="text-secondary">
                        Gestion de gerentes y meseros.
                    </p>

                    <a href="Usuarios.aspx" class="btn btn-info">
                        Ver Usuarios
                    </a>

                </div>
            </div>
        </div>


        <div class="col-md-4">
            <div class="card shadow-sm h-100">
                <div class="card-body text-center">

                    <h3>Reportes</h3>

                    <p class="text-secondary">
                        Consulta de reportes del restaurante.
                    </p>

                    <a href="Reportes.aspx" class="btn btn-dark">
                        Ver Reportes
                    </a>

                </div>
            </div>
        </div>

    </div>

</asp:Content>