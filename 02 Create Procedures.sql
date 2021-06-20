USE [Emp_Seguridad]
GO
CREATE PROCEDURE [dbo].[spAccesoSistema]
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
	AND US.estado_usuario = CAST(1 AS BIT)

END;

CREATE PROCEDURE [dbo].[spListaDireccionesFiltradas]
(@prmUser Varchar(50))
 AS
BEGIN

	SELECT 
		DIR.[dir_id]
		,DIR.[dir_calle]
		,DIR.[dir_altura]
		,DIR.[dir_piso]
		,DIR.[dir_dpto]
		,DIR.[dir_torre]
		,DIR.[dir_manzana]
		,DIR.[barrio_id]
		,BAR.[barrio_nombre]
		,DIR.[loc_id]
		,LOC.[loc_nombre]
		,LOC.[provinc_id]
		,PRV.[provincia_nombre]
		,DIR.[usu_CP]
	FROM [Emp_Seguridad].[dbo].[Direccion] AS DIR
	INNER JOIN [Emp_Seguridad].[dbo].[Usuarios] AS US ON
		US.usu_id = DIR.usu_id
	LEFT JOIN [Emp_Seguridad].[dbo].[Barrios] AS BAR ON
		BAR.[barrio_id] = DIR.[barrio_id]
	LEFT JOIN [Emp_Seguridad].[dbo].[Localidades] AS LOC ON
		DIR.[loc_id] = LOC.[loc_id]
	LEFT JOIN [Emp_Seguridad].[dbo].[Provincias] AS PRV ON
		LOC.[provinc_id] = LOC.[provinc_id]
	WHERE UPPER(@prmUser) = UPPER(US.usuario)
	 OR UPPER(@prmUser) = UPPER(CAST(US.usu_nomdoc AS VARCHAR))
	 OR UPPER(@prmUser) = UPPER(US.usu_email)

END;

CREATE PROCEDURE [dbo].[spListaRoles]
 AS
BEGIN
	SELECT
	DISTINCT
	[rol_descripcion]
	FROM dbo.roles
END;

CREATE PROCEDURE [dbo].[spListaUsuarios]
 AS
BEGIN

	SELECT
		US.[usu_id]
      ,US.[usuario]
      ,US.[usu_Nom]
      ,US.[usu_Ape]
      ,CONVERT(varchar,US.[fec_nac],23) AS [fec_nac]
      ,US.[usu_tipodoc]
      ,US.[usu_nomdoc]
      ,US.[usu_telefono]
      ,US.[usu_email]
      ,US.[usu_alta]
      ,CONVERT(varchar,US.[usu_fecalta],23) AS [usu_fecalta]
      ,US.[usu_modi]
      ,CONVERT(varchar,US.[usu_fecmodi],23) AS [usu_fecmodi]
      ,US.[dir_id]
      ,RL.rol_id,
	   RL.rol_descripcion
      ,US.[tel_id]
      ,US.[estado_usuario]
	FROM dbo.Usuarios AS US
	LEFT JOIN dbo.roles AS RL
	ON  US.rol_id = RL.rol_id
END;

CREATE PROCEDURE [dbo].[spRegistroUsuario]
(@prmUser Varchar(50),
@prmNombre Varchar(50),
@prmApellido Varchar(50),
@prmPass Varchar(50),
@prmFechaNacimiento datetime,
@prmTipoDocumento Varchar(10),
@prmDocumento int,
@prmEmail Varchar(50),
@prmUserAlta Varchar(50),
@prmFechaAlta datetime
) AS
BEGIN
	INSERT INTO [Emp_Seguridad].[dbo].[Usuarios] 
		([usuario]
		  ,[usu_Nom]
		  ,[usu_Ape]
		  ,[usu_pass]
		  ,[fec_nac]
		  ,[usu_tipodoc]
		  ,[usu_nomdoc]
		  ,[usu_email]
		  ,[usu_alta]
		  ,[usu_fecalta]
		  ,[estado_usuario])
	VALUES
		(
		@prmUser,
		@prmNombre,
		@prmApellido,
		@prmPass,
		@prmFechaNacimiento,
		@prmTipoDocumento,
		@prmDocumento,
		@prmEmail,
		@prmUserAlta,
		@prmFechaAlta,
		CAST(0 AS BIT)
		)
END