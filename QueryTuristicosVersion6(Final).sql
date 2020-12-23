create database dbGuiasTuristicos
go
use dbGuiasTuristicos
go
if(OBJECT_ID('Idioma') IS NOT NULL) drop table Idioma;
if(OBJECT_ID('Ruta') IS NOT NULL) drop table Ruta;
if(OBJECT_ID('Museo') IS NOT NULL) drop table Museo;
if(OBJECT_ID('Usuario') IS NOT NULL) drop table Usuario;
if(OBJECT_ID('ReservaOrganizaRuta') IS NOT NULL) drop table ReservaOrganizaRuta;
if(OBJECT_ID('ReservaOrganizaMuseo') IS NOT NULL) drop table ReservaOrganizaMuseo;
if(OBJECT_ID('Habla') IS NOT NULL) drop table Habla;
if(OBJECT_ID('Pais') IS NOT NULL) drop table Pais;
if(OBJECT_ID('Ciudad') IS NOT NULL) drop table Ciudad;
if(OBJECT_ID('Valoracion') IS NOT NULL) drop table Valoracion;

create table Pais
(
	id int IDENTITY(1,1) not null,
	nombre varchar(30) not null,

	PRIMARY KEY(id)
)
create table Ciudad
(
	id int IDENTITY(1,1) not null,
	nombre varchar(30) not null,
	pais_id int not null,

	PRIMARY KEY(id)
)
create table Usuario
(
	id int IDENTITY (1,1) not null,
	nombre varchar(30) not null,
	apellidos varchar(30) not null,
	nombre_usuario varchar(30) not null,
	tipoUsuario bit not null,	/*1:Guia 0:Turista*/
	contrasenya varchar(30) not null,
	descripcion varchar(500), 
    edad int,
    telf int,
	correo varchar(30) not null,
	idioma varchar(30) not null,
    
	PRIMARY KEY(id)
)

create table Ruta 
(
	id int IDENTITY (1,1) not null,
	fechaInicio datetime not null,
	fechaFinal datetime not null,
	lugar_quedada varchar(30) not null,
	plazas int not null,
	descripcion varchar(500),
	fotografia varchar(500) not null,
	precio int not null,
	ciudad_id int not null,
	guia_id int not null,

	PRIMARY KEY(id)
)
create table Museo
(
	id int IDENTITY (1,1) not null,
	fechaInicio datetime not null,
	fechaFin datetime not null,
	plazas int not null,
	nombre varchar(30)not null,
	fotografia varchar(500) not null,
	precio int not null,
	ciudad_id int not null,
	guia_id int not null,


	PRIMARY KEY(id),
)
create table ReservaOrganizaRuta
(
    id int IDENTITY (1,1) not null,
	usuario_id int not null,
	ruta_id int not null,
    
    PRIMARY KEY(id),
)
create table ReservaOrganizaMuseo
(
    id int IDENTITY (1,1) not null,
	usuario_id int not null,
	museo_id int not null,
    
    PRIMARY KEY(id),
)

create table Valoracion
(
	id int IDENTITY (1,1) not null,
	usuario_turista_id int not null,
	usuario_guia_id int not null,
	mensaje varchar(300),
	nota int,

	PRIMARY KEY(id)
)

alter table Ciudad add constraint fk_Ciudad_Pais foreign key(pais_id) references Pais(id);
alter table Ruta add constraint fk_Ruta_Ciudad foreign key (ciudad_id) references Ciudad(id);
alter table Ruta add constraint fk_Ruta_Guia foreign key (guia_id) references Usuario(id);
alter table Museo add constraint fk_Museo_Ciudad foreign key (ciudad_id) references Ciudad(id);
alter table Museo add constraint fk_Museo_Guia foreign key (guia_id) references Usuario(id);
alter table ReservaOrganizaRuta add constraint fk_ReservaOrganizaRuta_Ruta foreign key(ruta_id) references Ruta(id);
alter table ReservaOrganizaRuta add constraint fk_ReservaOrganizaRuta_Usuario foreign key(usuario_id) references Usuario(id);
alter table ReservaOrganizaMuseo add constraint fk_ReservaOrganizaMuseo_Museo foreign key(museo_id) references Museo(id);
alter table ReservaOrganizaMuseo add constraint fk_ReservaOrganizaMuseo_Usuario foreign key(usuario_id) references Usuario(id);
alter table Valoracion add constraint fk_Valoracion_Usuario foreign key(usuario_turista_id) references Usuario(id);
alter table Valoracion add constraint fk_Valoracion_UsuarioGuia foreign key(usuario_guia_id) references Usuario(id);

