<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Reportes.aspx.cs"
    Inherits="TPCuatrimestral_equipoD.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <h2>Reportes</h2>
        <p class="text-secondary">
            Informacion de pedidos y recaudacion
        </p>
    </div>

    <div class="card shadow-sm mb-4">
        <div class="card-body">

            <div class="row g-3 align-items-end">

                <div class="col-md-4">
                    <label class="form-label">Fecha desde</label>

                    <asp:TextBox
                        ID="txtDesde"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label class="form-label">Fecha hasta</label>

                    <asp:TextBox
                        ID="txtHasta"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">

                    <asp:Button
                        ID="btnGenerar"
                        runat="server"
                        Text="Generar Reporte"
                        CssClass="btn btn-primary"
                        OnClick="btnGenerar_Click" />

                </div>

            </div>

        </div>
    </div>


    <div class="row g-4 mb-4">

        <div class="col-md-6">
            <div class="card shadow-sm">
                <div class="card-body text-center">

                    <h5>Pedidos Cerrados</h5>

                    <asp:Label
                        ID="lblCantidadPedidos"
                        runat="server"
                        CssClass="fs-2 fw-bold">
                    </asp:Label>

                </div>
            </div>
        </div>


        <div class="col-md-6">
            <div class="card shadow-sm">
                <div class="card-body text-center">

                    <h5>Total Recaudado</h5>

                    <asp:Label
                        ID="lblTotalRecaudado"
                        runat="server"
                        CssClass="fs-2 fw-bold text-success">
                    </asp:Label>

                </div>
            </div>
        </div>

    </div>


    <div class="card shadow-sm mb-4">

        <div class="card-body">

            <h4 class="mb-3">
                Recaudacion por Mesa
            </h4>

            <asp:GridView
                ID="dgvMesas"
                runat="server"
                AutoGenerateColumns="false"
                CssClass="table table-hover">

                <Columns>

                    <asp:BoundField
                        DataField="Mesa"
                        HeaderText="Mesa" />

                    <asp:BoundField
                        DataField="Pedidos"
                        HeaderText="Cantidad de Pedidos" />

                    <asp:BoundField
                        DataField="Total"
                        HeaderText="Recaudacion"
                        DataFormatString="${0:N2}" />

                </Columns>

            </asp:GridView>

        </div>

    </div>


    <div class="card shadow-sm">

        <div class="card-body">

            <h4 class="mb-3">
                Recaudacion por Mesero
            </h4>

            <asp:GridView
                ID="dgvMeseros"
                runat="server"
                AutoGenerateColumns="false"
                CssClass="table table-hover">

                <Columns>

                    <asp:BoundField
                        DataField="Mesero"
                        HeaderText="Mesero" />

                    <asp:BoundField
                        DataField="Pedidos"
                        HeaderText="Cantidad de Pedidos" />

                    <asp:BoundField
                        DataField="Total"
                        HeaderText="Recaudacion"
                        DataFormatString="${0:N2}" />

                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>