<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GestionDeEventos.aspx.cs" Inherits="Gestion_administrativa.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="demo" style="width: 100%; height: 100vh;"></div>
       
        <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA8-R0ty7-YrSfSAZ2ETfn5vj9z0fam-D4&libraries=&v=weekly"> </script> 
        
        <script type="text/javascript"> 
		getlocation();
        function getlocation(){ 
            if(navigator.geolocation){ 
                navigator.geolocation.getCurrentPosition(showPos, showErr); 
            }
            else{
                alert("Sorry! your Browser does not support Geolocation API")
            }
        } 
        //Showing Current Poistion on Google Map
        function showPos(position){ 
            latt = position.coords.latitude; 
            long = position.coords.longitude; 
            var lattlong = new google.maps.LatLng(latt, long); 
            var myOptions = { 
                center: lattlong, 
                zoom: 15, 
                mapTypeControl: true, 
                navigationControlOptions: {style:google.maps.NavigationControlStyle.LARGE} 
            } 
            var maps = new google.maps.Map(document.getElementById("demo"), myOptions); 
            var markers = 
            new google.maps.Marker({position:lattlong, map:maps, title:"You are here!"}); 
        } 

        //Handling Error and Rejection
             function showErr(error) {
              switch(error.code){
              case error.PERMISSION_DENIED:
             alert("User denied the request for Geolocation API.");
              break;
             case error.POSITION_UNAVAILABLE:
             alert("USer location information is unavailable.");
            break;
            case error.TIMEOUT:
            alert("The request to get user location timed out.");
            break;
           case error.UNKNOWN_ERROR:
            alert("An unknown error occurred.");
            break;
           }
        }        </script> 

</asp:Content>
