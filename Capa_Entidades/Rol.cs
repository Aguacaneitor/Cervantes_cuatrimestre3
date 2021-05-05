using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Rol
    {
        public Int32 rol_id { get; set; }
        public string rol_descripcion { get; set; }
        public string usu_alta { get; set; }
        public DateTime usu_fecalta { get; set; }
        public string usu_modi { get; set; }
        public DateTime usu_fecmodi { get; set; }
        
        public Rol(){ }

        public Rol(Int32 rol_id, string rol_descripcion, string usu_alta, DateTime usu_fecalta, string usu_modi, DateTime usu_fecmodi)
        {
            this.rol_id = rol_id;
            this.rol_descripcion = rol_descripcion;
            this.usu_alta = usu_alta;
            this.usu_fecalta = usu_fecalta;
            this.usu_modi = usu_modi;
            this.usu_fecmodi = usu_fecmodi;
        }

    }
}
