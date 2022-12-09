CREATE DATABASE DBEmpleado;
USE DBEmpleado;

CREATE TABLE Departamento(
	IdDepartamento int primary key identity(1,1),
	Nombre varchar(50),
	FechaCreacion datetime default GETDATE()
);


CREATE TABLE Empleado(
	IdEmpleado int primary key identity(1,1),
	NombreCompleto varchar(50),
	IdDepartamento int REFERENCES Departamento(IdDepartamento),
	Sueldo decimal(10,2),
	FechaContrato datetime,
	FechaCreacion datetime default GETDATE()
);

INSERT INTO Departamento(Nombre)
VALUES ('Administracion'), ('Marketing'), ('Ventas'), ('Comercio');

INSERT INTO Empleado(NombreCompleto, IdDepartamento, Sueldo, FechaContrato)
VALUES ('Franco Hernandez', 1, 1400, '11-01-2021'), ('Boris Vargas', 4, 4400, '01-01-2022');

-- PAQUETES NUGET
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
AutoMapper
AutoMapper.Extensions.Microsoft.DependencyInjection

-- obtenemos los modelos y el contexto
Scaffold-DbContext "Server=DESKTOP-V3E5ICQ\MSSQLSERVERDEV; DataBase=DBEmpleado;Integrated Security=true" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Data\Models -ContextDir Data
