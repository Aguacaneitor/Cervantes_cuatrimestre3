<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Gestion_administrativa.WebForm1" %>

<!DOCTYPE html>

<html class="bg-dark" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags --content="width=device-width, initial-scale=1" -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-eOJMYsd53ii+scO/bJGFsiCZc+5NDVN2yr8+0RDqr0Ql0h+rP48ckxlpbzKgwra6" crossorigin="anonymous" />
    <!-- estilos personales CSS -->
    <link href="resources/css/estilos.css" rel="stylesheet" />

    <title>Acceso al Sistema</title>      
    
</head>
<body class="bg-dark">
      <div class="form-box bg-secondary contenedor" id="login-box">
        <div class="header text-white text-center">Iniciar Sesión</div>
        <form id="form1" runat="server">
            <div class="body bg-gray">
                <div class="form-group mx-2 py-2">
                    <asp:TextBox ID="txt_usuario" runat="server" CssClass="form-control" placeholder="Ingrese Usuario"></asp:TextBox>
                </div>
                <div class="form-group mx-2 py-2">
                    <asp:TextBox ID="txt_contrasena" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese Clave"></asp:TextBox>
                </div>   
            </div>
            <div class="form-group mx-2 py-2">
                <asp:Button ID="bt_logeo" runat="server" Text="Iniciar Sesión" cssclass="btn  btn-success text-white form-control" OnClick="bt_logeo_Click"/>
            </div>
        </form>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>
      <p>
          &nbsp;</p>
</body>
</html>
