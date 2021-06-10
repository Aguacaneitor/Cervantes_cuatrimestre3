<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GestionDeRol.aspx.cs" Inherits="Gestion_administrativa.GestionDeRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Gestion de Rol</h1>
    <div class="row">
        <div class="container bg-white border-top border-3 border-success">
            <div class="form-group">
                <table align="center" style="width: 95%">
                    <colgroup>
                        <col span="1" style="width: 10%;">
                        <col span="1" style="width: 85%;">
                    </colgroup>
                    <tbody>
                        <tr> 
                            <td class="align-bot">
                                <h2>ROL</h2>
                            </td>
                            <td class="align-top">
                                <asp:DropDownList ID="lb_rol" runat="server" CssClass="form-control"></asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <asp:GridView ID="gv_roles" runat="server" AutoGenerateColumns="false">
                <HeaderStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="rol_id" HeaderText="ID" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="rol_descripcion" HeaderText="Descripción" ItemStyle-Width="15%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="usu_alta" HeaderText="Usuario Alta" ItemStyle-Width="20%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="usu_fecalta" HeaderText="Fecha Alta" ItemStyle-Width="20%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="usu_modi" HeaderText="Usuario Ultima Modificación" ItemStyle-Width="20%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="usu_fecmodi" HeaderText="Fecha Ultima Modifciación" ItemStyle-Width="20%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                </Columns>                        
            </asp:GridView>
        </div>  
    </div>  
    <div class="row">       
        <div class="container bg-white border-top border-3">
            
        </div>
    </div>   
</asp:Content>
