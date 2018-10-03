<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="cargar.aspx.cs" Inherits="aw_transcript.cargar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="section">
                <div class="container">
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <div class="col-md-1">
                                <a href="ctrl_material.aspx">
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
                                    <asp:Label ID="lbl_reg" runat="server" Text="Carga de Videos"></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 center-block">

                                <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1"
                                    ThrobberID="myThrobber"
                                    ContextKeys="fred"
                                    AllowedFileTypes="wmv"
                                    MaximumNumberOfFiles="10"
                                    runat="server" OnUploadComplete="AjaxFileUpload1_UploadComplete" OnUploadCompleteAll="AjaxFileUpload1_UploadCompleteAll" OnUploadStart="AjaxFileUpload1_UploadStart" />
                            </div>
                            <div class="col-md-12 center-block">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <h3 class="text-center text-warning">
                                    <asp:Label ID="lbl_mnsj" runat="server" Visible="True"></asp:Label>
                                </h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>