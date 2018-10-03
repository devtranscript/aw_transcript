<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="acceso.aspx.cs" Inherits="aw_transcript.acceso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="section">
        <div class="container">

            <br>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4 img-thumbnail">
                    <br />
                    <h3 class="text-center">Control de Acceso</h3>
                    <img src="img/iconos/perfil.png" class="img-responsive center-block" width="64">
                    <br />
                    <div class="form-group">
                        <h5>
                            <asp:Label CssClass="control-label" ID="lbl_usuario" runat="server" Text="*Usuario"></asp:Label></h5>
                        <asp:TextBox CssClass="form-control" ID="txt_code_user" runat="server" TabIndex="1" placeholder="Capturar Usuario"></asp:TextBox>
                        <div class="text-right">
                            <asp:RequiredFieldValidator ID="rfv_code_user" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_code_user" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <h5>
                            <asp:Label CssClass="control-label" ID="lbl_password" runat="server" Text="*Contraseña"></asp:Label></h5>
                        <asp:TextBox CssClass="form-control" ID="txt_password" runat="server" TabIndex="3" placeholder="Capturar Contraseña" TextMode="Password"></asp:TextBox>
                        <div class="text-right">
                            <asp:RequiredFieldValidator ID="rfv_password" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_password" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:LinkButton CssClass="text-left" ID="lkb_registro" runat="server" Visible="false" Text="Registrar" OnClick="lkb_registro_Click"></asp:LinkButton>
                    </div>
                    <div class="form-group">
                        <asp:Button CssClass="btn btn-block" ID="cmd_login" runat="server" Text="Entrar" TabIndex="4" OnClick="cmd_login_Click" />
                    </div>
                </div>
                <div class="col-md-4"></div>
            </div>
        </div>
    </div>

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