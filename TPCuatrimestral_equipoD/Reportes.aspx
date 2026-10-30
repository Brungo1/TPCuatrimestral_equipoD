<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Reportes.aspx.cs" Inherits="TPCuatrimestral_equipoD.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <h2>Reportes</h2>
        <p class="text-secondary">
            Consulta de informacion del restaurante
        </p>
    </div>

    <div class="card shadow-sm mb-4">

        <div class="card-body">

            <h5 class="mb-3">Filtros</h5>

            <div class="row g-3">

                <div class="col-md-4">
                    <label class="form-label">Fecha desde</label>
                    <input type="date" class="form-control" />
                </div>

                <div class="col-md-4">
                    <label class="form-label">Fecha hasta</label>
                    <input type="date" class="form-control" />
                </div>

                <div class="col-md-4">
                    <label class="form-label">Mesero</label>

                    <select class="form-select">
                        <option>Todos</option>
                        <option>Juan Perez</option>
                        <option>Pedro Lopez</option>
                    </select>

                </div>

            </div>

            <div class="mt-4">
                <button type="button" class="btn btn-primary">
                    Generar Reporte
                </button>
            </div>

        </div>

    </div>


    <div class="row g-4">

        <div class="col-md-4">

            <div class="card shadow-sm h-100">

                <div class="card-body">

                    <h5>Ventas por Mesa</h5>

                    <p class="text-secondary">
                        Recaudacion generada por cada mesa.
                    </p>

                    <button type="button" class="btn btn-outline-primary">
                        Ver Reporte
                    </button>

                </div>

            </div>

        </div>


        <div class="col-md-4">

            <div class="card shadow-sm h-100">

                <div class="card-body">

                    <h5>Ventas por Mesero</h5>

                    <p class="text-secondary">
                        Recaudacion y pedidos atendidos por cada mesero.
                    </p>

                    <button type="button" class="btn btn-outline-primary">
                        Ver Reporte
                    </button>

                </div>

            </div>

        </div>


        <div class="col-md-4">

            <div class="card shadow-sm h-100">

                <div class="card-body">

                    <h5>Pedidos del Dia</h5>

                    <p class="text-secondary">
                        Resumen de pedidos abiertos y cerrados.
                    </p>

                    <button type="button" class="btn btn-outline-primary">
                        Ver Reporte
                    </button>

                </div>

            </div>

        </div>

    </div>

</asp:Content>