<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="menu_herramientas.aspx.cs" Inherits="aw_transcript.menu_herramientas" %>

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
                    <h1 class="text-center">Configuración</h1>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-md-2 text-center" id="div_salas" runat="server">
                    <h5>Consulta de Salas</h5>
                    <asp:ImageButton ID="img_salas" runat="server" ImageUrl="~/img/iconos/grabaciones@2x.png" OnClick="img_salas_Click" />
                </div>
                <div class="col-md-2 text-center" id="div_transformation" runat="server">
                    <h5>Fecha y Hora Carga de Videos</h5>
                    <asp:ImageButton ID="img_transformation" runat="server" ImageUrl="~/img/iconos/videos@2x.png" OnClick="img_transformation_Click" />
                </div>
                <div class="col-md-2 text-center" id="div_dayvideos" runat="server">
                    <h5>Dias de Respaldo de Videos</h5>
                    <asp:ImageButton ID="img_dayvideos" runat="server" ImageUrl="~/img/iconos/control de centros@2x.png" OnClick="img_dayvideos_Click" />
                </div>
                <div class="col-md-2 text-center" id="div1" runat="server">
                    <h5>Estatus Carga de Videos</h5>
                    <asp:ImageButton ID="img_conversion" runat="server" ImageUrl="~/img/iconos/herramientas@2x.png" OnClick="img_conversion_Click" />
                </div>
            </div>
            <div class="row text-center">

                <div class="col-md-12 text-center" id="div6" runat="server" visible="false">
                    <h5>Estatus Carga de Videos</h5>
                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/img/iconos/herramientas@2x.png" OnClick="img_conversion_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>