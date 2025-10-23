using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using university_25_26.Recursos;

namespace university_25_26
{
    public partial class Carrer : System.Web.UI.Page
    {
        ConexionMySql conexion = new ConexionMySql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFacultades();
                CargarDatos();
                btnLimpiar.Visible = false;
            }
        }

        private void MostrarAlerta(string tipo, string mensaje)
        {
            string script = $"mostrarMensaje('{tipo}', '{mensaje}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
        }

        private void CargarFacultades()
        {
            MySqlConnection con = conexion.AbrirConexion();
            string sql = "SELECT id_fac, name_fac FROM Faculty";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlFacultad.DataSource = dt;
            ddlFacultad.DataTextField = "name_fac";
            ddlFacultad.DataValueField = "id_fac";
            ddlFacultad.DataBind();

            ddlFacultad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione Facultad --", ""));
            conexion.CerrarConexion();
        }

        private void CargarDatos()
        {
            MySqlConnection con = conexion.AbrirConexion();
            string sql = @"SELECT c.id_carre, c.name_carre, c.duration_carre, c.director_carre, 
                                  c.code_carre, f.name_fac 
                           FROM Carrer c
                           INNER JOIN Faculty f ON c.id_fac = f.id_fac";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridCarrera.DataSource = dt;
            GridCarrera.DataBind();
            conexion.CerrarConexion();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDuracion.Text) ||
                string.IsNullOrWhiteSpace(txtDirector.Text) ||
                string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(ddlFacultad.SelectedValue))
            {
                MostrarAlerta("error", "Por favor complete todos los campos.");
                return false;
            }

            if (!int.TryParse(txtDuracion.Text, out _))
            {
                MostrarAlerta("error", "La duración debe ser numérica.");
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDuracion.Text = "";
            txtDirector.Text = "";
            txtCodigo.Text = "";
            ddlFacultad.SelectedIndex = 0;
            hfIdCarrera.Value = "";

            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
            btnLimpiar.Visible = false;
            lblFormularioTitulo.Text = "Agregar Carrera";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            MySqlConnection con = conexion.AbrirConexion();
            string sql = @"INSERT INTO Carrer (name_carre, duration_carre, director_carre, code_carre, id_fac)
                           VALUES (@name, @dur, @dir, @code, @fac)";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", txtNombre.Text);
            cmd.Parameters.AddWithValue("@dur", txtDuracion.Text);
            cmd.Parameters.AddWithValue("@dir", txtDirector.Text);
            cmd.Parameters.AddWithValue("@code", txtCodigo.Text);
            cmd.Parameters.AddWithValue("@fac", ddlFacultad.SelectedValue);

            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();

            MostrarAlerta("success", "Carrera guardada correctamente.");
            CargarDatos();
            LimpiarCampos();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            if (string.IsNullOrEmpty(hfIdCarrera.Value))
            {
                MostrarAlerta("error", "Seleccione una carrera para actualizar.");
                return;
            }

            MySqlConnection con = conexion.AbrirConexion();
            string sql = @"UPDATE Carrer 
                           SET name_carre=@name, duration_carre=@dur, director_carre=@dir, 
                               code_carre=@code, id_fac=@fac 
                           WHERE id_carre=@id";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", txtNombre.Text);
            cmd.Parameters.AddWithValue("@dur", txtDuracion.Text);
            cmd.Parameters.AddWithValue("@dir", txtDirector.Text);
            cmd.Parameters.AddWithValue("@code", txtCodigo.Text);
            cmd.Parameters.AddWithValue("@fac", ddlFacultad.SelectedValue);
            cmd.Parameters.AddWithValue("@id", hfIdCarrera.Value);

            int rows = cmd.ExecuteNonQuery();
            conexion.CerrarConexion();

            if (rows > 0)
                MostrarAlerta("success", "Carrera actualizada correctamente.");
            else
                MostrarAlerta("error", "No se pudo actualizar la carrera.");

            CargarDatos();
            LimpiarCampos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void GridCarrera_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                CargarDatosPorId(id);
                hfIdCarrera.Value = id.ToString();
                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
                btnLimpiar.Visible = true;
                lblFormularioTitulo.Text = "Editar Carrera";
            }
            else if (e.CommandName == "Eliminar")
            {
                EliminarPorId(id);
            }
        }

        private void CargarDatosPorId(int id)
        {
            MySqlConnection con = conexion.AbrirConexion();
            string sql = "SELECT * FROM Carrer WHERE id_carre=@id";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtNombre.Text = dr["name_carre"].ToString();
                txtDuracion.Text = dr["duration_carre"].ToString();
                txtDirector.Text = dr["director_carre"].ToString();
                txtCodigo.Text = dr["code_carre"].ToString();
                ddlFacultad.SelectedValue = dr["id_fac"].ToString();

                hfIdCarrera.Value = id.ToString();
                ///MostrarAlerta("info", "Carrera cargada para editar.");
            }

            dr.Close();
            conexion.CerrarConexion();
        }

        private void EliminarPorId(int id)
        {
            MySqlConnection con = conexion.AbrirConexion();
            string sql = "DELETE FROM Carrer WHERE id_carre=@id";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            int rows = cmd.ExecuteNonQuery();
            conexion.CerrarConexion();

            if (rows > 0)
                MostrarAlerta("success", "Carrera eliminada correctamente.");
            else
                MostrarAlerta("error", "No se encontró la carrera.");

            CargarDatos();
            LimpiarCampos();
        }

        protected void GridCarrera_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex % 2 == 0)
                e.Row.BackColor = System.Drawing.Color.LightGray;
        }
    }
}
