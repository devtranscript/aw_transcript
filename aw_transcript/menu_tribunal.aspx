<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="menu_tribunal.aspx.cs" Inherits="aw_transcript.menu_tribunal" %>

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
                    <h1 class="text-center">Control de Centros</h1>
                </div>
            </div>
            <div class="row animated bounceInUp">
                <div class="col-md-4 text-center" id="div_tribunales" runat="server">
                    <h5>Tribunal</h5>
                    <asp:ImageButton ID="img_tribunales" runat="server" ImageUrl="~/img/tribunal.png" Width="64" Height="64" OnClick="img_tribunales_Click" />
                </div>
                <div class="col-md-4 text-center" id="div_juzgado" runat="server">
                    <h5>Juzgados y Salas</h5>
                    <asp:ImageButton ID="img_juzgado" runat="server" ImageUrl="~/img/salas.png" Width="64" Height="64" OnClick="img_juzgado_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>