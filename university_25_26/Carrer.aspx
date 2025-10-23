<%@ Page Title="Carreras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrer.aspx.cs" Inherits="university_25_26.Carrer" %>

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

    <h2 class="text-center my-4">Gestión de Carreras</h2>

    <div class="container">
        <asp:HiddenField ID="hfIdCarrera" runat="server" />

        <!-- Tabla -->
        <div class="card shadow-sm mb-5">
            <div class="card-header bg-secondary text-white text-center">
                Listado de Carreras
            </div>
            <div class="card-body">
                <asp:GridView 
                    ID="GridCarrera" 
                    runat="server" 
                    AutoGenerateColumns="False" 
                    CssClass="table table-bordered table-hover"
                    DataKeyNames="id_carre"
                    OnRowCommand="GridCarrera_RowCommand"
                    OnRowDataBound="GridCarrera_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="id_carre" HeaderText="ID" ReadOnly="true" />
                        <asp:BoundField DataField="name_carre" HeaderText="Nombre" />
                        <asp:BoundField DataField="duration_carre" HeaderText="Duración (años)" />
                        <asp:BoundField DataField="director_carre" HeaderText="Director" />
                        <asp:BoundField DataField="code_carre" HeaderText="Código" />
                        <asp:BoundField DataField="name_fac" HeaderText="Facultad" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="d-flex gap-2">
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id_carre") %>' CssClass="btn btn-sm btn-primary">✏️</asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id_carre") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirmarEliminacion(this);" >🗑️</asp:LinkButton>
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
                <asp:Label ID="lblFormularioTitulo" runat="server" Text="Agregar Carrera"></asp:Label>
            </div>
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-md-6">
                        <asp:Label Text="Nombre:" runat="server" AssociatedControlID="txtNombre" CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label Text="Duración (años):" runat="server" AssociatedControlID="txtDuracion" CssClass="form-label" />
                        <asp:TextBox ID="txtDuracion" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label Text="Director:" runat="server" AssociatedControlID="txtDirector" CssClass="form-label" />
                        <asp:TextBox ID="txtDirector" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label Text="Código:" runat="server" AssociatedControlID="txtCodigo" CssClass="form-label" />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label Text="Facultad:" runat="server" AssociatedControlID="ddlFacultad" CssClass="form-label" />
                        <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="form-select" />
                    </div>
                </div>

                <div class="row mt-4">
                    <div class="col-md-4">
                        <asp:Button ID="btnLimpiar" Text="Cancelar" runat="server" OnClick="btnLimpiar_Click" CssClass="btn btn-danger w-100" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnGuardar" Text="Guardar" runat="server" OnClick="btnGuardar_Click" CssClass="btn btn-primary w-100" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnActualizar" Text="Actualizar" runat="server" OnClick="btnActualizar_Click" Visible="false" CssClass="btn btn-success w-100" />
                    </div>
                    
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="d-none text-danger mt-3"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>
