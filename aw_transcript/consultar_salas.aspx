<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="consultar_salas.aspx.cs" Inherits="aw_transcript.consultar_salas" %>

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
                                <div class="col-md-10 text-left">
                                    <div class="panel-group" runat="server" id="pg_credentials">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="text-center">Consulta IPs y Rutas de Salas</h4>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row" id="div_infcredentials" runat="server">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <h5>
                                                                <asp:Label CssClass="control-label" ID="lbl_especializa" runat="server" Text="*Tipo de Juzgado"></asp:Label></h5>
                                                            <asp:DropDownList CssClass="form-control" ID="ddl_especializa" runat="server" OnSelectedIndexChanged="ddl_especializa_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <div class="text-right">
                                                                <asp:RequiredFieldValidator ID="rfv_especializa" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_especializa" InitialValue="0" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <h5>
                                                                <asp:Label CssClass="control-label" ID="lbl_localidad" runat="server" Text="*Localidad"></asp:Label></h5>
                                                            <asp:DropDownList CssClass="form-control" ID="ddl_localidad" runat="server"></asp:DropDownList>
                                                            <div class="text-right">
                                                                <asp:RequiredFieldValidator ID="rfv_localidad" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="ddl_localidad" InitialValue="0" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 text-left">
                                                        <div class="form-group">
                                                            <h5></h5>
                                                            <br />
                                                            <asp:Button CssClass="btn btn-success" ID="btn_fconex" runat="server" OnClick="btn_fconex_Click" Text="Ver" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <br />
                                                        <asp:GridView CssClass="table" ID="gv_credentials" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10">
                                                            <Columns>
                                                                <asp:BoundField DataField="codigo_juzgado" HeaderText="ID" SortExpression="codigo_juzgado" Visible="true" />
                                                                <asp:BoundField DataField="desc_especializa" HeaderText="Tipo Juzgado" SortExpression="desc_especializa" />
                                                                <asp:BoundField DataField="localidad" HeaderText="Localidad" SortExpression="localidad" />
                                                                <asp:BoundField DataField="numero" HeaderText="Nombre y/o número Juzgado" SortExpression="numero" />
                                                                <%--      <asp:BoundField DataField="codigo_sala" HeaderText="ID Sala" SortExpression="codigo_sala" />--%>
                                                                <asp:BoundField DataField="nombre" HeaderText="Nomre Sala" SortExpression="nombre" />
                                                                <asp:BoundField DataField="ip" HeaderText="IP" SortExpression="ip" />

                                                                <%--<asp:BoundField DataField="id_ruta_videos" HeaderText="IP" SortExpression="id_ruta_videos" />--%>
                                                                <asp:BoundField DataField="desc_ruta_ini" HeaderText="Ruta" SortExpression="desc_ruta_ini" />
                                                                <%--<asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro" SortExpression="fecha_registro" DataFormatString="{0:dd/MM/yyyy}" />--%>
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