using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class perfil : System.Web.UI.Page
    {
        private static Guid guid_fidusuario, guid_fidcentro;

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
        }

        protected void cmd_save_Click(object sender, EventArgs e)
        {
            string str_nameuser = txt_name_user.Text.ToUpper();
            string str_apater = txt_apater.Text.ToUpper();
            string str_amater = txt_amater.Text.ToUpper();
            string str_codeuser = txt_code_user.Text.ToLower();
            string str_password = encrypta.Encrypt(txt_password.Text.ToLower());

            using (bd_tsEntities data_user = new bd_tsEntities())
            {
                var items_user = (from c in data_user.inf_usuarios
                                  where c.id_usuario == guid_fidusuario
                                  select c).ToList();

                if (items_user[0].codigo_usuario == str_codeuser)
                {
                    using (var data_userf = new bd_tsEntities())
                    {
                        var items_userf = (from c in data_userf.inf_usuarios
                                           where c.id_usuario == guid_fidusuario
                                           select c).FirstOrDefault();

                        items_userf.codigo_usuario = str_codeuser;
                        items_userf.nombres = str_nameuser;
                        items_userf.a_paterno = str_apater;
                        items_userf.a_materno = str_amater;
                        items_userf.clave = str_password;

                        data_userf.SaveChanges();
                    }

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

                    clean_data();

                    chkb_editar.Checked = false;

                    Mensaje("Datos de usuario actualizados con éxito.");
                }
                else
                {
                    using (bd_tsEntities data_userb = new bd_tsEntities())
                    {
                        var items_userb = (from c in data_userb.inf_usuarios
                                           where c.codigo_usuario == str_codeuser
                                           select c).ToList();

                        if (items_userb.Count == 0)
                        {
                            using (var data_userf = new bd_tsEntities())
                            {
                                var items_userf = (from c in data_userf.inf_usuarios
                                                   where c.id_usuario == guid_fidusuario
                                                   select c).FirstOrDefault();

                                items_userf.codigo_usuario = str_codeuser;
                                items_userf.nombres = str_nameuser;
                                items_userf.a_paterno = str_apater;
                                items_userf.a_materno = str_amater;
                                items_userf.clave = str_password;

                                data_userf.SaveChanges();
                            }

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

                            clean_data();

                            chkb_editar.Checked = false;

                            Mensaje("Datos de usuario actualizados con éxito.");
                        }
                        else
                        {
                            txt_code_user.Text = "";
                            Mensaje("Usuario ya existe en la base, agregar otro usuario.");
                        }
                    }
                }
            }
        }

        private void clean_data()
        {
            txt_name_user.Text = "";
            txt_apater.Text = "";
            txt_amater.Text = "";
            txt_code_user.Text = "";
            txt_password.Text = "";
        }

        protected void chkb_editar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_editar.Checked)
            {
                using (bd_tsEntities data_user = new bd_tsEntities())
                {
                    var inf_user = (from u in data_user.inf_usuarios
                                    join tu in data_user.fact_tipo_usuarios on u.id_tipo_usuario equals tu.id_tipo_usuario
                                    where u.id_usuario == guid_fidusuario

                                    select new
                                    {
                                        u.codigo_usuario,

                                        u.nombres,
                                        u.a_paterno,
                                        u.a_materno,
                                    }).FirstOrDefault();

                    txt_name_user.Text = inf_user.nombres;
                    txt_apater.Text = inf_user.a_paterno;
                    txt_amater.Text = inf_user.a_materno;
                    txt_code_user.Text = inf_user.codigo_usuario;
                }
            }
            else
            {
                clean_data();
            }
        }

        private void Mensaje(string contenido)
        {
            lblModalTitle.Text = "transcript";
            lblModalBody.Text = contenido;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }
    }
}