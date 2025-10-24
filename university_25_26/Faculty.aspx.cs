using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;

namespace university_25_26
{
    public partial class Faculty : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionMySql"]?.ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Error: La cadena de conexión 'ConexionMySql' no está definida en Web.config.");
            }

            if (!IsPostBack)
            {
                CargarFacultades();
            }
        }

        private void MostrarAlerta(string tipo, string mensaje)
        {
            string script = $"mostrarMensaje('{tipo}', '{mensaje}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
        }

        private void CargarFacultades()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Faculty";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridFacultad.DataSource = dt;
                GridFacultad.DataBind();
            }
        }

        public bool ValidarFacultad()
        {
            string SoloLetrasYNumeros = @"^[A-Za-z0-9\s]+$";

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarAlerta("error", "El nombre de la facultad está vacío.");
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNombre.Text, SoloLetrasYNumeros))
            {
                MostrarAlerta("error", "El nombre solo debe contener letras y números.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAcronimo.Text))
            {
                MostrarAlerta("error", "Debes ingresar un acrónimo.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDecano.Text))
            {
                MostrarAlerta("error", "Debes ingresar el nombre del decano.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MostrarAlerta("error", "Debes ingresar el teléfono.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MostrarAlerta("error", "El correo electrónico no es válido.");
                return false;
            }

            if (!int.TryParse(txtAnio.Text, out int anio) || anio < 1800 || anio > DateTime.Now.Year)
            {
                MostrarAlerta("error", "El año de fundación no es válido.");
                return false;
            }

            return true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFacultad()) return;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query;

                if (string.IsNullOrEmpty(hfIdFac.Value))
                {
                    query = @"INSERT INTO Faculty 
                              (name_fac, acronym_fac, dean_name_fac, phone_fac, email_fac, logo_fac, year_foundation_fac) 
                              VALUES (@name, @acro, @dean, @phone, @mail, @logo, @year)";
                }
                else
                {
                    query = @"UPDATE Faculty SET 
                              name_fac=@name, acronym_fac=@acro, dean_name_fac=@dean, phone_fac=@phone, 
                              email_fac=@mail, logo_fac=@logo, year_foundation_fac=@year 
                              WHERE id_fac=@id";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", txtNombre.Text);
                cmd.Parameters.AddWithValue("@acro", txtAcronimo.Text);
                cmd.Parameters.AddWithValue("@dean", txtDecano.Text);
                cmd.Parameters.AddWithValue("@phone", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@logo", txtLogo.Text);
                cmd.Parameters.AddWithValue("@year", txtAnio.Text);

                if (!string.IsNullOrEmpty(hfIdFac.Value))
                {
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hfIdFac.Value));
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MostrarAlerta("success", "Facultad guardada correctamente.");

                LimpiarFormulario();
                CargarFacultades();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            hfIdFac.Value = "";
            txtNombre.Text = "";
            txtAcronimo.Text = "";
            txtDecano.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtLogo.Text = "";
            txtAnio.Text = "";

            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
            btnCancelar.Visible = false;
            lblFormularioTitulo.Text = "Agregar Facultad";
        }

        protected void GridFacultad_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int idFacultad = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                CargarDatosFacultad(idFacultad);
                hfIdFac.Value = idFacultad.ToString();
                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
                btnCancelar.Visible = true;
                lblFormularioTitulo.Text = "Editar Facultad";
            }
            else if (e.CommandName == "Eliminar")
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM Faculty WHERE id_fac=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", idFacultad);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MostrarAlerta("success", "Facultad eliminada correctamente.");
                    CargarFacultades();
                }
            }
        }

        private void CargarDatosFacultad(int idFacultad)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Faculty WHERE id_fac=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idFacultad);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtNombre.Text = reader["name_fac"].ToString();
                    txtAcronimo.Text = reader["acronym_fac"].ToString();
                    txtDecano.Text = reader["dean_name_fac"].ToString();
                    txtTelefono.Text = reader["phone_fac"].ToString();
                    txtEmail.Text = reader["email_fac"].ToString();
                    txtLogo.Text = reader["logo_fac"].ToString();
                    txtAnio.Text = reader["year_foundation_fac"].ToString();
                }

                conn.Close();
            }
        }
    }
}
