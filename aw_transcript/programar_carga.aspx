<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="programar_carga.aspx.cs" Inherits="aw_transcript.programar_carga" %>

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
                            <div class="col-md-12 text-left">
                                <div class="panel-group" runat="server" id="pg_transformation">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="text-center">Fecha y Hora Carga de Videos</h4>
                                            <asp:RadioButton CssClass="radio-inline text-right" ID="rb_add_transformation" runat="server" Text="Agregar" AutoPostBack="True" OnCheckedChanged="rb_add_transformation_CheckedChanged" />
                                            <asp:RadioButton CssClass="radio-inline text-right" ID="rb_edit_transformation" runat="server" Text="Editar" AutoPostBack="True" OnCheckedChanged="rb_edit_transformation_CheckedChanged" />
                                        </div>
                                        <div class="panel-body">
                                            <div class="row" id="div_inftransformation" runat="server" visible=" false">
                                                <div class="col-md-12">
                                                    <br />
                                                    <asp:GridView CssClass="table" ID="gv_transformation" runat="server" AutoGenerateColumns="False" AllowPaging="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Seleccionar">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_select" runat="server" onclick="CheckOne(this)" OnCheckedChanged="chkselect_transformation" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="id_fecha_transformacion" HeaderText="ID" SortExpression="id_fecha_transformacion" Visible="true" />
                                                            <asp:BoundField DataField="horario" HeaderText="Horario" SortExpression="Fecha y Hora" />
                                                            <asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro" SortExpression="fecha_registro" DataFormatString="{0:dd/MM/yyyy}" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <br />
                                                </div>
                                                <div class=" col-md-2">
                                                    <div class="form-group">
                                                        <h5>
                                                            <asp:Label CssClass="control-label" ID="lbl_fecha" runat="server" Text="*Fecha"></asp:Label></h5>
                                                        <asp:TextBox CssClass="form-control" ID="txt_date" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="ce_date" runat="server" BehaviorID="ce_date" TargetControlID="txt_date" Format="yyyy/MM/dd" />
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_date" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_date" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class=" col-md-2">
                                                    <div class="form-group">
                                                        <h5>
                                                            <asp:Label CssClass="control-label" ID="lbl_hora" runat="server" Text="*Hora"></asp:Label></h5>
                                                        <div class="input-group">
                                                            <asp:TextBox CssClass="form-control" ID="txt_hora" runat="server" placeholder="hh/mm" TextMode="Time"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <asp:Button CssClass="btn btn-success" ID="cmd_save" runat="server" Text="Guardar" OnClick="cmd_save_Click" />
                                                            </span>
                                                        </div>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_hora" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_hora" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--    <div class="col-md-3">
                                                    <div class="form-group">
                                                        <h5>
                                                            <asp:Label CssClass="control-label" ID="lbl_fhora" runat="server" Text="*Formato"></asp:Label></h5>
                                                        <div class="input-group">
                                                            <asp:DropDownList CssClass="form-control" ID="ddl_fhora" runat="server">
                                                                <asp:ListItem>Seleccionar</asp:ListItem>
                                                                <asp:ListItem>a. m.</asp:ListItem>
                                                                <asp:ListItem>p. m.</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="input-group-btn">
                                                                <asp:Button CssClass="btn btn-success" ID="cmd_save" runat="server" Text="Guardar" OnClick="cmd_save_Click" />
                                                            </span>
                                                        </div>
                                                        <div class="text-right">
                                                            <asp:RequiredFieldValidator ID="rfv_fhora" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_fhora" InitialValue="0" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                                <div class="col-md-12">
                                                    <asp:GridView CssClass="table" ID="gv_transformationf" runat="server" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:BoundField DataField="id_fecha_transformacion" HeaderText="ID" SortExpression="id_fecha_transformacion" Visible="true" />
                                                            <asp:BoundField DataField="horario" HeaderText="Fecha y Hora" SortExpression="horario" />
                                                            <asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro" SortExpression="fecha_registro" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
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