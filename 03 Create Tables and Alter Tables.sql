Use [Emp_Seguridad]
go

Create table Producto
(
prod_id int primary key identity (1,1) not null,
prod_nombre varchar(50) not null,
);

CREATE TABLE Valores
(
val_id int primary key identity (1,1) not null,
val_precio numeric,
val_fechadesde datetime,
val_fechahaste datetime,
prod_id int,
CONSTRAINT fk_ValoresProducto FOREIGN KEY (prod_id) REFERENCES Producto (prod_id)
);

ALTER TABLE Comprobante
add prod_id int;

ALTER TABLE Comprobante
add CONSTRAINT fk_ComprobanteProducto FOREIGN KEY (prod_id) REFERENCES Producto (prod_id);




