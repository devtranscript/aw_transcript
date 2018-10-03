using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class seguimiento : System.Web.UI.Page
    {
        private static string str_session, str_video;
        private static Guid guid_fidusuario, guid_fidcentro;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //inf_user();
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
                if (e.Row.Cells[9].Text == "ERROR")
                {
                    btnButton.Text = "CARGAR";
                    btnButton.Enabled = true;
                }
                else if (e.Row.Cells[9].Text == "ACTIVO")
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
                else if (e.Row.Cells[9].Text == "CARGAR")
                {
                    btnButton.Text = "VALIDANDO";
                    btnButton.Enabled = false;
                }
                else if (e.Row.Cells[9].Text == "INACTIVO")
                {
                    btnButton.Text = "";
                    btnButton.Enabled = false;
                }
                else if (e.Row.Cells[9].Text == "NUEVO")
                {
                    btnButton.Text = "CARGAR";
                    btnButton.Enabled = true;
                }
            }
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

        protected void chk_OnCheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_files.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        div_panel.Visible = false;
                        UpdatePanel2.Update();
                        row.BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void gv_files_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                string status = gvr.Cells[9].Text.ToString().Trim();

                str_session = gvr.Cells[2].Text;
                str_video = gvr.Cells[6].Text + ".mp4";
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

        private Control CrearControlVideo(string str_namefile)
        {
            StringBuilder sa = new StringBuilder();
            // sa.Append("<center>");
            sa.Append("<OBJECT ID=\"Player\" Object Type=\"video/x-ms-wmv\" width=\"640\" height=\"480\" VIEWASTEXT > ");
            sa.Append("<PARAM name=\"autoStart\" value=\"false\">");
            sa.Append(string.Format("<PARAM name=\"SRC\" value=\"{0}\">", str_namefile));// IE needs this extra push when using MIME type not class id
            sa.Append(string.Format("<PARAM name=\"URL\" value=\"{0}\">", str_namefile));
            sa.Append("<PARAM name=\"AutoSize\" value=\"False\"");
            sa.Append("<PARAM name=\"rate\" value=\"1\">");
            sa.Append("<PARAM name=\"balance\" value=\"0\">");
            sa.Append("<PARAM name=\"enabled\" value=\"true\">");
            sa.Append("<PARAM name=\"enabledContextMenu\" value=\"true\">");
            sa.Append("<PARAM name=\"fullScreen\" value=\"false\">");
            sa.Append("<PARAM name=\"playCount\" value=\"1\">");
            sa.Append("<PARAM name=\"volume\" value=\"30\">  ");
            sa.Append("</OBJECT>");
            //  sa.Append("</center>");

            return new LiteralControl(sa.ToString());
        }

        protected void img_pdf_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow row in gv_files.Rows)
            {
                CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);
                if (chkRow.Checked)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        if (row.Cells[8].Text == "INACTIVO")
                        {
                        }
                        else
                        {
                            string str_session = row.Cells[1].Text;
                            string str_video = row.Cells[5].Text;

                            string d_pdf = "videos\\" + str_session + "\\ExtraFiles\\" + str_session + "_Report.pdf";
                            iframe_pdf.Visible = true;
                            iframe_pdf.Attributes["src"] = d_pdf;
                            UpdatePanel2.Update();
                        }
                    }
                }
            }
        }

        protected void cmd_search_Click(object sender, EventArgs e)
        {
            var two_user = new int?[] { 1, 4 };
            flist_user(two_user);
        }

        private void flist_user(int?[] str_idload)
        {
            string str_dateini = txt_dateini.Text;
            string str_datefin = txt_datefin.Text;
            string sessionf = txt_expedient.Text;
            DateTime str_fdateini = DateTime.Parse(str_dateini);
            DateTime str_fdatefin = DateTime.Parse(str_datefin);

            using (bd_tsEntities edm_material = new bd_tsEntities())
            {
                var i_material = (from inf_m in edm_material.inf_material
                                  join inf_em in edm_material.fact_estatus_material on inf_m.id_estatus_material equals inf_em.id_estatus_material
                                  join inf_rv in edm_material.inf_ruta_videos on inf_m.id_ruta_videos equals inf_rv.id_ruta_videos
                                  join inf_s in edm_material.inf_salas on inf_rv.id_sala equals inf_s.id_sala
                                  join inf_j in edm_material.inf_juzgados on inf_s.id_juzgado equals inf_j.id_juzgado
                                  where str_idload.Contains(inf_m.id_estatus_material)
                                  where inf_m.fecha_registro >= str_fdateini && inf_m.fecha_registro <= str_fdatefin
                                  where inf_m.sesion == sessionf
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
}