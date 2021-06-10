--create database Emp_Seguridad
Use [Emp_Seguridad]
go

Create table TipoComprobante
(
TC_id int primary key identity (1,1) not null,
TC_nombre varchar(50),
TC_signo int
);

Create table Roles
(
rol_id int primary key identity (1,1) not null,
rol_descripcion varchar(50),
usu_alta varchar(30),
usu_fecalta datetime,
usu_modi varchar(30),
usu_fecmodi datetime
);

Create table Provincias
(
provinc_id int primary key identity (1,1) not null,
provincia_nombre varchar(50)
);

CREATE TABLE Localidades
(
loc_id int primary key identity (1,1) not null,
loc_nombre varchar(50),
provinc_id int,
CONSTRAINT fk_LocalidadesProvincias FOREIGN KEY (provinc_id) REFERENCES Provincias (provinc_id)
);

CREATE TABLE Telefonos
(
tel_id int PRIMARY KEY IDENTITY	(1,1) NOT null,
tel_tipo varchar(25),
tel_nro varchar(25),
tel_prioridad int
)

Create table Barrios
(
barrio_id int primary key identity (1,1) not null,
barrio_nombre varchar(50)
);

CREATE TABLE Direccion
(
dir_id int PRIMARY KEY IDENTITY	(1,1) NOT null,
dir_calle varchar(30),
dir_altura int,
dir_piso varchar(15),
dir_dpto varchar(15),
dir_torre varchar(10),
dir_manzana varchar(10),
tel_id int,
barrio_id int,
loc_id int,
usu_CP varchar(10),
CONSTRAINT fk_DireccionTelefonos FOREIGN KEY (tel_id) REFERENCES Telefonos (tel_id),
CONSTRAINT fk_DireccionBarrios FOREIGN KEY (barrio_id) REFERENCES Barrios (barrio_id),
CONSTRAINT fk_DireccionLocalidades FOREIGN KEY (loc_id) REFERENCES Localidades (loc_id)
)

Create table Usuarios
(
usu_id int primary key identity (1,1) not null,
usuario varchar(30) not null,
usu_Nom varchar(40) not null,
usu_Ape varchar(30) not null,
usu_pass varchar(30) not null,
fec_nac datetime,
usu_tipodoc varchar(10),
usu_nomdoc int,
usu_telefono numeric(10,0),
usu_celular numeric(10,0),
usu_email varchar(50),
usu_alta varchar(30),
usu_fecalta datetime,
usu_modi varchar(30),
usu_fecmodi datetime,
dir_id int,
rol_id int,
tel_id int
CONSTRAINT fk_UsuariosDireccion FOREIGN KEY (dir_id) REFERENCES Direccion (dir_id),
CONSTRAINT fk_UsuariosRoles FOREIGN KEY (rol_id) REFERENCES Roles (rol_id),
CONSTRAINT fk_UsuariosTelefonos FOREIGN KEY (tel_id) REFERENCES Telefonos (tel_id)
);

Create table Comprobante
(
comp_id int primary key identity (1,1) not null,
comp_fecha datetime,
comp_letra varchar(5),
comp_suc int,
comp_numero int,
comp_neto float,
comp_iva float,
comp_total float,
usu_id int,
TC_id int
CONSTRAINT fk_ComprobanteUsuarios FOREIGN KEY (usu_id) REFERENCES Usuarios (usu_id),
CONSTRAINT fk_ComprobanteTipoComprobante FOREIGN KEY (TC_id) REFERENCES TipoComprobante (TC_id)
);

Create table Detalle
(
det_id int primary key identity (1, 1) not null,
det_concepto varchar (100),
det_ImporUnit float,
det_cant int,
det_iva float,
comp_id int,
CONSTRAINT fk_DetalleComprobante FOREIGN KEY (comp_id) REFERENCES Comprobante (comp_id)
);

Create table TipoEvento
(
TipEven_id int primary key identity (1, 1) not null,
TipEven_descripcion varchar(50) not null
);

Create table Servicios
(
serv_id int primary key identity (1, 1) not null,
serv_nombre varchar(50) not null,
serv_tel varchar(20),
serv_celular varchar(20)
);

Create table Evento
(
even_id int primary key identity (1, 1) not null,
even_descipcion varchar(50),
even_fecha datetime,
even_hora varchar(15),
even_geolocaliz varchar(50),
even_respuesta varchar(200),
serv_id int,
usu_id int,
TipEven_id int
CONSTRAINT fk_EventoServicios FOREIGN KEY (serv_id) REFERENCES Servicios (serv_id),
CONSTRAINT fk_EventoUsuarios FOREIGN KEY (usu_id) REFERENCES Usuarios (usu_id),
CONSTRAINT fk_EventoTipoEvento FOREIGN KEY (TipEven_id) REFERENCES TipoEvento (TipEven_id)
);