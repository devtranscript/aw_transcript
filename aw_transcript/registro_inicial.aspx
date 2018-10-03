<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="registro_inicial.aspx.cs" Inherits="aw_transcript.registro_inicial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="section">
                <div class="container">
                    <div class="form-group">
                        <%--  <div class="row">
                            <div class="col-md-1">
                                <br />
                                <a href="acceso.aspx">
                                    <i class="material-icons">exit_to_app</i><br />
                                    Salir </a>
                            </div>
                        </div>--%>
                        <div class="row">
                            <div class="col-md-12">
                                <h3 class="text-center">Registro Inicial</h3>
                                <br />
                            </div>

                            <div class="col-md-4">
                                <asp:Image CssClass="center-block img-responsive" ID="Image1" runat="server" ImageUrl="~/img/tribunal.png"
                                    Width="64" Height="64" />
                                <h3 class="text-center">Tribunal</h3>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_tribunal" runat="server" Text="*Nombre de Tribunal"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_tribunal" runat="server" placeholder="Capturar Nombre de Tribunal"></asp:TextBox>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_tribunal" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_tribunal" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_telefono" runat="server" Text="Teléfono"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_telefono" runat="server" placeholder="Capturar Teléfono" TextMode="Phone"></asp:TextBox>
                                    <div class="text-right">
                                        <asp:RegularExpressionValidator ID="revPhone" runat="server" ErrorMessage="*Teléfono incorrecto" ControlToValidate="txt_telefono" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$" ForeColor="DarkRed"></asp:RegularExpressionValidator>
                                    </div>
                                    <%--<ajaxToolkit:MaskedEditExtender ID="mee_telefono" runat="server" TargetControlID="txt_telefono" Mask="(52) 999.99.99.99.99.99 ext 99999" />--%>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_email" runat="server" Text="e-mail"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_email" runat="server" placeholder="Capturar e-mail" TextMode="Email"></asp:TextBox>
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
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_cp" runat="server" Text="*Código Postal"></asp:Label></h5>
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txt_cp" runat="server" placeholder="Capturar Código Postal" MaxLength="5" ToolTip="Base SEPOMEX"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="mee_cp" runat="server" TargetControlID="txt_cp" Mask="99999" />
                                        <span class="input-group-btn">
                                            <asp:Button CssClass="btn" ID="btn_cp" runat="server" Text="validar" OnClick="btn_cp_Click" />
                                        </span>
                                    </div>
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
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_estado" runat="server" Text="Estado"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_estado" runat="server" placeholder="Estado" Enabled="false" ToolTip="Base SEPOMEX"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Image CssClass="center-block img-responsive" ID="Image2" runat="server" ImageUrl="~/img/iconos/administrador@2x.png"
                                    Width="64" Height="64" />
                                <h3 class="text-center">Administrador</h3>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_name_user" runat="server" Text="*Nombre(s)"></asp:Label>
                                    </h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_name_user" runat="server" placeholder="Capturar Nombre(s)">
                                    </asp:TextBox>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_name_user" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_name_user" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_apater" runat="server" Text="Apellido Paterno"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_apater" runat="server" placeholder="Capturar Apellido Paterno"></asp:TextBox>
                                    <br />
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_amater" runat="server" Text="Apellido Materno"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_amater" runat="server" placeholder="Capturar Apellido Materno"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_code_user" runat="server" Text="*Usuario"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_code_user" runat="server" placeholder="Capturar Usuario"></asp:TextBox>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_code_user" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_code_user" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_password" runat="server" Text="*Contraseña"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_password" runat="server" placeholder="Capturar Contraseña" TextMode="Password"></asp:TextBox>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_password" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_password" ForeColor="DarkRed" Enabled="false"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-right">
                                <asp:Button CssClass="btn" ID="btn_guardar" runat="server" Text="Guardar" OnClick="btn_guardar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-success" data-dismiss="modal" aria-hidden="true" onclick="window.location.href='/acceso.aspx'">Ok</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>