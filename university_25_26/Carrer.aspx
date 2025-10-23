<%@ Page Title="Carreras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrer.aspx.cs" Inherits="university_25_26.Carrer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Carreras</h2>
    <hr />

    <!-- Búsqueda -->
    <asp:Label Text="Buscar por Nombre:" runat="server" />
    <asp:TextBox ID="txtBuscarNombre" runat="server" />
    <asp:Label Text="Buscar por Código:" runat="server" />
    <asp:TextBox ID="txtBuscarCodigo" runat="server" />
    <asp:Button ID="btnBuscar" Text="Buscar" runat="server" OnClick="btnBuscar_Click" />
    <br /><br />

    <!-- Tabla -->
    <asp:GridView 
        ID="GridCarrera" 
        runat="server" 
        AutoGenerateColumns="False" 
        CssClass="table"
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
                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id_carre") %>'>Editar</asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id_carre") %>' OnClientClick="return confirm('¿Seguro que deseas eliminar esta carrera?');">Eliminar</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <hr />

    <!-- Formulario -->
    <h3>Editar / Agregar Carrera</h3>
    <asp:HiddenField ID="hfIdCarrera" runat="server" />

    <div style="max-width: 500px; padding: 20px; border: 1px solid #ccc; border-radius: 10px; background-color: #f9f9f9;">

        <div style="margin-bottom: 10px;">
            <asp:Label Text="Nombre:" runat="server" AssociatedControlID="txtNombre" />
            <asp:TextBox ID="txtNombre" runat="server" CssClass="inputTextBox" />
        </div>

        <div style="margin-bottom: 10px;">
            <asp:Label Text="Duración (años):" runat="server" AssociatedControlID="txtDuracion" />
            <asp:TextBox ID="txtDuracion" runat="server" CssClass="inputTextBox" />
        </div>

        <div style="margin-bottom: 10px;">
            <asp:Label Text="Director:" runat="server" AssociatedControlID="txtDirector" />
            <asp:TextBox ID="txtDirector" runat="server" CssClass="inputTextBox" />
        </div>

        <div style="margin-bottom: 10px;">
            <asp:Label Text="Código:" runat="server" AssociatedControlID="txtCodigo" />
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="inputTextBox" />
        </div>

        <div style="margin-bottom: 20px;">
            <asp:Label Text="Facultad:" runat="server" AssociatedControlID="ddlFacultad" />
            <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="inputTextBox" />
        </div>

        <div style="text-align: center; gap: 10px;">
            <asp:Button ID="btnGuardar" Text="Guardar" runat="server" OnClick="btnGuardar_Click" CssClass="btnAction" />
            <asp:Button ID="btnActualizar" Text="Actualizar" runat="server" OnClick="btnActualizar_Click" Visible="false" CssClass="btnAction" />
            <asp:Button ID="btnLimpiar" Text="Limpiar" runat="server" OnClick="btnLimpiar_Click" CssClass="btnDelete" />
        </div>
    </div>

    <style>
        .inputTextBox {
            width: 100%;
            padding: 6px 8px;
            border-radius: 5px;
            border: 1px solid #ccc;
            box-sizing: border-box;
        }

        .btnAction {
            padding: 8px 15px;
            background-color: #007bff;
            border: none;
            color: white;
            border-radius: 5px;
            cursor: pointer;
        }

        .btnAction:hover {
            background-color: #0056b3;
        }

        .btnDelete {
            padding: 8px 15px;
            background-color: #dc3545;
            border: none;
            color: white;
            border-radius: 5px;
            cursor: pointer;
        }

        .btnDelete:hover {
            background-color: #a71d2a;
        }
    </style>

    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
