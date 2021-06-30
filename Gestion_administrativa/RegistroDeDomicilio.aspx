<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="RegistroDeDomicilio.aspx.cs" Inherits="Gestion_administrativa.RegistroDeDomicilio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align:center">GESTION DE DOMICILIOS</h1>
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
                                <asp:Button ID="btn_buscar_usuario" runat="server" Text="Buscar" CssClass="btn btn-info form-control" OnClick="btn_buscar_usuario_Click"/>
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
                                <h3>Domicilio:</h3>
                            </td>
                            <td class="align-top">
                                <asp:DropDownList ID="dpl_domicilio" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="dpl_domicilio_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <asp:GridView ID="gv_direcciones" runat="server" AutoGenerateColumns="false">
                <HeaderStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="dir_calle" HeaderText="Calle" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="dir_altura" HeaderText="Altura"  ItemStyle-Width="10%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="dir_piso" HeaderText="Piso"  ItemStyle-Width="5%"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="dir_dpto" HeaderText="Departamento"  ItemStyle-Width="5%"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="dir_barrio.barrio_nombre" HeaderText="Barrio"  ItemStyle-Width="15%"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="dir_barrio.barrio_localidad.loc_nombre" HeaderText="Localidad"  ItemStyle-Width="15%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="dir_barrio.barrio_localidad.loc_provincia.provincia_nombre" HeaderText="Provincia" ItemStyle-Width="15%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="usu_cp" HeaderText="CP" ItemStyle-Width="10%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                </Columns>                     
            </asp:GridView>
        </div>  
    </div>  
    <div class="row">       
        <div class="container bg-white border-top border-3">
            <h2>DATOS DE DOMICILIO</h2>                        
                        <div class="form-group">
                               <strong><label>* CALLE</label></strong>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_calle" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                               <strong><label>* ALTURA</label></strong>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_altura" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                               <label>MANZANA</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_manzana" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                               <label>TORRE</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_torre" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        </div>                        
                        <div class="form-group">
                               <label>PISO</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_piso" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                               <label>DEPARTAMENTO</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_dpto" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                               <strong><label><strong>*</strong> PROVINCIA</label></strong>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="dpl_provincia" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="dpl_provincia_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                               <strong><label>* LOCALIDAD</label></strong>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="dpl_localidad" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="dpl_localidad_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                               <strong><label>* BARRIO</label></strong>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="dpl_barrio" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="dpl_barrio_SelectedIndexChanged"></asp:DropDownList>
                        </div>                        
                        <div class="form-group">
                               <strong><label>* CODIGO POSTAL</label></strong>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_usu_CP" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        </div>
        </div>
    </div>
    <div class="row">
            <table align="center">
                <tr>
                    <td>
                        <asp:Button ID="btn_registrar" runat="server" Text="REGISTRAR" CssClass="btn btn-primary" ClientIDMode="static" OnClick="btn_registrar_Click"/>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                     <td>
                        <asp:Button ID="btn_eliminar" runat="server" Text="ELIMINAR"  Visible="false" CssClass="btn bg-warning" ClientIDMode="static" OnClientClick="javascript:return confirm('¿Esta seguro de la operación?');" OnClick="btn_eliminar_Click"/>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <asp:Button ID="btn_cancelar" runat="server" Text="CANCELAR" CssClass="btn btn-danger" ClientIDMode="static" OnClick="btn_cancelar_Click"/>
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>
