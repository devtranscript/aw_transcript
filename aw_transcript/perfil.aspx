<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="perfil.aspx.cs" Inherits="aw_transcript.perfil" %>

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
                                <a href="menu_usuarios.aspx">
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
                            <div class="col-md-12 text-right">
                                <asp:CheckBox CssClass="checkbox-inline" ID="chkb_editar" runat="server" AutoPostBack="true" Text="Editar" OnCheckedChanged="chkb_editar_CheckedChanged" />
                            </div>
                        </div>
                        <div class="row">
                            <h5 class="text-center">Datos de Usuario</h5>
                            <div class="col-md-4">
                                <asp:Image CssClass="center-block img-responsive" ID="Image1" runat="server" ImageUrl="~/img/iconos/perfil@2x.png" Width="64" Height="64" />
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_name_user" runat="server" Text="*Nombre(s)"></asp:Label>
                                    </h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_name_user" runat="server" placeholder="Capturar Nombre(s)">
                                    </asp:TextBox>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_name_user" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_name_user" ForeColor="DarkRed"></asp:RequiredFieldValidator>
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
                                        <asp:RequiredFieldValidator ID="rfv_code_user" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_code_user" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <h5>
                                        <asp:Label CssClass="control-label" ID="lbl_password" runat="server" Text="*Contraseña"></asp:Label></h5>
                                    <asp:TextBox CssClass="form-control" ID="txt_password" runat="server" placeholder="Capturar Contraseña" TextMode="Password"></asp:TextBox>
                                    <div class="text-right">
                                        <asp:RequiredFieldValidator ID="rfv_password" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_password" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button CssClass="btn btn-success" ID="cmd_save" runat="server" Text="Guardar" OnClick="cmd_save_Click" />
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