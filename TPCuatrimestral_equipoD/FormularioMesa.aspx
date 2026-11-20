<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioMesa.aspx.cs" Inherits="TPCuatrimestral_equipoD.FormularioMesa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center">

        <div class="col-md-6">

            <div class="card shadow-sm">

                <div class="card-body">

                    <h2 class="mb-4">Mesa</h2>

                    <asp:Label ID="lblMensaje"
                        runat="server"
                        CssClass="alert alert-danger d-block"
                        Visible="false">
                    </asp:Label>

                    <div class="mb-3">
                        <label class="form-label">Numero de Mesa</label>
                        <asp:TextBox ID="txtNumeroMesa" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Capacidad</label>
                        <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Mesero Asignado</label>
                        <asp:DropDownList ID="ddlMesero" runat="server" CssClass="form-select" />
                    </div>

                    <div class="form-check mb-4">
                        <asp:CheckBox ID="chkActiva" runat="server" Checked="true" />
                        <label class="form-check-label">Activa</label>
                    </div>

                    <div class="d-flex gap-2">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        <a href="Mesas.aspx" class="btn btn-secondary">Cancelar</a>
                    </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
