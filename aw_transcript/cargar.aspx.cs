using AjaxControlToolkit;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace aw_transcript
{
    public partial class cargar : System.Web.UI.Page
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
                Response.Redirect("ctrl_acceso.aspx");
            }
        }

        protected void cmd_save_Click(object sender, EventArgs e)
        {
            string ftpServer = "ftp://www.puntocero.biz/";
            string ftpusername = "ftp_puntocero";
            string ftppassword = "Fv9dq2&2";
            string fileurl = "C:/Users/Punto Cero/Videos/Patience.mp4";

            try
            {
                FtpWebRequest ftpClient = (FtpWebRequest)FtpWebRequest.Create(ftpServer + "demo.mp4");
                ftpClient.Credentials = new NetworkCredential(ftpusername, ftppassword);
                ftpClient.Method = WebRequestMethods.Ftp.UploadFile;
                ftpClient.UseBinary = true;
                ftpClient.KeepAlive = true;
                FileInfo fi = new FileInfo(fileurl);
                ftpClient.ContentLength = fi.Length;
                byte[] buffer = new byte[1024 * 1024];
                int bytes = 0;
                int total_bytes = (int)fi.Length;
                FileStream fs = fi.OpenRead();
                Stream rs = ftpClient.GetRequestStream();
                while (total_bytes > 0)
                {
                    bytes = fs.Read(buffer, 0, buffer.Length);
                    rs.Write(buffer, 0, bytes);
                    total_bytes = total_bytes - bytes;
                }
                fs.Flush();
                fs.Close();
                rs.Close();
                FtpWebResponse uploadResponse = (FtpWebResponse)ftpClient.GetResponse();
                string value = uploadResponse.StatusDescription;
                uploadResponse.Close();
            }
            catch (Exception ex)
            {
                lbl_mnsj.Text = ex.ToString();
            }
            lbl_mnsj.Text = "Carga de archivo con exito";
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

                        break;

                    case 4:

                        break;
                }
            }
        }

        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxFileUploadEventArgs e)
        {
            string str_filename, str_field;
            string path = Server.MapPath("~/videos/") + e.FileName;
            str_filename = e.FileName;
            string str_file_save = str_filename.Replace(".wmv", ".mp4");
            string str_filesize = e.FileSize.ToString();
            Int32 str_filesizemb = Convert.ToInt32(str_filesize) / 1024;
            str_field = e.FileId;

            //using (var insert_material = new bd_tsEntities())
            //{
            //    var items_user = new inf_material
            //    {
            //        expediente = "00001",
            //        sesion = "001",
            //        archivo = str_filename,
            //        bits = Convert.ToInt32(str_filesizemb),
            //        fecha_registro = DateTime.Now,
            //        id_estatus_material = 1,
            //        id_usuario = id_user,

            //    };
            //    insert_material.inf_material.Add(items_user);
            //    insert_material.SaveChanges();
            //}

            AjaxFileUpload1.SaveAs(path);

            //ReturnVideo(path);
            //var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            //ffMpeg.ConvertMedia(path, "videos/" + str_file_save, Format.mp4);
            lbl_mnsj.Visible = true;
            lbl_mnsj.Text = "Videa a mp4 con Exito";
            //Session["ss_id_user"] = id_user;
        }

        protected void AjaxFileUpload1_UploadCompleteAll(object sender, AjaxFileUploadCompleteAllEventArgs e)
        {
        }

        protected void AjaxFileUpload1_UploadStart(object sender, AjaxFileUploadStartEventArgs e)
        {
        }
    }
}