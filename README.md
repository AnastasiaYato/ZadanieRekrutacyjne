# ZadanieRekrutacyjne
Zadanie do TKOMP 2022r

Query do stworzenia bazy danych, dodania przykładowych danych oraz stworzenie user'a do zalogowania się

Domyślna nazwa użytkownika oraz hasło: TestUser/password

CREATE DATABASE [DevData]; 
GO

CREATE TABLE [DevData].dbo.Table_A (Col_A1 int, Col_A2 varchar(10), Col_A3 date);

CREATE TABLE [DevData].dbo.Table_B (Col_B1 int, Col_B2 nchar(10), Col_B3 int);

CREATE TABLE [DevData].dbo.Table_C (Col_C1 varchar(10), Col_C2 timestamp, Col_C3 int, Col_C4 char(10) );

INSERT INTO  [DevData].dbo.Table_A VALUES (31,'test','2015-02-05'),(64,'lisek','2019-08-11')

INSERT INTO [DevData].dbo.Table_B VALUES (666,'AB',421),(2137,'AB',999),(69,'AB',0)

INSERT INTO [DevData].dbo.Table_C VALUES ('abc',null,123,'X'),('abd',null,333,'Y'),('abb',null,321,'Z')
--Procedura
USE DevData
GO
CREATE PROC [dbo].[SearchAllTables] AS BEGIN select c.name as column_name from sys.columns c join sys.tables t on t.object_id = c.object_id where type_name(user_type_id) = 'int' order by c.column_id; END
--
CREATE LOGIN TestUser WITH PASSWORD = 'password' ALTER LOGIN TestUser WITH PASSWORD = 'password', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF

ALTER SERVER ROLE sysadmin ADD MEMBER TestUser ;
GO

CREATE LOGIN TestUser WITH PASSWORD = 'password'
ALTER LOGIN TestUser
WITH PASSWORD = 'password',
CHECK_POLICY = OFF,
CHECK_EXPIRATION = OFF

ALTER SERVER ROLE sysadmin ADD MEMBER TestUser ;  
GO  
