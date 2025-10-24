using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using university_25_26.Recursos;

namespace university_25_26
{
    public partial class Teacher : System.Web.UI.Page
    {
        ConexionMySql conexion = new ConexionMySql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarreras();
                CargarDocentes();
                btnCancelar.Visible = false;
            }
        }

        private void MostrarAlerta(string tipo, string mensaje)
        {
            string script = $"mostrarMensaje('{tipo}', '{mensaje}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
        }

        private void CargarCarreras()
        {
            MySqlConnection con = conexion.AbrirConexion();
            string sql = "SELECT id_carre, name_carre FROM Carrer";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlCarrera.DataSource = dt;
            ddlCarrera.DataTextField = "name_carre";
            ddlCarrera.DataValueField = "id_carre";
            ddlCarrera.DataBind();

            ddlCarrera.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione Carrera --", ""));
            conexion.CerrarConexion();
        }

        private void CargarDocentes()
        {
            MySqlConnection con = conexion.AbrirConexion();
            string sql = @"SELECT t.id_docente, t.names_teach, t.address_teach, t.email_teach, t.phone_teach, c.name_carre 
                           FROM Teacher t
                           INNER JOIN Carrer c ON t.id_carre = c.id_carre";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridTeacher.DataSource = dt;
            GridTeacher.DataBind();
            conexion.CerrarConexion();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(ddlCarrera.SelectedValue))
            {
                MostrarAlerta("error", "Por favor complete todos los campos.");
                return false;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                MostrarAlerta("error", "El correo electrónico no es válido.");
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            ddlCarrera.SelectedIndex = 0;
            hfIdDocente.Value = "";

            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
            btnCancelar.Visible = false;
            lblFormularioTitulo.Text = "Agregar Docente";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            MySqlConnection con = conexion.AbrirConexion();
            string sql = @"INSERT INTO Teacher (names_teach, address_teach, email_teach, phone_teach, id_carre)
                           VALUES (@name, @addr, @email, @phone, @carre)";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", txtNombre.Text);
            cmd.Parameters.AddWithValue("@addr", txtDireccion.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@phone", txtTelefono.Text);
            cmd.Parameters.AddWithValue("@carre", ddlCarrera.SelectedValue);

            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();

            MostrarAlerta("success", "DOCENTE GUARDADO CORRECTAMENTE.");
            CargarDocentes();
            LimpiarCampos();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            if (string.IsNullOrEmpty(hfIdDocente.Value))
            {
                MostrarAlerta("error", "SELECCIONE UN DOCENTE PARA ACTUALIZAR.");
                return;
            }

            MySqlConnection con = conexion.AbrirConexion();
            string sql = @"UPDATE Teacher 
                           SET names_teach=@name, address_teach=@addr, email_teach=@email, 
                               phone_teach=@phone, id_carre=@carre 
                           WHERE id_docente=@id";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", txtNombre.Text);
            cmd.Parameters.AddWithValue("@addr", txtDireccion.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@phone", txtTelefono.Text);
            cmd.Parameters.AddWithValue("@carre", ddlCarrera.SelectedValue);
            cmd.Parameters.AddWithValue("@id", hfIdDocente.Value);

            int rows = cmd.ExecuteNonQuery();
            conexion.CerrarConexion();

            if (rows > 0)
                MostrarAlerta("success", "DOCENTE ACTUALIZADO CORRECTAMENTE.");
            else
                MostrarAlerta("error", "No se pudo actualizar el docente.");

            CargarDocentes();
            LimpiarCampos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void GridTeacher_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                CargarDatosPorId(id);
                hfIdDocente.Value = id.ToString();
                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
                btnCancelar.Visible = true;
                lblFormularioTitulo.Text = "Editar Docente";
            }
            else if (e.CommandName == "Eliminar")
            {
                EliminarPorId(id);
            }
        }

        private void CargarDatosPorId(int id)
        {
            MySqlConnection con = conexion.AbrirConexion();
            string sql = "SELECT * FROM Teacher WHERE id_docente=@id";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtNombre.Text = dr["names_teach"].ToString();
                txtDireccion.Text = dr["address_teach"].ToString();
                txtEmail.Text = dr["email_teach"].ToString();
                txtTelefono.Text = dr["phone_teach"].ToString();
                ddlCarrera.SelectedValue = dr["id_carre"].ToString();

                hfIdDocente.Value = id.ToString();
                ////MostrarAlerta("info", "Docente cargado para editar.");
            }

            dr.Close();
            conexion.CerrarConexion();
        }

        private void EliminarPorId(int id)
        {
            MySqlConnection con = conexion.AbrirConexion();
            string sql = "DELETE FROM Teacher WHERE id_docente=@id";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            int rows = cmd.ExecuteNonQuery();
            conexion.CerrarConexion();

            if (rows > 0)
                MostrarAlerta("success", "Docente eliminado correctamente.");
            else
                MostrarAlerta("error", "No se encontró el docente.");

            CargarDocentes();
            LimpiarCampos();
        }

        protected void GridTeacher_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow && e.Row.RowIndex % 2 == 0)
                e.Row.BackColor = System.Drawing.Color.LightGray;
        }
    }
}
