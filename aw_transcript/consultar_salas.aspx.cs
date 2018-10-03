using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class consultar_salas : System.Web.UI.Page
    {
        private static Guid guid_fidusuario, guid_fidcentro;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    inf_user();
                    load_ddl();
                }
                else
                {
                }
            }
            catch
            {
                Response.Redirect("acceso.aspx");
            }
        }

        private void inf_user()
        {
            guid_fidusuario = (Guid)(Session["ss_id_user"]);

            using (bd_tsEntities edm_usuario = new bd_tsEntities())
            {
                var i_usuario = (from i_u in edm_usuario.inf_usuarios
                                 join i_tu in edm_usuario.fact_tipo_usuarios on i_u.id_tipo_usuario equals i_tu.id_tipo_usuario
                                 join i_e in edm_usuario.inf_tribunal on i_u.id_tribunal equals i_e.id_tribunal
                                 where i_u.id_usuario == guid_fidusuario
                                 select new
                                 {
                                     i_u.nombres,
                                     i_u.a_paterno,
                                     i_u.a_materno,
                                     i_tu.desc_tipo_usuario,
                                     i_tu.id_tipo_usuario,
                                     i_e.nombre,
                                     i_e.id_tribunal
                                 }).FirstOrDefault();

                lbl_fuser.Text = i_usuario.nombres + " " + i_usuario.a_paterno + " " + i_usuario.a_materno;
                lbl_profileuser.Text = i_usuario.desc_tipo_usuario;
                lbl_idprofileuser.Text = i_usuario.id_tipo_usuario.ToString();
                lbl_centername.Text = i_usuario.nombre;
                guid_fidcentro = i_usuario.id_tribunal;
            }

            using (bd_tsEntities edm_conexion = new bd_tsEntities())
            {
                var i_conexion = (from i_j in edm_conexion.v_salas

                                  select new
                                  {
                                      i_j.codigo_juzgado,
                                      i_j.desc_especializa,
                                      i_j.localidad,
                                      i_j.numero,
                                      i_j.codigo_sala,
                                      i_j.nombre,
                                      i_j.ip,
                                      i_j.usuario,
                                      i_j.desc_ruta_ini,
                                      i_j.ruta_user_ini
                                  }).ToList();

                if (i_conexion.Count == 0)
                {
                    lblModalTitle.Text = "transcript";
                    lblModalBody.Text = "Sin registros, favor de agregarlo en la pantalla de Control de Juzgados";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
        }

        protected void ddl_especializa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int int_idespecializa = int.Parse(ddl_especializa.SelectedValue);

            ddl_localidad.Items.Clear();
            using (bd_tsEntities m_especializa = new bd_tsEntities())
            {
                var i_especializa = (from c in m_especializa.inf_juzgados
                                     where c.id_especializa == int_idespecializa
                                     select c).Distinct().ToList();

                ddl_localidad.DataSource = i_especializa;
                ddl_localidad.DataTextField = "localidad";
                ddl_localidad.DataValueField = "id_juzgado";
                ddl_localidad.DataBind();
            }

            ddl_localidad.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void ddl_localidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str_localidad = ddl_localidad.SelectedValue;
        }

        private void load_ddl()
        {
            using (bd_tsEntities m_especializa = new bd_tsEntities())
            {
                var i_especializa = (from c in m_especializa.fact_especializa
                                     select c).ToList();

                ddl_especializa.DataSource = i_especializa;
                ddl_especializa.DataTextField = "desc_especializa";
                ddl_especializa.DataValueField = "id_especializa";
                ddl_especializa.DataBind();
            }
            ddl_especializa.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddl_localidad.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void ddl_nomnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            int int_idespecializa = int.Parse(ddl_especializa.SelectedValue);
            string str_localidad = ddl_localidad.SelectedValue;

            Guid guid_idjusgado;

            using (bd_tsEntities m_especializa = new bd_tsEntities())
            {
                var i_especializa = (from c in m_especializa.inf_juzgados
                                     join i_tu in m_especializa.inf_salas on c.id_juzgado equals i_tu.id_juzgado
                                     where c.id_especializa == int_idespecializa
                                     where c.localidad == str_localidad

                                     select c).FirstOrDefault();

                guid_idjusgado = i_especializa.id_juzgado;
            }
        }

        protected void btn_fconex_Click(object sender, EventArgs e)
        {
            int int_idespecializa = int.Parse(ddl_especializa.SelectedValue);
            Guid str_localidad = Guid.Parse(ddl_localidad.SelectedValue);

            using (bd_tsEntities edm_conexion = new bd_tsEntities())
            {
                var i_conexion = (from i_j in edm_conexion.v_salas
                                  where i_j.id_juzgado == str_localidad
                                  select new
                                  {
                                      i_j.codigo_juzgado,
                                      i_j.desc_especializa,
                                      i_j.localidad,
                                      i_j.numero,
                                      i_j.codigo_sala,
                                      i_j.nombre,
                                      i_j.ip,
                                      i_j.usuario,
                                      i_j.desc_ruta_ini,
                                      i_j.ruta_user_ini
                                  }).ToList();

                if (i_conexion.Count == 0)
                {
                    gv_credentials.Visible = false;
                }
                else
                {
                    gv_credentials.DataSource = i_conexion;
                    gv_credentials.DataBind();
                    gv_credentials.Visible = true;
                }
            }
        }
    }
}