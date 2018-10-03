using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class registro_inicial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    load_ddl();
                }
                else
                {
                }
            }
            catch
            {
            }
        }

        private void load_ddl()
        {
            ddl_colonia.Items.Clear();
            ddl_colonia.Items.Insert(0, new ListItem("*Colonia", "0"));
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            guarda_registro();
        }

        private void guarda_registro()
        {
            Guid guid_fempresa = Guid.NewGuid();
            Guid id_fempresa = Guid.Parse("9A3C8442-2B53-45B7-9B5C-144BFA9C93BE");

            string str_empresa = txt_tribunal.Text.ToUpper();
            string str_telefono = txt_telefono.Text;
            string str_email = txt_email.Text;
            string str_callenum = txt_callenum.Text.ToUpper();
            string str_cp = txt_cp.Text;
            int int_colony = Convert.ToInt32(ddl_colonia.SelectedValue);
            int int_idcodigocp;
            Guid guid_nusuario = Guid.NewGuid();

            string str_nombres = txt_name_user.Text.ToUpper();
            string str_apaterno = txt_apater.Text.ToUpper();
            string str_amaterno = txt_amater.Text.ToUpper();

            string str_usuairo = txt_code_user.Text.ToLower();
            string str_password = encrypta.Encrypt(txt_password.Text);

            using (bd_tsEntities db_sepomex = new bd_tsEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                   where c.d_codigo == str_cp
                                   where c.id_asenta_cpcons == int_colony
                                   select c).ToList();

                int_idcodigocp = tbl_sepomex[0].id_codigo;
            }

            using (var m_empresa = new bd_tsEntities())
            {
                var i_empresa = new inf_tribunal
                {
                    id_tribunal = guid_fempresa,

                    id_estatus = 1,
                    nombre = str_empresa,
                    telefono = str_telefono,
                    email = str_email,
                    callenum = str_callenum,
                    id_codigo = int_idcodigocp,
                    fecha_registro = DateTime.Now,
                    id_empresa = id_fempresa
                };

                m_empresa.inf_tribunal.Add(i_empresa);
                m_empresa.SaveChanges();
            }

            using (var m_usuario = new bd_tsEntities())
            {
                var i_usuario = new inf_usuarios
                {
                    id_usuario = guid_nusuario,
                    id_estatus = 1,
                    id_tipo_usuario = 1,
                    nombres = str_nombres,
                    a_paterno = str_apaterno,
                    a_materno = str_amaterno,
                    codigo_usuario = str_usuairo,
                    clave = str_password,
                    fecha_registro = DateTime.Now,
                    id_tribunal = guid_fempresa
                };
                m_usuario.inf_usuarios.Add(i_usuario);
                m_usuario.SaveChanges();
            }

            limpiar_textbox();

            lblModalTitle.Text = "transcript";
            lblModalBody.Text = "Datos de administrador y tribunal actualizados con éxito";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        protected void btn_cp_Click(object sender, EventArgs e)
        {
            string str_codigo = txt_cp.Text;

            datos_sepomex(str_codigo);
        }

        private void datos_sepomex(string str_codigo)
        {
            using (bd_tsEntities db_sepomex = new bd_tsEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                   where c.d_codigo == str_codigo
                                   select c).ToList();

                ddl_colonia.DataSource = tbl_sepomex;
                ddl_colonia.DataTextField = "d_asenta";
                ddl_colonia.DataValueField = "id_asenta_cpcons";
                ddl_colonia.DataBind();

                if (tbl_sepomex.Count == 1)
                {
                    txt_municipio.Text = tbl_sepomex[0].D_mnpio;
                    txt_estado.Text = tbl_sepomex[0].d_estado;
                    rfv_colonia.Enabled = true;
                    rfv_name_user.Enabled = true;
                    rfv_code_user.Enabled = true;
                    rfv_password.Enabled = true;
                }
                if (tbl_sepomex.Count > 1)
                {
                    ddl_colonia.Items.Insert(0, new ListItem("*Colonia", "0"));

                    txt_municipio.Text = tbl_sepomex[0].D_mnpio;
                    txt_estado.Text = tbl_sepomex[0].d_estado;
                    rfv_colonia.Enabled = true;
                    rfv_name_user.Enabled = true;
                    rfv_code_user.Enabled = true;
                    rfv_password.Enabled = true;
                }
                else if (tbl_sepomex.Count == 0)
                {
                    ddl_colonia.Items.Clear();
                    ddl_colonia.Items.Insert(0, new ListItem("*Colonia", "0"));
                    txt_municipio.Text = "";
                    txt_estado.Text = "";
                    rfv_colonia.Enabled = false;
                    rfv_name_user.Enabled = false;
                    rfv_code_user.Enabled = false;
                    rfv_password.Enabled = false;
                }
            }
        }

        private void limpiar_textbox()
        {
            txt_tribunal.Text = "";
            txt_telefono.Text = "";
            txt_email.Text = "";
            txt_callenum.Text = "";
            txt_cp.Text = "";
            ddl_colonia.Items.Clear();
            ddl_colonia.Items.Insert(0, new ListItem("*Colonia", "0"));
            ddl_colonia.SelectedValue = "0";
            txt_municipio.Text = "";
            txt_estado.Text = "";

            txt_name_user.Text = "";
            txt_apater.Text = "";
            txt_amater.Text = "";

            txt_code_user.Text = "";
            txt_password.Text = "";
        }
    }
}