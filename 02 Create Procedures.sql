USE [Emp_Seguridad]
GO

CREATE PROCEDURE [dbo].[spAccesoSistema]
(@prmUser Varchar(50),
@prmPass Varchar(50)
) AS
BEGIN

	SELECT
	US.usu_id,	US.usuario, US.usu_Nom, US.usu_Ape, US.usu_pass,
	RL.rol_id, RL.rol_descripcion, US.estado_usuario
	FROM dbo.Usuarios AS US
	LEFT JOIN dbo.roles AS RL
	ON  US.rol_id = RL.rol_id
	WHERE US.usuario = @prmUser
	AND US.usu_pass = @prmPass
END;

CREATE PROCEDURE [dbo].[spActualizarDomicilio]
(
@prmDir_ID INTEGER,
@prmUser Varchar(50),
@prmCalle Varchar(50),
@prmAltura INTEGER,
@prmPiso Varchar(50),
@prmDpto Varchar(50),
@prmTorre Varchar(50),
@prmManzana Varchar(50),
@prmBarrio_id INTEGER,
@prmUsu_CP Varchar(50)
) AS
BEGIN
UPDATE [Emp_Seguridad].[dbo].[Direccion] 
SET [dir_calle] = @prmCalle,
	[dir_altura] = @prmAltura,
    [dir_piso] = CASE WHEN @prmDpto = '' THEN null ELSE @prmDpto END, 
    [dir_dpto] = CASE WHEN @prmDpto = '' THEN null ELSE @prmDpto END,
    [dir_torre] = CASE WHEN @prmTorre = '' THEN null ELSE @prmTorre END,
    [dir_manzana] = CASE WHEN @prmManzana = '' THEN null ELSE @prmManzana END,
    [barrio_id] = @prmBarrio_id,
    [usu_CP] = @prmUsu_CP,
    [usu_id] = (SELECT [usu_id] FROM [Emp_Seguridad].[dbo].[Usuarios] WHERE @prmUser = UPPER([usuario]))
WHERE [dir_id] = @prmDir_ID
END;

CREATE PROCEDURE [dbo].[spCambiarEstadoUsuario]
(@prmUser Varchar(50),
@prmCambio INTEGER,
@prmEditor Varchar(50)
) AS
BEGIN
	UPDATE dbo.Usuarios
	SET estado_usuario = CAST(@prmCambio AS BIT),
	[usu_modi] = @prmEditor,
    [usu_fecmodi] = GETDATE()
	WHERE  UPPER(@prmUser) = UPPER(usuario);
END;

CREATE PROCEDURE [dbo].[spEditarTelefono]
(
@prmTel_Tipo VARCHAR(25),
@prmTel_nro VARCHAR(25),
@prmTel_prioridad INTEGER,
@prmTelID INTEGER
) AS
BEGIN
UPDATE [Emp_Seguridad].[dbo].[Telefonos] SET
	  [tel_tipo] = @prmTel_Tipo,
      [tel_nro] = @prmTel_nro,
      [tel_prioridad] = @prmTel_prioridad
WHERE [tel_id] = @prmTelID
END;

CREATE PROCEDURE [dbo].[spEditarUsuario]
(@prmUser Varchar(50),
@prmNombre Varchar(50),
@prmApellido Varchar(50),
@prmPass Varchar(50),
@prmFechaNacimiento Varchar(50),
@prmRol Varchar(50),
@prmEmail Varchar(50),
@prmUserModi Varchar(50)
) AS
BEGIN
	UPDATE [Emp_Seguridad].[dbo].[Usuarios] SET
		[usu_Nom] = @prmNombre
		  ,[usu_Ape] = @prmApellido
		  ,[usu_pass] = @prmPass 
		  ,[fec_nac] = convert(datetime,@prmFechaNacimiento,120)
		  ,[rol_id] = (SELECT  [rol_id] FROM [Emp_Seguridad].[dbo].[Roles]   WHERE UPPER(@prmRol) = UPPER([rol_descripcion]))
		  ,[usu_email] = @prmEmail
		  ,[usu_modi] = @prmUserModi
		  ,[usu_fecmodi] = GETDATE()
	WHERE UPPER(usuario) = UPPER(@prmUser)
END;

CREATE PROCEDURE [dbo].[spEliminarDomicilio]
(@prmDir_ID INTEGER)
 AS
BEGIN
DELETE FROM [Emp_Seguridad].[dbo].[Direccion]
WHERE [dir_id] = @prmDir_ID
END;

