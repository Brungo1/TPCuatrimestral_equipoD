<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Mesas.aspx.cs" Inherits="TPCuatrimestral_equipoD.Mesas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">

        <div>
            <h2>Mesas</h2>
            <p class="text-secondary">
                Estado actual de las mesas del restaurante
            </p>
        </div>

        <button type="button" class="btn btn-primary">
            Agregar Mesa
        </button>

    </div>


    <div class="row g-4">

        <div class="col-md-4">

            <div class="card shadow-sm h-100">

                <div class="card-body">

                    <h4 class="card-title">
                        Mesa 1
                    </h4>

                    <p>
                        Capacidad: 4 personas
                    </p>

                    <p>
                        Estado:
                        <span class="badge bg-success">
                            Libre
                        </span>
                    </p>

                    <p class="text-secondary">
                        Sin mesero asignado
                    </p>

                    <button type="button" class="btn btn-outline-primary">
                        Gestionar
                    </button>

                </div>

            </div>

        </div>


        <div class="col-md-4">

            <div class="card shadow-sm h-100">

                <div class="card-body">

                    <h4 class="card-title">
                        Mesa 2
                    </h4>

                    <p>
                        Capacidad: 2 personas
                    </p>

                    <p>
                        Estado:
                        <span class="badge bg-danger">
                            Ocupada
                        </span>
                    </p>

                    <p class="text-secondary">
                        Mesero: Juan Perez
                    </p>

                    <button type="button" class="btn btn-outline-primary">
                        Gestionar
                    </button>

                </div>

            </div>

        </div>


        <div class="col-md-4">

            <div class="card shadow-sm h-100">

                <div class="card-body">

                    <h4 class="card-title">
                        Mesa 3
                    </h4>

                    <p>
                        Capacidad: 6 personas
                    </p>

                    <p>
                        Estado:
                        <span class="badge bg-secondary">
                            Cerrada
                        </span>
                    </p>

                    <p class="text-secondary">
                        Sin mesero asignado
                    </p>

                    <button type="button" class="btn btn-outline-primary">
                        Gestionar
                    </button>

                </div>

            </div>

        </div>

    </div>

</asp:Content>