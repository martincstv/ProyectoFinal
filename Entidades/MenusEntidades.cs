using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MenusEntidades
    {
        public string NombreMenu { get; set; }
        public string Icono { get; set; }
        public List<SubMenusEntidades> ListaSubMenu { get; set; }
    }
}
