use AulaDB

CREATE TABLE Curriculos(
	cpf varchar(12) NOT NULL primary key,
	nome varchar(50) NOT NULL,
	data_nascimento datetime,
	endereco varchar(100),
	telefone varchar(15),
	email varchar(50),
	salario decimal(18,2),
	cargo varchar(20),
	formacao1 varchar(50),
	formacao2 varchar(50),
	formacao3 varchar(50),
	formacao4 varchar(50),
	formacao5 varchar(50),
	experiencia1 varchar(50),
	experiencia2 varchar(50),
	experiencia3 varchar(50),
	idioma1 varchar(20),
	idioma2 varchar(20),
	idioma3 varchar(20)
)

drop table Curriculos

