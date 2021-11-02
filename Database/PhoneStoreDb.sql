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

create table Brand
(
	ID int primary key identity(1,1),
	DisplayName nvarchar(100),
	Image varchar(500),
)
go

create table Product
(
	ID int primary key identity(1,1),
	DisplayName nvarchar(100),
	Price int,
	DiscountPercent int,
	Rating float,
	ViewCount int,
	CommentCount int,
	Image1 varchar(500),
	Image2 varchar(500),
	Image3 varchar(500),
	Image4 varchar(500),
	Description nvarchar(500),
	CategoryID int foreign key references Category(ID),
	BrandID int foreign key references Brand(ID),
)
go

create table ProductDetail
(
	ID int primary key identity(1,1),
	ProductID int foreign key references Product(ID),
	DetailName nvarchar(100),
	DetailDescription nvarchar(500),
)
go

--insert Advertisement
insert into Advertisement values('advertisement1.jpg')
insert into Advertisement values('advertisement2.jpg')
insert into Advertisement values('advertisement3.jpg')
go

--insert Category
insert into Category values(N'Điện thoại')
insert into Category values(N'Laptop')
go

--insert Brand
insert into Brand values(N'Samsung', 'samsung.jpg')
insert into Brand values(N'iPhone', 'iphone.jpg')
insert into Brand values(N'Oppo', 'oppo.jpg')
go

--insert Product
insert into Product values(N'Điện thoại Samsung Galaxy Z Fold3 5G 512GB', 43990000, 10, 0, 0, 0, 'samsung1_1.jpg', 'samsung1_2.jpg', 'samsung1_3.jpg', 'samsung1_4.jpg', N'Galaxy Z Fold3 5G đánh dấu bước tiến mới của Samsung trong phân khúc điện thoại gập cao cấp khi được cải tiến về độ bền cùng những nâng cấp đáng giá về cấu hình hiệu năng, hứa hẹn sẽ mang đến trải nghiệm sử dụng đột phá cho người dùng.', 1, 1)
insert into Product values(N'Điện thoại iPhone 12 64GB', 20490000, 5, 0, 0, 0, 'iphone1_1.jpg', 'iphone1_2.jpg', 'iphone1_3.jpg', 'iphone1_4.jpg', N'Trong những tháng cuối năm 2020, Apple đã chính thức giới thiệu đến người dùng cũng như iFan thế hệ iPhone 12 series mới với hàng loạt tính năng bứt phá, thiết kế được lột xác hoàn toàn, hiệu năng đầy mạnh mẽ và một trong số đó chính là iPhone 12 64GB.', 1, 2)
insert into Product values(N'Điện thoại OPPO Reno6 Z 5G', 9490000, 0, 0, 0, 0, 'oppo1_1.jpg', 'opppo1_2.jpg', 'oppo1_3.jpg', 'oppo1_4.jpg', N'Reno6 Z 5G đến từ nhà OPPO với hàng loạt sự nâng cấp và cải tiến không chỉ ngoại hình bên ngoài mà còn sức mạnh bên trong.', 1, 3)
go

--insert ProductDetail
insert into ProductDetail values(1, N'Màn hình', N'Dynamic AMOLED 2X, Chính 7.6" & Phụ 6.2", Full HD+')
insert into ProductDetail values(1, N'Hệ điều hành', N'Android 11')
insert into ProductDetail values(1, N'Camera sau', N'3 camera 12 MP')
insert into ProductDetail values(1, N'Camera trước', N'10 MP & 4 MP')
insert into ProductDetail values(1, N'Chip', N'Snapdragon 888')
insert into ProductDetail values(1, N'RAM', N'12 GB')
insert into ProductDetail values(1, N'Bộ nhớ trong', N'512 GB')
insert into ProductDetail values(1, N'SIM', N'2 Nano SIM, Hỗ trợ 5G')
insert into ProductDetail values(1, N'Pin, Sạc', N'4400 mAh, 25 W')

insert into ProductDetail values(2, N'Màn hình', N'OLED, 6.1", Super Retina XDR')
insert into ProductDetail values(2, N'Hệ điều hành', N'iOS 14')
insert into ProductDetail values(2, N'Camera sau', N'2 camera 12 MP')
insert into ProductDetail values(2, N'Camera trước', N'12 MP')
insert into ProductDetail values(2, N'Chip', N'Apple A14 Bionic')
insert into ProductDetail values(2, N'RAM', N'4 GB')
insert into ProductDetail values(2, N'Bộ nhớ trong', N'64 GB')
insert into ProductDetail values(2, N'SIM', N'1 Nano SIM & 1 eSIM, Hỗ trợ 5G')
insert into ProductDetail values(2, N'Pin, Sạc', N'2815 mAh, 20 W')

insert into ProductDetail values(3, N'Màn hình', N'AMOLED, 6.43", Full HD+')
insert into ProductDetail values(3, N'Hệ điều hành', N'Android 11')
insert into ProductDetail values(3, N'Camera sau', N'Chính 64 MP & Phụ 8 MP, 2 MP')
insert into ProductDetail values(3, N'Camera trước', N'32 MP')
insert into ProductDetail values(3, N'Chip', N'MediaTek Dimensity 800U 5G')
insert into ProductDetail values(3, N'RAM', N'8 GB')
insert into ProductDetail values(3, N'Bộ nhớ trong', N'128 GB')
insert into ProductDetail values(3, N'SIM', N'2 Nano SIM, Hỗ trợ 5G')
insert into ProductDetail values(3, N'Pin, Sạc', N'4310 mAh, 30 W')
go

--drop database PhoneStore
