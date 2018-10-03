using System;
using System.Linq;
using System.Web.UI;

namespace aw_transcript
{
    public partial class menu_tribunal : System.Web.UI.Page
    {
        private static Guid guguid_fidusuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    inf_user();
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
            guguid_fidusuario = (Guid)(Session["ss_id_user"]);
            //Session.Abandon();

            using (bd_tsEntities data_user = new bd_tsEntities())
            {
                var inf_user = (from i_u in data_user.inf_usuarios
                                join i_tu in data_user.fact_tipo_usuarios on i_u.id_tipo_usuario equals i_tu.id_tipo_usuario
                                join i_e in data_user.inf_tribunal on i_u.id_tribunal equals i_e.id_tribunal
                                where i_u.id_usuario == guguid_fidusuario
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

                lbl_fuser.Text = inf_user.nombres + " " + inf_user.a_paterno + " " + inf_user.a_materno;
                lbl_profileuser.Text = inf_user.desc_tipo_usuario;
                lbl_idprofileuser.Text = inf_user.id_tipo_usuario.ToString();
                lbl_centername.Text = inf_user.nombre;
                lbl_idcenter.Text = inf_user.id_tribunal.ToString();

                int str_id_type_user = inf_user.id_tipo_usuario;
                switch (str_id_type_user)
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:
                        div_tribunales.Visible = false;

                        break;

                    case 4:
                        div_tribunales.Visible = false;

                        break;
                }
            }
        }

        protected void img_tribunales_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("tribunal.aspx");
        }

        protected void img_juzgado_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("juzgados_salas.aspx");
        }
    }
}