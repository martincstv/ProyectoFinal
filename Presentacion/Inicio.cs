using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class Inicio : Form
    {
        UsuariosEntidades usuarioE = new UsuariosEntidades();
        RolesEntidades rolesE = new RolesEntidades();

        public  int usuarioID { get; set; }

        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<MenusEntidades> permisos_esperaddos = UsuariosNegocio.ObtenerPermisos(usuarioID);
            MenuStrip menu = new MenuStrip();
            foreach (MenusEntidades objMenu in permisos_esperaddos)
            {
                ToolStripMenuItem menuPadre = new ToolStripMenuItem(objMenu.NombreMenu);
                menuPadre.TextImageRelation = TextImageRelation.ImageAboveText;

                string rutaImagen = Path.GetFullPath(Path.Combine(Application.StartupPath, @"../../") + objMenu.Icono);

                menuPadre.Image = new Bitmap(rutaImagen);
                menuPadre.ImageScaling = ToolStripItemImageScaling.None;

                foreach (SubMenusEntidades objSubMenu in objMenu.ListaSubMenu)
                {
                    ToolStripMenuItem menuHijo = new ToolStripMenuItem(objSubMenu.NombreSubMenu,null,Click_en_Menu,objSubMenu.NombreFormulario);

                    menuPadre.DropDownItems.Add(menuHijo);
                }

                menu.Items.Add(menuPadre);
            }

            ToolStripMenuItem menuItemSalir = new ToolStripMenuItem();
            menuItemSalir.Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Bold);
            string rutaImagen1 = Path.GetFullPath(Path.Combine(Application.StartupPath, @"../../") + @"\Iconos\salida.png");
            menuItemSalir.Image = new Bitmap(rutaImagen1);
    
            menu.Items.Add(menuItemSalir);
            this.MainMenuStrip = menu;
            Controls.Add(menu);
           
            CargarValoresIniciales();
        }

        private void ToolStripMenuItemSalir_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                this.Close();
            }
        }

        private void Click_en_Menu(object sender, System.EventArgs e)
        {
            ToolStripMenuItem menuSeleccionado = (ToolStripMenuItem) sender;


            Assembly asm = Assembly.GetEntryAssembly();
            Type elemento = asm.GetType(asm.GetName().Name + "." + menuSeleccionado.Name);
            if (elemento == null)
            {
                MessageBox.Show("Formulario no encontrado");
            }
            else
            {
                Form formularioCreado = (Form)Activator.CreateInstance(elemento);

                int encontrado = this.MdiChildren.Where(x => x.Name == formularioCreado.Name).ToList().Count();

                if (encontrado > 0)
                {
                    ((Form)(this.MdiChildren.Where(x => x.Name == formularioCreado.Name).FirstOrDefault())).WindowState = FormWindowState.Normal;
                    ((Form)(this.MdiChildren.Where(x => x.Name == formularioCreado.Name).FirstOrDefault())).Activate();
                }
                else
                {

                    formularioCreado.MdiParent = this;
                    formularioCreado.Show();
                }


            }
        }

        private void CargarValoresIniciales()
        {
            CargarUsuario();
        }

        private void CargarUsuario()
        {
            usuarioE = UsuariosNegocio.DevolverUsuario(usuarioID);
            rolesE = RolesNegocio.DevolverRol(usuarioE.RolID);
            toolStripStatusLabel_Nombre.Text = $"Usuario: {usuarioE.Nombre}";
            toolStripStatusLabel_Rol.Text = $"Rol: {rolesE.NombreRol}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            this.Hide();
            l.Show();
        }
    }
}
