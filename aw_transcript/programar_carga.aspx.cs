using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class programar_carga : System.Web.UI.Page
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

            using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
            {
                var i_fecha_transf = (from c in edm_fecha_transf.inf_fecha_transformacion
                                      select c).ToList();

                if (i_fecha_transf.Count != 0)
                {
                    rb_add_transformation.Visible = false;
                }
                else
                {
                    lblModalTitle.Text = "transcript";
                    lblModalBody.Text = "Sin registro, favor de agregar uno";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
        }

        protected void rb_add_transformation_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit_transformation.Checked = false;
            clean_txt();
            div_inftransformation.Visible = true;
        }

        private void clean_txt()
        {
            txt_date.Text = "";
            txt_hora.Text = "";
            //ddl_fhora.SelectedValue = "Seleccionar";
            gv_transformationf.Visible = false;
            gv_transformation.Visible = false;
        }

        protected void rb_edit_transformation_CheckedChanged(object sender, EventArgs e)
        {
            div_inftransformation.Visible = true;
            rb_add_transformation.Checked = false;
            gv_transformationf.Visible = false;
            clean_txt();

            using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
            {
                var i_fecha_transf = (from u in edm_fecha_transf.inf_fecha_transformacion
                                      select new
                                      {
                                          u.id_fecha_transformacion,
                                          u.horario,
                                          u.fecha_registro
                                      }).ToList();

                if (i_fecha_transf.Count == 0)
                {
                    rb_edit_transformation.Checked = false;

                    lblModalTitle.Text = "transcript";
                    lblModalBody.Text = "Sin registro, favor de agregar uno";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    gv_transformation.DataSource = i_fecha_transf;
                    gv_transformation.DataBind();
                    gv_transformation.Visible = true;
                }
            }
        }

        protected void chkselect_transformation(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_transformation.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.YellowGreen;
                        int str_code = Convert.ToInt32(row.Cells[1].Text);

                        using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                        {
                            var i_fecha_transf = (from u in edm_fecha_transf.inf_fecha_transformacion
                                                  where u.id_fecha_transformacion == str_code
                                                  select new
                                                  {
                                                      u.id_fecha_transformacion,
                                                      u.horario,
                                                      u.fecha_registro
                                                  }).FirstOrDefault();

                            CultureInfo ci = CultureInfo.InvariantCulture;

                            DateTime str_date = Convert.ToDateTime(i_fecha_transf.horario);
                            //txt_date.Attributes["value"] = str_date.ToShortDateString();
                            txt_date.Text = str_date.ToString("yyyy/MM/dd");
                            //txt_date = str_date.ToString("yyyy/MM/dd");
                            txt_hora.Text = str_date.ToString("hh:mm.F", ci);
                            string str_tt = i_fecha_transf.horario.Value.ToString("tt");
                            //ddl_fhora.SelectedValue = str_tt.ToLower();
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                        chkRow.Checked = false;
                    }
                }
            }
        }

        public int id_accion()
        {
            if (rb_add_transformation.Checked)
            {
                return 1;
            }
            else if (rb_edit_transformation.Checked)
            {
                return 2;
            }
            //else if (.Checked)
            //{
            //    return 3;
            //}
            else
            {
                return 4;
            }
        }

        protected void cmd_save_Click(object sender, EventArgs e)
        {
            if (rb_add_transformation.Checked || rb_edit_transformation.Checked)
            {
                string str_date = txt_date.Text;
                string str_hora = txt_hora.Text;
                //string str_format = ddl_fhora.SelectedValue;
                //string dateString = str_date + " " + str_hora + " " + str_format;
                string dateString = str_date + " " + str_hora;
                DateTime str_horario = DateTime.Parse(dateString);

                if (rb_add_transformation.Checked)
                {
                    using (var edm_fecha_transf = new bd_tsEntities())
                    {
                        var ii_fecha_transf = new inf_fecha_transformacion
                        {
                            horario = str_horario,
                            id_usuario = guid_fidusuario,
                            id_tribunal = guid_fidcentro,
                            fecha_registro = DateTime.Now
                        };
                        edm_fecha_transf.inf_fecha_transformacion.Add(ii_fecha_transf);
                        edm_fecha_transf.SaveChanges();
                    }

                    using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                    {
                        var ii_fecha_transf = (from u in edm_fecha_transf.inf_fecha_transformacion
                                               select u).ToList();

                        if (ii_fecha_transf.Count == 0)
                        {
                        }
                        else
                        {
                            using (var insert_user = new bd_tsEntities())
                            {
                                var items_user = new inf_fecha_transformacion_dep
                                {
                                    id_usuario = guid_fidusuario,
                                    id_fecha_transformacion = ii_fecha_transf[0].id_fecha_transformacion,
                                    id_tipo_accion = id_accion(),
                                    fecha_registro = DateTime.Now,
                                };
                                insert_user.inf_fecha_transformacion_dep.Add(items_user);
                                insert_user.SaveChanges();
                            }
                        }
                    }

                    clean_txt();
                    using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                    {
                        var ii_fecha_transf = (from u in edm_fecha_transf.inf_fecha_transformacion
                                               where u.horario == str_horario
                                               select new
                                               {
                                                   u.id_fecha_transformacion,
                                                   u.horario,
                                                   u.fecha_registro
                                               }).ToList();

                        gv_transformationf.DataSource = ii_fecha_transf;
                        gv_transformationf.DataBind();
                        gv_transformationf.Visible = true;
                    }
                    rb_add_transformation.Visible = false;
                    lblModalTitle.Text = "transcript";
                    lblModalBody.Text = "Horario de carga de videos, agregado con éxito";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else if (rb_edit_transformation.Checked)
                {
                    foreach (GridViewRow row in gv_transformation.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int str_code = Convert.ToInt32(row.Cells[1].Text);

                                using (var edm_fecha_transf = new bd_tsEntities())
                                {
                                    var ii_fecha_transf = (from c in edm_fecha_transf.inf_fecha_transformacion
                                                           where c.id_fecha_transformacion == str_code
                                                           select c).FirstOrDefault();

                                    ii_fecha_transf.horario = str_horario;

                                    edm_fecha_transf.SaveChanges();
                                }
                                using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                                {
                                    var ii_fecha_transf = (from u in edm_fecha_transf.inf_fecha_transformacion
                                                           select u).ToList();

                                    if (ii_fecha_transf.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        using (var insert_user = new bd_tsEntities())
                                        {
                                            var items_user = new inf_fecha_transformacion_dep
                                            {
                                                id_usuario = guid_fidusuario,
                                                id_fecha_transformacion = ii_fecha_transf[0].id_fecha_transformacion,
                                                id_tipo_accion = id_accion(),
                                                fecha_registro = DateTime.Now,
                                            };
                                            insert_user.inf_fecha_transformacion_dep.Add(items_user);
                                            insert_user.SaveChanges();
                                        }
                                    }
                                }
                                clean_txt();
                                lblModalTitle.Text = "transcript";
                                lblModalBody.Text = "Horario de carga de videos, actualizado con éxito";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();

                                using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                                {
                                    var ii_fecha_transf = (from u in edm_fecha_transf.inf_fecha_transformacion
                                                           select new
                                                           {
                                                               u.id_fecha_transformacion,
                                                               u.horario,
                                                               u.fecha_registro
                                                           }).ToList();

                                    gv_transformation.DataSource = ii_fecha_transf;
                                    gv_transformation.DataBind();
                                    gv_transformation.Visible = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                lblModalTitle.Text = "transcript";
                lblModalBody.Text = "Favor de seleccionar una accion";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }
    }
}