/*Datos Usuario*/
insert into Usuario (nombre, apellidos, tipoUsuario, contrasenya, descripcion, correo, nombre_usuario, idioma, edad) values ('Alex','Sepulveda', 1,'MTIzNA==', 'I am an adventurous boy with a lots of knowledge', 'alex.sepulveda@gmail.com', 'Sepul', 'English', 20);/*Pass: 1234*/
insert into Usuario (nombre, apellidos, tipoUsuario, contrasenya, correo, nombre_usuario, idioma) values ('Sean','Saez', 0,'MTIzNDU=', 'sean.saez.f@gmail.com', 'Xons001', 'English');/*Pass: 12345*/
insert into Usuario (nombre, apellidos, tipoUsuario, contrasenya, descripcion, correo, nombre_usuario, idioma, edad) values ('Quique','Garcia', 1, 'MTIzNDY=', 'I am outgoing and I connect easily with people, without fear of answering any questions', 'quique@gmail.com', 'Quique18', 'English', 24);/*Pass: 12346*/
insert into Usuario (nombre, apellidos, tipoUsuario, contrasenya, correo, nombre_usuario, idioma) values ('Pol','Graguera Traidor', 0,'MTIzNDU2', 'pol@gmail.com', 'Polako', 'English');/*Pass: 123456*/
insert into Usuario (nombre, apellidos, tipoUsuario, contrasenya, descripcion, correo, nombre_usuario, idioma, edad) values ('Romina','Medina', 1,'MTIz', 'I am an adventurous girl who wants people to have more knowledge and culture about the places that I come to tell you about!', 'romina@gmail.com', 'RomaXioma', 'English', 26);/*Pass: 123*/

/*Datos Pais*/
insert into Pais (nombre) values ('Ireland');
insert into Pais (nombre) values ('Spain');
insert into Pais (nombre) values ('Poland');
insert into Pais (nombre) values ('Italy');
insert into Pais (nombre) values ('France');
insert into Pais (nombre) values ('Germany');
insert into Pais (nombre) values ('Chile');
insert into Pais (nombre) values ('Peru');

/*Datos Ciudad*/
insert into Ciudad (nombre, pais_id) values ('Dublin', 1);
insert into Ciudad (nombre, pais_id) values ('Barcelona', 2);
insert into Ciudad (nombre, pais_id) values ('Madrid', 2);
insert into Ciudad (nombre, pais_id) values ('Krakow', 3);
insert into Ciudad (nombre, pais_id) values ('Rome', 4);
insert into Ciudad (nombre, pais_id) values ('Paris', 5);
insert into Ciudad (nombre, pais_id) values ('Berlin', 6);
insert into Ciudad (nombre, pais_id) values ('Santiago de Chile', 7);
insert into Ciudad (nombre, pais_id) values ('Cusco', 8);

/*Datos Museo*/
insert into Museo (fechaInicio, fechaFin, plazas, nombre, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'05-01-21 9:00:00 AM',5), convert(datetime,'05-01-21 11:00:00 AM',5), 10,'Guinness Store', 20, '../../Content/img/GuinnessStore.jpg', 1, 1);
insert into Museo (fechaInicio, fechaFin, plazas, nombre, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'06-01-21 10:00:00 AM',5), convert(datetime,'06-01-21 12:00:00 AM',5), 10,'Cosmo Caixa', 9, '../../Content/img/CosmoCaixa.jpg', 2, 1);
insert into Museo (fechaInicio, fechaFin, plazas, nombre, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'08-01-21 16:00:00 PM',5), convert(datetime,'08-01-21 18:00:00 PM',5), 6,'Real Madrid Museum', 25, '../../Content/img/MuseoRealMadrid.jpg', 3, 3);
insert into Museo (fechaInicio, fechaFin, plazas, nombre, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'08-01-21 11:00:00 PM',5), convert(datetime,'08-01-21 18:00:00 PM',5), 15,'Auschwitz', 40, '../../Content/img/Auschwitz.jpg', 4, 1);
insert into Museo (fechaInicio, fechaFin, plazas, nombre, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'10-01-21 16:00:00 PM',5), convert(datetime,'10-01-21 20:00:00 PM',5), 15,'Rome Coliseum', 25, '../../Content/img/ColiseoRoma.jpg', 5, 3)
insert into Museo (fechaInicio, fechaFin, plazas, nombre, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'12-02-21 18:00:00 PM',5), convert(datetime,'12-02-21 20:00:00 PM',5), 4,'Eiffel Tower', 20, '../../Content/img/TorreEiffel.jpg', 6, 1);
insert into Museo (fechaInicio, fechaFin, plazas, nombre, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'22-01-21 9:00:00 AM',5), convert(datetime,'22-03-21 12:00:00 AM',5), 6,'Altes Museum', 15, '../../Content/img/AltesMuseum.jpg', 7, 3);

