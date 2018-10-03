using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class tribunal : System.Web.UI.Page
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
            //Session.Abandon();

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

                lbl_fuser.Text = inf_user.nombres + " " + inf_user.a_paterno + " " + inf_user.a_materno;
                lbl_profileuser.Text = inf_user.desc_tipo_usuario;
                lbl_idprofileuser.Text = inf_user.id_tipo_usuario.ToString();
                lbl_centername.Text = inf_user.nombre;
                guid_fidcentro = inf_user.id_tribunal;

                int str_id_type_user = inf_user.id_tipo_usuario;
                switch (str_id_type_user)
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;
                }
            }
        }

        private void load_ddl()
        {
            ddl_colonia.Items.Clear();
            ddl_colonia.Items.Insert(0, new ListItem("Seleccionar", "0"));
            chkb_editar.Checked = true;

            if (chkb_editar.Checked)
            {
                using (bd_tsEntities m_tribunal = new bd_tsEntities())
                {
                    var i_tribunal = (from u in m_tribunal.inf_tribunal
                                      where u.id_tribunal == guid_fidcentro
                                      select u).FirstOrDefault();

                    txt_tribunal.Text = i_tribunal.nombre;
                    txt_telefono.Text = i_tribunal.telefono;
                    txt_email.Text = i_tribunal.email;
                    txt_callenum.Text = i_tribunal.callenum;

                    using (bd_tsEntities db_sepomex = new bd_tsEntities())
                    {
                        var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                           where c.id_codigo == i_tribunal.id_codigo
                                           select c).ToList();

                        ddl_colonia.DataSource = tbl_sepomex;
                        ddl_colonia.DataTextField = "d_asenta";
                        ddl_colonia.DataValueField = "id_asenta_cpcons";
                        ddl_colonia.DataBind();

                        txt_cp.Text = tbl_sepomex[0].d_codigo;
                        ddl_colonia.SelectedValue = tbl_sepomex[0].id_asenta_cpcons.ToString();
                        txt_municipio.Text = tbl_sepomex[0].D_mnpio;
                        txt_estado.Text = tbl_sepomex[0].d_estado;
                    }
                }
            }
            else
            {
                limpiar_textbox();
            }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            guarda_registro();
        }

        public int id_accion()
        {
            if (chkb_editar.Checked)
            {
                return 2;
            }
            //else if (rb_edit_routevideos.Checked)
            //{
            //    return 2;
            //}
            //else if (.Checked)
            //{
            //    return 3;
            //}
            else
            {
                return 4;
            }
        }

        private void guarda_registro()
        {
            string str_empresa = txt_tribunal.Text.ToUpper();
            string str_telefono = txt_telefono.Text;
            string str_email = txt_email.Text;
            string str_callenum = txt_callenum.Text.ToUpper();
            string str_cp = txt_cp.Text;
            int int_idcodigocp;
            int int_colony = Convert.ToInt32(ddl_colonia.SelectedValue);

            using (bd_tsEntities db_sepomex = new bd_tsEntities())
            {
                var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                   where c.d_codigo == str_cp
                                   where c.id_asenta_cpcons == int_colony
                                   select c).ToList();

                int_idcodigocp = tbl_sepomex[0].id_codigo;
            }

            using (var data_user = new bd_tsEntities())
            {
                var items_user = (from c in data_user.inf_tribunal
                                  where c.id_tribunal == guid_fidcentro
                                  select c).FirstOrDefault();

                items_user.nombre = str_empresa;
                items_user.telefono = str_telefono;
                items_user.email = str_email;
                items_user.callenum = str_callenum;

                items_user.id_codigo = int_idcodigocp;

                data_user.SaveChanges();
            }
            using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
            {
                var ii_fecha_transf = (from u in edm_fecha_transf.inf_tribunal
                                       select u).ToList();

                if (ii_fecha_transf.Count == 0)
                {
                }
                else
                {
                    using (var insert_userf = new bd_tsEntities())
                    {
                        var items_userf = new inf_tribunal_dep
                        {
                            id_usuario = guid_fidusuario,
                            id_tribunal = ii_fecha_transf[0].id_tribunal,
                            id_tipo_accion = id_accion(),
                            fecha_registro = DateTime.Now,
                        };
                        insert_userf.inf_tribunal_dep.Add(items_userf);
                        insert_userf.SaveChanges();
                    }
                }
            }
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

                lbl_fuser.Text = inf_user.nombres + " " + inf_user.a_paterno + " " + inf_user.a_materno;
                lbl_profileuser.Text = inf_user.desc_tipo_usuario;
                lbl_idprofileuser.Text = inf_user.id_tipo_usuario.ToString();
                lbl_centername.Text = inf_user.nombre;
                guid_fidcentro = inf_user.id_tribunal;
            }

            using (bd_tsEntities m_tribunal = new bd_tsEntities())
            {
                var i_tribunal = (from u in m_tribunal.inf_tribunal
                                  where u.id_tribunal == guid_fidcentro
                                  select u).FirstOrDefault();

                txt_tribunal.Text = i_tribunal.nombre;
                txt_telefono.Text = i_tribunal.telefono;
                txt_email.Text = i_tribunal.email;
                txt_callenum.Text = i_tribunal.callenum;

                using (bd_tsEntities db_sepomex = new bd_tsEntities())
                {
                    var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                       where c.id_codigo == i_tribunal.id_codigo
                                       select c).ToList();

                    ddl_colonia.DataSource = tbl_sepomex;
                    ddl_colonia.DataTextField = "d_asenta";
                    ddl_colonia.DataValueField = "id_asenta_cpcons";
                    ddl_colonia.DataBind();

                    txt_cp.Text = tbl_sepomex[0].d_codigo;
                    ddl_colonia.SelectedValue = tbl_sepomex[0].id_asenta_cpcons.ToString();
                    txt_municipio.Text = tbl_sepomex[0].D_mnpio;
                    txt_estado.Text = tbl_sepomex[0].d_estado;
                }
            }

            lblModalTitle.Text = "transcript";
            lblModalBody.Text = "Datos de tribunal actualizado con éxito";
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
                }
                if (tbl_sepomex.Count > 1)
                {
                    ddl_colonia.Items.Insert(0, new ListItem("Seleccionar", "0"));

                    txt_municipio.Text = tbl_sepomex[0].D_mnpio;
                    txt_estado.Text = tbl_sepomex[0].d_estado;
                    rfv_colonia.Enabled = true;
                }
                else if (tbl_sepomex.Count == 0)
                {
                    ddl_colonia.Items.Clear();
                    ddl_colonia.Items.Insert(0, new ListItem("Seleccionar", "0"));
                    txt_municipio.Text = "";
                    txt_estado.Text = "";
                    rfv_colonia.Enabled = false;
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
            ddl_colonia.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddl_colonia.SelectedValue = "0";
            txt_municipio.Text = "";
            txt_estado.Text = "";
        }

        protected void chkb_editar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_editar.Checked)
            {
                using (bd_tsEntities m_tribunal = new bd_tsEntities())
                {
                    var i_tribunal = (from u in m_tribunal.inf_tribunal
                                      where u.id_tribunal == guid_fidcentro
                                      select u).FirstOrDefault();

                    txt_tribunal.Text = i_tribunal.nombre;
                    txt_telefono.Text = i_tribunal.telefono;
                    txt_email.Text = i_tribunal.email;
                    txt_callenum.Text = i_tribunal.callenum;

                    using (bd_tsEntities db_sepomex = new bd_tsEntities())
                    {
                        var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                                           where c.id_codigo == i_tribunal.id_codigo
                                           select c).ToList();

                        ddl_colonia.DataSource = tbl_sepomex;
                        ddl_colonia.DataTextField = "d_asenta";
                        ddl_colonia.DataValueField = "id_asenta_cpcons";
                        ddl_colonia.DataBind();

                        txt_cp.Text = tbl_sepomex[0].d_codigo;
                        ddl_colonia.SelectedValue = tbl_sepomex[0].id_asenta_cpcons.ToString();
                        txt_municipio.Text = tbl_sepomex[0].D_mnpio;
                        txt_estado.Text = tbl_sepomex[0].d_estado;
                    }
                }
            }
            else
            {
                limpiar_textbox();
            }
        }
    }
}