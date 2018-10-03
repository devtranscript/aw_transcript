<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="menu_resumen.aspx.cs" Inherits="aw_transcript.menu_resumen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="section">
        <div class="container">
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
                    <h1 class="text-center">Resumen</h1>
                </div>
            </div>
            <div class="row animated bounceInUp">
                <div class="col-md-12 text-center" id="div_tracing" runat="server">
                    <h5>Videos Consultados</h5>
                    <asp:ImageButton ID="img_summary_video" runat="server" ImageUrl="~/img/iconos/resumen estadisticas@2x.png" Width="64" Height="64" OnClick="img_summary_video_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>