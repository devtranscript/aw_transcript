<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="juzgados_salas.aspx.cs" Inherits="aw_transcript.juzgados_salas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="section">
                <div class="container">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-1">
                                <a href="menu_tribunal.aspx">
                                    <img alt="" src="img/ico_back.png" /></a>
                            </div>
                            <div class="col-md-1">
                                <a href="acceso.aspx">
                                    <img alt="" src="img/ico_exit.png" /></a>
                            </div>
                            <br />
                            <div class="col-md-10">
                                <p class="text-right">
                                    <asp:Label ID="lbl_welcome" runat="server" Text="Bienvenid@: "></asp:Label>
                                    <asp:Label ID="lbl_fuser" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_idfuser" runat="server" Text="" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_profile" runat="server" Text="Perfil: "></asp:Label>
                                    <asp:Label ID="lbl_profileuser" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_idprofileuser" runat="server" Text="" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_center" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_centername" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_idcenter" runat="server" Text="" Visible="false"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <h2 class="text-center">
                                    <asp:Label ID="lbl_reg" runat="server" Text=""></asp:Label></h2>
                            </div>
                            <div class="col-md-12 text-right">
                                <h3 class="text-center">Control de Juzgados</h3>
                                <br />
                                <asp:RadioButton CssClass="radio-inline" ID="rb_agregar_juzgado" runat="server" Text="Agregar" AutoPostBack="True" OnCheckedChanged="rb_agregar_juzgado_CheckedChanged" />
                                <asp:RadioButton CssClass="radio-inline" ID="rb_editar_juzgado" runat="server" Text="Editar" AutoPostBack="True" OnCheckedChanged="rb_editar_juzgado_CheckedChanged" />
                                <asp:RadioButton CssClass="radio-inline" ID="rb_eliminar_juzgado" runat="server" Text="Eliminar" AutoPostBack="True" OnCheckedChanged="rb_eliminar_juzgado_CheckedChanged" />
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txt_buscar_juzgado" runat="server" placeholder="Buscar" Visible="false"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button CssClass="btn btn-success" ID="btn_buscar_juzgado" runat="server" Text="Ir" Visible="false" OnClick="btn_buscar_juzgado_Click" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView CssClass="table" ID="gv_juzgado" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gv_juzgado_PageIndexChanging" PageSize="5">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_juzgado" runat="server" onclick="CheckOne(this)" OnCheckedChanged="chk_juzgado_CheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="desc_especializa" HeaderText="Tipo" SortExpression="desc_especializa" />
                                        <asp:BoundField DataField="localidad" HeaderText="Localidad" SortExpression="localidad" />
                                        <asp:BoundField DataField="numero" HeaderText="Nombre y/o número" SortExpression="numero" />
                                        <asp:BoundField DataField="fecha_registro" HeaderText="Registro" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                        <asp:BoundField DataField="codigo_juzgado" HeaderText="ID Base" SortExpression="codigo_juzgado" Visible="true" />
                                    </Columns>
                                    <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/img/next_arrow.png" PreviousPageImageUrl="~/img/back_arrow.png" />
                                </asp:GridView>
                                <br />
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-md-6">
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_especializa" runat="server" Text="*Tipo de Juzgado"></asp:Label></h5>
                                    <asp:DropDownList CssClass="form-control" ID="ddl_especializa" runat="server"></asp:DropDownList>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_especializa" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_especializa" InitialValue="0" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_localidad" runat="server" Text="*Localidad"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_localidad" runat="server" placeholder="Capturar Localidad"></asp:TextBox>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_localidad" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_localidad" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_numero" runat="server" Text="*Nombre y/o número"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_numero" runat="server" placeholder="Capturar Nombre y/o número"></asp:TextBox>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_numero" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_numero" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_callenum" runat="server" Text="*Calle y número"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_callenum" runat="server" placeholder="Capturar Calle y número"></asp:TextBox>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_callenum" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_callenum" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_cp" runat="server" Text="*Código Postal"></asp:Label></h5>

                                    <asp:TextBox CssClass="form-control" ID="txt_cp" runat="server" placeholder="Capturar Código Postal" MaxLength="5" ToolTip="Base SEPOMEX" AutoPostBack="true" OnTextChanged="txt_cp_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="mee_cp" runat="server" TargetControlID="txt_cp" Mask="99999" />

                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_cp" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_cp" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_colonia" runat="server" Text="*Colonia"></asp:Label></h5>
                                    <asp:DropDownList CssClass="form-control" ID="ddl_colonia" runat="server" ToolTip="Base SEPOMEX"></asp:DropDownList>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_colonia" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_colonia" InitialValue="0" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_municipio" runat="server" Text="Municipio"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_municipio" runat="server" placeholder="Municipio" Enabled="false" ToolTip="Base SEPOMEX"></asp:TextBox>
                                </div>
                                <br />
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_estado" runat="server" Text="Estado"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_estado" runat="server" placeholder="Estado" Enabled="false" ToolTip="Base SEPOMEX"></asp:TextBox>
                                </div>
                                <div class="col-md-6 text-left">
                                    <asp:CheckBox CssClass="checkbox-inline" ID="chkbox_sala" runat="server" AutoPostBack="true" Text="Agregar Sala" OnCheckedChanged="chkbox_sala_CheckedChanged" />
                                </div>
                                <div class="col-md-6 text-right">

                                    <asp:Button CssClass="btn btn-success text-right" ID="btn_guardar_juzgado" runat="server" Text="Guardar" OnClick="btn_guardar_juzgado_Click" />
                                </div>
                            </div>

                            <div id="div_salas" runat="server">

                                <div class="col-md-12 text-right">
                                    <hr />
                                    <h3 class="text-center">Control de Salas</h3>
                                    <br />
                                    <asp:RadioButton CssClass="radio-inline" ID="rb_agregar_sala" runat="server" Text="Agregar" AutoPostBack="True" OnCheckedChanged="rb_agregar_sala_CheckedChanged" Enabled="false" />
                                    <asp:RadioButton CssClass="radio-inline" ID="rb_editar_sala" runat="server" Text="Editar" AutoPostBack="True" OnCheckedChanged="rb_editar_sala_CheckedChanged" Enabled="false" />
                                    <asp:RadioButton CssClass="radio-inline" ID="rb_eliminar_sala" runat="server" Text="Eliminar" AutoPostBack="True" OnCheckedChanged="rb_eliminar_sala_CheckedChanged" Enabled="false" />
                                </div>
                                <div class="col-md-12 text-right">
                                    <asp:GridView CssClass="table" ID="gv_sala" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gv_sala_PageIndexChanging" PageSize="5">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_sala" runat="server" onclick="CheckOne(this)" OnCheckedChanged="chk_sala_CheckedChanged" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre y/o número" SortExpression="nombre" />
                                            <%--		<asp:BoundField DataField="ip" HeaderText="IP" SortExpression="ip" />--%>
                                            <asp:BoundField DataField="fecha_registro" HeaderText="Registro" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                            <asp:BoundField DataField="codigo_sala" HeaderText="ID Base" SortExpression="codigo_sala" Visible="true" />
                                        </Columns>
                                        <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/img/next_arrow.png" PreviousPageImageUrl="~/img/back_arrow.png" />
                                    </asp:GridView>
                                    <br />
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label CssClass="control-label" ID="lbl_sala" runat="server" Text="*Nombre y/o número"></asp:Label></h5>
                                        <asp:TextBox CssClass="form-control" ID="txt_sala" runat="server" placeholder="Capturar Nombre y/o número" Enabled="false"></asp:TextBox>
                                        <div class="text-right">
                                            <asp:RequiredFieldValidator ID="rfv_sala" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_sala" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label CssClass="control-label" ID="lbl_ip" runat="server" Text="*IP"></asp:Label></h5>
                                        <asp:TextBox CssClass="form-control" ID="txt_ip" runat="server" placeholder="Capturar IP" Enabled="false" AutoPostBack="true" OnTextChanged="txt_ip_TextChanged"></asp:TextBox>
                                        <div class="text-right">
                                            <asp:RequiredFieldValidator ID="rfv_ip" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_ip" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="IpValidator" ControlToValidate="txt_ip" runat="server" ValidationExpression="^((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})$"
                                                ErrorMessage="Formato de IP Invalido" CssClass="comments" Display="None"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="PNReqE" TargetControlID="IpValidator" HighlightCssClass="highlight" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label CssClass="control-label" ID="lbl_user_ip" runat="server" Text="*Usuario IP"></asp:Label></h5>
                                        <asp:TextBox CssClass="form-control" ID="txt_user_ip" runat="server" placeholder="Capturar Usuario" Enabled="false"></asp:TextBox>
                                        <div class="text-right">
                                            <asp:RequiredFieldValidator ID="rfv_user_ip" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_user_ip" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label CssClass="control-label" ID="lbl_pass_ip" runat="server" Text="*Contraseña IP"></asp:Label></h5>
                                        <asp:TextBox CssClass="form-control" ID="txt_pass_ip" runat="server" placeholder="Capturar Contraseña" TextMode="Password" Enabled="false"></asp:TextBox>
                                        <div class="text-right">
                                            <asp:RequiredFieldValidator ID="rfv_pass_ip" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_pass_ip" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_path_videos" runat="server" Text="*Ruta de Carpeta"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_path_videos" runat="server" placeholder=" Capturar Ruta de Carpeta" Enabled="false" AutoPostBack="true" OnTextChanged="txt_path_videos_TextChanged"></asp:TextBox>
                                <div class="text-right">
                                    <asp:RequiredFieldValidator ID="rfv_path_videos" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_path_videos" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_user_path" runat="server" Text="*Usuario Ruta"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_user_path" runat="server" placeholder="Capturar Usuario" Enabled="false"></asp:TextBox>
                                <div class="text-right">
                                    <asp:RequiredFieldValidator ID="rfv_user_path" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_user_path" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_pass_path" runat="server" Text="*Contraseña Ruta"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_pass_path" runat="server" placeholder="Capturar Contraseña" TextMode="Password" Enabled="false"></asp:TextBox>
                                <div class="text-right">
                                    <asp:RequiredFieldValidator ID="rfv_pass_path" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_pass_path" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 text-right">
                            <div class="form-group">
                                <br />
                                <asp:Button CssClass="btn btn-success" ID="btn_guarda_sala" runat="server" OnClick="btn_guarda_sala_Click" Visible="false" Enabled="false" Text="Guardar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Bootstrap Modal Dialog -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-success" data-dismiss="modal" aria-hidden="true">Ok</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>