function validaFecha(){
var in_dia = document.getElementById('drp_dia');
var in_mes = document.getElementById('drp_mes');
var in_agno = document.getElementById('drp_agno');

var dia = in_dia.options[in_dia.selectedIndex].value;
var mes = in_mes.options[in_mes.selectedIndex].value;
var agno = in_agno.options[in_agno.selectedIndex].value;

var fecha_f = formatDate(new Date(agno,mes-1,dia));

document.getElementById("txt_fecha_nacimiento").value = fecha_f;
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) 
        month = '0' + month;
    if (day.length < 2) 
        day = '0' + day;

    return [year, month, day].join('-');
}