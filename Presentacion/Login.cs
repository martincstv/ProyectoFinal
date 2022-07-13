using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class Login : Form
    {
        UsuariosEntidades usuarioE = new UsuariosEntidades();

        public Login()
        {
            InitializeComponent();
            textBox_Usuario.Focus();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        #region Activar/Desactivar mostrar contraseña
        private void label_Contraseña_MouseDown(object sender, MouseEventArgs e)
        {
            textBox_Contraseña.UseSystemPasswordChar = false;
        }

        private void label_Contraseña_MouseUp(object sender, MouseEventArgs e)
        {
            textBox_Contraseña.UseSystemPasswordChar = true;
        }
        #endregion

        private void Logueo()
        {
            var respuesta = UsuariosNegocio.Login(textBox_Usuario.Text, textBox_Contraseña.Text);
            if (respuesta != 0)
            {
                Inicio i = new Inicio();
                i.usuarioID = respuesta;
                this.Hide();
                i.Show();
            }
            else
                MessageBox.Show("Credenciales incorrectas","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void button_IniciarSesion_Click(object sender, EventArgs e)
        {
            Logueo();
        }

        private void button_Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
