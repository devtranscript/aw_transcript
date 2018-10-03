using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class estatus_carga_ext : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cmd_search_Click(object sender, EventArgs e)
        {
            gv_usr_ext.Visible = false;

            using (var edm_materialf = new bd_tsEntities())
            {
                var i_materialf = (from c in edm_materialf.inf_master_jvl
                                   select c).ToList();

                if (i_materialf.Count == 0)
                {
                    gv_usr_ext.Visible = false;
                    gv_usuarios.Visible = false;
                    lblModalTitle.Text = "transcript";
                    lblModalBody.Text = "Sin registros, favor de reintentar o contactar con el administrador";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    iframe_pdf.Visible = false;
                    play_video.Visible = false;

                    upModal.Update();
                }
                else
                {
                    var inf_user = (from i_m in edm_materialf.inf_master_jvl
                                    join i_tu in edm_materialf.fact_est_exp on i_m.id_estatus_exp equals i_tu.id_est_exp
                                    join i_rv in edm_materialf.inf_ruta_videos on i_m.id_ruta_videos equals i_rv.id_ruta_videos
                                    join i_s in edm_materialf.inf_salas on i_rv.id_sala equals i_s.id_sala
                                    join i_j in edm_materialf.inf_juzgados on i_s.id_juzgado equals i_j.id_juzgado
                                    select new
                                    {
                                        i_m.sesion,
                                        i_j.localidad,
                                        i_j.numero,
                                        i_s.nombre,
                                        i_m.titulo,
                                        i_m.err_carga,
                                        i_tu.desc_est_exp,
                                        i_m.fecha_registro
                                    }).ToList();

                    gv_usuarios.DataSource = inf_user;
                    gv_usuarios.DataBind();
                    gv_usuarios.Visible = true;
                }
            }
        }

        protected void btn_video_ext_Click(object sender, EventArgs e)
        {
        }

        protected void gv_usuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnButton = (Button)e.Row.FindControl("btn_pdf_ext");

                if (e.Row.Cells[5].Text == "Sin Errores" && e.Row.Cells[6].Text == "CARGADO")
                {
                    btnButton.Text = "MOSTRAR";
                    btnButton.Enabled = true;
                }
                else if (e.Row.Cells[5].Text == "Sin Errores" && e.Row.Cells[6].Text == "NUEVO")
                {
                    btnButton.Text = "CARGAR";
                    btnButton.Enabled = true;
                }
                else if (e.Row.Cells[5].Text == "Sin Errores" && e.Row.Cells[6].Text == "CARGANDO")
                {
                    btnButton.Text = "ESPERAR";
                    btnButton.Enabled = false;
                }
                else if (e.Row.Cells[6].Text == "ERR CARGA")
                {
                    btnButton.Text = "CSV";
                    btnButton.Enabled = true;
                }
                else if (e.Row.Cells[4].Text == "INACTIVO")
                {
                }
                else
                {
                }
            }
        }

        protected void gv_usuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                string status = gvr.Cells[3].Text.ToString().Trim();
                string id_sessionf = gvr.Cells[0].Text;

                if (gvr.Cells[5].Text == "Sin Errores" && gvr.Cells[6].Text == "CARGADO")
                {
                    using (bd_tsEntities data_user = new bd_tsEntities())
                    {
                        var inf_user = (from i_m in data_user.inf_master_jvl
                                        join i_tu in data_user.fact_est_exp on i_m.id_estatus_exp equals i_tu.id_est_exp
                                        join i_rv in data_user.inf_ruta_videos on i_m.id_ruta_videos equals i_rv.id_ruta_videos
                                        join i_s in data_user.inf_salas on i_rv.id_sala equals i_s.id_sala
                                        join i_j in data_user.inf_juzgados on i_s.id_juzgado equals i_j.id_juzgado
                                        where i_m.sesion == id_sessionf
                                        select new
                                        {
                                            i_m.sesion,
                                            i_j.localidad,
                                            i_j.numero,
                                            i_s.nombre,
                                            i_m.titulo,
                                            i_m.err_carga,
                                            i_tu.desc_est_exp,
                                            i_m.fecha_registro,
                                            i_m.id_control_exp
                                        }).ToList();

                        gv_usuarios.DataSource = inf_user;
                        gv_usuarios.DataBind();
                        gv_usuarios.Visible = true;

                        Guid f = inf_user[0].id_control_exp;

                        var inf_maat_ext = (from i_m in data_user.inf_exp_mat
                                            where i_m.id_control_exp == f
                                            select i_m).ToList();

                        gv_usr_ext.DataSource = inf_maat_ext;
                        gv_usr_ext.DataBind();
                        gv_usr_ext.Visible = true;
                    }
                }
                else if (gvr.Cells[5].Text == "Sin Errores" && gvr.Cells[6].Text == "NUEVO")
                {
                    using (var act_media = new bd_tsEntities())
                    {
                        var a_media = (from c in act_media.inf_master_jvl
                                       where c.sesion == id_sessionf
                                       select c).ToList();

                        a_media[0].id_estatus_exp = 2;
                        act_media.SaveChanges();
                    }

                    using (bd_tsEntities data_user = new bd_tsEntities())
                    {
                        var inf_user = (from i_m in data_user.inf_master_jvl
                                        join i_tu in data_user.fact_est_exp on i_m.id_estatus_exp equals i_tu.id_est_exp
                                        join i_rv in data_user.inf_ruta_videos on i_m.id_ruta_videos equals i_rv.id_ruta_videos
                                        join i_s in data_user.inf_salas on i_rv.id_sala equals i_s.id_sala
                                        join i_j in data_user.inf_juzgados on i_s.id_juzgado equals i_j.id_juzgado
                                        select new
                                        {
                                            i_m.sesion,
                                            i_j.localidad,
                                            i_j.numero,
                                            i_s.nombre,
                                            i_m.titulo,
                                            i_m.err_carga,
                                            i_tu.desc_est_exp,
                                            i_m.fecha_registro
                                        }).ToList();

                        gv_usuarios.DataSource = inf_user;
                        gv_usuarios.DataBind();
                        gv_usuarios.Visible = true;
                    }
                }
                else
                {
                }
            }
            catch
            { }
        }

        protected void lkb_pdf_exp_Click(object sender, EventArgs e)
        {
        }

        protected void lkb_video_exp_Click(object sender, EventArgs e)
        {
        }

        protected void gv_usr_ext_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str_session, str_video, str_pdf, str_mp4, str_exp;
            int int_estatus;
            Guid guid_exp;
            try
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                string nom_archivo = gvr.Cells[0].Text.ToString().Trim();

                using (var data_mat = new bd_tsEntities())
                {
                    var items_mat = (from c in data_mat.inf_exp_mat
                                     where c.nom_archivo == nom_archivo
                                     select c).FirstOrDefault();

                    guid_exp = Guid.Parse(items_mat.id_control_exp.ToString());
                    str_pdf = items_mat.ruta_ext.Replace("C:\\inetpub\\wwwroot", "");
                    str_mp4 = items_mat.ruta_archivo.Replace("C:\\inetpub\\wwwroot", "");
                    int_estatus = 1;

                    var i_extmat = (from c in data_mat.inf_master_jvl
                                    where c.id_control_exp == guid_exp
                                    select c).FirstOrDefault();

                    str_exp = i_extmat.sesion;
                }
                //using (var data_mat = new bd_tsEntities())
                //{
                //    var items_mat = (from c in data_mat.inf_material
                //                     where c.id_material == id_session
                //                     select c).FirstOrDefault();

                //    str_sessionf = items_mat.sesion;
                //}

                str_session = nom_archivo;

                switch (int_estatus)
                {
                    case 1:

                        //using (var edm_material = new bd_tsEntities())
                        //{
                        //    var i_material = new inf_material_dep
                        //    {
                        //        sesion = str_session,
                        //        video = str_video,
                        //        id_usuario = guid_fidusuario,
                        //        id_material = 0,
                        //        fecha_registro = DateTime.Now,
                        //        fecha_registro_alt = DateTime.Now
                        //    };

                        //    edm_material.inf_material_dep.Add(i_material);
                        //    edm_material.SaveChanges();
                        //}

                        //string d_pdf = "videos\\" + str_sessionf + "\\" + str_session + "\\" + str_session + "\\ExtraFiles\\" + str_session + "_Report.pdf";
                        //iframe_pdf.Visible = true;
                        //iframe_pdf.Attributes["src"] = d_pdf;

                        //string str_namefile = @"videos\\" + str_sessionf + "\\" + str_session + "\\" + str_session + "\\" + str_video;

                        LinkButton btn = e.CommandSource as LinkButton;
                        string name_btn = btn.ID;

                        if (name_btn == "lkb_pdf_exp")
                        {
                            iframe_pdf.Visible = true;
                            iframe_pdf.Attributes["src"] = str_pdf;
                            lblModalTitle.Text = "transcript";
                            Label1.Text = "Expediente:" + str_exp + " - Sesión:" + str_session + "";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_pdf", "$('#myModal_pdf').modal();", true);
                            up_pdf.Update();
                        }
                        else
                        {
                            play_video.Visible = true;
                            play_video.Attributes["src"] = str_mp4;
                            lblModalTitle.Text = "transcript";
                            Label2.Text = "Expediente:" + str_exp + " - Sesión:" + str_session + "";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_video", "$('#myModal_video').modal();", true);
                            up_video.Update();
                        }

                        break;

                    case 2:

                        using (var data_mat = new bd_tsEntities())
                        {
                            var items_mat = (from c in data_mat.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 3;

                            data_mat.SaveChanges();
                        }

                        //flist_user(two_user);
                        break;

                    case 3:

                        using (var data_mat = new bd_tsEntities())
                        {
                            var items_mat = (from c in data_mat.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 3;

                            data_mat.SaveChanges();
                        }

                        //flist_user(two_user);
                        break;
                }
            }
            catch
            { }
        }
    }
}