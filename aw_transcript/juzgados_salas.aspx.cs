using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class juzgados_salas : System.Web.UI.Page
    {
        private static Guid guid_fidusuario, guid_fidcentro, guid_ftribunal, guid_idjuzgado, guid_nsala, guid_njuzgado;

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

            using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
            {
                var i_fecha_transf = (from c in edm_fecha_transf.inf_juzgados
                                      select c).ToList();

                if (i_fecha_transf.Count == 0)
                {
                    lblModalTitle.Text = "transcript";
                    lblModalBody.Text = "No Existen Juzgados, favor de agregarlos";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    rb_editar_juzgado.Checked = true;
                    rb_agregar_juzgado.Checked = false;
                    rb_eliminar_juzgado.Checked = false;

                    txt_buscar_juzgado.Visible = false;
                    btn_buscar_juzgado.Visible = false;

                    chkbox_sala.Visible = false;

                    using (bd_tsEntities data_user = new bd_tsEntities())
                    {
                        var inf_user = (from i_u in data_user.inf_juzgados
                                        join i_e in data_user.fact_especializa on i_u.id_especializa equals i_e.id_especializa
                                        where i_u.id_tribunal == guid_fidcentro
                                        where i_u.id_estatus == 1
                                        select new
                                        {
                                            i_u.codigo_juzgado,
                                            i_e.desc_especializa,
                                            i_u.localidad,
                                            i_u.numero,
                                            i_u.fecha_registro,
                                        }).ToList();

                        gv_juzgado.DataSource = inf_user;
                        gv_juzgado.DataBind();
                        gv_juzgado.Visible = true;
                    }
                }
            }
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
            ddl_colonia.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public int id_accion_juzgado()
        {
            if (rb_agregar_juzgado.Checked)
            {
                return 1;
            }
            else if (rb_editar_juzgado.Checked)
            {
                return 2;
            }
            else if (rb_eliminar_juzgado.Checked)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        public int id_accion_salas()
        {
            if (rb_agregar_sala.Checked)
            {
                return 1;
            }
            else if (rb_editar_sala.Checked)
            {
                return 2;
            }
            else if (rb_eliminar_sala.Checked)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        public int valida_sala()
        {
            if (string.IsNullOrEmpty(txt_sala.Text) && string.IsNullOrEmpty(txt_ip.Text) && string.IsNullOrEmpty(txt_user_ip.Text) && string.IsNullOrEmpty(txt_pass_ip.Text) && string.IsNullOrEmpty(txt_path_videos.Text) && string.IsNullOrEmpty(txt_user_path.Text) && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 1;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) == false && string.IsNullOrEmpty(txt_user_ip.Text) && string.IsNullOrEmpty(txt_pass_ip.Text) && string.IsNullOrEmpty(txt_path_videos.Text) && string.IsNullOrEmpty(txt_user_path.Text) && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 2;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) == false && string.IsNullOrEmpty(txt_user_ip.Text) == false && string.IsNullOrEmpty(txt_pass_ip.Text) && string.IsNullOrEmpty(txt_path_videos.Text) && string.IsNullOrEmpty(txt_user_path.Text) && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 2;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) == false && string.IsNullOrEmpty(txt_user_ip.Text) == false && string.IsNullOrEmpty(txt_pass_ip.Text) == false && string.IsNullOrEmpty(txt_path_videos.Text) && string.IsNullOrEmpty(txt_user_path.Text) && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 2;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) && string.IsNullOrEmpty(txt_user_ip.Text) && string.IsNullOrEmpty(txt_pass_ip.Text) && string.IsNullOrEmpty(txt_path_videos.Text) == false && string.IsNullOrEmpty(txt_user_path.Text) && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 3;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) && string.IsNullOrEmpty(txt_user_ip.Text) && string.IsNullOrEmpty(txt_pass_ip.Text) && string.IsNullOrEmpty(txt_path_videos.Text) == false && string.IsNullOrEmpty(txt_user_path.Text) == false && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 3;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) && string.IsNullOrEmpty(txt_user_ip.Text) && string.IsNullOrEmpty(txt_pass_ip.Text) && string.IsNullOrEmpty(txt_path_videos.Text) == false && string.IsNullOrEmpty(txt_user_path.Text) == false && string.IsNullOrEmpty(txt_pass_path.Text) == false)
            {
                return 3;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) == false && string.IsNullOrEmpty(txt_user_ip.Text) && string.IsNullOrEmpty(txt_pass_ip.Text) && string.IsNullOrEmpty(txt_path_videos.Text) == false && string.IsNullOrEmpty(txt_user_path.Text) && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 4;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) == false && string.IsNullOrEmpty(txt_user_ip.Text) == false && string.IsNullOrEmpty(txt_pass_ip.Text) == false && string.IsNullOrEmpty(txt_path_videos.Text) == false && string.IsNullOrEmpty(txt_user_path.Text) == false && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 4;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) == false && string.IsNullOrEmpty(txt_user_ip.Text) == false && string.IsNullOrEmpty(txt_pass_ip.Text) && string.IsNullOrEmpty(txt_path_videos.Text) == false && string.IsNullOrEmpty(txt_user_path.Text) && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 4;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) == false && string.IsNullOrEmpty(txt_user_ip.Text) == false && string.IsNullOrEmpty(txt_pass_ip.Text) == false && string.IsNullOrEmpty(txt_path_videos.Text) == false && string.IsNullOrEmpty(txt_user_path.Text) == false && string.IsNullOrEmpty(txt_pass_path.Text) == false)
            {
                return 4;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) == false && string.IsNullOrEmpty(txt_user_ip.Text) == false && string.IsNullOrEmpty(txt_pass_ip.Text) == false && string.IsNullOrEmpty(txt_path_videos.Text) == false && string.IsNullOrEmpty(txt_user_path.Text) == false && string.IsNullOrEmpty(txt_pass_path.Text) == false)
            {
                return 4;
            }
            else if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) == false && string.IsNullOrEmpty(txt_user_ip.Text) == false && string.IsNullOrEmpty(txt_pass_ip.Text) == false && string.IsNullOrEmpty(txt_path_videos.Text) == false && string.IsNullOrEmpty(txt_user_path.Text) == false && string.IsNullOrEmpty(txt_pass_path.Text) == false)
            {
                return 4;
            }
            if (string.IsNullOrEmpty(txt_sala.Text) == false && string.IsNullOrEmpty(txt_ip.Text) && string.IsNullOrEmpty(txt_user_ip.Text) && string.IsNullOrEmpty(txt_pass_ip.Text) && string.IsNullOrEmpty(txt_path_videos.Text) && string.IsNullOrEmpty(txt_user_path.Text) && string.IsNullOrEmpty(txt_pass_path.Text))
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        protected void chkbox_sala_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbox_sala.Checked == true)
            {
                rb_agregar_sala.Enabled = true;
                rb_agregar_sala.Checked = true;

                rb_editar_sala.Enabled = false;
                rb_eliminar_sala.Enabled = false;
                txt_sala.Enabled = true;

                txt_ip.Enabled = true;

                txt_user_ip.Enabled = true;
                txt_pass_ip.Enabled = true;

                txt_path_videos.Enabled = true;

                txt_user_path.Enabled = true;
                txt_pass_path.Enabled = true;
                btn_guarda_sala.Enabled = true;

                rb_editar_sala.Checked = false;
                rb_eliminar_sala.Checked = false;
                txt_sala.Text = null;

                txt_ip.Text = null;

                txt_user_ip.Text = null;
                txt_pass_ip.Text = null;

                txt_path_videos.Text = null;

                txt_user_path.Text = null;
                txt_pass_path.Text = null;

                rfv_sala.Enabled = true;
            }
            else
            {
                rb_agregar_sala.Checked = false;
                rb_editar_sala.Checked = false;
                rb_eliminar_sala.Checked = false;
                txt_sala.Text = null;

                txt_ip.Text = null;

                txt_user_ip.Text = null;
                txt_pass_ip.Text = null;

                txt_path_videos.Text = null;

                txt_user_path.Text = null;
                txt_pass_path.Text = null;

                rb_agregar_sala.Enabled = false;
                rb_editar_sala.Enabled = false;
                rb_eliminar_sala.Enabled = false;
                txt_sala.Enabled = false;

                txt_ip.Enabled = false;

                txt_user_ip.Enabled = false;
                txt_pass_ip.Enabled = false;

                txt_path_videos.Enabled = false;

                txt_user_path.Enabled = false;
                txt_pass_path.Enabled = false;
                btn_guarda_sala.Enabled = false;

                rfv_sala.Enabled = false;
                rfv_ip.Enabled = false;
                rfv_user_ip.Enabled = false;
                rfv_pass_ip.Enabled = false;
            }
        }

        protected void btn_guarda_sala_Click(object sender, EventArgs e)
        {
            if (rb_agregar_sala.Checked == false & rb_editar_sala.Checked == false & rb_eliminar_sala.Checked == false)
            {
                lblModalTitle.Text = "transcript";
                lblModalBody.Text = "Favor de seleccionar una acción";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            else
            {
                if (rb_agregar_sala.Checked == true)
                {
                    if (valida_sala() == 5)
                    {
                        guarda_sala();
                    }
                    else if (valida_sala() == 2)
                    {
                        guarda_sala();
                    }
                    else if (valida_sala() == 3)
                    {
                        guarda_sala();
                    }
                    else if (valida_sala() == 4)
                    {
                        guarda_sala();
                    }
                    else if (valida_sala() == 1)
                    {
                        guarda_sala();
                    }
                }
                else if (rb_editar_sala.Checked == true)
                {
                    int add_j = 0;
                    foreach (GridViewRow row in gv_sala.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_sala") as CheckBox);
                            if (chkRow.Checked)
                            {
                                add_j = add_j + 1;
                            }
                        }
                    }
                    if (add_j != 0)
                    {
                        guarda_sala();
                    }
                }
                else if (rb_eliminar_sala.Checked == true)
                {
                    guarda_sala();
                }
            }
        }

        private void guarda_sala()
        {
            guid_nsala = Guid.NewGuid();
            string str_ip = txt_ip.Text;
            string str_nsala = txt_sala.Text.ToUpper();
            string str_user_ip = txt_user_ip.Text;
            string str_pass_ip = txt_pass_ip.Text;

            string str_user_path = txt_user_path.Text;
            string str_pass_path = txt_pass_path.Text;
            var networkPath = txt_path_videos.Text;
            int str_count = 0;
            if (rb_agregar_sala.Checked)
            {
                guid_nsala = Guid.NewGuid();
                foreach (GridViewRow row in gv_juzgado.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox fchkRow = (row.Cells[1].FindControl("chk_juzgado") as CheckBox);
                        if (fchkRow.Checked)
                        {
                            int int_codeuser = int.Parse(row.Cells[5].Text);
                            Guid codeuser;
                            using (var data_user = new bd_tsEntities())
                            {
                                var items_user = (from c in data_user.inf_juzgados
                                                  where c.codigo_juzgado == int_codeuser
                                                  select c).FirstOrDefault();

                                codeuser = items_user.id_juzgado;
                            }

                            using (bd_tsEntities emd_salas = new bd_tsEntities())
                            {
                                var i_salas = (from u in emd_salas.inf_salas
                                               where u.id_juzgado == codeuser
                                               select u).ToList();
                                if (i_salas.Count == 0)
                                {
                                    using (var edm_conexion = new bd_tsEntities())
                                    {
                                        var ii_conexion = new inf_salas
                                        {
                                            id_sala = guid_nsala,
                                            nombre = str_nsala,
                                            id_juzgado = codeuser,
                                            id_estatus = 1,
                                            fecha_registro = DateTime.Now
                                        };
                                        edm_conexion.inf_salas.Add(ii_conexion);
                                        edm_conexion.SaveChanges();
                                    }
                                }
                                else
                                {
                                    Guid guid_nsalaf = i_salas[0].id_sala;

                                    using (var edm_salasf = new bd_tsEntities())
                                    {
                                        var i_salasf = (from c in edm_salasf.inf_salas
                                                        where c.id_sala == guid_nsalaf
                                                        select c).ToList();

                                        if (i_salasf.Count == 0)
                                        {
                                            using (var edm_conexion = new bd_tsEntities())
                                            {
                                                var ii_conexion = new inf_salas
                                                {
                                                    id_sala = guid_nsala,
                                                    nombre = str_nsala,
                                                    id_juzgado = codeuser,
                                                    id_estatus = 1,
                                                    fecha_registro = DateTime.Now
                                                };
                                                edm_conexion.inf_salas.Add(ii_conexion);
                                                edm_conexion.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            using (var edm_conexion = new bd_tsEntities())
                                            {
                                                var ii_conexion = new inf_salas
                                                {
                                                    id_sala = guid_nsala,
                                                    nombre = str_nsala,
                                                    id_juzgado = codeuser,
                                                    id_estatus = 1,
                                                    fecha_registro = DateTime.Now
                                                };
                                                edm_conexion.inf_salas.Add(ii_conexion);
                                                edm_conexion.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }

                            using (bd_tsEntities emd_d = new bd_tsEntities())
                            {
                                var i_d = (from u in emd_d.inf_dispositivos
                                           where u.id_sala == guid_nsala
                                           select u).ToList();
                                if (i_d.Count == 0)
                                {
                                    using (var edm_conexion = new bd_tsEntities())
                                    {
                                        var ii_conexion = new inf_dispositivos
                                        {
                                            ip = str_ip,
                                            usuario = str_user_ip,
                                            clave = str_pass_ip,
                                            id_sala = guid_nsala,
                                            fecha_registro = DateTime.Now
                                        };
                                        edm_conexion.inf_dispositivos.Add(ii_conexion);
                                        edm_conexion.SaveChanges();
                                    }
                                }
                                else
                                {
                                    using (var emd_df = new bd_tsEntities())
                                    {
                                        var i_df = (from c in emd_df.inf_dispositivos
                                                    where c.id_sala == guid_nsala
                                                    select c).FirstOrDefault();

                                        i_df.ip = str_ip;
                                        i_df.usuario = str_user_ip;
                                        i_df.clave = str_pass_ip;
                                        emd_df.SaveChanges();
                                    }
                                }
                            }
                            using (bd_tsEntities emd_d = new bd_tsEntities())
                            {
                                var i_d = (from u in emd_d.inf_ruta_videos
                                           where u.id_sala == guid_nsala
                                           select u).ToList();
                                if (i_d.Count == 0)
                                {
                                    using (var insert_fiscal = new bd_tsEntities())
                                    {
                                        var items_fiscal = new inf_ruta_videos
                                        {
                                            desc_ruta_fin = @"C:\inetpub\wwwroot\ts_operacion",
                                            ruta_user_ini = str_user_path,
                                            ruta_pass_ini = str_pass_path,
                                            desc_ruta_ini = networkPath,
                                            id_usuario = guid_fidusuario,
                                            id_sala = guid_nsala,

                                            fecha_registro = DateTime.Now
                                        };
                                        insert_fiscal.inf_ruta_videos.Add(items_fiscal);
                                        insert_fiscal.SaveChanges();
                                    }
                                }
                                else
                                {
                                    using (var emd_df = new bd_tsEntities())
                                    {
                                        var i_df = (from c in emd_df.inf_ruta_videos
                                                    where c.id_sala == guid_nsala
                                                    select c).FirstOrDefault();

                                        i_df.desc_ruta_ini = networkPath;
                                        i_df.ruta_user_ini = str_user_path;
                                        i_df.ruta_pass_ini = str_pass_path;
                                        emd_df.SaveChanges();
                                    }
                                }
                            }

                            rb_agregar_sala.Checked = false;
                            rb_editar_sala.Checked = true;
                            rb_eliminar_sala.Checked = false;

                            txt_sala.Text = null;

                            txt_ip.Text = null;
                            txt_user_ip.Text = null;
                            txt_pass_ip.Text = null;

                            txt_path_videos.Text = null;
                            txt_user_path.Text = null;
                            txt_pass_path.Text = null;

                            using (bd_tsEntities edm_s = new bd_tsEntities())
                            {
                                var i_s = (from i_u in edm_s.inf_salas
                                           where i_u.id_juzgado == codeuser
                                           where i_u.id_estatus == 1
                                           select new
                                           {
                                               i_u.codigo_sala,
                                               i_u.nombre,

                                               i_u.fecha_registro,
                                           }).ToList();

                                gv_sala.DataSource = i_s;
                                gv_sala.DataBind();
                                gv_sala.Visible = true;
                            }
                            lblModalTitle.Text = "transcript";
                            lblModalBody.Text = "Datos de Sala agregados con éxito";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                    }
                }
            }
            else if (rb_editar_sala.Checked)
            {
                foreach (GridViewRow row in gv_sala.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox fchkRow = (row.Cells[1].FindControl("chk_sala") as CheckBox);
                        if (fchkRow.Checked)
                        {
                            int int_codeuser = int.Parse(row.Cells[3].Text);
                            Guid codeuser;
                            using (var data_user = new bd_tsEntities())
                            {
                                var items_user = (from c in data_user.inf_salas
                                                  where c.codigo_sala == int_codeuser
                                                  select c).FirstOrDefault();

                                codeuser = items_user.id_sala;
                            }

                            using (bd_tsEntities emd_salas = new bd_tsEntities())
                            {
                                var i_salas = (from u in emd_salas.inf_salas
                                               where u.id_sala == codeuser
                                               select u).ToList();
                                if (i_salas.Count == 0)
                                {
                                }
                                else
                                {
                                    guid_nsala = i_salas[0].id_sala;
                                    using (var edm_salasf = new bd_tsEntities())
                                    {
                                        var i_salasf = (from c in edm_salasf.inf_salas
                                                        where c.id_sala == codeuser
                                                        select c).FirstOrDefault();

                                        i_salasf.nombre = str_nsala;
                                        edm_salasf.SaveChanges();
                                    }
                                }
                            }

                            using (bd_tsEntities emd_d = new bd_tsEntities())
                            {
                                var i_d = (from u in emd_d.inf_dispositivos
                                           where u.id_sala == guid_nsala
                                           select u).ToList();
                                if (i_d.Count == 0)
                                {
                                    using (var edm_conexion = new bd_tsEntities())
                                    {
                                        var ii_conexion = new inf_dispositivos
                                        {
                                            ip = str_ip,
                                            usuario = str_user_ip,
                                            clave = str_pass_ip,
                                            id_sala = codeuser,
                                            fecha_registro = DateTime.Now
                                        };
                                        edm_conexion.inf_dispositivos.Add(ii_conexion);
                                        edm_conexion.SaveChanges();
                                    }
                                }
                                else
                                {
                                    using (var emd_df = new bd_tsEntities())
                                    {
                                        var i_df = (from c in emd_df.inf_dispositivos
                                                    where c.id_sala == guid_nsala
                                                    select c).FirstOrDefault();

                                        i_df.ip = str_ip;
                                        i_df.usuario = str_user_ip;
                                        i_df.clave = str_pass_ip;
                                        emd_df.SaveChanges();
                                    }
                                }
                            }
                            using (bd_tsEntities emd_d = new bd_tsEntities())
                            {
                                var i_d = (from u in emd_d.inf_ruta_videos
                                           where u.id_sala == guid_nsala
                                           select u).ToList();
                                if (i_d.Count == 0)
                                {
                                    using (var insert_fiscal = new bd_tsEntities())
                                    {
                                        var items_fiscal = new inf_ruta_videos
                                        {
                                            desc_ruta_fin = @"C:\inetpub\wwwroot\ts_operacion",
                                            ruta_user_ini = str_user_path,
                                            ruta_pass_ini = str_pass_path,
                                            desc_ruta_ini = networkPath,
                                            id_usuario = guid_fidusuario,
                                            id_sala = codeuser,

                                            fecha_registro = DateTime.Now
                                        };
                                        insert_fiscal.inf_ruta_videos.Add(items_fiscal);
                                        insert_fiscal.SaveChanges();
                                    }
                                }
                                else
                                {
                                    using (var emd_df = new bd_tsEntities())
                                    {
                                        var i_df = (from c in emd_df.inf_ruta_videos
                                                    where c.id_sala == guid_nsala
                                                    select c).FirstOrDefault();

                                        i_df.desc_ruta_ini = networkPath;
                                        i_df.ruta_user_ini = str_user_path;
                                        i_df.ruta_pass_ini = str_pass_path;
                                        emd_df.SaveChanges();
                                    }
                                }
                            }

                            rb_agregar_sala.Enabled = false;
                            rb_editar_sala.Enabled = true;
                            rb_eliminar_sala.Enabled = false;

                            txt_sala.Text = null;

                            txt_ip.Text = null;
                            txt_user_ip.Text = null;
                            txt_pass_ip.Text = null;

                            txt_path_videos.Text = null;
                            txt_user_path.Text = null;
                            txt_pass_path.Text = null;

                            using (bd_tsEntities edm_s = new bd_tsEntities())
                            {
                                var i_s = (from i_u in edm_s.inf_salas
                                           where i_u.id_juzgado == guid_idjuzgado
                                           where i_u.id_estatus == 1
                                           select new
                                           {
                                               i_u.codigo_sala,
                                               i_u.nombre,

                                               i_u.fecha_registro,
                                           }).ToList();

                                gv_sala.DataSource = i_s;
                                gv_sala.DataBind();
                                gv_sala.Visible = true;
                            }
                            lblModalTitle.Text = "transcript";
                            lblModalBody.Text = "Datos de Sala actualizados con éxito";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                    }
                }
            }
            else if (rb_eliminar_sala.Checked)
            {
                foreach (GridViewRow row in gv_sala.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox fchkRow = (row.Cells[0].FindControl("chk_sala") as CheckBox);
                        if (fchkRow.Checked)
                        {
                            row.BackColor = Color.YellowGreen;
                            int int_codeuser = int.Parse(row.Cells[3].Text);
                            Guid codeuser;

                            using (var data_user = new bd_tsEntities())
                            {
                                var items_user = (from c in data_user.inf_salas
                                                  where c.codigo_sala == int_codeuser
                                                  select c).FirstOrDefault();

                                codeuser = items_user.id_sala;
                            }

                            var elimna_sala = new inf_salas { id_sala = codeuser };

                            using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                            {
                                var ii_fecha_transf = (from u in edm_fecha_transf.inf_salas
                                                       where u.id_sala == codeuser
                                                       select u).ToList();

                                if (ii_fecha_transf.Count == 0)
                                {
                                }
                                else
                                {
                                    using (var insert_userf = new bd_tsEntities())
                                    {
                                        var items_userf = new inf_salas_dep
                                        {
                                            id_usuario = guid_fidusuario,
                                            id_sala = ii_fecha_transf[0].id_sala,
                                            id_tipo_accion = id_accion_salas(),
                                            fecha_registro = DateTime.Now,
                                        };
                                        insert_userf.inf_salas_dep.Add(items_userf);
                                        insert_userf.SaveChanges();
                                    }
                                }
                            }
                            using (var data_user = new bd_tsEntities())
                            {
                                var items_user = (from c in data_user.inf_salas
                                                  where c.id_sala == codeuser
                                                  select c).FirstOrDefault();

                                data_user.inf_salas.Remove(items_user);
                                data_user.SaveChanges();
                            }
                            try
                            {
                                using (var data_user = new bd_tsEntities())
                                {
                                    var items_user = (from c in data_user.inf_dispositivos
                                                      where c.id_sala == codeuser
                                                      select c).FirstOrDefault();

                                    data_user.inf_dispositivos.Remove(items_user);
                                    data_user.SaveChanges();
                                }
                            }
                            catch
                            {
                            }
                            try
                            {
                                using (var data_user = new bd_tsEntities())
                                {
                                    var items_user = (from c in data_user.inf_ruta_videos
                                                      where c.id_sala == codeuser
                                                      select c).FirstOrDefault();

                                    data_user.inf_ruta_videos.Remove(items_user);
                                    data_user.SaveChanges();
                                }
                            }
                            catch
                            {
                            }
                            using (bd_tsEntities data_user = new bd_tsEntities())
                            {
                                var inf_user = (from i_u in data_user.inf_salas
                                                where i_u.id_juzgado == guid_idjuzgado
                                                where i_u.id_estatus == 1
                                                select new
                                                {
                                                    i_u.codigo_sala,
                                                    i_u.nombre,

                                                    i_u.fecha_registro,
                                                }).ToList();

                                gv_sala.DataSource = inf_user;
                                gv_sala.DataBind();
                                gv_sala.Visible = true;
                            }

                            rb_eliminar_sala.Checked = false;

                            rb_agregar_sala.Checked = false;
                            rb_editar_sala.Checked = false;
                            rb_eliminar_sala.Checked = false;
                            txt_sala.Text = null;

                            txt_ip.Text = null;

                            txt_user_ip.Text = null;
                            txt_pass_ip.Text = null;

                            txt_path_videos.Text = null;

                            txt_user_path.Text = null;
                            txt_pass_path.Text = null;

                            lblModalTitle.Text = "transcript";
                            lblModalBody.Text = "Datos de sala eliminado con éxito";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                        else
                        {
                            row.BackColor = Color.White;
                        }
                    }
                }
            }
            else
            {
                if (valida_sala() == 5)
                {
                }
                else if (valida_sala() == 2)
                {
                }
                else if (valida_sala() == 3)
                {
                }
                else if (valida_sala() == 4)
                {
                }
            }
        }

        protected void btn_guardar_juzgado_Click(object sender, EventArgs e)
        {
            if (rb_agregar_juzgado.Checked == false & rb_editar_juzgado.Checked == false & rb_eliminar_juzgado.Checked == false)
            {
                lblModalTitle.Text = "transcript";
                lblModalBody.Text = "Favor de seleccionar una acción";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            else
            {
                if (rb_agregar_juzgado.Checked == true)
                {
                    if (chkbox_sala.Checked)
                    {
                        if (valida_sala() == 5)
                        {
                            guardar_juzgado();
                        }
                        else if (valida_sala() == 2)
                        {
                            guardar_juzgado();
                        }
                        else if (valida_sala() == 3)
                        {
                            guardar_juzgado();
                        }
                        else if (valida_sala() == 4)
                        {
                            guardar_juzgado();
                        }
                        else if (valida_sala() == 1)
                        {
                            guardar_juzgado();
                        }
                    }
                    else
                    {
                        guardar_juzgado();
                    }
                }
                else if (rb_editar_juzgado.Checked == true)
                {
                    int add_j = 0;
                    foreach (GridViewRow row in gv_juzgado.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_juzgado") as CheckBox);
                            if (chkRow.Checked)
                            {
                                add_j = add_j + 1;
                            }
                        }
                    }
                    if (add_j == 1)
                    {
                        guardar_juzgado();
                    }
                }
                else if (rb_eliminar_juzgado.Checked == true)
                {
                    guardar_juzgado();
                }
            }
        }

        private void guardar_juzgado()
        {
            guid_njuzgado = Guid.NewGuid();
            guid_nsala = Guid.NewGuid();
            int int_especializa = Convert.ToInt32(ddl_especializa.SelectedValue);
            string str_localidad = txt_localidad.Text.ToUpper();
            string str_numero = txt_numero.Text.ToUpper();
            string str_callenum = txt_callenum.Text.ToUpper();
            string str_cp = txt_cp.Text;
            int int_colony = Convert.ToInt32(ddl_colonia.SelectedValue);
            int int_idcodigocp;
            guid_nsala = Guid.NewGuid();
            string str_ip = txt_ip.Text;
            string str_nsala = txt_sala.Text.ToUpper();
            string str_user_ip = txt_user_ip.Text;
            string str_pass_ip = txt_pass_ip.Text;

            string str_user_path = txt_user_path.Text;
            string str_pass_path = txt_pass_path.Text;
            var networkPath = txt_path_videos.Text;
            int str_count = 0;

            if (rb_agregar_juzgado.Checked)
            {
                //no valida ni guarda sala si es false
                if (chkbox_sala.Checked == true)
                {
                    using (var data_user = new bd_tsEntities())
                    {
                        var items_user = (from c in data_user.inf_juzgados
                                          where c.id_especializa == int_especializa
                                          where c.localidad == str_localidad
                                          where c.numero == str_numero
                                          select c).ToList();

                        if (items_user.Count == 0)
                        {
                            try
                            {
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
                                    var i_empresa = new inf_juzgados
                                    {
                                        id_juzgado = guid_njuzgado,
                                        id_especializa = int_especializa,
                                        id_estatus = 1,
                                        localidad = str_localidad,
                                        numero = str_numero,
                                        callenum = str_callenum,

                                        id_codigo = int_idcodigocp,
                                        fecha_registro = DateTime.Now,
                                        id_tribunal = guid_fidcentro
                                    };

                                    m_empresa.inf_juzgados.Add(i_empresa);
                                    m_empresa.SaveChanges();
                                }

                                using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                                {
                                    var ii_fecha_transf = (from u in edm_fecha_transf.inf_juzgados
                                                           where u.id_juzgado == guid_njuzgado
                                                           select u).ToList();

                                    if (ii_fecha_transf.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        using (var insert_userf = new bd_tsEntities())
                                        {
                                            var items_userf = new inf_juzgados_dep
                                            {
                                                id_usuario = guid_fidusuario,
                                                id_juzgado = ii_fecha_transf[0].id_juzgado,
                                                id_tipo_accion = id_accion_juzgado(),
                                                fecha_registro = DateTime.Now,
                                            };
                                            insert_userf.inf_juzgados_dep.Add(items_userf);
                                            insert_userf.SaveChanges();
                                        }
                                    }
                                }

                                using (bd_tsEntities emd_salas = new bd_tsEntities())
                                {
                                    var i_salas = (from u in emd_salas.inf_salas
                                                   where u.id_juzgado == guid_njuzgado
                                                   select u).ToList();
                                    if (i_salas.Count == 0)
                                    {
                                        using (var edm_conexion = new bd_tsEntities())
                                        {
                                            var ii_conexion = new inf_salas
                                            {
                                                id_sala = guid_nsala,
                                                nombre = str_nsala,
                                                id_juzgado = guid_njuzgado,
                                                id_estatus = 1,
                                                fecha_registro = DateTime.Now
                                            };
                                            edm_conexion.inf_salas.Add(ii_conexion);
                                            edm_conexion.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        guid_nsala = i_salas[0].id_sala;
                                        using (var edm_salasf = new bd_tsEntities())
                                        {
                                            var i_salasf = (from c in edm_salasf.inf_salas
                                                            where c.id_juzgado == guid_njuzgado
                                                            select c).FirstOrDefault();

                                            i_salasf.nombre = str_nsala;
                                            edm_salasf.SaveChanges();
                                        }
                                    }
                                }

                                using (bd_tsEntities emd_d = new bd_tsEntities())
                                {
                                    var i_d = (from u in emd_d.inf_dispositivos
                                               where u.id_sala == guid_nsala
                                               select u).ToList();
                                    if (i_d.Count == 0)
                                    {
                                        using (var edm_conexion = new bd_tsEntities())
                                        {
                                            var ii_conexion = new inf_dispositivos
                                            {
                                                ip = str_ip,
                                                usuario = str_user_ip,
                                                clave = str_pass_ip,
                                                id_sala = guid_nsala,
                                                fecha_registro = DateTime.Now
                                            };
                                            edm_conexion.inf_dispositivos.Add(ii_conexion);
                                            edm_conexion.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        using (var emd_df = new bd_tsEntities())
                                        {
                                            var i_df = (from c in emd_df.inf_dispositivos
                                                        where c.id_sala == guid_nsala
                                                        select c).FirstOrDefault();

                                            i_df.ip = str_ip;
                                            i_df.usuario = str_user_ip;
                                            i_df.clave = str_pass_ip;
                                            emd_df.SaveChanges();
                                        }
                                    }
                                }
                                using (bd_tsEntities emd_d = new bd_tsEntities())
                                {
                                    var i_d = (from u in emd_d.inf_ruta_videos
                                               where u.id_sala == guid_nsala
                                               select u).ToList();
                                    if (i_d.Count == 0)
                                    {
                                        using (var insert_fiscal = new bd_tsEntities())
                                        {
                                            var items_fiscal = new inf_ruta_videos
                                            {
                                                desc_ruta_fin = @"C:\inetpub\wwwroot\ts_operacion",
                                                ruta_user_ini = str_user_path,
                                                ruta_pass_ini = str_pass_path,
                                                desc_ruta_ini = networkPath,
                                                id_usuario = guid_fidusuario,
                                                id_sala = guid_nsala,

                                                fecha_registro = DateTime.Now
                                            };
                                            insert_fiscal.inf_ruta_videos.Add(items_fiscal);
                                            insert_fiscal.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        using (var emd_df = new bd_tsEntities())
                                        {
                                            var i_df = (from c in emd_df.inf_ruta_videos
                                                        where c.id_sala == guid_nsala
                                                        select c).FirstOrDefault();

                                            i_df.desc_ruta_ini = networkPath;
                                            i_df.ruta_user_ini = str_user_path;
                                            i_df.ruta_pass_ini = str_pass_path;
                                            emd_df.SaveChanges();
                                        }
                                    }
                                }

                                limpiar_textbox_juzgado();

                                chkbox_sala.Checked = false;

                                rb_agregar_juzgado.Checked = false;
                                rb_editar_juzgado.Checked = true;
                                rb_eliminar_juzgado.Checked = false;

                                txt_buscar_juzgado.Visible = false;
                                btn_buscar_juzgado.Visible = false;

                                chkbox_sala.Visible = false;

                                using (bd_tsEntities edm_j = new bd_tsEntities())
                                {
                                    var i_j = (from i_u in edm_j.inf_juzgados
                                               join i_e in edm_j.fact_especializa on i_u.id_especializa equals i_e.id_especializa
                                               where i_u.id_tribunal == guid_fidcentro
                                               where i_u.id_estatus == 1
                                               select new
                                               {
                                                   i_u.codigo_juzgado,
                                                   i_e.desc_especializa,
                                                   i_u.localidad,
                                                   i_u.numero,
                                                   i_u.fecha_registro,
                                               }).ToList();

                                    gv_juzgado.DataSource = i_j;
                                    gv_juzgado.DataBind();
                                    gv_juzgado.Visible = true;
                                }

                                lblModalTitle.Text = "transcript";
                                lblModalBody.Text = "Datos de juzgado agregado con éxito";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                            catch
                            {
                                rfv_colonia.Enabled = true;
                            }
                        }
                        else
                        {
                            lblModalTitle.Text = "transcript";
                            lblModalBody.Text = "Datos de juzgado ya existe, favor de agregar otro";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                    }
                }
                else
                {
                    using (var data_user = new bd_tsEntities())
                    {
                        var items_user = (from c in data_user.inf_juzgados
                                          where c.id_especializa == int_especializa
                                          where c.localidad == str_localidad
                                          where c.numero == str_numero
                                          select c).ToList();

                        if (items_user.Count == 0)
                        {
                            try
                            {
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
                                    var i_empresa = new inf_juzgados
                                    {
                                        id_juzgado = guid_njuzgado,
                                        id_especializa = int_especializa,
                                        id_estatus = 1,
                                        localidad = str_localidad,
                                        numero = str_numero,
                                        callenum = str_callenum,

                                        id_codigo = int_idcodigocp,
                                        fecha_registro = DateTime.Now,
                                        id_tribunal = guid_fidcentro
                                    };

                                    m_empresa.inf_juzgados.Add(i_empresa);
                                    m_empresa.SaveChanges();
                                }

                                using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                                {
                                    var ii_fecha_transf = (from u in edm_fecha_transf.inf_juzgados
                                                           where u.id_juzgado == guid_njuzgado
                                                           select u).ToList();

                                    if (ii_fecha_transf.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        using (var insert_userf = new bd_tsEntities())
                                        {
                                            var items_userf = new inf_juzgados_dep
                                            {
                                                id_usuario = guid_fidusuario,
                                                id_juzgado = ii_fecha_transf[0].id_juzgado,
                                                id_tipo_accion = id_accion_juzgado(),
                                                fecha_registro = DateTime.Now,
                                            };
                                            insert_userf.inf_juzgados_dep.Add(items_userf);
                                            insert_userf.SaveChanges();
                                        }
                                    }
                                }

                                limpiar_textbox_juzgado();

                                chkbox_sala.Checked = false;

                                rb_agregar_juzgado.Checked = false;
                                rb_editar_juzgado.Checked = true;
                                rb_eliminar_juzgado.Checked = false;

                                txt_buscar_juzgado.Visible = false;
                                btn_buscar_juzgado.Visible = false;

                                chkbox_sala.Visible = false;

                                using (bd_tsEntities edm_j = new bd_tsEntities())
                                {
                                    var i_j = (from i_u in edm_j.inf_juzgados
                                               join i_e in edm_j.fact_especializa on i_u.id_especializa equals i_e.id_especializa
                                               where i_u.id_tribunal == guid_fidcentro
                                               where i_u.id_estatus == 1
                                               select new
                                               {
                                                   i_u.codigo_juzgado,
                                                   i_e.desc_especializa,
                                                   i_u.localidad,
                                                   i_u.numero,
                                                   i_u.fecha_registro,
                                               }).ToList();

                                    gv_juzgado.DataSource = i_j;
                                    gv_juzgado.DataBind();
                                    gv_juzgado.Visible = true;
                                }

                                lblModalTitle.Text = "transcript";
                                lblModalBody.Text = "Datos de juzgado agregado con éxito";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                            catch
                            {
                                rfv_colonia.Enabled = true;
                            }
                        }
                        else
                        {
                            lblModalTitle.Text = "transcript";
                            lblModalBody.Text = "Datos de juzgado ya existe, favor de agregar otro";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                    }
                }
            }
            else if (rb_editar_juzgado.Checked)
            {
                foreach (GridViewRow row in gv_juzgado.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_juzgado") as CheckBox);
                        if (chkRow.Checked)
                        {
                            row.BackColor = Color.YellowGreen;
                            int int_codeuser = int.Parse(row.Cells[5].Text);
                            Guid codeuser;

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
                                var items_user = (from c in data_user.inf_juzgados
                                                  where c.codigo_juzgado == int_codeuser
                                                  select c).FirstOrDefault();

                                items_user.id_especializa = int_especializa;
                                items_user.localidad = str_localidad;
                                items_user.numero = str_numero;

                                items_user.callenum = str_callenum;

                                items_user.id_codigo = int_idcodigocp;

                                data_user.SaveChanges();
                            }
                            using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                            {
                                var ii_fecha_transf = (from u in edm_fecha_transf.inf_juzgados
                                                       where u.codigo_juzgado == int_codeuser
                                                       select u).ToList();

                                if (ii_fecha_transf.Count == 0)
                                {
                                }
                                else
                                {
                                    using (var insert_userf = new bd_tsEntities())
                                    {
                                        var items_userf = new inf_juzgados_dep
                                        {
                                            id_usuario = guid_fidusuario,
                                            id_juzgado = ii_fecha_transf[0].id_juzgado,
                                            id_tipo_accion = id_accion_juzgado(),
                                            fecha_registro = DateTime.Now,
                                        };
                                        insert_userf.inf_juzgados_dep.Add(items_userf);
                                        insert_userf.SaveChanges();
                                    }
                                }
                            }

                            using (bd_tsEntities data_user = new bd_tsEntities())
                            {
                                var inf_user = (from i_u in data_user.inf_juzgados
                                                join i_e in data_user.fact_especializa on i_u.id_especializa equals i_e.id_especializa
                                                where i_u.id_tribunal == guid_fidcentro
                                                where i_u.id_estatus == 1
                                                select new
                                                {
                                                    i_u.codigo_juzgado,
                                                    i_e.desc_especializa,
                                                    i_u.localidad,
                                                    i_u.numero,
                                                    i_u.fecha_registro,
                                                }).ToList();

                                gv_juzgado.DataSource = inf_user;
                                gv_juzgado.DataBind();
                                gv_juzgado.Visible = true;
                            }

                            gv_sala.Visible = false;

                            limpiar_textbox_juzgado();
                            rb_editar_juzgado.Checked = false;

                            chkbox_sala.Checked = false;

                            rb_agregar_sala.Checked = false;
                            rb_editar_sala.Checked = false;
                            rb_eliminar_sala.Checked = false;
                            txt_sala.Text = null;

                            txt_ip.Text = null;

                            txt_user_ip.Text = null;
                            txt_pass_ip.Text = null;

                            txt_path_videos.Text = null;

                            txt_user_path.Text = null;
                            txt_pass_path.Text = null;

                            rb_agregar_sala.Enabled = false;
                            rb_editar_sala.Enabled = false;
                            rb_eliminar_sala.Enabled = false;
                            txt_sala.Enabled = false;

                            txt_ip.Enabled = false;

                            txt_user_ip.Enabled = false;
                            txt_pass_ip.Enabled = false;

                            txt_path_videos.Enabled = false;

                            txt_user_path.Enabled = false;
                            txt_pass_path.Enabled = false;
                            btn_guarda_sala.Enabled = false;

                            lblModalTitle.Text = "transcript";
                            lblModalBody.Text = "Datos de juzgado actualizados con éxito";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                        else
                        {
                            row.BackColor = Color.White;
                        }
                    }
                }
            }
            else if (rb_eliminar_juzgado.Checked)
            {
                foreach (GridViewRow row in gv_juzgado.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_juzgado") as CheckBox);
                        if (chkRow.Checked)
                        {
                            row.BackColor = Color.YellowGreen;
                            int int_codeuser = int.Parse(row.Cells[5].Text);
                            Guid codeuser, code_salas;

                            using (var data_user = new bd_tsEntities())
                            {
                                var items_user = (from c in data_user.inf_juzgados
                                                  where c.codigo_juzgado == int_codeuser
                                                  select c).FirstOrDefault();

                                codeuser = items_user.id_juzgado;
                            }
                            using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                            {
                                var ii_fecha_transf = (from u in edm_fecha_transf.inf_juzgados
                                                       where u.id_juzgado == codeuser
                                                       select u).ToList();

                                if (ii_fecha_transf.Count == 0)
                                {
                                }
                                else
                                {
                                    using (var insert_userf = new bd_tsEntities())
                                    {
                                        var items_userf = new inf_juzgados_dep
                                        {
                                            id_usuario = guid_fidusuario,
                                            id_juzgado = ii_fecha_transf[0].id_juzgado,
                                            id_tipo_accion = id_accion_juzgado(),
                                            fecha_registro = DateTime.Now,
                                        };
                                        insert_userf.inf_juzgados_dep.Add(items_userf);
                                        insert_userf.SaveChanges();
                                    }
                                }
                            }

                            using (var edm_j = new bd_tsEntities())
                            {
                                var i_j = (from c in edm_j.inf_juzgados
                                           where c.id_juzgado == codeuser
                                           select c).FirstOrDefault();

                                edm_j.inf_juzgados.Remove(i_j);
                                edm_j.SaveChanges();
                            }

                            using (var edm_s = new bd_tsEntities())
                            {
                                var i_s = (from c in edm_s.inf_salas
                                           where c.id_juzgado == codeuser
                                           select c).ToList();
                                if (i_s.Count != 0)
                                {
                                    code_salas = i_s[0].id_sala;
                                    i_s.ForEach(c => edm_s.inf_salas.Remove(c));
                                    edm_s.SaveChanges();

                                    using (var edm_js = new bd_tsEntities())
                                    {
                                        var i_js = (from c in edm_js.inf_dispositivos
                                                    where c.id_sala == code_salas
                                                    select c).ToList();
                                        if (i_js.Count == 0)
                                        {
                                        }
                                        else
                                        {
                                            foreach (var i_jsf in i_js)
                                            {
                                                edm_js.inf_dispositivos.Remove(i_jsf);
                                                edm_js.SaveChanges();
                                            }
                                        }
                                    }
                                    using (var edm_rs = new bd_tsEntities())
                                    {
                                        var i_rs = (from c in edm_rs.inf_ruta_videos
                                                    where c.id_sala == code_salas
                                                    select c).ToList();
                                        if (i_rs.Count == 0)
                                        {
                                        }
                                        else
                                        {
                                            foreach (var i_rsf in i_rs)
                                            {
                                                edm_rs.inf_ruta_videos.Remove(i_rsf);
                                                edm_rs.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }

                            using (bd_tsEntities data_user = new bd_tsEntities())
                            {
                                var inf_user = (from i_u in data_user.inf_juzgados
                                                join i_e in data_user.fact_especializa on i_u.id_especializa equals i_e.id_especializa
                                                where i_u.id_tribunal == guid_fidcentro
                                                where i_u.id_estatus == 1
                                                select new
                                                {
                                                    i_u.codigo_juzgado,
                                                    i_e.desc_especializa,
                                                    i_u.localidad,
                                                    i_u.numero,
                                                    i_u.fecha_registro,
                                                }).ToList();

                                gv_juzgado.DataSource = inf_user;
                                gv_juzgado.DataBind();
                                gv_juzgado.Visible = true;
                            }

                            gv_sala.Visible = false;

                            limpiar_textbox_juzgado();
                            rb_eliminar_juzgado.Checked = false;
                            chkbox_sala.Checked = false;

                            rb_agregar_sala.Checked = false;
                            rb_editar_sala.Checked = false;
                            rb_eliminar_sala.Checked = false;
                            txt_sala.Text = null;

                            txt_ip.Text = null;

                            txt_user_ip.Text = null;
                            txt_pass_ip.Text = null;

                            txt_path_videos.Text = null;

                            txt_user_path.Text = null;
                            txt_pass_path.Text = null;

                            rb_agregar_sala.Enabled = false;
                            rb_editar_sala.Enabled = false;
                            rb_eliminar_sala.Enabled = false;
                            txt_sala.Enabled = false;

                            txt_ip.Enabled = false;

                            txt_user_ip.Enabled = false;
                            txt_pass_ip.Enabled = false;

                            txt_path_videos.Enabled = false;

                            txt_user_path.Enabled = false;
                            txt_pass_path.Enabled = false;
                            btn_guarda_sala.Enabled = false;

                            lblModalTitle.Text = "transcript";
                            lblModalBody.Text = "Datos de juzgado eliminado con éxito";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                        else
                        {
                            row.BackColor = Color.White;
                        }
                    }
                }
            }

            rfv_sala.Enabled = false;

            rfv_ip.Enabled = false;
            rfv_user_ip.Enabled = false;
            rfv_pass_ip.Enabled = false;

            rfv_path_videos.Enabled = false;
            rfv_user_path.Enabled = false;
            rfv_pass_path.Enabled = false;
        }

        protected void rb_agregar_juzgado_CheckedChanged(object sender, EventArgs e)
        {
            rfv_sala.Enabled = false;
            rfv_ip.Enabled = false;
            rfv_user_ip.Enabled = false;
            rfv_pass_ip.Enabled = false;
            rfv_path_videos.Enabled = false;
            rfv_user_path.Enabled = false;
            rfv_pass_path.Enabled = false;

            rb_editar_juzgado.Checked = false;
            rb_eliminar_juzgado.Checked = false;

            btn_guarda_sala.Visible = false;

            txt_buscar_juzgado.Visible = false;
            btn_buscar_juzgado.Visible = false;
            gv_juzgado.Visible = false;
            rb_agregar_sala.Checked = false;
            rb_editar_sala.Checked = false;
            rb_eliminar_sala.Checked = false;
            txt_sala.Text = "";
            txt_ip.Text = "";

            gv_sala.Visible = false;

            chkbox_sala.Visible = true;
            chkbox_sala.Checked = false;

            rb_agregar_sala.Checked = false;
            rb_editar_sala.Checked = false;
            rb_eliminar_sala.Checked = false;
            txt_sala.Text = null;

            txt_ip.Text = null;

            txt_user_ip.Text = null;
            txt_pass_ip.Text = null;

            txt_path_videos.Text = null;

            txt_user_path.Text = null;
            txt_pass_path.Text = null;

            rb_agregar_sala.Enabled = false;
            rb_editar_sala.Enabled = false;
            rb_eliminar_sala.Enabled = false;
            txt_sala.Enabled = false;

            txt_ip.Enabled = false;

            txt_user_ip.Enabled = false;
            txt_pass_ip.Enabled = false;

            txt_path_videos.Enabled = false;

            txt_user_path.Enabled = false;
            txt_pass_path.Enabled = false;
            btn_guarda_sala.Enabled = false;
        }

        private void limpiar_textbox_juzgado()
        {
            ddl_especializa.SelectedValue = "0";
            txt_localidad.Text = "";
            txt_numero.Text = "";
            txt_callenum.Text = "";
            txt_cp.Text = "";
            ddl_colonia.Items.Clear();
            ddl_colonia.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddl_colonia.SelectedValue = "0";
            txt_municipio.Text = "";
            txt_estado.Text = "";
        }

        protected void rb_editar_juzgado_CheckedChanged(object sender, EventArgs e)
        {
            rfv_sala.Enabled = false;
            rfv_ip.Enabled = false;
            rfv_user_ip.Enabled = false;
            rfv_pass_ip.Enabled = false;
            rfv_path_videos.Enabled = false;
            rfv_user_path.Enabled = false;
            rfv_pass_path.Enabled = false;

            rb_agregar_juzgado.Checked = false;
            rb_eliminar_juzgado.Checked = false;

            limpiar_textbox_juzgado();
            txt_buscar_juzgado.Visible = false;
            btn_buscar_juzgado.Visible = false;

            chkbox_sala.Visible = false;

            using (bd_tsEntities data_user = new bd_tsEntities())
            {
                var inf_user = (from i_u in data_user.inf_juzgados
                                join i_e in data_user.fact_especializa on i_u.id_especializa equals i_e.id_especializa
                                where i_u.id_tribunal == guid_fidcentro
                                where i_u.id_estatus == 1
                                select new
                                {
                                    i_u.codigo_juzgado,
                                    i_e.desc_especializa,
                                    i_u.localidad,
                                    i_u.numero,
                                    i_u.fecha_registro,
                                }).ToList();

                gv_juzgado.DataSource = inf_user;
                gv_juzgado.DataBind();
                gv_juzgado.Visible = true;
            }
        }

        protected void rb_eliminar_juzgado_CheckedChanged(object sender, EventArgs e)
        {
            rfv_sala.Enabled = false;
            rfv_ip.Enabled = false;
            rfv_user_ip.Enabled = false;
            rfv_pass_ip.Enabled = false;
            rfv_path_videos.Enabled = false;
            rfv_user_path.Enabled = false;
            rfv_pass_path.Enabled = false;

            rb_agregar_juzgado.Checked = false;
            rb_editar_juzgado.Checked = false;
            limpiar_textbox_juzgado();
            txt_buscar_juzgado.Visible = false;
            btn_buscar_juzgado.Visible = false;

            rb_agregar_sala.Visible = true;
            rb_editar_sala.Visible = true;
            rb_eliminar_sala.Visible = true;

            chkbox_sala.Visible = false;

            rb_agregar_sala.Enabled = true;

            rb_editar_sala.Enabled = true;
            rb_eliminar_sala.Enabled = true;
            txt_sala.Enabled = true;

            txt_ip.Enabled = true;

            txt_user_ip.Enabled = true;
            txt_pass_ip.Enabled = true;

            txt_path_videos.Enabled = true;

            txt_user_path.Enabled = true;
            txt_pass_path.Enabled = true;
            btn_guarda_sala.Enabled = true;

            rb_editar_sala.Checked = false;
            rb_eliminar_sala.Checked = false;
            txt_sala.Text = null;

            txt_ip.Text = null;

            txt_user_ip.Text = null;
            txt_pass_ip.Text = null;

            txt_path_videos.Text = null;

            txt_user_path.Text = null;
            txt_pass_path.Text = null;

            using (bd_tsEntities data_user = new bd_tsEntities())
            {
                var inf_user = (from i_u in data_user.inf_juzgados
                                join i_e in data_user.fact_especializa on i_u.id_especializa equals i_e.id_especializa
                                where i_u.id_tribunal == guid_fidcentro
                                where i_u.id_estatus == 1
                                select new
                                {
                                    i_u.codigo_juzgado,
                                    i_e.desc_especializa,
                                    i_u.localidad,
                                    i_u.numero,
                                    i_u.fecha_registro,
                                }).ToList();

                gv_juzgado.DataSource = inf_user;
                gv_juzgado.DataBind();
                gv_juzgado.Visible = true;
            }
        }

        protected void chk_juzgado_CheckedChanged(object sender, EventArgs e)
        {
            rb_agregar_sala.Enabled = true;
            rb_agregar_sala.Visible = true;
            rb_editar_sala.Visible = true;
            rb_eliminar_sala.Visible = true;

            rb_editar_sala.Enabled = true;
            rb_eliminar_sala.Enabled = true;
            txt_sala.Enabled = true;

            txt_ip.Enabled = true;

            txt_user_ip.Enabled = true;
            txt_pass_ip.Enabled = true;

            txt_path_videos.Enabled = true;

            txt_user_path.Enabled = true;
            txt_pass_path.Enabled = true;
            btn_guarda_sala.Enabled = true;

            rb_editar_sala.Checked = false;
            rb_eliminar_sala.Checked = false;
            txt_sala.Text = null;

            txt_ip.Text = null;

            txt_user_ip.Text = null;
            txt_pass_ip.Text = null;

            txt_path_videos.Text = null;

            txt_user_path.Text = null;
            txt_pass_path.Text = null;

            foreach (GridViewRow row in gv_juzgado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_juzgado") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.YellowGreen;
                        int int_code = int.Parse(row.Cells[5].Text);
                        using (bd_tsEntities m_tribunal = new bd_tsEntities())
                        {
                            var i_tribunal = (from u in m_tribunal.inf_juzgados
                                              where u.codigo_juzgado == int_code
                                              select u).FirstOrDefault();

                            ddl_especializa.SelectedValue = i_tribunal.id_especializa.ToString();
                            txt_localidad.Text = i_tribunal.localidad;
                            txt_numero.Text = i_tribunal.numero;
                            txt_callenum.Text = i_tribunal.callenum;
                            //txt_cp.Text = i_tribunal.cp;
                            guid_idjuzgado = i_tribunal.id_juzgado;

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

                        rb_agregar_sala.Checked = false;
                        rb_editar_sala.Checked = false;
                        rb_eliminar_sala.Checked = false;
                        txt_sala.Text = null;
                        txt_ip.Text = null;

                        txt_user_ip.Text = null;
                        txt_pass_ip.Text = null;

                        txt_path_videos.Text = null;

                        txt_user_path.Text = null;
                        txt_pass_path.Text = null;
                        using (bd_tsEntities data_user = new bd_tsEntities())
                        {
                            var inf_user = (from i_u in data_user.inf_salas
                                            where i_u.id_juzgado == guid_idjuzgado
                                            where i_u.id_estatus == 1
                                            select new
                                            {
                                                i_u.codigo_sala,
                                                i_u.nombre,

                                                i_u.fecha_registro,
                                            }).ToList();

                            gv_sala.DataSource = inf_user;
                            gv_sala.DataBind();
                            gv_sala.Visible = true;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void rb_agregar_sala_CheckedChanged(object sender, EventArgs e)
        {
            rfv_sala.Enabled = true;
            rb_editar_sala.Checked = false;
            rb_eliminar_sala.Checked = false;

            btn_guarda_sala.Visible = true;

            txt_sala.Text = null;
            txt_ip.Text = null;
            txt_user_ip.Text = null;
            txt_pass_ip.Text = null;
            txt_path_videos.Text = null;
            txt_user_path.Text = null;
            txt_pass_path.Text = null;
            gv_sala.Visible = false;
        }

        protected void rb_editar_sala_CheckedChanged(object sender, EventArgs e)
        {
            rfv_sala.Enabled = true;
            rb_agregar_sala.Checked = false;
            rb_eliminar_sala.Checked = false;

            btn_guarda_sala.Visible = true;

            foreach (GridViewRow row in gv_juzgado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_juzgado") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.YellowGreen;
                        int int_code = int.Parse(row.Cells[5].Text);
                        using (bd_tsEntities m_tribunal = new bd_tsEntities())
                        {
                            var i_tribunal = (from u in m_tribunal.inf_juzgados
                                              where u.codigo_juzgado == int_code
                                              select u).FirstOrDefault();

                            ddl_especializa.SelectedValue = i_tribunal.id_especializa.ToString();
                            txt_localidad.Text = i_tribunal.localidad;
                            txt_numero.Text = i_tribunal.numero;
                            txt_callenum.Text = i_tribunal.callenum;
                            //txt_cp.Text = i_tribunal.cp;
                            guid_idjuzgado = i_tribunal.id_juzgado;

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

                        using (bd_tsEntities data_user = new bd_tsEntities())
                        {
                            var inf_user = (from i_u in data_user.inf_salas
                                            where i_u.id_juzgado == guid_idjuzgado
                                            where i_u.id_estatus == 1
                                            select new
                                            {
                                                i_u.codigo_sala,
                                                i_u.nombre,

                                                i_u.fecha_registro,
                                            }).ToList();

                            gv_sala.DataSource = inf_user;
                            gv_sala.DataBind();
                            gv_sala.Visible = true;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void rb_eliminar_sala_CheckedChanged(object sender, EventArgs e)
        {
            rb_editar_sala.Checked = false;
            rb_agregar_sala.Checked = false;

            btn_guarda_sala.Visible = true;

            foreach (GridViewRow row in gv_juzgado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_juzgado") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.YellowGreen;
                        int int_code = int.Parse(row.Cells[5].Text);
                        using (bd_tsEntities m_tribunal = new bd_tsEntities())
                        {
                            var i_tribunal = (from u in m_tribunal.inf_juzgados
                                              where u.codigo_juzgado == int_code
                                              select u).FirstOrDefault();

                            ddl_especializa.SelectedValue = i_tribunal.id_especializa.ToString();
                            txt_localidad.Text = i_tribunal.localidad;
                            txt_numero.Text = i_tribunal.numero;
                            txt_callenum.Text = i_tribunal.callenum;
                            //txt_cp.Text = i_tribunal.cp;
                            guid_idjuzgado = i_tribunal.id_juzgado;

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

                        using (bd_tsEntities data_user = new bd_tsEntities())
                        {
                            var inf_user = (from i_u in data_user.inf_salas
                                            where i_u.id_juzgado == guid_idjuzgado
                                            where i_u.id_estatus == 1
                                            select new
                                            {
                                                i_u.codigo_sala,
                                                i_u.nombre,

                                                i_u.fecha_registro,
                                            }).ToList();

                            gv_sala.DataSource = inf_user;
                            gv_sala.DataBind();
                            gv_sala.Visible = true;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void btn_buscar_juzgado_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_buscar_juzgado.Text))
            {
                txt_buscar_juzgado.BackColor = Color.Yellow;
            }
            else
            {
                txt_buscar_juzgado.BackColor = Color.Transparent;
                string str_userb = txt_buscar_juzgado.Text;

                using (bd_tsEntities data_user = new bd_tsEntities())
                {
                    var inf_user = (from i_u in data_user.inf_juzgados
                                    join i_e in data_user.fact_especializa on i_u.id_especializa equals i_e.id_especializa
                                    where i_u.localidad.Contains(str_userb)
                                    where i_u.id_tribunal == guid_fidcentro
                                    where i_u.id_estatus == 1
                                    select new
                                    {
                                        i_u.codigo_juzgado,
                                        i_u.localidad,
                                        i_u.numero,
                                        i_e.desc_especializa,
                                        i_u.fecha_registro,
                                    }).ToList();

                    gv_juzgado.DataSource = inf_user;
                    gv_juzgado.DataBind();
                    gv_juzgado.Visible = true;
                }
            }
        }

        protected void chk_sala_CheckedChanged(object sender, EventArgs e)
        {
            txt_sala.Text = null;
            txt_ip.Text = null;
            txt_user_ip.Text = null;
            txt_pass_ip.Text = null;
            txt_path_videos.Text = null;
            txt_user_path.Text = null;
            txt_pass_path.Text = null;

            if (rb_editar_sala.Checked || rb_eliminar_sala.Checked)
            {
                foreach (GridViewRow row in gv_sala.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_sala") as CheckBox);
                        if (chkRow.Checked)
                        {
                            row.BackColor = Color.YellowGreen;

                            int int_code = int.Parse(row.Cells[3].Text);
                            Guid guid_idsala;

                            using (bd_tsEntities data_user = new bd_tsEntities())
                            {
                                var inf_user = (from i_s in data_user.inf_salas

                                                where i_s.codigo_sala == int_code
                                                where i_s.id_estatus == 1
                                                select new
                                                {
                                                    i_s.id_sala,
                                                    i_s.nombre,
                                                }).FirstOrDefault();

                                txt_sala.Text = inf_user.nombre;
                                guid_idsala = inf_user.id_sala;
                            }

                            using (bd_tsEntities edm_conexion = new bd_tsEntities())
                            {
                                var i_conexion = (from c in edm_conexion.inf_dispositivos
                                                  where c.id_sala == guid_idsala
                                                  select c).ToList();

                                if (i_conexion.Count == 0)
                                {
                                }
                                else
                                {
                                    using (bd_tsEntities data_user = new bd_tsEntities())
                                    {
                                        var inf_user = (from i_s in data_user.inf_salas
                                                        join i_d in data_user.inf_dispositivos on i_s.id_sala equals i_d.id_sala

                                                        where i_s.codigo_sala == int_code
                                                        where i_s.id_estatus == 1
                                                        select new
                                                        {
                                                            i_d.ip,
                                                            i_d.usuario,
                                                            i_d.clave,
                                                        }).FirstOrDefault();

                                        txt_ip.Text = inf_user.ip;
                                        txt_user_ip.Text = inf_user.usuario;
                                        txt_pass_ip.Text = inf_user.clave;
                                    }
                                }
                            }

                            using (bd_tsEntities edm_conexion = new bd_tsEntities())
                            {
                                var i_conexion = (from c in edm_conexion.inf_ruta_videos
                                                  where c.id_sala == guid_idsala
                                                  select c).ToList();

                                if (i_conexion.Count == 0)
                                { }
                                else
                                {
                                    using (bd_tsEntities data_user = new bd_tsEntities())
                                    {
                                        var inf_user = (from i_s in data_user.inf_salas

                                                        join i_r in data_user.inf_ruta_videos on i_s.id_sala equals i_r.id_sala
                                                        where i_s.codigo_sala == int_code
                                                        where i_s.id_estatus == 1
                                                        select new
                                                        {
                                                            i_s.id_sala,
                                                            i_s.nombre,
                                                            i_s.fecha_registro,

                                                            i_r.desc_ruta_ini,
                                                            i_r.ruta_user_ini,
                                                            i_r.ruta_pass_ini
                                                        }).FirstOrDefault();

                                        txt_path_videos.Text = inf_user.desc_ruta_ini;
                                        txt_user_path.Text = inf_user.ruta_user_ini;
                                        txt_pass_path.Text = inf_user.ruta_pass_ini;
                                    }
                                }
                            }

                            try
                            {
                                using (bd_tsEntities data_user = new bd_tsEntities())
                                {
                                    var inf_user = (from i_s in data_user.inf_salas
                                                    join i_d in data_user.inf_dispositivos on i_s.id_sala equals i_d.id_sala
                                                    join i_r in data_user.inf_ruta_videos on i_s.id_sala equals i_r.id_sala
                                                    where i_s.codigo_sala == int_code
                                                    where i_s.id_estatus == 1
                                                    select new
                                                    {
                                                        i_s.id_sala,
                                                        i_s.nombre,
                                                        i_s.fecha_registro,
                                                        i_d.ip,
                                                        i_d.usuario,
                                                        i_d.clave,
                                                        i_r.desc_ruta_ini,
                                                        i_r.ruta_user_ini,
                                                        i_r.ruta_pass_ini
                                                    }).FirstOrDefault();

                                    txt_sala.Text = inf_user.nombre;
                                    txt_ip.Text = inf_user.ip;
                                    txt_user_ip.Text = inf_user.usuario;
                                    txt_pass_ip.Text = encrypta.Decrypt(inf_user.clave);

                                    txt_path_videos.Text = inf_user.desc_ruta_ini;
                                    txt_user_path.Text = inf_user.ruta_user_ini;
                                    txt_pass_path.Text = encrypta.Decrypt(inf_user.ruta_pass_ini);
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            row.BackColor = Color.White;
                        }
                    }
                }
            }
        }

        protected void btn_ip_validar_Click(object sender, EventArgs e)
        {
            GetLocations();
        }

        private void GetLocations()
        {
            string str_ip = txt_ip.Text;
            string str_usuario = txt_user_ip.Text;
            string str_pass = txt_pass_ip.Text;

            try
            {
                Dns.GetHostEntry(str_ip); //using System.Net;
                lblModalTitle.Text = "transcript";
                lblModalBody.Text = "IP Activa";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            catch (Exception e)
            {
                lblModalTitle.Text = "transcript";
                lblModalBody.Text = "Falla de conexión, favor de reitentar o contactar al administrador";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }

        protected void gv_juzgado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_juzgado.PageIndex = e.NewPageIndex;

            using (bd_tsEntities data_user = new bd_tsEntities())
            {
                var inf_user = (from i_u in data_user.inf_juzgados
                                join i_e in data_user.fact_especializa on i_u.id_especializa equals i_e.id_especializa
                                where i_u.id_tribunal == guid_fidcentro
                                where i_u.id_estatus == 1
                                select new
                                {
                                    i_u.codigo_juzgado,
                                    i_e.desc_especializa,
                                    i_u.localidad,
                                    i_u.numero,
                                    i_u.fecha_registro,
                                }).ToList();

                gv_juzgado.DataSource = inf_user;
                gv_juzgado.DataBind();
                gv_juzgado.Visible = true;
            }
        }

        protected void gv_sala_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_sala.PageIndex = e.NewPageIndex;
            foreach (GridViewRow row in gv_juzgado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_juzgado") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.YellowGreen;
                        int int_code = int.Parse(row.Cells[5].Text);

                        using (bd_tsEntities m_tribunal = new bd_tsEntities())
                        {
                            var i_tribunal = (from u in m_tribunal.inf_juzgados
                                              where u.codigo_juzgado == int_code
                                              select u).FirstOrDefault();

                            ddl_especializa.SelectedValue = i_tribunal.id_especializa.ToString();
                            txt_localidad.Text = i_tribunal.localidad;
                            txt_numero.Text = i_tribunal.numero;
                            txt_callenum.Text = i_tribunal.callenum;

                            guid_idjuzgado = i_tribunal.id_juzgado;

                            //using (bd_tsEntities db_sepomex = new bd_tsEntities())
                            //{
                            //    var tbl_sepomex = (from c in db_sepomex.inf_sepomex
                            //                       where c.d_codigo == i_tribunal.cp.ToString()
                            //                       select c).ToList();

                            //    ddl_colonia.DataSource = tbl_sepomex;
                            //    ddl_colonia.DataTextField = "d_asenta";
                            //    ddl_colonia.DataValueField = "id_asenta_cpcons";
                            //    ddl_colonia.DataBind();

                            //    ddl_colonia.SelectedValue = i_tribunal.id_asenta_cpcons.ToString();
                            //    txt_municipio.Text = tbl_sepomex[0].D_mnpio;
                            //    txt_estado.Text = tbl_sepomex[0].d_estado;
                            //}

                            using (bd_tsEntities data_user = new bd_tsEntities())
                            {
                                var inf_user = (from i_u in data_user.inf_salas
                                                where i_u.id_juzgado == guid_idjuzgado
                                                where i_u.id_estatus == 1
                                                select new
                                                {
                                                    i_u.codigo_sala,
                                                    i_u.nombre,

                                                    i_u.fecha_registro,
                                                }).ToList();

                                gv_sala.DataSource = inf_user;
                                gv_sala.DataBind();
                                gv_sala.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
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
                    ddl_colonia.Items.Insert(0, new ListItem("*Colonia", "0"));

                    txt_municipio.Text = tbl_sepomex[0].D_mnpio;
                    txt_estado.Text = tbl_sepomex[0].d_estado;
                    rfv_colonia.Enabled = true;
                }
                else if (tbl_sepomex.Count == 0)
                {
                    ddl_colonia.Items.Clear();
                    ddl_colonia.Items.Insert(0, new ListItem("*Colonia", "0"));
                    txt_municipio.Text = "";
                    txt_estado.Text = "";
                    rfv_colonia.Enabled = false;
                }
            }
        }

        protected void txt_cp_TextChanged(object sender, EventArgs e)
        {
            string str_codigo = txt_cp.Text;

            datos_sepomex(str_codigo);
        }

        protected void txt_ip_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ip.Text))
            {
                rfv_ip.Enabled = true;
                rfv_user_ip.Enabled = true;
                rfv_pass_ip.Enabled = true;
                rfv_path_videos.Enabled = false;
                rfv_user_path.Enabled = false;
                rfv_pass_path.Enabled = false;
            }
            else
            {
                rfv_ip.Enabled = true;
                rfv_user_ip.Enabled = true;
                rfv_pass_ip.Enabled = true;
                rfv_path_videos.Enabled = false;
                rfv_user_path.Enabled = false;
                rfv_pass_path.Enabled = false;
            }
        }

        protected void txt_path_videos_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_path_videos.Text))
            {
                rfv_ip.Enabled = true;
                rfv_user_ip.Enabled = true;
                rfv_pass_ip.Enabled = true;
                rfv_path_videos.Enabled = false;
                rfv_user_path.Enabled = false;
                rfv_pass_path.Enabled = false;
            }
            else
            {
                rfv_ip.Enabled = false;
                rfv_user_ip.Enabled = false;
                rfv_pass_ip.Enabled = false;
                rfv_path_videos.Enabled = true;
                rfv_user_path.Enabled = true;
                rfv_pass_path.Enabled = true;
            }
        }

        public class NetworkConnection : IDisposable
        {
            #region Variables

            /// <summary>
            /// The full path of the directory.
            /// </summary>
            private readonly string _networkName;

            #endregion Variables

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="NetworkConnection"/> class.
            /// </summary>
            /// <param name="networkName">
            /// The full path of the network share.
            /// </param>
            /// <param name="credentials">
            /// The credentials to use when connecting to the network share.
            /// </param>
            public NetworkConnection(string networkName, NetworkCredential credentials)
            {
                _networkName = networkName;

                var netResource = new NetResource
                {
                    Scope = ResourceScope.GlobalNetwork,
                    ResourceType = ResourceType.Disk,
                    DisplayType = ResourceDisplaytype.Share,
                    RemoteName = networkName.TrimEnd('\\')
                };

                var result = WNetAddConnection2(
                    netResource, credentials.Password, credentials.UserName, 0);

                if (result != 0)
                {
                    throw new Win32Exception(result);
                }
            }

            #endregion Constructors

            #region Events

            /// <summary>
            /// Occurs when this instance has been disposed.
            /// </summary>
            public event EventHandler<EventArgs> Disposed;

            #endregion Events

            #region Public methods

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion Public methods

            #region Protected methods

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    var handler = Disposed;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }

                WNetCancelConnection2(_networkName, 0, true);
            }

            #endregion Protected methods

            #region Private static methods

            /// <summary>
            ///The WNetAddConnection2 function makes a connection to a network resource. The function can redirect a local device to the network resource.
            /// </summary>
            /// <param name="netResource">A <see cref="NetResource"/> structure that specifies details of the proposed connection, such as information about the network resource, the local device, and the network resource provider.</param>
            /// <param name="password">The password to use when connecting to the network resource.</param>
            /// <param name="username">The username to use when connecting to the network resource.</param>
            /// <param name="flags">The flags. See http://msdn.microsoft.com/en-us/library/aa385413%28VS.85%29.aspx for more information.</param>
            /// <returns></returns>
            [DllImport("mpr.dll")]
            private static extern int WNetAddConnection2(NetResource netResource,
                                                         string password,
                                                         string username,
                                                         int flags);

            /// <summary>
            /// The WNetCancelConnection2 function cancels an existing network connection. You can also call the function to remove remembered network connections that are not currently connected.
            /// </summary>
            /// <param name="name">Specifies the name of either the redirected local device or the remote network resource to disconnect from.</param>
            /// <param name="flags">Connection type. The following values are defined:
            /// 0: The system does not update information about the connection. If the connection was marked as persistent in the registry, the system continues to restore the connection at the next logon. If the connection was not marked as persistent, the function ignores the setting of the CONNECT_UPDATE_PROFILE flag.
            /// CONNECT_UPDATE_PROFILE: The system updates the user profile with the information that the connection is no longer a persistent one. The system will not restore this connection during subsequent logon operations. (Disconnecting resources using remote names has no effect on persistent connections.)
            /// </param>
            /// <param name="force">Specifies whether the disconnection should occur if there are open files or jobs on the connection. If this parameter is FALSE, the function fails if there are open files or jobs.</param>
            /// <returns></returns>
            [DllImport("mpr.dll")]
            private static extern int WNetCancelConnection2(string name, int flags, bool force);

            #endregion Private static methods

            /// <summary>
            /// Finalizes an instance of the <see cref="NetworkConnection"/> class.
            /// Allows an <see cref="System.Object"></see> to attempt to free resources and perform other cleanup operations before the <see cref="System.Object"></see> is reclaimed by garbage collection.
            /// </summary>
            ~NetworkConnection()
            {
                Dispose(false);
            }
        }

        #region Objects needed for the Win32 functions

#pragma warning disable 1591

        /// <summary>
        /// The net resource.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplaytype DisplayType;
            public int Usage;
            public string LocalName;
            public string RemoteName;
            public string Comment;
            public string Provider;
        }

        /// <summary>
        /// The resource scope.
        /// </summary>
        public enum ResourceScope
        {
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
        };

        /// <summary>
        /// The resource type.
        /// </summary>
        public enum ResourceType
        {
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
        }

        /// <summary>
        /// The resource displaytype.
        /// </summary>
        public enum ResourceDisplaytype
        {
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            Shareadmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            Ndscontainer = 0x0b
        }

#pragma warning restore 1591

        #endregion Objects needed for the Win32 functions
    }
}