create database TaskAppDB
go

use TaskAppDB
go

CREATE TABLE Task
(
    Id uniqueidentifier NOT NULL,
    Description NVARCHAR(250) NOT NULL,
	Date datetime NOT NULL,
	Status INT NOT NULL,
    InsertedAt  datetime2,
    UpdatedAt datetime2
	PRIMARY KEY CLUSTERED 
	(
		Id ASC
	)
);
go
