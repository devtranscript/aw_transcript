using System;
using System.Linq;
using System.Web.UI;

namespace aw_transcript
{
    public partial class menu_usuarios : System.Web.UI.Page
    {
        private static Guid guid_fidusuario;

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

        protected void img_perfil_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("perfil.aspx");
        }

        private void inf_user()
        {
            guid_fidusuario = (Guid)(Session["ss_id_user"]);
            using (bd_tsEntities data_user = new bd_tsEntities())
            {
                var inf_user = (from i_u in data_user.inf_usuarios
                                join i_tu in data_user.fact_tipo_usuarios on i_u.id_tipo_usuario equals i_tu.id_tipo_usuario
                                join i_e in data_user.inf_tribunal on i_u.id_tribunal equals i_e.id_tribunal
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

                lbl_name.Text = inf_user.nombres + " " + inf_user.a_paterno + " " + inf_user.a_materno;
                lbl_profile_user.Text = inf_user.desc_tipo_usuario;
                lbl_id_profile_user.Text = inf_user.id_tipo_usuario.ToString();
                lbl_user_centerCP.Text = inf_user.nombre;
                lbl_id_centerCP.Text = inf_user.id_tribunal.ToString();

                int str_id_type_user = inf_user.id_tipo_usuario;
                switch (str_id_type_user)
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:
                        div_administrador.Visible = false;

                        break;

                    case 4:
                        div_administrador.Visible = false;
                        div_superintendent.Visible = false;
                        div_operator.Visible = false;
                        break;
                }
            }
        }

        protected void img_administrador_Click(object sender, ImageClickEventArgs e)
        {
            Session["ss_save_user"] = 2;
            Response.Redirect("usuarios.aspx");
        }

        protected void img_superintendent_Click(object sender, ImageClickEventArgs e)
        {
            Session["ss_save_user"] = 3;
            Response.Redirect("usuarios.aspx");
        }

        protected void img_operator_Click(object sender, ImageClickEventArgs e)
        {
            Session["ss_save_user"] = 4;
            Response.Redirect("usuarios.aspx");
        }
    }
}