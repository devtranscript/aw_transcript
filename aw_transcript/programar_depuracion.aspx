<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="programar_depuracion.aspx.cs" Inherits="aw_transcript.programar_depuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="section">
                <div class="container">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-1">
                                <a href="menu_herramientas.aspx">
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

                            <div class="row">
                                <div class="col-md-12 text-left">
                                    <div class="panel-group" runat="server" id="pg_dayvideos">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="text-center">Dias de Respaldo de Videos</h4>
                                                <asp:RadioButton CssClass="radio-inline text-right" ID="rb_add_dayvideos" runat="server" Text="Agregar" AutoPostBack="True" OnCheckedChanged="rb_add_dayvideos_CheckedChanged" />
                                                <asp:RadioButton CssClass="radio-inline text-right" ID="rb_edit_dayvideos" runat="server" Text="Editar" AutoPostBack="True" OnCheckedChanged="rb_edit_dayvideos_CheckedChanged" />
                                            </div>
                                            <div class="panel-body">
                                                <div class="row" id="div_infdayvideos" runat="server" visible="false">
                                                    <div class="col-md-12">
                                                        <asp:GridView CssClass="table" ID="gv_dayvideos" runat="server" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Seleccionar">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk_select" runat="server" onclick="CheckOne(this)" OnCheckedChanged="chkselect_dayvideos" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="id_caducidad_videos" HeaderText="ID" SortExpression="id_caducidad_videos" />
                                                                <asp:BoundField DataField="dias_caducidad" HeaderText="Dias de respaldo" SortExpression="dias_caducidad" />
                                                                <asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro" SortExpression="fecha_registro" DataFormatString="{0:dd/MM/yyyy}" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <h5>
                                                                <asp:Label CssClass="control-label" ID="lbl_days" runat="server" Text="Dias"></asp:Label></h5>
                                                            <div class="input-group">
                                                                <asp:TextBox CssClass="form-control" ID="txt_days" runat="server" placeholder="Número"></asp:TextBox>
                                                                <span class="input-group-btn">
                                                                    <asp:Button CssClass="btn btn-success" ID="cmd_save_days" runat="server" Text="Guardar" OnClick="cmd_save_days_Click" />
                                                                </span>
                                                            </div>
                                                            <div class="text-right">
                                                                <asp:RequiredFieldValidator ID="rfv_days" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_days" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:GridView CssClass="table" ID="gv_dayvideosf" runat="server" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_caducidad_videos" HeaderText="ID" SortExpression="id_caducidad_videos" />
                                                                <asp:BoundField DataField="dias_caducidad" HeaderText="Dias de respaldo" SortExpression="dias_caducidad" />
                                                                <asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro" SortExpression="fecha_registro" DataFormatString="{0:dd/MM/yyyy}" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
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