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

        <button type="button" class="btn btn-primary">
            Agregar Usuario
        </button>

    </div>


    <div class="card shadow-sm">

        <div class="card-body">

            <div class="table-responsive">

                <table class="table table-hover align-middle">

                    <thead>
                        <tr>
                            <th>Usuario</th>
                            <th>Nombre</th>
                            <th>Apellido</th>
                            <th>Perfil</th>
                            <th>Estado</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>

                    <tbody>

                        <tr>
                            <td>gerente</td>
                            <td>Carlos</td>
                            <td>Gomez</td>

                            <td>
                                <span class="badge bg-dark">
                                    Gerente
                                </span>
                            </td>

                            <td>
                                <span class="badge bg-success">
                                    Activo
                                </span>
                            </td>

                            <td>
                                <button type="button" class="btn btn-sm btn-outline-primary">
                                    Editar
                                </button>
                            </td>
                        </tr>


                        <tr>
                            <td>mesero1</td>
                            <td>Juan</td>
                            <td>Perez</td>

                            <td>
                                <span class="badge bg-primary">
                                    Mesero
                                </span>
                            </td>

                            <td>
                                <span class="badge bg-success">
                                    Activo
                                </span>
                            </td>

                            <td>
                                <button type="button" class="btn btn-sm btn-outline-primary">
                                    Editar
                                </button>
                            </td>
                        </tr>

                    </tbody>

                </table>

            </div>

        </div>

    </div>

</asp:Content>