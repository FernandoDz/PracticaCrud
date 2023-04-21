create database Practica_Crud;
use Practica_Crud;

create table Roles(
Id int not null identity(1,1),
Nombre nvarchar(50) not null,
Descripcion nvarchar(100) not null,
primary key(Id)
);

create table Usuarios(
Id int not null identity(1,1),
RolId int not null,
Nombre nvarchar(50) not null,
Correo nvarchar(100) not null,
[Password] nchar(50) not null,
foreign key(RolId) references  Roles(Id)
);
