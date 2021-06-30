<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GestionDePagos.aspx.cs" Inherits="Gestion_administrativa.GestionDePagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section class="content-header">
        <h1 style="text-align:center">GESTIÓN DE COMPROBANTES</h1>
        <asp:Button ID="btn_generarCuponesPago" Text="Generar Cupones" runat="server" OnClick="btn_generarCuponesPago_Click"/>
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
                        <h2>CUOTAS</h2>
                        <asp:GridView ID="gv_cupones_totales" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="comp_fecha_formateado" HeaderText="Fecha" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="comp_total_formateado" HeaderText="Monto"  ItemStyle-Width="20%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="com_TipoComprobante.TC_nombre" HeaderText="Tipo"  ItemStyle-Width="20%"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                            </Columns>                     
                        </asp:GridView>
                        <br>
                        <div class="form-group">
                            <asp:DropDownList ID="dpl_cuotaHistorica" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="dpl_cuotaHistorica_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <asp:Button ID="btn_eliminarCuota" runat="server" Text="ELIMINAR"  Visible="false" CssClass="btn bg-warning" ClientIDMode="static" OnClientClick="javascript:return confirm('¿Esta seguro de la operación?');" OnClick="btn_eliminarCuota_Click"/>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="container bg-white border-top border-3 border-success">
                        <h2>CUOTAS PENDIENTES</h2>                        
                        <asp:GridView ID="gv_cupones_pendientes" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="comp_fecha_formateado" HeaderText="Fecha" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="comp_total_formateado" HeaderText="Monto"  ItemStyle-Width="20%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="com_TipoComprobante.TC_nombre" HeaderText="Tipo"  ItemStyle-Width="20%"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                            </Columns>                     
                        </asp:GridView>
                        <br>
                        <div class="form-group">
                            <asp:DropDownList ID="dpl_cuotaPagar" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                
            <table align="center">
                <tr>
                    <td>
                        <asp:Button ID="btn_registrarPago" runat="server" Text="REGISTRAR PAGO" CssClass="btn btn-primary" OnClick="btn_registrarPago_Click"/>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                     <td>
                        <asp:Button ID="btn_eliminar" runat="server" Text="ELIMINAR"  Visible="false" CssClass="btn bg-warning" ClientIDMode="static" OnClientClick="javascript:return confirm('¿Esta seguro de la operación?');" OnClick="btn_eliminar_Click"/>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <asp:Button ID="btn_cancelar" runat="server" Text="CANCELAR" CssClass="btn btn-danger" OnClick="btn_cancelar_Click"/>
                    </td>
                </tr>
            </table>
               </div>
            </div>
        <div class="row">
            
        </div>
        </div>
    </section>

</asp:Content>
