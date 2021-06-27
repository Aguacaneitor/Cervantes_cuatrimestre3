using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Direccion
    {
        public Int32 dir_id { get; set; }
        public string dir_calle { get; set; }
        public Int32 dir_altura { get; set; }
        public string dir_piso { get; set; }
        public string dir_dpto { get; set; }
        public string dir_torre { get; set; }
        public string dir_manzana { get; set; }
        public Barrio dir_barrio { get; set; }
        public string usu_CP { get; set; }
        public Usuario dir_usuario { get; set; }
        public Direccion() { }

    }
}
