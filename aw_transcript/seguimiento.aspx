<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="seguimiento.aspx.cs" Inherits="aw_transcript.seguimiento" %>

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
                                <a href="menu.aspx">
                                    <img alt="" src="img/ico_back.png" /></a>
                            </div>
                            <div class="col-md-1">
                                <a href="acceso.aspx">
                                    <img alt="" src="img/ico_exit.png" /></a>
                            </div>
                            <br />
                            <%--   <div class="col-md-10">
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
                            </div>--%>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <h2 class="text-center">
                                    <asp:Label ID="lbl_reg" runat="server" Text=""></asp:Label></h2>
                            </div>
                        </div>
                        <div class=" col-md-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_expedient" runat="server" Text="*Expediente"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_expedient" runat="server" placeholder="Capturar Expediente"></asp:TextBox>
                                <div class="text-right">
                                    <asp:RequiredFieldValidator ID="rfv_expedient" runat="server" ErrorMessage="*Campo Obligatorio" ControlToValidate="txt_expedient" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                </div>
                            </div>
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
                            <asp:GridView HeaderStyle-CssClass="gvHeader"
                                GridLines="None"
                                CssClass="gvRow"
                                AlternatingRowStyle-CssClass="gvAltRow" ID="gv_files" DataKeyNames="id_material" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="gv_files_RowCommand" OnRowDataBound="gv_files_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <a href="JavaScript:StateCity('div<%# Eval("id_material") %>');">
                                                <img id="imgdiv<%# Eval("id_material") %>" src="img/plus.png" />
                                                <%--<asp:ImageButton ID="imgdiv<%# Eval("id_material") %>" runat="server" ImageUrl="~/img/plus.png" OnClick="prueba()" />--%>
                                            </a>
                                            <div id="div<%# Eval("id_material") %>" style="display: none;">
                                                <asp:GridView ID="gv_material_ext" runat="server"
                                                    Width="100%"
                                                    GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="id_material"
                                                    HeaderStyle-CssClass="gvChildHeader"
                                                    CssClass="gvRow"
                                                    Style="padding: 0; margin: 0"
                                                    AlternatingRowStyle-CssClass="gvAltRow" OnRowCommand="gv_material_ext_RowCommand" OnRowDataBound="gv_material_ext_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="id_material_ext" HeaderText="ID" SortExpression="id_material" />
                                                        <asp:BoundField DataField="sesion" HeaderText="Sesión" SortExpression="sesion" />
                                                        <asp:BoundField DataField="archivo" HeaderText="Archivo" SortExpression="archivo" />
                                                        <asp:BoundField DataField="duracion" HeaderText="Duración" SortExpression="duracion" DataFormatString="{00:00,0}" />
                                                        <asp:BoundField DataField="fecha_registro" HeaderText="Registro" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                                        <asp:BoundField DataField="desc_estatus_material" HeaderText="Estatus" SortExpression="desc_estatus_material" />
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-success" ID="btn_pdf_ext" runat="server" Text="" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-success" ID="btn_video_ext" runat="server" Text="" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_material" HeaderText="ID" SortExpression="id_material" />
                                    <asp:BoundField DataField="sesion" HeaderText="Expediente" SortExpression="sesion" />
                                    <asp:BoundField DataField="titulo" HeaderText="Titulo" SortExpression="titulo" />
                                    <asp:BoundField DataField="localizacion" HeaderText="Localización" SortExpression="localizacion" />
                                    <asp:BoundField DataField="tipo" HeaderText="Tipo" SortExpression="tipo" />
                                    <asp:BoundField DataField="archivo" HeaderText="Archivo" SortExpression="archivo" />
                                    <asp:BoundField DataField="duracion" HeaderText="Duración" SortExpression="duracion" DataFormatString="{00:00,0}" />
                                    <asp:BoundField DataField="fecha_registro" HeaderText="Registro" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                    <asp:BoundField DataField="desc_estatus_material" HeaderText="Estatus" SortExpression="desc_estatus_material" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button CssClass="btn btn-success" ID="btn_pdf" runat="server" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:Button CssClass="btn btn-success" ID="btn_video" runat="server" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row" id="div_panel_ie" runat="server">
                <div class="col-md-12 text-center">
                    <asp:Panel ID="Panel1" runat="server" CssClass=" img-thumbnail"></asp:Panel>
                </div>
            </div>
            <div class="row" id="div_panel" runat="server">
                <div class="col-md-12 text-center">
                    <video id="play_video" runat="server" visible="false" class="img-thumbnail" controls="controls">
                        <source src="demo" type='video/mp4;codecs="avc1.42E01E, mp4a.40.2"' />
                    </video>
                    <iframe id="iframe_pdf" src="" width="600" height="500" runat="server" visible="false"></iframe>
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

                            <iframe id="iframe1" src="" width="600" height="500" runat="server" visible="false"></iframe>
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

                            <video id="Video1" runat="server" visible="false" class="img-thumbnail" controls="controls">
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