<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioInsumo.aspx.cs" Inherits="TPCuatrimestral_equipoD.FormularioInsumo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center">

        <div class="col-md-6">

            <div class="card shadow-sm">

                <div class="card-body">

                    <h2 class="mb-4">Insumo</h2>

                    <asp:Label ID="lblMensaje"
                        runat="server"
                        CssClass="alert alert-danger d-block"
                        Visible="false">
                    </asp:Label>

                    <div class="mb-3">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Descripcion</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Categoria</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Precio</label>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Stock Actual</label>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>

                    <div class="form-check mb-4">
                        <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
                        <label class="form-check-label">Activo</label>
                    </div>

                    <div class="d-flex gap-2">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        <a href="Insumos.aspx" class="btn btn-secondary">Cancelar</a>
                    </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
