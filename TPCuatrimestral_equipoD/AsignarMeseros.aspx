<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="AsignarMeseros.aspx.cs" Inherits="TPCuatrimestral_equipoD.AsignarMeseros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
            <h2>Asignacion de Meseros</h2>
            <p class="text-secondary">
                Asigne o reasigne los meseros responsables de cada mesa activa
            </p>
        </div>
        <a href="Mesas.aspx" class="btn btn-outline-secondary">Volver a Mesas</a>
    </div>

    <asp:Label
        ID="lblError"
        runat="server"
        CssClass="alert alert-danger d-block"
        Visible="false">
    </asp:Label>

    <asp:Label
        ID="lblExito"
        runat="server"
        CssClass="alert alert-success d-block"
        Visible="false">
    </asp:Label>

    <div class="card shadow-sm">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView
                    ID="dgvAsignaciones"
                    runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover align-middle"
                    EmptyDataText="No hay mesas activas para asignar."
                    OnRowDataBound="dgvAsignaciones_RowDataBound">

                    <Columns>

                        <asp:TemplateField HeaderText="Mesa">
                            <ItemTemplate>
                                Mesa <%# Eval("numeromesa") %>
                                <asp:HiddenField ID="hfIdMesa" runat="server" Value='<%# Eval("id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Capacidad">
                            <ItemTemplate>
                                <%# Eval("capacidad") %> personas
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='<%# ObtenerClaseBadge((Dominio.EstadoMesa)Eval("estado")) %>'>
                                    <%# Eval("estado") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mesero Asignado">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMesero" runat="server" CssClass="form-select form-select-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>

            <div class="d-flex justify-content-between align-items-center mt-3">
                <asp:Button
                    ID="btnLimpiarAsignaciones"
                    runat="server"
                    Text="Limpiar Asignaciones"
                    CssClass="btn btn-outline-danger"
                    OnClick="btnLimpiarAsignaciones_Click"
                    OnClientClick="return confirm('Seguro que queres quitar las asignaciones de todas las mesas no ocupadas?');" />

                <asp:Button
                    ID="btnGuardarAsignaciones"
                    runat="server"
                    Text="Guardar Asignaciones"
                    CssClass="btn btn-primary"
                    OnClick="btnGuardarAsignaciones_Click" />
            </div>
        </div>
    </div>

</asp:Content>

