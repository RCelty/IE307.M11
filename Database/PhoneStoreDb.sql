create database PhoneStore
go

use PhoneStore
go

create table Category
(
	ID int primary key identity(1,1),
	DisplayName nvarchar(100)
)
go

insert into Category values(N'Điện thoại')
insert into Category values(N'Laptop')
go

/*drop database PhoneStore*/
