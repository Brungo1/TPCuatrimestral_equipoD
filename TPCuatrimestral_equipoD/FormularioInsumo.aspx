<%@ Page Title="Formulario Insumo" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioInsumo.aspx.cs" Inherits="TPCuatrimestral_equipoD.FormularioInsumo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-6">
            <h2>Agregar/Editar Insumo</h2>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" 
                    ErrorMessage="El nombre es requerido" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="ddlCategoria" class="form-label">Categoría</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="txtPrecio" class="form-label">Precio</label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio" 
                    ErrorMessage="Ingrese un precio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtPrecio"
                    ValidationExpression="^\d+(\,\d+)?$" ErrorMessage="Solo números (use coma para decimales)" 
                    CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>

            <div class="mb-3">
                <label for="txtStock" class="form-label">Stock Actual</label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvStock" runat="server" ControlToValidate="txtStock" 
                    ErrorMessage="Ingrese el stock" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
            <a href="Insumos.aspx" class="btn btn-secondary">Cancelar</a>
        </div>
    </div>
</asp:Content>