using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Provincia
    {
        public Int32 provinc_id { get; set; }
        public string provincia_nombre { get; set; }
        
        public Provincia() { }

        public Provincia(Int32 provinc_id, string provincia_nombre)
        {
            this.provinc_id = provinc_id;
            this.provincia_nombre = provincia_nombre;
        }
    }
}
