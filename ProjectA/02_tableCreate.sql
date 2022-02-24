USE SuperherosDb

CREATE TABLE Superhero(
Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
Name nvarchar(50) NULL,
Alias nvarchar(50) NULL,
Origin nvarchar(50) NULL,
);


CREATE TABLE Assistant(
Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
Name nvarchar(50) NULL,
);


CREATE TABLE Power(
Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
Name nvarchar(50) NULL,
Description nvarchar(60) NULL,
);