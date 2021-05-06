using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gestion_administrativa
{
    public partial class RegistroDeUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
            {
                cal_fec_nac.Visible = false;
            }*/
            if (!IsPostBack)
            {
                var dia = new List<int>();
                var mes = new List<int>();
                var agno = new List<int>();
                for (int i = 1; i <= 31; i++)
                {
                    dia.Add(i);
                }
                for (int i = 1; i <= 12; i++)
                {
                    mes.Add(i);
                }
                for (int i = 1900; i <= DateTime.Now.Year; i++)
                {
                    agno.Add(i);
                }

                drp_dia.DataSource = dia;
                drp_mes.DataSource = mes;
                drp_agno.DataSource = agno;
                drp_dia.DataBind();
                drp_mes.DataBind();
                drp_agno.DataBind();
            }

        }

        protected void img_calendario_Click(object sender, ImageClickEventArgs e)
        {
            /*if (cal_fec_nac.Visible)
            {
                cal_fec_nac.Visible = false;
            } else
            {
                cal_fec_nac.Visible = true;
            }*/
        }

        protected void cal_fec_nac_SelectionChanged(object sender, EventArgs e)
        {
            /*txt_cal_fec_nac.Text = cal_fec_nac.SelectedDate.ToShortDateString();
            cal_fec_nac.Visible = false;*/
        }

        protected void btn_fecha_Click(object sender, EventArgs e)
        {
           /* int dia = Convert.ToInt32(drp_dia.SelectedValue);
            int mes = Convert.ToInt32(drp_mes.SelectedValue);
            int agno = Convert.ToInt32(drp_agno.SelectedValue);
            string dia_f = (dia < 10) ? "0" + dia : "" + dia;
            string mes_f = (mes < 10) ? "0" + mes : "" + mes;
            try
            {
                string fecha = DateTime.Parse(agno + "-" + mes_f + "-" + dia_f).ToString("yyyy-MM-dd");
                txt_fecha_nacimiento.Text = fecha;
            } catch(Exception ex)
            {
                Response.Write("<script>alert('Fecha ingresada es invalida')</script>");
            } */        
        }
    }
}