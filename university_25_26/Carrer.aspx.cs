using MySql.Data.MySqlClient;
using System;
using System.Data;
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
            }
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

            ddlFacultad.Items.Insert(0, new ListItem("-- Seleccione Facultad --", ""));
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
                lblMensaje.Text = "Por favor complete todos los campos.";
                return false;
            }

            if (!int.TryParse(txtDuracion.Text, out _))
            {
                lblMensaje.Text = "La duración debe ser numérica.";
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

            lblMensaje.Text = "Carrera guardada correctamente.";
            CargarDatos();
            LimpiarCampos();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            if (string.IsNullOrEmpty(hfIdCarrera.Value))
            {
                lblMensaje.Text = "Seleccione una carrera para actualizar.";
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

            lblMensaje.Text = rows > 0 ? "Carrera actualizada correctamente." : "No se pudo actualizar la carrera.";
            CargarDatos();
            LimpiarCampos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            lblMensaje.Text = "";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = txtBuscarNombre.Text.Trim();
            string codigo = txtBuscarCodigo.Text.Trim();

            string sql = @"SELECT c.id_carre, c.name_carre, c.duration_carre, c.director_carre, 
                                  c.code_carre, f.name_fac 
                           FROM Carrer c
                           INNER JOIN Faculty f ON c.id_fac = f.id_fac
                           WHERE 1=1";

            MySqlConnection con = conexion.AbrirConexion();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;

            if (!string.IsNullOrEmpty(nombre))
            {
                sql += " AND c.name_carre LIKE @name";
                cmd.Parameters.AddWithValue("@name", "%" + nombre + "%");
            }
            if (!string.IsNullOrEmpty(codigo))
            {
                sql += " AND c.code_carre LIKE @code";
                cmd.Parameters.AddWithValue("@code", "%" + codigo + "%");
            }

            cmd.CommandText = sql;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridCarrera.DataSource = dt;
            GridCarrera.DataBind();

            conexion.CerrarConexion();
            lblMensaje.Text = dt.Rows.Count > 0 ? "Resultados encontrados." : "No se encontraron resultados.";
        }

        protected void GridCarrera_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                CargarDatosPorId(id);
                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
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
                lblMensaje.Text = "Carrera cargada para editar.";
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

            lblMensaje.Text = rows > 0 ? "Carrera eliminada correctamente." : "No se encontró la carrera.";
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