CREATE PROCEDURE [dbo].[spEliminarTelefono]
(
@prmTelID INTEGER
) AS
BEGIN
DELETE FROM [Emp_Seguridad].[dbo].[Telefonos]
WHERE [tel_id] = @prmTelID
END;

CREATE PROCEDURE [dbo].[spListaBarrios]
 AS
BEGIN
	SELECT
		BA.[barrio_id]
      ,BA.[barrio_nombre]
      ,BA.[loc_id]
	  ,LO.[loc_nombre]
      ,LO.[provinc_id]
      ,PRO.[provincia_nombre]
	FROM [Emp_Seguridad].[dbo].[Barrios] AS BA
	LEFT JOIN [Emp_Seguridad].[dbo].[Localidades] AS LO
		ON BA.[loc_id] = LO.[loc_id]
	LEFT JOIN [Emp_Seguridad].[dbo].[Provincias] AS PRO
		ON LO.[provinc_id] = PRO.[provinc_id]
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
		,BAR.[loc_id]
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
		BAR.[loc_id] = LOC.[loc_id]
	LEFT JOIN [Emp_Seguridad].[dbo].[Provincias] AS PRV ON
		LOC.[provinc_id] = PRV.[provinc_id]
	WHERE UPPER(@prmUser) = UPPER(US.usuario)
	 OR UPPER(@prmUser) = UPPER(CAST(US.usu_nomdoc AS VARCHAR))
	 OR UPPER(@prmUser) = UPPER(US.usu_email)

END;

CREATE PROCEDURE [dbo].[spListaLocalidades]
 AS
BEGIN
	SELECT
		[loc_id]
		,[loc_nombre]
		,LO.[provinc_id]
		,PRO.[provincia_nombre]
	FROM [Emp_Seguridad].[dbo].[Localidades] AS LO
	LEFT JOIN [Emp_Seguridad].[dbo].[Provincias] AS PRO
	ON LO.[provinc_id] = PRO.[provinc_id]
END;

CREATE PROCEDURE [dbo].[spListaRoles]
 AS
BEGIN
	SELECT
	DISTINCT
	[rol_descripcion]
	FROM dbo.roles
END;

CREATE PROCEDURE [dbo].[spListaTelefonosFiltrados]
(
@prmUser Varchar(50)
) AS
BEGIN
SELECT
	 TL.[tel_id]
	,TL.[tel_tipo]
	,TL.[tel_nro]
	,TL.[tel_prioridad]
	,TL.[usu_id]
	, US.[usuario]
FROM [Emp_Seguridad].[dbo].[Telefonos] AS TL
INNER JOIN [Emp_Seguridad].[dbo].[Usuarios] AS US
ON TL.[usu_id] = US.[usu_id]
WHERE UPPER(US.[usuario]) = UPPER(@prmUser)
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
      ,US.[usu_email]
      ,US.[usu_alta]
      ,CONVERT(varchar,US.[usu_fecalta],23) AS [usu_fecalta]
      ,US.[usu_modi]
      ,CONVERT(varchar,US.[usu_fecmodi],23) AS [usu_fecmodi]
      ,RL.rol_id,
	   RL.rol_descripcion
      ,US.[estado_usuario]
	FROM dbo.Usuarios AS US
	LEFT JOIN dbo.roles AS RL
	ON  US.rol_id = RL.rol_id

END;

CREATE PROCEDURE [dbo].[spObtenerUsuario]
(@prmUser Varchar(50)
) AS
BEGIN
	SELECT
	US.usu_id,
	US.usuario,
	US.usu_Nom,
	US.usu_Ape,
	US.usu_pass,
	CONVERT(varchar,US.[fec_nac],23) AS fec_nac,
	US.usu_tipodoc,
	US.usu_nomdoc,
	US.usu_email,
	RL.rol_id,
	RL.rol_descripcion,
	US.estado_usuario
	FROM dbo.Usuarios AS US
	LEFT JOIN dbo.roles AS RL
	ON  US.rol_id = RL.rol_id
	WHERE UPPER(@prmUser) = UPPER(US.usuario)
	 OR UPPER(@prmUser) = UPPER(CAST(US.usu_nomdoc AS VARCHAR))
	 OR UPPER(@prmUser) = UPPER(US.usu_email)
END;

