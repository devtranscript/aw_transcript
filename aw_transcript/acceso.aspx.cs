using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class acceso : System.Web.UI.Page
    {
        private static Guid str_id_user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Inf_user();
            }
            else
            {
            }
        }

        private void Inf_user()
        {
            try
            {
                using (bd_tsEntities edm_usuario = new bd_tsEntities())
                {
                    var i_usuario = (from u in edm_usuario.inf_usuarios
                                     where u.id_tipo_usuario == 1
                                     select u).ToList();

                    if (i_usuario.Count == 0)
                    {
                        rfv_code_user.Enabled = false;
                        rfv_password.Enabled = false;
                        lkb_registro.Visible = true;
                        Mensaje("No existe administrador ni tribunal en la aplicación, favor de registrarlos.");
                    }
                    else
                    {
                        rfv_code_user.Enabled = true;
                        rfv_password.Enabled = true;
                        lkb_registro.Visible = false;
                    }
                }
            }
            catch
            {
                Mensaje("Sin conexión a base de datos, contactar al administrador.");
            }
        }

        protected void cmd_login_Click(object sender, EventArgs e)
        {
            string str_codeuser = txt_code_user.Text;
            string str_password = encrypta.Encrypt(txt_password.Text);
            string str_password_V;
            int? str_id_type_user, str_iduser_status;

            try
            {
                using (bd_tsEntities edm_usuario = new bd_tsEntities())
                {
                    var i_usuario = (from c in edm_usuario.inf_usuarios
                                     where c.codigo_usuario == str_codeuser
                                     select c).FirstOrDefault();

                    str_id_user = i_usuario.id_usuario;
                    str_password_V = i_usuario.clave;
                    str_id_type_user = i_usuario.id_tipo_usuario;
                    str_iduser_status = i_usuario.id_estatus;

                    if (str_password_V == str_password && str_iduser_status == 1)
                    {
                        Session["ss_id_user"] = str_id_user;
                        Response.Redirect("menu.aspx");
                    }
                    else
                    {
                        Mensaje("Contraseña incorrecta, favor de contactar al Administrador.");
                    }
                }
            }
            catch
            {
                Mensaje("Usuario incorrecto, favor de contactar al Administrador.");
            }
        }

        private void Mensaje(string contenido)
        {
            lblModalTitle.Text = "transcript";
            lblModalBody.Text = contenido;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        protected void lkb_registro_Click(object sender, EventArgs e)
        {
            Response.Redirect("registro_inicial.aspx");
        }
    }
}