<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="RegistroDeUsuario.aspx.cs" Inherits="Gestion_administrativa.RegistroDeUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/validaciones.js">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1 style="text-align:center">GESTIÓN DE USUARIO</h1>
    </section>
    <section class="content">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                <div class="form-group">
                <table align="center" style="width: 95%">
                    <colgroup>
                        <col span="1" style="width: 20%;">
                        <col span="2" style="width: 60%;">
                        <col span="3" style="width: 15%;">
                    </colgroup>
                    <tbody>
                        <tr> 
                            <td class="align-bot">
                                <h3>Buscar Usuario:</h3>
                            </td>
                            <td class="align-top">
                                <asp:textbox ID="txt_usuario_buscado" runat="server" CssClass="form-control"></asp:textbox>
                            </td>
                            <td class="align-top">
                                <asp:Button ID="btn_buscar_usuario" runat="server" Text="Buscar" CssClass="btn btn-info form-control" OnClick="btn_buscar_usuario_Click"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        </div>
        <div class="row">
                <div class="col-md-6">
                    <div class="container bg-white border-top border-success">
                        <h2>DATOS PERSONALES</h2>
                        <div class="form-group">

                               <label>DOCUMENTO DE IDENTIDAD</label>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <table align="center" style="width: 95%">
                                    <colgroup>
                                        <col span="1" style="width: 20%;">
                                        <col span="1" style="width: 75%;">
                                    </colgroup>
                                    <tbody>
                                        <tr> 
                                            <td class="align-top">
                                                <asp:DropDownList ID="drp_tipodoc" runat="server" ClientIDMode="static" CssClass="form-control"></asp:DropDownList> 
                                            </td>
                                            <td class="align-top">
                                                <asp:TextBox ID="txt_usu_nomdoc" runat="server" Text="" CssClass="form-control" ClientIDMode="static"></asp:TextBox>
                                             </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-group">
                               <label>NOMBRES</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_Nom" runat="server" Text="" CssClass="form-control" ClientIDMode="static"></asp:TextBox>
                        </div>
                        <div class="form-group">
                               <label>APELLIDOS</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_Ape" runat="server" Text="" CssClass="form-control" ClientIDMode="static"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>FECHA DE NACIMIENTO</label>
                        </div>
                        <div class="form-group">
                            <div class="row">
                             <table align="center">
                                <!--<tr class="px-2">
                                    <asp:TextBox ID="txt_fecha_nacimiento" runat="server" Text="" CssClass="form-control" ReadOnly="true" ClientIDMode="static"></asp:TextBox>
                                -->
                                 <colgroup>
                                    <col span="1" style="width: 5%;">
                                    <col span="2" style="width: 5%;">
                                    <col span="3" style="width: 5%;">
                                    <col span="4" style="width: 5%;">
                                    <col span="5" style="width: 5%;">
                                    <col span="6" style="width: 5%;">
                                </colgroup>
                                <tbody>
                                     <tr> 
                                    <td class="align-bottom">
                                        <label>DIA:</label>
                                    </td>
                                    <td class="align-top">
                                        <asp:TextBox ID="drp_dia" runat="server" ClientIDMode="static" width="100px"></asp:TextBox>                                                                      
                                    </td>
                                    <td class="align-bottom">
                                        <label>MES:</label>
                                    </td>
                                    <td class="align-top">
                                        <asp:TextBox ID="drp_mes" runat="server" ClientIDMode="static"  width="100px"></asp:TextBox>
                                    </td>
                                    <td class="align-bottom">
                                        <label>AÑO:</label>
                                    </td>
                                    <td class="align-top">
                                        <asp:TextBox ID="drp_agno" runat="server" ClientIDMode="static" width="100px"></asp:TextBox>  
                                    </td>
                                  </tr>
                                </tbody>
                            </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="container bg-white border-top border-3 border-success">
                        <h2>DATOS DE USUARIO</h2>
                        <div class="form-group">
                               <label>MAIL ELECTRONICO</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_email" runat="server" Text="" CssClass="form-control" ClientIDMode="static"></asp:TextBox>
                        </div>
                        <div class="form-group">
                               <label>USUARIO</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usuario" runat="server" Text="" CssClass="form-control" ClientIDMode="static"></asp:TextBox>
                        </div>

                        <div class="form-group">
                               <label>CONTRASEÑA</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_pass" runat="server" TextMode="Password" Text="" CssClass="form-control" ClientIDMode="static"></asp:TextBox>
                        </div>

                        <div class="form-group">
                               <label>ROL</label>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="lb_rol" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        <div class="row">
            <table align="center">
                <tr>
                    <td>
                        <asp:Button ID="btn_registrar" runat="server" Text="REGISTRAR" CssClass="btn btn-primary" OnClick="btn_registrar_Click"/>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                     <td>
                        <asp:Button ID="btn_eliminar" runat="server" Text="DESACTIVAR"  Visible="false" CssClass="btn bg-warning" ClientIDMode="static" OnClick="btn_eliminar_Click"/>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <asp:Button ID="btn_cancelar" runat="server" Text="CANCELAR" CssClass="btn btn-danger" OnClick="btn_cancelar_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        </div>
    </section>
</asp:Content>
