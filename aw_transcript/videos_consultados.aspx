<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="videos_consultados.aspx.cs" Inherits="aw_transcript.videos_consultados" %>

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
                                <a href="menu_resumen.aspx">
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
                                    <asp:Label ID="lbl_name" runat="server" Text=""></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_profilelbl" runat="server" Text="Perfil: "></asp:Label>
                                    <asp:Label ID="lbl_profile_user" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_id_profile_user" runat="server" Text="" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_user_centerP" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_user_centerCP" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_id_centerCP" runat="server" Text="" Visible="false"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <h2 class="text-center">
                                    <asp:Label ID="lbl_reg" runat="server" Text="Videos Consultados"></asp:Label></h2>
                            </div>
                        </div>
                        <div class="col-md-12 text-right">
                            <asp:RadioButton CssClass="radio-inline" ID="rb_internos" runat="server" Text="Internos" AutoPostBack="True" OnCheckedChanged="rb_internos_CheckedChanged" />
                            <asp:RadioButton CssClass="radio-inline" ID="rb_externos" runat="server" Text="Externos" AutoPostBack="True" OnCheckedChanged="rb_externos_CheckedChanged" />
                        </div>
                        <div class=" col-md-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_fechaini" runat="server" Text="*Fecha Inicial"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_dateini" runat="server" placeholder="Buscar fecha incial"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ce_dateini" runat="server" BehaviorID="ce_dateini" TargetControlID="txt_dateini" Format="yyyy/MM/dd" />
                                <div class="text-right">
                                    <asp:RequiredFieldValidator ID="rfv_dateini" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_dateini" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class=" col-md-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_fechafin" runat="server" Text="*Fecha Final"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_datefin" runat="server" placeholder="Buscar fecha final"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="ce_datefin" TargetControlID="txt_datefin" Format="yyyy/MM/dd" />
                                <div class="text-right">
                                    <asp:RequiredFieldValidator ID="rfv_datefin" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_datefin" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Button CssClass="btn btn-success" ID="cmd_search" runat="server" Text="Buscar" OnClick="cmd_search_Click" />
                        </div>
                        <div class="col-md-12">
                            <br />
                            <asp:GridView CssClass="table" ID="gv_files" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gv_usuarios_PageIndexChanging" PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="id__material_dep" HeaderText="ID" SortExpression="id__material_dep" Visible="true" />
                                    <asp:BoundField DataField="sesion" HeaderText="Expediente" SortExpression="sesion" />
                                    <asp:BoundField DataField="video" HeaderText="Video" SortExpression="video" Visible="true" />
                                    <asp:BoundField DataField="nombres" HeaderText="Nombre de Usuario" SortExpression="nombres" />
                                    <asp:BoundField DataField="a_paterno" HeaderText="Apellido Paterno" SortExpression="a_paterno" />
                                    <asp:BoundField DataField="a_materno" HeaderText="Apellido Materno" SortExpression="a_materno" />
                                    <asp:BoundField DataField="fecha_registro" HeaderText="Hora Registro" SortExpression="fecha_registro" DataFormatString="{0:hh:mm:ss tt}" HtmlEncode="false" />
                                    <asp:BoundField DataField="fecha_registro_alt" HeaderText="Fecha Registro" SortExpression="fecha_registro_alt" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                </Columns>
                                <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/img/next_arrow.png" PreviousPageImageUrl="~/img/back_arrow.png" />
                            </asp:GridView>
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