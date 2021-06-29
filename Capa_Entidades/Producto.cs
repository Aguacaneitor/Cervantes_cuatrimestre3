using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Producto
    {
        public int prod_id { get; set; }
        public String prod_nombre { get; set; }

        public Producto() { }
        public Producto(int prod_id, String prod_nombre)
        {
            this.prod_id = prod_id;
            this.prod_nombre = prod_nombre;
        }
    }
}
