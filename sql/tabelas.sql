CREATE DATABASE SuperHero;
use SuperHero;

DROP TABLE [dbo].[Editor]

CREATE TABLE [dbo].[Editor] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
    CONSTRAINT PK_Editor_Id PRIMARY KEY CLUSTERED (Id)
)

SELECT * FROM Editor
INSERT INTO EDITOR (Name) VALUES ('Bandai')
SELECT SCOPE_IDENTITY();

CREATE TABLE [dbo].[Hero] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [IdEditor] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
    [Created] DateTime NOT NULL
)



SELECT * FROM Hero
UPDATE HERO SET Age=91 where id = 4

SELECT H.Id,
            H.Name,
            E.Id as IdEditor,
            E.Name as Editor,
            H.Age,
            H.Created
        FROM VILAO H
      JOIN Editor E ON H.IdEditor = E.Id

    SELECT * FROM Hero





ALTER TABLE [dbo].[Hero]
   ADD CONSTRAINT PK_Hero_Id PRIMARY KEY CLUSTERED (Id);

  ALTER TABLE [dbo].[Hero]
   ADD CONSTRAINT FK_Hero_Editor FOREIGN KEY (IdEditor)
      REFERENCES [dbo].[Editor] (Id)






/*CREATE TABLE Production.TransactionHistoryArchive1
(
    TransactionID int IDENTITY (1,1) NOT NULL
    , CONSTRAINT PK_TransactionHistoryArchive_TransactionID PRIMARY KEY CLUSTERED (TransactionID)
)*/

INSERT INTO 
    Editor (Name)
VALUES ('Marvel')


INSERT INTO 
    HERO (Name, IdEditor, Age, Created)
VALUES ('Homem de Ferro', 1, 40, getdate())

SELECT *
    FROM HERO 
WHERE Created BETWEEN '2020-10-10 16:45:00' 
                    and '2020-10-10 16:46:59'

UPDATE HERO 
    SET Name='Batman', Editor='DC'
WHERE Id = 1

DELETE 
    FROM Hero
WHERE Id = 1


SELECT H.Name AS NomeHeroi, E.Name AS NomeEditora
    FROM HERO H
    JOIN EDITOR E ON H.IdEditor = E.Id
	
	
/*
CREATE TABLE [dbo].[Profile] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	CONSTRAINT PK_Profile_Id PRIMARY KEY CLUSTERED (Id)
)*/

/*
CREATE TABLE [dbo].[Users] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [IdProfile] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](255) NOT NULL,
    [Created] DateTime NOT NULL
)*/

  /* ALTER TABLE [dbo].[Users]
		ADD CONSTRAINT FK_Users_Profile FOREIGN KEY (IdProfile)
      REFERENCES [dbo].[Profile] (Id)/*


INSERT INTO 
    Profile (Description)
VALUES ('Produtor')

SELECT * FROM dbo.Users