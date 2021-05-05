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
        public Int32 usu_tipodoc { get; set; }
        public string usu_nomdoc { get; set; }
        public string usu_calle { get; set; }
        public Int32 usu_altura { get; set; }
        public string usu_piso { get; set; }
        public string usu_dpto { get; set; }
        public Int32 barrio_id { get; set; }
        public Int32 loc_id { get; set; }
        public string usu_CP { get; set; }
        public Int32 usu_telefono { get; set; }
        public Int32 usu_celular { get; set; }
        public string usu_email { get; set; }
        public string usu_alta { get; set; }
        public DateTime usu_fecalta { get; set; }
        public string usu_modi { get; set; }
        public string usu_fecmodi { get; set; }
        public Rol o_rol { get; set; }

    }
}