CREATE PROCEDURE [dbo].[spRegistroDomicilio]
(@prmUser Varchar(50),
@prmCalle Varchar(50),
@prmAltura INTEGER,
@prmPiso Varchar(50),
@prmDpto Varchar(50),
@prmTorre Varchar(50),
@prmManzana Varchar(50),
@prmBarrio_id INTEGER,
@prmUsu_CP Varchar(50)
) AS
BEGIN
INSERT INTO [Emp_Seguridad].[dbo].[Direccion] 
		([dir_calle]
      ,[dir_altura]
      ,[dir_piso]
      ,[dir_dpto]
      ,[dir_torre]
      ,[dir_manzana]
      ,[barrio_id]
      ,[usu_CP]
      ,[usu_id])
	VALUES
		(
		@prmCalle,
		@prmAltura,
		@prmPiso,
		@prmDpto,
		@prmTorre,
		@prmManzana,
		@prmBarrio_id,
		@prmUsu_CP,
		(SELECT [usu_id] FROM [Emp_Seguridad].[dbo].[Usuarios] WHERE @prmUser = UPPER([usuario]))
		)
END;

CREATE  PROCEDURE [dbo].[spRegistroTelefono]
(
@prmTel_Tipo VARCHAR(25),
@prmTel_nro VARCHAR(25),
@prmTel_prioridad INTEGER,
@prmUser VARCHAR(50),
@prmUserID INTEGER
) AS
BEGIN
INSERT INTO [Emp_Seguridad].[dbo].[Telefonos] 
	  ([tel_tipo]
      ,[tel_nro]
      ,[tel_prioridad]
      ,[usu_id])
VALUES 
	(@prmTel_Tipo,
	@prmTel_nro,
	@prmTel_prioridad,
	CASE WHEN @prmUser = ''
	THEN @prmUserID
	ELSE (SELECT usu_id FROM [Emp_Seguridad].[dbo].[Usuarios] WHERE @prmUser = usuario)
	END)
END;

CREATE PROCEDURE [dbo].[spRegistroUsuario]
(@prmUser Varchar(50),
@prmNombre Varchar(50),
@prmApellido Varchar(50),
@prmPass Varchar(50),
@prmFechaNacimiento Varchar(50),
@prmRol Varchar(50),
@prmTipoDocumento Varchar(10),
@prmDocumento int,
@prmEmail Varchar(50),
@prmUserAlta Varchar(50),
@prmFechaAlta Varchar(50)
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
		  ,[rol_id]
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
		convert(datetime,@prmFechaNacimiento,120),
		@prmTipoDocumento,
		@prmDocumento,
		(SELECT  [rol_id] FROM [Emp_Seguridad].[dbo].[Roles]   WHERE UPPER(@prmRol) = UPPER([rol_descripcion])),
		@prmEmail,
		@prmUserAlta,
		convert(datetime,@prmFechaAlta,120),
		CAST(0 AS BIT)
		)
END;

----------------COMPROBANTES------------------------
CREATE PROCEDURE [dbo].[spEliminarComprobante]
(@prmComID INTEGER
) AS
BEGIN
	DELETE
  FROM [Emp_Seguridad].[dbo].[Comprobante]
  WHERE @prmComID = [comp_id]
END;

CREATE PROCEDURE [dbo].[spRegistroComprobante]
(
@prmTCNombre AS VARCHAR(50),
@prmFechaComprobante AS VARCHAR(50),
@prmLetra AS VARCHAR(5),
@prmSuc AS INTEGER,
@prmUserID AS INTEGER,
@prmProductoID AS INTEGER,
@comp_fecha_relacionado AS VARCHAR(50)
)
AS
BEGIN
DECLARE @TC_id AS INTEGER;
DECLARE @Precio AS FLOAT;
SET @TC_id = (SELECT TC_id FROM [Emp_Seguridad].[dbo].[TipoComprobante] WHERE [TC_nombre] = @prmTCNombre);
SET @Precio = (SELECT [val_precio] FROM [Emp_Seguridad].[dbo].[Valores] WHERE prod_id = @prmProductoID AND (GETDATE() BETWEEN [val_fechadesde] AND ISNULL([val_fechahaste], GETDATE())));
INSERT INTO [Emp_Seguridad].[dbo].[Comprobante] (
[comp_fecha]
,[comp_letra]
,[comp_suc]
,[comp_numero]
,[comp_neto]
,[comp_iva]
,[comp_total]
,[usu_id]
,[TC_id]
,[prod_id]
,[comp_fecha_relacionado])
VALUES (
convert(datetime,@prmFechaComprobante,120),
@prmLetra,
@prmSuc,
ISNULL((SELECT MAX(comp_numero) FROM [Emp_Seguridad].[dbo].[Comprobante] WHERE comp_suc = @prmSuc AND TC_id = @TC_id),0)+1,
@Precio,
null,
@Precio,
@prmUserID,
@TC_id,
@prmProductoID,
CASE WHEN UPPER(@comp_fecha_relacionado) = 'NULL' THEN null ELSE convert(datetime,@comp_fecha_relacionado,120) END
)
END;

