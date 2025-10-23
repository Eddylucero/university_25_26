<%@ Page Title="Gestión de Facultades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Faculty.aspx.cs" Inherits="university_25_26.Faculty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function confirmarEliminacion(btn) {
            event.preventDefault();

            Swal.fire({
                title: "¿Estás seguro?",
                text: "Esta acción no se puede deshacer.",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Sí, eliminar",
                cancelButtonText: "Cancelar"
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack(btn.name, '');
                }
            });

            return false;
        }

        function mostrarMensaje(tipo, mensaje) {
            Swal.fire({
                icon: tipo,
                title: mensaje,
                confirmButtonText: "Aceptar"
            });
        }
    </script>

    <h2 class="text-center my-4">Gestión de Facultades</h2>

    <div class="container">
        <asp:HiddenField ID="hfIdFac" runat="server" />

        <!-- Tabla -->
        <div class="card shadow-sm mb-5">
            <div class="card-header bg-secondary text-white text-center">
                Lista de Facultades
            </div>
            <div class="card-body">
                <asp:GridView ID="GridFacultad" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="id_fac" OnRowCommand="GridFacultad_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="id_fac" HeaderText="ID" />
                        <asp:BoundField DataField="name_fac" HeaderText="Nombre" />
                        <asp:BoundField DataField="acronym_fac" HeaderText="Acrónimo" />
                        <asp:BoundField DataField="dean_name_fac" HeaderText="Decano" />
                        <asp:BoundField DataField="phone_fac" HeaderText="Teléfono" />
                        <asp:BoundField DataField="email_fac" HeaderText="Email" />
                        <asp:BoundField DataField="year_foundation_fac" HeaderText="Año Fundación" />
                        <asp:TemplateField HeaderText="Opciones">
                            <ItemTemplate>
                                <div class="d-flex gap-2">
                                    <asp:Button runat="server" Text="✏️" CssClass="btn btn-sm btn-primary" CommandName="Editar" CommandArgument='<%# Eval("id_fac") %>' />
                                    <asp:Button runat="server" Text="🗑️" CssClass="btn btn-sm btn-danger"
                                        CommandName="Eliminar" CommandArgument='<%# Eval("id_fac") %>' 
                                        OnClientClick="return confirmarEliminacion(this);" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Formulario -->
        <div class="card shadow-sm mb-5">
            <div class="card-header bg-primary text-white text-center">
                <asp:Label ID="lblFormularioTitulo" runat="server" Text="Agregar Facultad"></asp:Label>
            </div>
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">Nombre:</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Acrónimo:</label>
                        <asp:TextBox ID="txtAcronimo" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Decano:</label>
                        <asp:TextBox ID="txtDecano" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Teléfono:</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Logo (URL):</label>
                        <asp:TextBox ID="txtLogo" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Año de Fundación:</label>
                        <asp:TextBox ID="txtAnio" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                </div>

                <div class="row mt-4">
                    <div class="col-md-4">
                        <asp:Button ID="btnGuardar" runat="server" Text="Agregar Facultad" CssClass="btn btn-primary w-100" OnClick="btnGuardar_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Facultad" CssClass="btn btn-success w-100" OnClick="btnGuardar_Click" Visible="false" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger w-100" OnClick="btnCancelar_Click" Visible="false" />
                    </div>
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="d-none text-danger mt-3"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>
