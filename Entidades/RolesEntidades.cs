﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class RolesEntidades
    {
        public int RolID { get; set; }
        public string NombreRol { get; set; }

        public RolesEntidades()
        {

        }

        public RolesEntidades(int rolID, string nombreRol)
        {
            RolID = rolID;
            NombreRol = nombreRol;
        }
    }
}
