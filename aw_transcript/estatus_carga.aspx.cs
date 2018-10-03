using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class estatus_carga : System.Web.UI.Page
    {
        private static string str_session, str_video;
        private static Guid guid_fidusuario, guid_fidcentro;
        private static int s_gn;

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
                Response.Redirect("ctrl_acceso.aspx");
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
                }
            }
        }

        protected void chk_OnCheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_files.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);

                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void cmd_search_Click(object sender, EventArgs e)
        {
            //play_video.Visible = false;
            //div_panel.Visible = false;
            //UpdatePanel2.Update();

            using (var edm_materialf = new bd_tsEntities())
            {
                var i_materialf = (from c in edm_materialf.inf_material
                                   select c).ToList();

                if (i_materialf.Count == 0)
                {
                    gv_files.Visible = false;
                    lblModalTitle.Text = "transcript";
                    lblModalBody.Text = "Sin videos por Cargar, favor de reintentar o contactar con el administrador";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    iframe_pdf.Visible = false;
                    play_video.Visible = false;

                    upModal.Update();
                }
                else
                {
                    using (var edm_material = new bd_tsEntities())
                    {
                        var i_material = (from c in edm_material.inf_material
                                          select c).ToList();

                        if (i_material.Count == 0)
                        {
                            lblModalTitle.Text = "transcript";
                            lblModalBody.Text = "Sin videos por Cargar";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        }
                        else
                        {
                            using (var edm_materialff = new bd_tsEntities())
                            {
                                var i_materialff = (from c in edm_materialff.inf_material
                                                    where c.id_estatus_material == 6
                                                    select c).ToList();

                                if (i_materialff.Count == 0)
                                {
                                    var two_userff = new int?[] { 1, 3, 4, 5, 8, 9 };
                                    flist_user(two_userff);

                                    iframe_pdf.Visible = false;
                                    play_video.Visible = false;
                                    upModal.Update();
                                }
                                else
                                {
                                    lblModalTitle.Text = "transcript";
                                    lblModalBody.Text = "Se estan CARGANDO videos, favor de esperar";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);

                                    var two_userff = new int?[] { 1, 3, 4, 5, 8, 9 };
                                    flist_user(two_userff);
                                    iframe_pdf.Visible = false;
                                    play_video.Visible = false;

                                    upModal.Update();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void flist_user(int?[] str_idload)
        {
            CultureInfo en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;
            string str_dateini = txt_dateini.Text;
            string str_datefin = txt_datefin.Text;

            DateTime str_fdateini = DateTime.Parse(str_dateini);
            DateTime str_fdatefin = DateTime.Parse(str_datefin);

            if (lbl_idprofileuser.Text == "4")
            {
                using (bd_tsEntities edm_material = new bd_tsEntities())
                {
                    var i_material = (from inf_m in edm_material.inf_material
                                      join inf_em in edm_material.fact_estatus_material on inf_m.id_estatus_material equals inf_em.id_estatus_material
                                      where str_idload.Contains(inf_m.id_estatus_material)
                                      where inf_m.fecha_registro >= str_fdateini && inf_m.fecha_registro <= str_fdatefin
                                      select new
                                      {
                                          inf_m.sesion,
                                          inf_m.titulo,
                                          inf_m.localizacion,
                                          inf_m.tipo,
                                          inf_m.archivo,
                                          inf_m.duracion,
                                          inf_m.fecha_registro,
                                          inf_em.desc_estatus_material,
                                          inf_m.id_material
                                      }).ToList();

                    gv_files.DataSource = i_material;
                    gv_files.DataBind();
                    gv_files.Visible = true;
                }
            }
            else
            {
                using (bd_tsEntities edm_material = new bd_tsEntities())
                {
                    var i_material = (from inf_m in edm_material.inf_material
                                      join inf_em in edm_material.fact_estatus_material on inf_m.id_estatus_material equals inf_em.id_estatus_material
                                      join inf_rv in edm_material.inf_ruta_videos on inf_m.id_ruta_videos equals inf_rv.id_ruta_videos
                                      join inf_s in edm_material.inf_salas on inf_rv.id_sala equals inf_s.id_sala
                                      join inf_j in edm_material.inf_juzgados on inf_s.id_juzgado equals inf_j.id_juzgado
                                      where str_idload.Contains(inf_m.id_estatus_material)
                                      where inf_m.fecha_registro >= str_fdateini && inf_m.fecha_registro <= str_fdatefin
                                      select new
                                      {
                                          inf_j.localidad,
                                          inf_j.numero,
                                          inf_m.sesion,
                                          inf_m.titulo,
                                          inf_m.localizacion,
                                          inf_m.tipo,
                                          inf_m.archivo,
                                          inf_m.duracion,
                                          inf_m.fecha_registro,
                                          inf_em.desc_estatus_material,
                                          inf_m.id_material
                                      }).ToList();

                    gv_files.DataSource = i_material;
                    gv_files.DataBind();
                    gv_files.Visible = true;
                }
            }
        }

        protected void gv_files_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                string status = gvr.Cells[11].Text.ToString().Trim();

                str_session = gvr.Cells[4].Text;
                str_video = gvr.Cells[8].Text + ".mp4";
                var two_user = new int?[] { 1, 3, 4, 5, 8, 9 };

                switch (status)
                {
                    case "ACTIVO":

                        using (var edm_material = new bd_tsEntities())
                        {
                            var i_material = new inf_material_dep
                            {
                                sesion = str_session,
                                video = str_video,
                                id_usuario = guid_fidusuario,
                                id_material = 0,
                                fecha_registro = DateTime.Now,
                                fecha_registro_alt = DateTime.Now
                            };

                            edm_material.inf_material_dep.Add(i_material);
                            edm_material.SaveChanges();
                        }

                        string d_pdf = "videos\\" + str_session + "\\" + str_session + "\\ExtraFiles\\" + str_session + "_Report.pdf";
                        iframe_pdf.Visible = true;
                        iframe_pdf.Attributes["src"] = d_pdf;

                        string str_namefile = @"videos\\" + str_session + "\\" + str_session + "\\" + str_video;

                        play_video.Visible = true;
                        play_video.Attributes["src"] = str_namefile;
                        Button btn = e.CommandSource as Button;
                        string name_btn = btn.Text;

                        if (name_btn == "PDF")
                        {
                            lblModalTitle.Text = "transcript";
                            Label1.Text = "Expediente:" + str_session + " - Archivo:" + str_video.Replace(".mp4", "");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_pdf", "$('#myModal_pdf').modal();", true);
                            up_pdf.Update();
                        }
                        else

                        {
                            lblModalTitle.Text = "transcript";
                            Label2.Text = "Expediente:" + str_session + " - Archivo:" + str_video.Replace(".mp4", "");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_video", "$('#myModal_video').modal();", true);
                            up_video.Update();
                        }

                        break;

                    case "ERROR":

                        using (var data_mat = new bd_tsEntities())
                        {
                            var items_mat = (from c in data_mat.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 3;

                            data_mat.SaveChanges();
                        }

                        flist_user(two_user);
                        break;

                    case "NUEVO":

                        using (var data_mat = new bd_tsEntities())
                        {
                            var items_mat = (from c in data_mat.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 3;

                            data_mat.SaveChanges();
                        }

                        flist_user(two_user);
                        break;
                }
            }
            catch
            { }
        }

        protected void gv_files_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int customerId = int.Parse(gv_files.DataKeys[e.Row.RowIndex].Value.ToString());
                var gv_material_ext = (GridView)e.Row.FindControl("gv_material_ext");
                using (bd_tsEntities edm_material = new bd_tsEntities())
                {
                    var i_material = (from inf_m in edm_material.inf_material_ext
                                      join inf_em in edm_material.fact_estatus_material on inf_m.id_estatus_material equals inf_em.id_estatus_material
                                      join inf_rv in edm_material.inf_ruta_videos on inf_m.id_ruta_videos equals inf_rv.id_ruta_videos
                                      join inf_s in edm_material.inf_salas on inf_rv.id_sala equals inf_s.id_sala
                                      join inf_j in edm_material.inf_juzgados on inf_s.id_juzgado equals inf_j.id_juzgado
                                      where inf_m.id_material == customerId
                                      select new
                                      {
                                          inf_m.sesion,
                                          inf_m.archivo,
                                          inf_m.duracion,
                                          inf_m.fecha_registro,
                                          inf_em.desc_estatus_material,
                                          inf_m.id_material,
                                          inf_m.id_material_ext
                                      }).ToList();
                    gv_material_ext.DataSource = i_material;
                    gv_material_ext.DataBind();
                    gv_material_ext.Visible = true;

                    //if (gv_material_ext.Rows.Count == 0)
                    //{
                    //}
                    //else
                    //{
                    //    Button btnButton_ext = (Button)gv_material_ext.FindControl("cmd_action_ext");
                    //    string _EstatusExt = gv_material_ext.Rows[1].Cells[4].Text;
                    //    if (_EstatusExt == "ERROR")
                    //    {
                    //        //btnButton_ext.Text = "CARGAR";
                    //        //btnButton_ext.Enabled = true;
                    //    }

                    //    else if (_EstatusExt == "ACTIVO")
                    //    {
                    //        //btnButton_ext.Text = "VER";
                    //        //btnButton_ext.Enabled = true;
                    //    }
                    //    else if (_EstatusExt == "CARGANDO")
                    //    {
                    //        //btnButton_ext.Text = "ESPERAR";
                    //        //btnButton_ext.Enabled = false;
                    //    }
                    //    else if (_EstatusExt == "CARGAR")
                    //    {
                    //        //btnButton_ext.Text = "VALIDANDO";
                    //        //btnButton_ext.Enabled = false;
                    //    }
                    //    else if (_EstatusExt == "INACTIVO")
                    //    {
                    //        //btnButton_ext.Text = "";
                    //        //btnButton_ext.Enabled = false;
                    //    }

                    //}
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnButton = (Button)e.Row.FindControl("btn_pdf");
                Button btnButton_v = (Button)e.Row.FindControl("btn_video");
                if (e.Row.Cells[11].Text == "ERROR")
                {
                    btnButton.Text = "CARGAR";
                    btnButton.Enabled = true;
                }
                else if (e.Row.Cells[11].Text == "ACTIVO")
                {
                    btnButton.Text = "PDF";
                    btnButton.Enabled = true;
                    btnButton_v.Text = "VER";
                    btnButton_v.Enabled = true;
                }
                else if (e.Row.Cells[11].Text == "CARGANDO")
                {
                    btnButton.Text = "ESPERAR";
                    btnButton.Enabled = false;
                }
                else if (e.Row.Cells[11].Text == "CARGAR")
                {
                    btnButton.Text = "VALIDANDO";
                    btnButton.Enabled = false;
                }
                else if (e.Row.Cells[11].Text == "INACTIVO")
                {
                    btnButton.Text = "";
                    btnButton.Enabled = false;
                }
                else if (e.Row.Cells[11].Text == "NUEVO")
                {
                    btnButton.Text = "CARGAR";
                    btnButton.Enabled = true;
                }
            }
        }

        public String MyNewRow(object idmaterial)
        {
            /*
                * 1. Close current cell in our example phone </TD>
                * 2. Close Current Row </TR>
                * 3. Cretae new Row with ID and class <TR id='...' style='...'>
                * 4. Create blank cell <TD></TD>
                * 5. Create new cell to contain the grid <TD>
                * 6. Finall grid will close its own row
                ************************************************************/
            return String.Format(@"</td></tr><tr id ='tr{0}' style='collapsed-row'>
               <td></td><td colspan='100' style='padding:0px; margin:0px;'>", idmaterial);
        }

        protected void rb_internos_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void rb_externos_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void gv_material_ext_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                string status = gvr.Cells[5].Text.ToString().Trim();
                int id_sessionf = int.Parse(gvr.Cells[0].Text);
                int id_session;
                string str_sessionf;
                using (var data_mat = new bd_tsEntities())
                {
                    var items_mat = (from c in data_mat.inf_material_ext
                                     where c.id_material_ext == id_sessionf
                                     select c).FirstOrDefault();

                    id_session = items_mat.id_material;
                }
                using (var data_mat = new bd_tsEntities())
                {
                    var items_mat = (from c in data_mat.inf_material
                                     where c.id_material == id_session
                                     select c).FirstOrDefault();

                    str_sessionf = items_mat.sesion;
                }

                str_session = gvr.Cells[1].Text;
                str_video = gvr.Cells[2].Text + ".mp4";
                var two_user = new int?[] { 1, 3, 4, 5, 8, 9 };

                switch (status)
                {
                    case "ACTIVO":

                        using (var edm_material = new bd_tsEntities())
                        {
                            var i_material = new inf_material_dep
                            {
                                sesion = str_session,
                                video = str_video,
                                id_usuario = guid_fidusuario,
                                id_material = 0,
                                fecha_registro = DateTime.Now,
                                fecha_registro_alt = DateTime.Now
                            };

                            edm_material.inf_material_dep.Add(i_material);
                            edm_material.SaveChanges();
                        }

                        string d_pdf = "videos\\" + str_sessionf + "\\" + str_session + "\\" + str_session + "\\ExtraFiles\\" + str_session + "_Report.pdf";
                        iframe_pdf.Visible = true;
                        iframe_pdf.Attributes["src"] = d_pdf;

                        string str_namefile = @"videos\\" + str_sessionf + "\\" + str_session + "\\" + str_session + "\\" + str_video;

                        play_video.Visible = true;
                        play_video.Attributes["src"] = str_namefile;
                        Button btn = e.CommandSource as Button;
                        string name_btn = btn.Text;

                        if (name_btn == "PDF")
                        {
                            lblModalTitle.Text = "transcript";
                            Label1.Text = "Expediente:" + str_sessionf + " - Sesión:" + str_session + " - Archivo:" + str_video.Replace(".mp4", "");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_pdf", "$('#myModal_pdf').modal();", true);
                            up_pdf.Update();
                        }
                        else

                        {
                            lblModalTitle.Text = "transcript";
                            Label2.Text = "Expediente:" + str_sessionf + " - Sesión:" + str_session + " - Archivo:" + str_video.Replace(".mp4", "");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_video", "$('#myModal_video').modal();", true);
                            up_video.Update();
                        }

                        break;

                    case "ERROR":

                        using (var data_mat = new bd_tsEntities())
                        {
                            var items_mat = (from c in data_mat.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 3;

                            data_mat.SaveChanges();
                        }

                        flist_user(two_user);
                        break;

                    case "NUEVO":

                        using (var data_mat = new bd_tsEntities())
                        {
                            var items_mat = (from c in data_mat.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 3;

                            data_mat.SaveChanges();
                        }

                        flist_user(two_user);
                        break;
                }
            }
            catch
            { }
        }

        protected void gv_material_ext_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnButton = (Button)e.Row.FindControl("btn_pdf_ext");
                Button btnButton_v = (Button)e.Row.FindControl("btn_video_ext");
                if (e.Row.Cells[5].Text == "ERROR")
                {
                    btnButton.Text = "CARGAR";
                    btnButton.Enabled = true;
                }
                else if (e.Row.Cells[5].Text == "ACTIVO")
                {
                    btnButton.Text = "PDF";
                    btnButton.Enabled = true;
                    btnButton_v.Text = "VER";
                    btnButton_v.Enabled = true;
                }
                else if (e.Row.Cells[5].Text == "CARGANDO")
                {
                    btnButton.Text = "ESPERAR";
                    btnButton.Enabled = false;
                }
                else if (e.Row.Cells[5].Text == "CARGAR")
                {
                    btnButton.Text = "VALIDANDO";
                    btnButton.Enabled = false;
                }
                else if (e.Row.Cells[5].Text == "INACTIVO")
                {
                    btnButton.Text = "";
                    btnButton.Enabled = false;
                }
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            //play_video.Visible = false;
            //div_panel.Visible = false;
            //UpdatePanel2.Update();
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

            var two_user = new int?[] { 1, 3, 4, 5, 8, 9 };
            string status = row.Cells[5].Text.ToString().Trim();

            int id_m_ext = int.Parse(row.Cells[0].Text);
            int id_m;
            switch (status)
            {
                case "XML":

                    break;

                case "ACTIVO":

                    string str_session_p;
                    str_session = row.Cells[1].Text;
                    str_video = row.Cells[2].Text + ".mp4";

                    //using (var data_mat = new bd_tsEntities())
                    //{
                    //    var items_mat = (from c in data_mat.inf_material
                    //                     where c.sesion == str_session
                    //                     select c).FirstOrDefault();
                    //    str_session_p
                    //}

                    using (var edm_material = new bd_tsEntities())
                    {
                        var i_material = new inf_material_dep
                        {
                            sesion = str_session,
                            video = str_video,
                            id_usuario = guid_fidusuario,
                            id_material = 0,
                            fecha_registro = DateTime.Now,
                            fecha_registro_alt = DateTime.Now
                        };

                        edm_material.inf_material_dep.Add(i_material);
                        edm_material.SaveChanges();
                    }

                    using (var data_mat = new bd_tsEntities())
                    {
                        var items_mat = (from c in data_mat.inf_material_ext
                                         where c.id_material_ext == id_m_ext
                                         select c).FirstOrDefault();

                        id_m = items_mat.id_material;
                    }

                    using (var data_mat = new bd_tsEntities())
                    {
                        var items_mat = (from c in data_mat.inf_material
                                         where c.id_material == id_m
                                         select c).FirstOrDefault();

                        string sessionf = items_mat.sesion;

                        string d_pdf = "videos\\" + sessionf + "\\" + str_session + "\\" + str_session + "\\ExtraFiles\\" + str_session + "_Report.pdf";
                        iframe_pdf.Visible = true;
                        iframe_pdf.Attributes["src"] = d_pdf;

                        string str_namefile = @"videos\\" + sessionf + "\\" + str_session + "\\" + str_session + "\\" + str_video;

                        play_video.Attributes["src"] = str_namefile;
                    }

                    s_gn = 1;

                    break;

                case "ERROR":
                    using (var data_mat = new bd_tsEntities())
                    {
                        var items_mat = (from c in data_mat.inf_material
                                         where c.sesion == str_session
                                         select c).FirstOrDefault();

                        items_mat.id_estatus_material = 3;

                        data_mat.SaveChanges();
                    }

                    break;

                case "NUEVO":

                    using (var data_mat = new bd_tsEntities())
                    {
                        var items_mat = (from c in data_mat.inf_material
                                         where c.sesion == str_session
                                         select c).FirstOrDefault();

                        items_mat.id_estatus_material = 3;

                        data_mat.SaveChanges();
                    }

                    flist_user(two_user);
                    break;
            }
            //play_video.Visible = true;
            //div_panel.Visible = true;
            //UpdatePanel2.Update();
            //Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btn_csv_Click(object sender, EventArgs e)
        {
            string constr = conn_svr.strconn_sql;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT inf_log_ruta.id_log_ruta, inf_material.sesion as expediente, inf_log_ruta.desc_error, inf_log_ruta.id_control, inf_log_ruta.fecha_registro FROM inf_log_ruta INNER JOIN inf_ruta_videos ON inf_log_ruta.id_ruta_videos = inf_ruta_videos.id_ruta_videos INNER JOIN inf_material ON inf_log_ruta.id_control = inf_material.id_control"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            foreach (DataColumn column in dt.Columns)
                            {
                                //Add the Header row for CSV file.
                                csv += column.ColumnName + ',';
                            }

                            //Add new line.
                            csv += "\r\n";

                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }

                                //Add new line.
                                csv += "\r\n";
                            }

                            //Download the CSV file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=transcript_err.csv");
                            Response.Charset = "";
                            Response.ContentType = "application/text";
                            Response.Output.Write(csv);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }
    }
}