<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="university_25_26._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <div class="container mt-5">
        <div class="text-center mb-4">
            <h1 class="display-5 fw-bold">Bienvenido al Sistema Universitario</h1>
            <p class="lead">Administra facultades, carreras y docentes de forma eficiente y moderna.</p>
        </div>

        <div class="row g-4 justify-content-center">
            <!-- Facultades -->
            <div class="col-md-4">
                <div class="card shadow-sm h-100 text-center">
                    <div class="card-body">
                        <i class="bi bi-building fs-1 text-primary"></i>
                        <h5 class="card-title mt-3">Facultades</h5>
                        <p class="card-text">Gestiona las facultades, sus datos y autoridades.</p>
                        <a href="Faculty.aspx" class="btn btn-primary w-100">Ir a Facultades</a>
                    </div>
                </div>
            </div>

            <!-- Carreras -->
            <div class="col-md-4">
                <div class="card shadow-sm h-100 text-center">
                    <div class="card-body">
                        <i class="bi bi-journal-text fs-1 text-success"></i>
                        <h5 class="card-title mt-3">Carreras</h5>
                        <p class="card-text">Administra las carreras académicas y su duración.</p>
                        <a href="Carrer.aspx" class="btn btn-success w-100">Ir a Carreras</a>
                    </div>
                </div>
            </div>

            <!-- Docentes -->
            <div class="col-md-4">
                <div class="card shadow-sm h-100 text-center">
                    <div class="card-body">
                        <i class="bi bi-person-badge fs-1 text-danger"></i>
                        <h5 class="card-title mt-3">Docentes</h5>
                        <p class="card-text">Registra y gestiona la información de los docentes.</p>
                        <a href="Teacher.aspx" class="btn btn-danger w-100">Ir a Docentes</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap Icons CDN -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" />

</asp:Content>
