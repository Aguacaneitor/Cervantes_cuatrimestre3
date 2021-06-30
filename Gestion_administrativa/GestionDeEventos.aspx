<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GestionDeEventos.aspx.cs" Inherits="Gestion_administrativa.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script crossorigin src="https://unpkg.com/react@17/umd/react.production.min.js"></script>
<script crossorigin src="https://unpkg.com/react-dom@17/umd/react-dom.production.min.js"></script>

<!-- Load Babel -->
<!-- v6 <script src="https://unpkg.com/babel-standalone@6/babel.min.js"></script> -->
<script src="https://unpkg.com/@babel/standalone/babel.min.js"></script>

<link rel="stylesheet" type="text/css" href="estilos.css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    		<div class="content">
  <div>
         <h2>Find Your Location in below Map</h2> 
        <button onclick="getlocation(); return false;"> Show Position</button> 
        <div id="demo" style="width: 100%; height: 400px; margin-left: 200px;"></div> 
       
        <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA8-R0ty7-YrSfSAZ2ETfn5vj9z0fam-D4&callback=initMap&libraries=&v=weekly"> </script> 
        
        <script type="text/javascript"> 
        var markers=[];
        var maps =null;
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
            maps = new google.maps.Map(document.getElementById("demo"), myOptions); 
            markers = [
            new google.maps.Marker({position:lattlong, map:maps, title:"You are here!"})]; 
            ////
            
       
        } 
        function setMapOnAll(m) {
            for (let i = 0; i < markers.length; i++) {
                markers[i].setMap(m);
            }
        }
        function addPlace(cordenadas, nombre){
            //{lat: -31.40932291772798, lng:-64.1958225108002} plaza colon
            var esta=false;
            var i =0;
            while (!esta && i<markers.length){
                esta = esta || (markers[i].position.lat()== 
                cordenadas.lat() 
                && markers[i].long== 
                cordenadas.long);
                i++;
            }
            console.log(i +" "+ markers.length+" "+esta);
            var x=0;
        
            //for (x in markers){ console.log(""+x.label);}
            if (!esta){// agrega la direccion al mapa
                const marker = new google.maps.Marker({
                    position: cordenadas ,
                    map:maps,
                    label: nombre,
                });
                markers.push(marker); 
            } else { //Borra el marcador del mapa es importante asignar el null antes de borrar, o se queda en el mapa y el recolector de basura no lo eliminara nunca
                markers[i-1].setMap(null);
                markers.splice(i-1,1);}
            //for (x in markers){ console.log(x.label);}
            var x=0;
            while ( x<markers.length){
                console.log(""+markers[x].label);
                x++;
            }
            //setMapOnAll(null);
            //setMapOnAll(maps);
            
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
        } </script>
  </div>

  <div id="sidebarigth">
    <div id="sidebarcontentsrigth"> <!-- Here are the sidebar contents -->
	
		
		<button onclick="addPlace(new google.maps.LatLng(-31.40932291772798,-64.1958225108002), 'Plaza Colon'); return false;" style= " 
        border: none;
        color: black;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        margin: 4px 2px;
        cursor: pointer;"> Plaza Colon </button> 
		
        
        <button onclick="addPlace(new google.maps.LatLng(-31.413050582453536, -64.20458920576739), 'Nuevo centro'); return false;" style= " 
        border: none;
        color: black;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        margin: 4px 2px;
        cursor: pointer;"> Nuevo centro </button> 
	</div>
  </div>
</div>   

</asp:Content>
