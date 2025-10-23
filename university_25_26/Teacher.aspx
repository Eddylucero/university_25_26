<%@ Page Title="Docentes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Teacher.aspx.cs" Inherits="university_25_26.Teacher" %>

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

    <h2 class="text-center my-4">Gestión de Docentes</h2>

    <div class="container">
        <asp:HiddenField ID="hfIdDocente" runat="server" />

        <!-- Tabla -->
        <div class="card shadow-sm mb-5">
            <div class="card-header bg-secondary text-white text-center">
                Lista de Docentes
            </div>
            <div class="card-body">
                <asp:GridView 
                    ID="GridTeacher" 
                    runat="server" 
                    AutoGenerateColumns="False" 
                    CssClass="table table-bordered table-hover"
                    DataKeyNames="id_docente"
                    OnRowCommand="GridTeacher_RowCommand"
                    OnRowDataBound="GridTeacher_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="id_docente" HeaderText="ID" />
                        <asp:BoundField DataField="names_teach" HeaderText="Nombre" />
                        <asp:BoundField DataField="address_teach" HeaderText="Dirección" />
                        <asp:BoundField DataField="email_teach" HeaderText="Email" />
                        <asp:BoundField DataField="phone_teach" HeaderText="Teléfono" />
                        <asp:BoundField DataField="name_carre" HeaderText="Carrera" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="d-flex gap-2">
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id_docente") %>' CssClass="btn btn-sm btn-success">✏️</asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id_docente") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirmarEliminacion(this);">🗑️</asp:LinkButton>
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
                <asp:Label ID="lblFormularioTitulo" runat="server" Text="Agregar Docente"></asp:Label>
            </div>
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-md-6">
                        <asp:Label Text="Nombre:" runat="server" AssociatedControlID="txtNombre" CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label Text="Dirección:" runat="server" AssociatedControlID="txtDireccion" CssClass="form-label" />
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label Text="Email:" runat="server" AssociatedControlID="txtEmail" CssClass="form-label" />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label Text="Teléfono:" runat="server" AssociatedControlID="txtTelefono" CssClass="form-label" />
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label Text="Carrera:" runat="server" AssociatedControlID="ddlCarrera" CssClass="form-label" />
                        <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="form-select" />
                    </div>
                </div>

                <div class="row mt-4">
                    <div class="col-md-4">
                        <asp:Button ID="btnCancelar" Text="Cancelar" runat="server" OnClick="btnCancelar_Click" CssClass="btn btn-danger w-100" Visible="false" />
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
