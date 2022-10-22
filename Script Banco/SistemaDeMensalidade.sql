
-- Script Criacao do banco de dados
create database dbTeste
go

use dbTeste
go

-- Script Criacao Tabela Solicitante

create table Solicitante(
	Codigo int identity(1,1) PRIMARY KEY,
	Nome varchar(100) not null,
	Telefone varchar(100) not null,	
	DataCadastro datetime not null default(getdate()),
	DataAtualizacao datetime
)

-- Script Criacao Tabela Mensalidade

create table Mensalidade(
	Id int identity(1,1) PRIMARY KEY,
	Mes int not null,
	Ano int not null,
	Valor decimal not null,	
	Situacao varchar(100) not null,
	SolicitanteId int
	
)

--Criando procs
go
CREATE PROC sp_listarTodosSolicitantes
AS
    SELECT *
    FROM   solicitante
go

CREATE PROC sp_listarSolicitantePorId @Codigo INT
AS
    SELECT *
    FROM   solicitante
    WHERE  Codigo = @Codigo
go 

go
CREATE PROC sp_cadastrarSolicitante @Nome            VARCHAR(100),
									@Telefone		 VARCHAR(100)									
									
AS
    INSERT INTO solicitante
                (Nome,
                 Telefone)
    VALUES     (@Nome,
                @Telefone)
go 

CREATE PROC sp_atualizarSolicitante @Codigo              INT,
									@Nome            VARCHAR(100),
									@Telefone	     VARCHAR(100)
AS
    UPDATE solicitante
    SET    Nome = @Nome,          
           Telefone = @Telefone,  
           DataAtualizacao = GETDATE()
    WHERE  Codigo = @Codigo

go

go 

CREATE PROC sp_deletarSolicitantePorId @Id INT
AS
    DELETE 
    FROM   Solicitante
    WHERE  Codigo = @Id
go 


CREATE PROC sp_listarMensalidades
AS
    SELECT m.Id, 
		   m.Mes, 
		   m.Ano, 
		   m.Situacao, 
		   m.SolicitanteId,
		   m.valor,
		   s.Nome as SolicitanteNome
    FROM   Mensalidade m
		inner join Solicitante s on s.Codigo = m.SolicitanteId
go


go
CREATE PROC sp_gerarMensalidade		@Mes		       int,
									@Ano			   int,
									@Valor			   decimal,
									@Situacao          varchar(100),
									@SolicitanteId     int 
									
AS
    INSERT INTO Mensalidade
                (Mes,
                 Ano,
				 Valor,
				 Situacao,
				 SolicitanteId)
    VALUES     (@Mes,
                @Ano,
				@Valor,
				@Situacao,
				@SolicitanteId)

go 