CREATE ALTER PROCEDURE [dbo].[spListaComprobantesFiltrados]
(@prmUserID INTEGER,
@prmTC Varchar(50)
) AS
BEGIN
	SELECT
		 COM.[comp_id]
		,CONVERT(varchar,COM.[comp_fecha],23) AS comp_fecha
		,COM.[comp_letra]
		,COM.[comp_suc]
		,COM.[comp_numero]
		,COM.[comp_neto]
		,COM.[comp_iva]
		,COM.[comp_total]
		,COM.[usu_id]
		,COM.[TC_id]
		,TC.[TC_nombre]
		,TC.[TC_signo]
		,COM.[prod_id]
	FROM [Emp_Seguridad].[dbo].[Comprobante] AS COM
	LEFT JOIN [Emp_Seguridad].[dbo].[TipoComprobante] AS TC
		ON COM.[TC_id] = TC.[TC_id]
	WHERE @prmUserID = [usu_id] AND @prmTC = TC.[TC_nombre]
END;

CREATE PROCEDURE [dbo].[spListaComprobantesFiltradosPagos]
(@prmUserID INTEGER,
@prmTC Varchar(50)
) AS
BEGIN
	SELECT 
		COM.[comp_id]
		,COM.[comp_fecha]
		,COM.[comp_letra]
		,COM.[comp_suc]
		,COM.[comp_numero]
		,COM.[comp_neto]
		,COM.[comp_iva]
		,COM.[comp_total]
		,COM.[usu_id]
		,COM.[TC_id]
		,COM.[prod_id]
		,TC.[TC_nombre]
		,TC.[TC_signo]
	INTO #Cobro
	FROM [Emp_Seguridad].[dbo].[Comprobante] AS COM
	LEFT JOIN [Emp_Seguridad].[dbo].[TipoComprobante] AS TC
		ON COM.[TC_id] = TC.[TC_id]
	WHERE TC.[TC_signo] = 1

	SELECT 
		COM.[comp_id]
		,COM.[comp_fecha]
		,COM.[comp_letra]
		,COM.[comp_suc]
		,COM.[comp_numero]
		,COM.[comp_neto]
		,COM.[comp_iva]
		,COM.[comp_total]
		,COM.[usu_id]
		,COM.[TC_id]
		,COM.[prod_id]
		,TC.[TC_nombre]
		,TC.[TC_signo]
		,COM.[comp_fecha_relacionado]
	INTO #Pago
	FROM [Emp_Seguridad].[dbo].[Comprobante] AS COM
	LEFT JOIN [Emp_Seguridad].[dbo].[TipoComprobante] AS TC
		ON COM.[TC_id] = TC.[TC_id]
	WHERE TC.[TC_signo] = -1

	SELECT
		CO.[comp_id]
		,CO.[comp_fecha]
		,CO.[comp_letra]
		,CO.[comp_suc]
		,CO.[comp_numero]
		,CO.[comp_neto]
		,CO.[comp_iva]
		,CO.[comp_total]
		,CO.[usu_id]
		,CO.[TC_id]
		,CO.[prod_id]
		,CASE WHEN PA.comp_id IS NULL THEN 'Pendiente' ELSE 'Cancelado' END AS TC_nombre
		,CO.[TC_signo],
		CASE WHEN PA.comp_id IS NULL THEN 0 ELSE 1 END AS Pagado,
		CONCAT('$',CO.[comp_total]) AS comp_total_formateado,
		CONVERT(varchar,CO.[comp_fecha],23) AS comp_fecha_formateado,
		ISNULL(PA.[comp_id],-1) AS comp_id_pago
	FROM #Cobro AS CO
	LEFT JOIN #Pago AS PA
		ON CO.usu_id = PA.usu_id
		AND CO.prod_id = PA.prod_id
		AND CO.comp_fecha = PA.comp_fecha_relacionado
	WHERE @prmUserID = CO.[usu_id] AND @prmTC = CO.[TC_nombre]
	DROP TABLE #Cobro, #Pago
END;