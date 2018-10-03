using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aw_transcript
{
    public partial class usuarios : System.Web.UI.Page
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
                Response.Redirect("ctrl_acceso.aspx");
            }
        }

        private void inf_user()
        {
            guid_fidusuario = (Guid)(Session["ss_id_user"]);
            int save_user = (int)(Session["ss_save_user"]);

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

                switch (save_user)
                {
                    case 2:
                        lbl_reg.Text = "Registro de Administrador";
                        Image1.Attributes["src"] = "/img/iconos/administrador@2x.png";
                        using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                        {
                            var i_fecha_transf = (from c in edm_fecha_transf.inf_usuarios
                                                  where c.id_tipo_usuario == 2
                                                  select c).ToList();

                            if (i_fecha_transf.Count == 0)
                            {
                                lblModalTitle.Text = "transcript";
                                lblModalBody.Text = "No Existen Administradores, favor de agregarlos";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                        }
                        break;

                    case 3:
                        lbl_reg.Text = "Registro de Supervisor";
                        Image1.Attributes["src"] = "/img/iconos/supervisor@2x.png";
                        using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                        {
                            var i_fecha_transf = (from c in edm_fecha_transf.inf_usuarios
                                                  where c.id_tipo_usuario == 3
                                                  select c).ToList();

                            if (i_fecha_transf.Count == 0)
                            {
                                lblModalTitle.Text = "transcript";
                                lblModalBody.Text = "No Existen Supervisores, favor de agregarlos";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                        }
                        break;

                    case 4:
                        lbl_reg.Text = "Registro de Operador";
                        Image1.Attributes["src"] = "/img/iconos/operador@2x.png";
                        using (bd_tsEntities edm_fecha_transf = new bd_tsEntities())
                        {
                            var i_fecha_transf = (from c in edm_fecha_transf.inf_usuarios
                                                  where c.id_tipo_usuario == 4
                                                  select c).ToList();

                            if (i_fecha_transf.Count == 0)
                            {
                                lblModalTitle.Text = "transcript";
                                lblModalBody.Text = "No Existen Operadores, favor de agregarlos";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                        }
                        break;
                }
            }
        }

        protected void cmd_save_Click(object sender, EventArgs e)
        {
            if (rb_add.Checked == false & rb_edit.Checked == false & rb_del.Checked == false)
            {
                Mensaje("Favor de seleccionar una acción.");
            }
            else
            {
                Guid str_iduser = Guid.NewGuid();

                string str_nameuser = txt_name_user.Text.ToUpper();
                string str_apater = txt_apater.Text.ToUpper();
                string str_amater = txt_amater.Text.ToUpper();
                string str_codeuser = txt_code_user.Text.ToLower();
                string str_password = encrypta.Encrypt(txt_password.Text.ToLower());
                Guid f_id_user;

                if (rb_add.Checked)
                {
                    try
                    {
                        string str_filter_code;
                        using (bd_tsEntities data_user = new bd_tsEntities())
                        {
                            var items_user = (from c in data_user.inf_usuarios
                                              where c.codigo_usuario == str_codeuser
                                              select c).FirstOrDefault();

                            str_filter_code = items_user.codigo_usuario.ToString();
                        }

                        if (str_codeuser == str_filter_code)
                        {
                            txt_code_user.Text = "";
                            Mensaje("Usuario ya existe en la base, agregar otro usuario.");
                        }
                    }
                    catch
                    {
                        int save_user = (int)(Session["ss_save_user"]);

                        using (var insert_user = new bd_tsEntities())
                        {
                            var items_user = new inf_usuarios
                            {
                                id_usuario = str_iduser,
                                nombres = str_nameuser,
                                a_paterno = str_apater,
                                a_materno = str_amater,
                                id_tipo_usuario = save_user,
                                codigo_usuario = str_codeuser,
                                clave = str_password,
                                id_estatus = 1,
                                fecha_registro = DateTime.Now,
                                id_tribunal = guid_fidcentro
                            };
                            insert_user.inf_usuarios.Add(items_user);
                            insert_user.SaveChanges();
                        }

                        using (var insert_user = new bd_tsEntities())
                        {
                            var items_user = new inf_usuarios_dep
                            {
                                id_usuario = guid_fidusuario,
                                id_tipo_accion = id_accion(),
                                id_usuario_alt = str_iduser,
                                fecha_registro = DateTime.Now,
                            };
                            insert_user.inf_usuarios_dep.Add(items_user);
                            insert_user.SaveChanges();
                        }

                        clean_data();
                        Mensaje("Datos de usuario guardados con éxito.");
                    }
                }
                else if (rb_edit.Checked)
                {
                    foreach (GridViewRow row in gv_usuarios.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[1].FindControl("chk_select") as CheckBox);
                            if (chkRow.Checked)
                            {
                                string codeuser = row.Cells[1].Text;

                                string str_filter_code;

                                if (codeuser == str_codeuser)
                                {
                                    using (bd_tsEntities data_user = new bd_tsEntities())
                                    {
                                        var items_user = (from c in data_user.inf_usuarios
                                                          where c.codigo_usuario == codeuser
                                                          select c).FirstOrDefault();

                                        f_id_user = items_user.id_usuario;
                                    }

                                    using (var data_user = new bd_tsEntities())
                                    {
                                        var items_user = (from c in data_user.inf_usuarios
                                                          where c.id_usuario == f_id_user
                                                          select c).FirstOrDefault();

                                        items_user.nombres = str_nameuser;
                                        items_user.a_paterno = str_apater;
                                        items_user.a_materno = str_amater;
                                        items_user.codigo_usuario = str_codeuser;
                                        items_user.clave = str_password;

                                        data_user.SaveChanges();
                                    }
                                    using (var insert_user = new bd_tsEntities())
                                    {
                                        var items_user = new inf_usuarios_dep
                                        {
                                            id_usuario = guid_fidusuario,
                                            id_tipo_accion = id_accion(),
                                            id_usuario_alt = str_iduser,
                                            fecha_registro = DateTime.Now,
                                        };
                                        insert_user.inf_usuarios_dep.Add(items_user);
                                        insert_user.SaveChanges();
                                    }
                                    clean_data();

                                    rb_edit.Checked = false;

                                    gv_usuarios.Visible = false;
                                    txt_search.Visible = false;
                                    cmd_search.Visible = false;
                                    Mensaje("Datos de usuario actualizados con éxito.");
                                }
                                else
                                {
                                    using (bd_tsEntities data_user = new bd_tsEntities())
                                    {
                                        var items_user = (from c in data_user.inf_usuarios
                                                          where c.codigo_usuario == str_codeuser
                                                          select c).ToList();

                                        if (items_user.Count == 0)
                                        {
                                            using (bd_tsEntities data_userf = new bd_tsEntities())
                                            {
                                                var items_userf = (from c in data_userf.inf_usuarios
                                                                   where c.codigo_usuario == codeuser
                                                                   select c).FirstOrDefault();

                                                f_id_user = items_userf.id_usuario;
                                            }

                                            using (var data_userb = new bd_tsEntities())
                                            {
                                                var items_userb = (from c in data_userb.inf_usuarios
                                                                   where c.id_usuario == f_id_user
                                                                   select c).FirstOrDefault();

                                                items_userb.nombres = str_nameuser;
                                                items_userb.a_paterno = str_apater;
                                                items_userb.a_materno = str_amater;
                                                items_userb.codigo_usuario = str_codeuser;
                                                items_userb.clave = str_password;

                                                data_userb.SaveChanges();
                                            }
                                            using (var insert_user = new bd_tsEntities())
                                            {
                                                var items_userb = new inf_usuarios_dep
                                                {
                                                    id_usuario = guid_fidusuario,
                                                    id_tipo_accion = id_accion(),
                                                    id_usuario_alt = str_iduser,
                                                    fecha_registro = DateTime.Now,
                                                };
                                                insert_user.inf_usuarios_dep.Add(items_userb);
                                                insert_user.SaveChanges();
                                            }
                                            clean_data();

                                            rb_edit.Checked = false;

                                            gv_usuarios.Visible = false;
                                            txt_search.Visible = false;
                                            cmd_search.Visible = false;

                                            Mensaje("Datos de usuario actualizados con éxito.");
                                        }
                                        else
                                        {
                                            str_filter_code = items_user[0].codigo_usuario.ToString();

                                            if (str_codeuser == str_filter_code)
                                            {
                                                txt_code_user.Text = "";
                                                Mensaje("Usuario ya existe en la base, agregar otro usuario.");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (rb_del.Checked)
                {
                    foreach (GridViewRow row in gv_usuarios.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                            if (chkRow.Checked)
                            {
                                string codeuser = row.Cells[1].Text;

                                using (bd_tsEntities data_user = new bd_tsEntities())
                                {
                                    var items_user = (from c in data_user.inf_usuarios
                                                      where c.codigo_usuario == codeuser
                                                      select c).FirstOrDefault();

                                    f_id_user = items_user.id_usuario;
                                }

                                using (var data_user = new bd_tsEntities())
                                {
                                    var items_user = (from c in data_user.inf_usuarios
                                                      where c.id_usuario == f_id_user
                                                      select c).FirstOrDefault();

                                    data_user.inf_usuarios.Remove(items_user);
                                    data_user.SaveChanges();
                                }
                                using (var insert_user = new bd_tsEntities())
                                {
                                    var items_user = new inf_usuarios_dep
                                    {
                                        id_usuario = guid_fidusuario,
                                        id_tipo_accion = id_accion(),
                                        id_usuario_alt = str_iduser,
                                        fecha_registro = DateTime.Now,
                                    };
                                    insert_user.inf_usuarios_dep.Add(items_user);
                                    insert_user.SaveChanges();
                                }
                                clean_data();

                                rb_del.Checked = false;

                                gv_usuarios.Visible = false;
                                txt_search.Visible = false;
                                cmd_search.Visible = false;

                                Mensaje("Datos de usuario eliminados con éxito.");
                            }
                        }
                    }
                }
            }
        }

        public int id_accion()

        {
            if (rb_add.Checked)
            {
                return 1;
            }
            else if (rb_edit.Checked)
            {
                return 2;
            }
            else if (rb_del.Checked)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        private void flist_user(int?[] id_flist_user)
        {
            if (lbl_idprofileuser.Text == "2")
            {
                using (bd_tsEntities data_user = new bd_tsEntities())
                {
                    var inf_user = (from u in data_user.inf_usuarios
                                    join e in data_user.fact_estatus on u.id_estatus equals e.id_estatus
                                    where id_flist_user.Contains(u.id_tipo_usuario)
                                    where u.id_usuario != guid_fidusuario
                                    where u.id_estatus == 1

                                    select new
                                    {
                                        u.codigo_usuario,
                                        e.desc_estatus,
                                        u.fecha_registro,
                                        u.nombres,
                                        u.a_paterno,
                                        u.a_materno
                                    }).ToList();

                    gv_usuarios.DataSource = inf_user;
                    gv_usuarios.DataBind();
                    gv_usuarios.Visible = true;
                }
            }
            else
            {
                using (bd_tsEntities data_user = new bd_tsEntities())
                {
                    var inf_user = (from u in data_user.inf_usuarios
                                    join e in data_user.fact_estatus on u.id_estatus equals e.id_estatus
                                    where id_flist_user.Contains(u.id_tipo_usuario)
                                    where u.id_usuario != guid_fidusuario
                                    where u.id_estatus == 1
                                    select new
                                    {
                                        u.codigo_usuario,
                                        e.desc_estatus,
                                        u.fecha_registro,
                                        u.nombres,
                                        u.a_paterno,
                                        u.a_materno
                                    }).ToList();

                    gv_usuarios.DataSource = inf_user;
                    gv_usuarios.DataBind();
                    gv_usuarios.Visible = true;
                }
            }

            if (rb_edit.Checked)
            {
            }
        }

        protected void rb_add_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit.Checked = false;
            rb_del.Checked = false;
            gv_usuarios.Visible = false;
            txt_search.Visible = false;
            cmd_search.Visible = false;
        }

        protected void rb_edit_CheckedChanged(object sender, EventArgs e)
        {
            rb_add.Checked = false;
            rb_del.Checked = false;
            txt_search.Visible = false;
            cmd_search.Visible = false;
            int? save_user = (int)(Session["ss_save_user"]);
            var two_user = new int?[] { save_user };
            flist_user(two_user);
        }

        protected void rb_drop_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit.Checked = false;
            rb_add.Checked = false;
            rb_del.Checked = false;
            txt_search.Visible = false;
            cmd_search.Visible = false;
            int? save_user = (int)(Session["ss_save_user"]);
            var two_user = new int?[] { save_user };
            flist_user(two_user);
        }

        protected void rb_del_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit.Checked = false;
            rb_add.Checked = false;
            txt_search.Visible = false;
            cmd_search.Visible = false;
            clean_data();
            int? save_user = (int)(Session["ss_save_user"]);
            var two_user = new int?[] { save_user };
            flist_user(two_user);
        }

        private void clean_data()
        {
            txt_search.Text = "";
            txt_name_user.Text = "";
            txt_apater.Text = "";
            txt_amater.Text = "";
            txt_code_user.Text = "";
            txt_password.Text = "";
        }

        protected void chk_OnCheckedChanged(object sender, EventArgs e)
        {
            Guid guid_fidusuario;

            foreach (GridViewRow row in gv_usuarios.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.YellowGreen;
                        string codeuser = row.Cells[1].Text;

                        using (bd_tsEntities data_user = new bd_tsEntities())
                        {
                            var items_user = (from c in data_user.inf_usuarios
                                              where c.codigo_usuario == codeuser
                                              select c).FirstOrDefault();

                            guid_fidusuario = items_user.id_usuario;
                        }
                        int? save_user = (int)(Session["ss_save_user"]);

                        if (save_user == 2)
                        {
                            using (bd_tsEntities data_user = new bd_tsEntities())
                            {
                                var inf_user = (from u in data_user.inf_usuarios
                                                where u.id_usuario == guid_fidusuario
                                                select new
                                                {
                                                    u.nombres,
                                                    u.a_paterno,
                                                    u.a_materno,
                                                    u.codigo_usuario,
                                                    u.clave
                                                }).FirstOrDefault();

                                txt_name_user.Text = inf_user.nombres;
                                txt_apater.Text = inf_user.a_paterno;
                                txt_amater.Text = inf_user.a_materno;
                                txt_code_user.Text = inf_user.codigo_usuario;
                                txt_password.Text = inf_user.clave;
                            }
                        }
                        else
                        {
                            using (bd_tsEntities data_user = new bd_tsEntities())
                            {
                                var inf_user = (from u in data_user.inf_usuarios
                                                where u.id_usuario == guid_fidusuario
                                                select new
                                                {
                                                    u.nombres,
                                                    u.a_paterno,
                                                    u.a_materno,
                                                    u.codigo_usuario,
                                                    u.clave
                                                }).FirstOrDefault();

                                txt_name_user.Text = inf_user.nombres;
                                txt_apater.Text = inf_user.a_paterno;
                                txt_amater.Text = inf_user.a_materno;
                                txt_code_user.Text = inf_user.codigo_usuario;
                                txt_password.Text = inf_user.clave;
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

        protected void cmd_search_Click(object sender, EventArgs e)
        {
            string str_userb = txt_search.Text;
            int? save_user = (int)(Session["ss_save_user"]);
            var two_user = new int?[] { save_user };

            using (bd_tsEntities data_user = new bd_tsEntities())
            {
                var inf_user = (from u in data_user.inf_usuarios
                                join est in data_user.fact_estatus on u.id_estatus equals est.id_estatus
                                where u.nombres.Contains(str_userb)
                                where two_user.Contains(u.id_tipo_usuario)
                                where u.id_usuario != guid_fidusuario
                                where u.id_estatus == 1

                                select new
                                {
                                    u.codigo_usuario,
                                    est.desc_estatus,
                                    u.fecha_registro,
                                    u.nombres,
                                    u.a_paterno,
                                    u.a_materno
                                }).ToList();

                if (inf_user.Count == 0)
                {
                    gv_usuarios.DataSource = inf_user;
                    gv_usuarios.DataBind();
                    gv_usuarios.Visible = true;

                    lblModalTitle.Text = "transcript";
                    lblModalBody.Text = "Usuario no exite o tiene un perfil diferente";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    gv_usuarios.DataSource = inf_user;
                    gv_usuarios.DataBind();
                    gv_usuarios.Visible = true;
                }
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