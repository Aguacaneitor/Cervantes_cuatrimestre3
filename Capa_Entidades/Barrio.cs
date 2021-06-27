using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Barrio
    {
        public Int32 barrio_id { get; set; }
        public string barrio_nombre { get; set; }
        public Localidad barrio_localidad { get; set; }

        public Barrio() { }

        public Barrio(Int32 barrio_id, string barrio_nombre)
        {            
            this.barrio_id = barrio_id;
            this.barrio_nombre = barrio_nombre;
        }
    }
}
