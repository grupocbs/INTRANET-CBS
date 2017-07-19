using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Configuration;

public partial class RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_2 : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
      //Session.Add("usuario", "1");
        if (Session["usuario"] == null)
        {
            Response.Redirect("INTRANET_LOGIN.aspx");
        }
        if (!IsPostBack)
        {
            //cmb_razon_social.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            //cmb_razon_social.Items.Add(new ListEditItem("BARCELO", "BARCELO"));
            //cmb_razon_social.Items.Add(new ListEditItem("CASA DE LA COSTA", "CASA DE LA COSTA"));
            //cmb_razon_social.Items.Add(new ListEditItem("CBS SRL", "CBS SRL"));
            //cmb_razon_social.SelectedIndex = 0;

            //txt_fecha_solicitud.Text = DateTime.Now.ToString("dd/MM/yyyy");


            string sql = "select ID,N=CAST(ID AS VARCHAR) + ' - ' + NOMBRE_PUESTO + ' - ' + CONVERT(VARCHAR(10),FECHA_SOLICITUD,103) + ' - ' + cast(u.USUARIO as varchar) COLLATE Modern_Spanish_CI_AS + ' - ' + CASE WHEN PENDIENTE=1 THEN 'PENDIENTE' ELSE 'FINALIZADA' END from RRHH_SOLICITUD_RECLUTAMIENTO r with (nolock)  left join USUARIOS u with(nolock) on r.usuario=u.IDUSUARIO order by id desc";
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            if (dt.Rows.Count > 0)
            {
                cmb_solicitudes.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                foreach (DataRow dr in dt.Rows)
                {
                    cmb_solicitudes.Items.Add(new ListEditItem(dr["N"].ToString(), dr["ID"].ToString()));
                }
                cmb_solicitudes.SelectedIndex = 0;
            }


           
        }
       
         
       
    }

    protected void cmb_solicitudes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_solicitudes.SelectedItem.Value.ToString() != "SELECCIONE")
        {
            string sql = "select FECHA_VAC=CONVERT(VARCHAR(10),FECHA_VACANTE,103),FECHA_SOL=CONVERT(VARCHAR(10),FECHA_SOLICITUD,103),ALIAS=ISNULL(ALIAS,''),* from RRHH_SOLICITUD_RECLUTAMIENTO S with (nolock) LEFT JOIN RRHH_REC_PUESTOS P WITH(NOLOCK) ON S.NOMBRE_PUESTO=P.DESCRIPCION where S.ID='" + cmb_solicitudes.SelectedItem.Value.ToString() + "' order by S.ID DESC";
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            if (dt.Rows.Count > 0)
            {
                string sql1 = "select * from CONFIGURACION with (nolock)  where NOMBRE='TEXTO AVISO RECLUTAMIENTO'";
                DataTable dt1 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql1);
                if (dt1.Rows.Count > 0)
                {
                    txt_descripcion.Html = dt1.Rows[0]["DESCRIPCION"].ToString();
                }


                if (dt.Rows[0]["ALIAS"].ToString().Length > 0)
                {
                    txt_descripcion.Html = txt_descripcion.Html.Replace("[Nombre del Puesto]", dt.Rows[0]["ALIAS"].ToString().ToUpper());
                }
                if (dt.Rows[0]["CONT_PUESTO_TRABAJO"].ToString().Length > 0)
                {
                    txt_descripcion.Html = txt_descripcion.Html.Replace("[Contenido del Puesto de Trabajo]", dt.Rows[0]["CONT_PUESTO_TRABAJO"].ToString().ToUpper());
                }
                if (dt.Rows[0]["DEDICACION_ESP"].ToString().Length > 0)
                {
                    txt_descripcion.Html = txt_descripcion.Html.Replace("[Dedicación]", dt.Rows[0]["DEDICACION_ESP"].ToString().ToUpper());
                }
                if (dt.Rows[0]["EDAD_MINIMA"].ToString().Length > 0)
                {
                    txt_descripcion.Html = txt_descripcion.Html.Replace("[Edad]", dt.Rows[0]["EDAD_MINIMA"].ToString() + "-" + dt.Rows[0]["EDAD_MAXIMA"].ToString().ToUpper()); 
                }
                if (dt.Rows[0]["FORMACION"].ToString().Length > 0)
                {
                    txt_descripcion.Html = txt_descripcion.Html.Replace("[Tipo]", dt.Rows[0]["FORMACION"].ToString().ToUpper()); 
                }
                if (dt.Rows[0]["ESTADO_FORMACION"].ToString().Length > 0)
                {
                    txt_descripcion.Html = txt_descripcion.Html.Replace("[Estado]", dt.Rows[0]["ESTADO_FORMACION"].ToString().ToUpper()); 
                }
                if (dt.Rows[0]["TITULO"].ToString().Length > 0)
                {
                    txt_descripcion.Html = txt_descripcion.Html.Replace("[Titulo]", dt.Rows[0]["TITULO"].ToString().ToUpper()); 
                }
                if (dt.Rows[0]["EXPERIENCIA"].ToString().Length > 0)
                {
                    txt_descripcion.Html = txt_descripcion.Html.Replace("[Experiencia]", dt.Rows[0]["EXPERIENCIA"].ToString().ToUpper()); 
                }
                if (dt.Rows[0]["TIEMPO"].ToString().Length > 0)
                {
                    txt_descripcion.Html = txt_descripcion.Html.Replace("[Tiempo]", dt.Rows[0]["TIEMPO"].ToString().ToUpper()); 
                }
                //txt_fecha_solicitud.Text = dt.Rows[0]["FECHA_SOL"].ToString();
                //cmb_razon_social.SelectedIndex = cmb_razon_social.Items.IndexOf(cmb_razon_social.Items.FindByValue(dt.Rows[0]["RAZON_SOCIAL"].ToString()));
                //txt_nombre_pesto.Text = dt.Rows[0]["NOMBRE_PUESTO"].ToString();
                //txt_area.Text = dt.Rows[0]["AREA"].ToString();
                //txt_fecha_vacante.Text = dt.Rows[0]["FECHA_VAC"].ToString();
                //rbtl_tipo.SelectedIndex = rbtl_tipo.Items.IndexOf(rbtl_tipo.Items.FindByValue(dt.Rows[0]["INFO_VACANTE"].ToString()));
                //rbtl_motivo.SelectedIndex = rbtl_motivo.Items.IndexOf(rbtl_motivo.Items.FindByValue(dt.Rows[0]["MOTIVO_VACANTE"].ToString()));
                //rbtl_ded_esp.SelectedIndex = rbtl_ded_esp.Items.IndexOf(rbtl_ded_esp.Items.FindByValue(dt.Rows[0]["DEDICACION_ESP"].ToString()));
                //txt_cant_hs.Text = dt.Rows[0]["CANT_HS"].ToString();
                //txt_horario_de.Text = dt.Rows[0]["HORA_DE"].ToString();
                //txt_horario_hasta.Text = dt.Rows[0]["HORA_HASTA"].ToString();
                //txt_horario_yde.Text = dt.Rows[0]["HORA_DE_Y"].ToString();
                //txt_horario_yhasta.Text = dt.Rows[0]["HORA_HASTA_Y"].ToString();
                //cmb_dia_de.SelectedIndex = cmb_dia_de.Items.IndexOf(cmb_dia_de.Items.FindByValue(dt.Rows[0]["DIA_DE"].ToString()));
                //cmb_dia_a.SelectedIndex = cmb_dia_a.Items.IndexOf(cmb_dia_a.Items.FindByValue(dt.Rows[0]["DIA_HASTA"].ToString()));
                //rbtl_turno_t.SelectedIndex = rbtl_turno_t.Items.IndexOf(rbtl_turno_t.Items.FindByValue(dt.Rows[0]["TURNO"].ToString()));
                //rbtl_deb_v.SelectedIndex = rbtl_deb_v.Items.IndexOf(rbtl_deb_v.Items.FindByValue(dt.Rows[0]["DEBE_VIAJAR"].ToString()));
                //txt_zonas.Text = dt.Rows[0]["ZONAS"].ToString();
                //rbtl_deb_c.SelectedIndex = rbtl_deb_c.Items.IndexOf(rbtl_deb_c.Items.FindByValue(dt.Rows[0]["DEBE_CONDUCIR"].ToString()));
                //rbtl_deb_cm.SelectedIndex = rbtl_deb_cm.Items.IndexOf(rbtl_deb_cm.Items.FindByValue(dt.Rows[0]["MOV_PROPIA"].ToString()));
                //txt_objetivo.Text = dt.Rows[0]["CONT_PUESTO_TRABAJO"].ToString();
                //txt_responsabilildades.Text = dt.Rows[0]["RESPONSABILIDADES"].ToString();
                //txt_funciones.Text = dt.Rows[0]["FUNCIONES"].ToString();
                //txt_relacion.Text = dt.Rows[0]["RELACION"].ToString();
                //txt_edad_minima.Text = dt.Rows[0]["EDAD_MINIMA"].ToString();
                //txt_edad_maxima.Text = dt.Rows[0]["EDAD_MAXIMA"].ToString();
                //rbtl_genero.SelectedIndex = rbtl_genero.Items.IndexOf(rbtl_genero.Items.FindByValue(dt.Rows[0]["GENERO"].ToString()));
                //rbtl_genero_estado_civil.SelectedIndex = rbtl_genero_estado_civil.Items.IndexOf(rbtl_genero_estado_civil.Items.FindByValue(dt.Rows[0]["ESTADO_CIVIL"].ToString()));
                //rbtl_formacion.SelectedIndex = rbtl_formacion.Items.IndexOf(rbtl_formacion.Items.FindByValue(dt.Rows[0]["FORMACION"].ToString()));
                //rbtl_formacion_estado.SelectedIndex = rbtl_formacion_estado.Items.IndexOf(rbtl_formacion_estado.Items.FindByValue(dt.Rows[0]["ESTADO_FORMACION"].ToString()));
                //txt_titulo.Text = dt.Rows[0]["TITULO"].ToString();
                //txt_experiencia.Text = dt.Rows[0]["EXPERIENCIA"].ToString();
                //rbtl_experiencia.SelectedIndex = rbtl_experiencia.Items.IndexOf(rbtl_experiencia.Items.FindByValue(dt.Rows[0]["TIEMPO"].ToString()));
            }
        }

        else
        {
            //  Response.Redirect("RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE.aspx");
        }
    }

 


    

   
 
}