<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="estatus_carga_ext.aspx.cs" Inherits="aw_transcript.estatus_carga_ext" %>

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
                            <div class="col-md-12">
                                <h3 class="text-center">
                                    <asp:Label ID="lbl_reg" runat="server" Text="Estatus Carga de Videos"></asp:Label></h3>
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
                            <div class="col-md-2">
                                <div class="form-group">
                                    <br />
                                    <br />
                                    <asp:Button CssClass="btn btn-success" ID="cmd_search" runat="server" Text="Buscar" OnClick="cmd_search_Click" />
                                    <asp:Button CssClass="btn btn-success" ID="btn_csv" runat="server" Text="Descargar Errores(.csv)" Visible="false" />
                                </div>
                            </div>

                            <div class="col-md-12">

                                <asp:GridView CssClass="table" ID="gv_usuarios" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnRowDataBound="gv_usuarios_RowDataBound" OnRowCommand="gv_usuarios_RowCommand">
                                    <Columns>

                                        <asp:BoundField DataField="sesion" HeaderText="Expediente" SortExpression="sesion" />
                                        <asp:BoundField DataField="localidad" HeaderText="Localidad" SortExpression="localidad" />
                                        <asp:BoundField DataField="numero" HeaderText="Nombre y/o número" SortExpression="numero" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre y/o número" SortExpression="nombre" />
                                        <asp:BoundField DataField="titulo" HeaderText="Titulo" SortExpression="titulo" />
                                        <asp:BoundField DataField="err_carga" HeaderText="Error" SortExpression="err_carga" />
                                        <asp:BoundField DataField="desc_est_exp" HeaderText="Estatus" SortExpression="desc_est_exp" />
                                        <asp:BoundField DataField="fecha_registro" HeaderText="Registro" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button CssClass="btn btn-success" ID="btn_pdf_ext" runat="server" Text="" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" />
                                    <HeaderStyle BackColor="Gray" ForeColor="White" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="Inicio" LastPageText="Final" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <br />
                            </div>

                            <div class="col-md-12">

                                <asp:GridView CssClass="table" ID="gv_usr_ext" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnRowCommand="gv_usr_ext_RowCommand">
                                    <Columns>

                                        <asp:BoundField DataField="nom_archivo" HeaderText="Archivo" SortExpression="nom_archivo" />
                                        <asp:BoundField DataField="duracion" HeaderText="Duración" SortExpression="duracion" />
                                        <asp:BoundField DataField="fecha_registro" HeaderText="Registro" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton CssClass="buttonClass" ID="lkb_pdf_exp" runat="server" OnClick="lkb_pdf_exp_Click" Text="PDF ">
                                                    <i class="far fa-file-pdf" id="i_pdf_exp" runat="server"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton CssClass="buttonClass" ID="lkb_video_exp" runat="server" OnClick="lkb_video_exp_Click" Text="Video ">
                                                    <i class="fas fa-video" id="i_video_exp" runat="server"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" />
                                    <HeaderStyle BackColor="Gray" ForeColor="White" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="Inicio" LastPageText="Final" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_csv" />
        </Triggers>
    </asp:UpdatePanel>
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
    <div class="modal" id="myModal_pdf" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="up_pdf" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body text-center">

                            <iframe id="iframe_pdf" src="" width="600" height="500" runat="server" visible="false"></iframe>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-success" data-dismiss="modal" aria-hidden="true">Ok</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="myModal_video" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="up_video" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body text-center">

                            <video id="play_video" runat="server" visible="false" class="img-thumbnail" controls="controls">
                                <source src="demo" type='video/mp4;codecs="avc1.42E01E, mp4a.40.2"' />
                            </video>
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