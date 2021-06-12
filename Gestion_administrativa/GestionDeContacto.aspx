<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GestionDeContacto.aspx.cs" Inherits="Gestion_administrativa.GestionDeContacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 style="text-align:center">GESTION DE TELEFONOS</h1>
    <div class="row">
        <div class="container bg-white border-top border-3 border-success">
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
                                <asp:Button ID="btn_buscar_usuario" runat="server" Text="Buscar" CssClass="btn btn-info form-control"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="form-group">
                <table align="center" style="width: 95%">
                    <colgroup>
                        <col span="1" style="width: 10%;">
                        <col span="1" style="width: 85%;">
                    </colgroup>
                    <tbody>
                        <tr> 
                            <td class="align-bot">
                                <h3>Contacto:</h3>
                            </td>
                            <td class="align-top">
                                <asp:DropDownList ID="dpl_contacto" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <asp:GridView ID="gv_direcciones" runat="server" AutoGenerateColumns="false">
                <HeaderStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="tel_tipo" HeaderText="Tipo" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="tel_nro" HeaderText="Número"  ItemStyle-Width="40%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="tel_prioridad" HeaderText="Prioridad"  ItemStyle-Width="10%"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>

                </Columns>                     
            </asp:GridView>
        </div>  
    </div>  
    <div class="row">       
        <div class="container bg-white border-top border-3">
            <h2>DATOS DE TELEFONO</h2>
                        <div class="form-group">
                               <label>TELEFONO</label>
                        </div>
                        <table align="center" style="width: 95%">
                            <colgroup>
                                <col span="1" style="width: 15%;">
                                <col span="1" style="width: 80%;">
                            </colgroup>
                            <tbody>
                                <tr> 
                                    <td class="align-center">
                                        <asp:DropDownList ID="dpl_codigoarea" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                    <td class="align-center form-group">
                                        <asp:TextBox ID="txt_nro" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>                        
                          <table align="center" style="width: 90%">
                                <colgroup>
                                    <col span="1" style="width: 25%;">
                                    <col span="1" style="width: 35%;">
                                    <col span="1" style="width: 35%;">
                                </colgroup>
                                <tbody>
                                    <tr>
                                        <td class="align-bot form-group"> 
                                            <label>TIPO</label>
                                        </td>                                      
                                        <td class="align-bot form-group">
                                            <asp:RadioButton ID="rb_movil" runat="server" Text="Movil" />
                                        </td>
                                        <td class="align-bot form-group">
                                            <asp:RadioButton ID="rb_fijo" runat="server" Text="Fijo" />
                                        </td>
                                    </tr>
                                </tbody>
                        </table>  
                        <div class="form-group">
                               <label>PRIORIDAD</label>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="dpl_prioridad" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>                        
        </div>
    </div>
    <div class="row">
            <table align="center">
                <tr>
                    <td>
                        <asp:Button ID="btn_registrar" runat="server" Text="REGISTRAR" CssClass="btn btn-primary" ClientIDMode="static"/>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <asp:Button ID="btn_cancelar" runat="server" Text="CANCELAR" CssClass="btn btn-danger" ClientIDMode="static"/>
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>
