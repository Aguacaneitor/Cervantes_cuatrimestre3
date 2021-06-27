using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Usuario
    {
        public Int32 usu_id { get; set; }
        public string usuario { get; set; }
        public string usu_Nom { get; set; }
        public string usu_Ape { get; set; }
        public string usu_pass { get; set; }
        public DateTime fec_nac { get; set; }
        public string usu_tipodoc { get; set; }
        public string usu_nomdoc { get; set; }
        public string usu_email { get; set; }
        public string usu_alta { get; set; }
        public DateTime usu_fecalta { get; set; }
        public string usu_modi { get; set; }
        public DateTime usu_fecmodi { get; set; }
        public Rol o_rol { get; set; }
        public bool estado_usuario { get; set; }

    }
}
