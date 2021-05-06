--create database Emp_Seguridad
Use [Emp_Seguridad]
go

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
usu_calle varchar(30),
usu_altura int,
usu_piso varchar(15),
usu_dpto varchar(15),
barrio_id int,
loc_id int,
usu_CP varchar(10),
usu_telefono numeric(10,0),
usu_celular numeric(10,0),
usu_email varchar(50),
usu_alta varchar(30),
usu_fecalta datetime,
usu_modi varchar(30),
usu_fecmodi datetime,
rol_id int
);

Create table roles
(
rol_id int primary key identity (1,1) not null,
rol_descripcion varchar(50),
usu_alta varchar(30),
usu_fecalta datetime,
usu_modi varchar(30),
usu_fecmodi datetime
);

Create table localidades
(
loc_id int primary key identity (1,1) not null,
loc_nombre varchar(50),
provinc_id int
);

Create table provincias
(
provinc_id int primary key identity (1,1) not null,
provincia_nombre varchar(50)
);

Create table barrios
(
barrio_id int primary key identity (1,1) not null,
barrio_nombre varchar(50)
);
----------------------------------------------------------------------------
----------------------------PROCEDIMIENTOS----------------------------------
----------------------------------------------------------------------------
CREATE PROCEDURE dbo.spAccesoSistema
(@prmUser Varchar(50),
@prmPass Varchar(50)
) AS
BEGIN

	SELECT
	US.usu_id,	US.usuario, US.usu_Nom, US.usu_Ape, US.usu_pass,
	RL.rol_id, RL.rol_descripcion
	FROM dbo.Usuarios AS US
	LEFT JOIN dbo.roles AS RL
	ON  US.rol_id = RL.rol_id
	WHERE US.usuario = @prmUser
	AND US.usu_pass = @prmPass

END
;
----------------------------------------------------------------------------
----------------------------DATOS DE TESTING--------------------------------
----------------------------------------------------------------------------
INSERT INTO [dbo].[roles] (rol_descripcion,usu_alta,usu_fecalta)
VALUES ('admin', 'admin', GETDATE());
INSERT INTO [dbo].[Usuarios] (usuario, usu_Nom, usu_Ape, usu_pass, usu_alta, usu_fecalta, rol_id)
VALUES ('admin', 'admin', 'admin', 'admin', 'admin', GETDATE(),1);
