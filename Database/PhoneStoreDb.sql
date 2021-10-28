create database PhoneStore
go

use PhoneStore
go

create table Advertisement
(
	ID int primary key identity(1,1),
	Image varchar(500),
)
go

create table Category
(
	ID int primary key identity(1,1),
	DisplayName nvarchar(100)
)
go

--insert Advertisement
insert into Advertisement values('advertisement1.jpg')
insert into Advertisement values('advertisement2.jpg')
insert into Advertisement values('advertisement3.jpg')

--insert Category
insert into Category values(N'Điện thoại')
insert into Category values(N'Laptop')
go

/*drop database PhoneStore*/
