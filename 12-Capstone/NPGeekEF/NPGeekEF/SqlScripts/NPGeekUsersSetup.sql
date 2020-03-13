USE master
Go

-- Delete previously existing DB
DROP DATABASE IF Exists NPGeekUsers

-- Create a new database
CREATE DATABASE NPGeekUsers
Go

-- Change to the new DB
USE NPGeekUsers
GO

CREATE TABLE [User]
(
	Id			int	not null Primary Key identity(1,1),
	UserName	nvarchar(50)	not null,
	Password	nvarchar(50)	not null,
	Salt		varchar(50)	not null,
	Role		varchar(50)	default('user'),
	TempPref	varchar(1) default('F'),
)

INSERT INTO [user] (UserName, Password, Salt, Role, TempPref)
Values 
('NPGeekAdmin@te.com', 'HJ3XrE0nHys3xZWqYq6/ZAdV4aU=', '4ODfIqplEYg=', 'Admin', 'F'),
('NPGeekUser@te.com', 'a+AtW3V0RaJxe7RHwdvWjoEnQ8M=', 'FrKbXsBX/Ws=', 'User', 'F');