/*Datos Rutas*/
insert into Ruta (fechaInicio, fechaFinal, lugar_quedada, plazas, descripcion, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'05-01-21 9:00:00 AM',5), convert(datetime,'27-01-21 15:00:00 PM',5), 'Fontana di Trevi', 10,'These will be the places that we will visit throughout the morning: Colisseum (Outside), Arch of Constantine, Venice Square, Trevi Fountain. We will have a half hour break for breakfast.', 25, '../../Content/img/FontanaDiTrevi.jpg', 5, 1);
insert into Ruta (fechaInicio, fechaFinal, lugar_quedada, plazas, descripcion, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'05-01-21 17:00:00 PM',5), convert(datetime,'28-01-21 21:00:00 PM',5), 'Panteon', 10,'These will be the places that we will visit throughout the afternoon: Panteon, Piazza Navona, Castel Sant Angelo, Vatican. We will have a half hour break for breakfast.', 30, '../../Content/img/Panteon.jpg', 5, 3);
insert into Ruta (fechaInicio, fechaFinal, lugar_quedada, plazas, descripcion, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'16-01-21 9:00:00 AM',5), convert(datetime,'16-01-21 15:00:00 PM',5), 'Museums island', 5,'These will be the places we will visit throughout the morning: Museum Island, Barrio San Nicolás, Berlin Cathedral, La Nueva Guardia. We will have a half hour break for breakfast.', 15, '../../Content/img/IslaDeLosMuseos.jpg', 6, 3);
insert into Ruta (fechaInicio, fechaFinal, lugar_quedada, plazas, descripcion, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'28-03-21 9:00:00 AM',5), convert(datetime,'28-03-21 15:00:00 PM',5), 'Hill of San Cristobal', 8, 'We will go up the mountain by cable car. Upon reaching the top I will guide you throughout the morning through all the areas until you reach the hill where the statue is. We will have a half hour breakfast break when we get to the shops.', 15, '../../Content/img/CerroSanCristobal.jpg', 8, 5);
insert into Ruta (fechaInicio, fechaFinal, lugar_quedada, plazas, descripcion, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'17-04-21 9:00:00 AM',5), convert(datetime,'17-04-21 20:00:00 PM',5), 'Machu Pichu', 10, 'We will explore all the ruins and I will tell you all the anecdotes that there were. I will also explain why they are in ruins. When we get to the top where you can see all of Machu Pichu we will eat and rest for an hour.', 40, '../../Content/img/MachuPichu.jpg', 9, 5);
insert into Ruta (fechaInicio, fechaFinal, lugar_quedada, plazas, descripcion, precio, fotografia, ciudad_id, guia_id) values (convert(datetime,'17-04-21 9:00:00 AM',5), convert(datetime,'17-04-21 20:00:00 PM',5), 'Lagoon Humantay', 10, 'We will surround the entire lagoon exploring all the legends and historical data that were in this lake, Experiences that the indigenous people had in the past. We will have a break to eat and bathe for a while in an available area that I will take you personally', 10, '../../Content/img/LagunaHumantay.jpg', 9, 5);

/*Datos Valoracion*/
insert into Valoracion(usuario_turista_id, usuario_guia_id, mensaje ,nota) values (2, 1, 'It has been great! I would go back to him', 10);
insert into Valoracion(usuario_turista_id, usuario_guia_id, mensaje ,nota) values (2, 3, 'It has benn a courious experience,but very slow', 8);

/*Datos ReservasRutas*/
insert into ReservaOrganizaRuta(usuario_id, ruta_id) values (2, 1);
insert into ReservaOrganizaRuta(usuario_id, ruta_id) values (2, 2);
insert into ReservaOrganizaRuta(usuario_id, ruta_id) values (4, 1);
insert into ReservaOrganizaRuta(usuario_id, ruta_id) values (4, 3);

/*Datos ReservasMuseo*/
insert into ReservaOrganizaMuseo(usuario_id, museo_id) values (2, 1);
insert into ReservaOrganizaMuseo(usuario_id, museo_id) values (2, 2);
insert into ReservaOrganizaMuseo(usuario_id, museo_id) values (2, 5);
insert into ReservaOrganizaMuseo(usuario_id, museo_id) values (4, 1);
insert into ReservaOrganizaMuseo(usuario_id, museo_id) values (4, 3);
insert into ReservaOrganizaMuseo(usuario_id, museo_id) values (4, 4);