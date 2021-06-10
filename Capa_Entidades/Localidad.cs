using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Localidad
    {
        public Int32 loc_id { get; set; }
        public string loc_nombre { get; set; }
        public Provincia loc_provincia { get; set; }

        public Localidad() { }

        public Localidad(Int32 loc_id, string loc_nombre, Provincia loc_provincia, DateTime usu_fecalta, string usu_modi, DateTime usu_fecmodi)
        {            
            this.loc_id = loc_id;
            this.loc_nombre = loc_nombre;
            this.loc_provincia = loc_provincia;
        }

    }
}
