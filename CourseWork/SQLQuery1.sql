CREATE database LogisticApp;

Use LogisticApp
Create table Users
( FirstName nvarchar(20) not null,
  LastName nvarchar(20) not null,
  Email nvarchar(20) not null,
  Login nvarchar(20) not null primary key,
  Country nvarchar(20),
  Organization nvarchar(20) not null,
  Password nvarchar(20) not null,
  TelephoneNumber nvarchar(15) ,
  Admin bit not null
)

Use LogisticApp
Create table Goods
( 
	GoodId int identity(1,1) not null primary key,
	GoodName nvarchar(20) not null,
	GoodType nvarchar(20) not null,
	Weight decimal not null,
	Size decimal not null
)


Use LogisticApp
Create table Orders
( 
    OrderId int identity(1,1) not null primary key,
	Good int not null foreign key references Goods(GoodId),
	Customer nvarchar(20) not null foreign key references Users(Login),
	TransportType nvarchar(20) not null,
	DepartureAddress nvarchar(50) not null,
	DestinationAddress nvarchar(50) not null,
	TimeOfLoading date not null,
	TimeOfUnloading date not null,
	Accepted bit not null,
	Complited bit not null

)

INSERT into Users values('�������', '������','petral@gmail.com','AlPet','��������','NatLan','pass','375292298912',0)
INSERT into Users values('������', '�������','ZyBmIha@gmail.com','ZybM','������','�������','gaz','375336245986',0)
INSERT into Users values('��������', '�������','workSid@mail.ru','Sidr','��������','LogComp','admin','375292242287',1)
INSERT into Users values('����'	,'���������'	,'mazolyaka@mail.ru'	,'mazolyaka'	,'����������'	,'SmokePeople',	'meel'	,'37526543298',0)
INSERT into Users values('��������', '�������','workSid@mail.ru','admin','��������','LogComp','admin','375292242287',1)
INSERT into Users values('��������', '�������','workSid@mail.ru','user','��������','LogComp','pass','375292242287',1)

INSERT into Goods values('����', '�������', 2000 , 100)
INSERT into Goods values('������', '������������', 900 , 200)


INSERT into Orders values(1, 'user','���������','Germany,Munich','Australia,Sydney','2010-11-10','2011-01-15',0,0)
INSERT into Orders values(1, 'user','�������','Russia, Vladivostok','Spain, Madrid','2013-11-10','2014-01-15',0,0)
INSERT into Orders values(1, 'AlPet','���������������','Armenia,Erevan','Belarus, Vitebsk','2013-11-10','2014-01-15',0,0)
INSERT into Orders values(1, 'AlPet','�������','Australia,Sydney','USA, Los Angeles','2013-11-10','2014-01-15',0,0)
INSERT into Orders values(2, 'user','����������','Belarus,Minsk','Italy, Milan','2019-12-20','2019-12-26',0,0)
INSERT into Orders values(1, 'ZybM','���������������','Belarus,Brest','Russia,Saratov','2020-01-20','2020-07-26',0,0)



drop table Orders
drop table Users
drop Table Goods



select * from Orders
select * from Goods
select * from Users


Delete from Users Where login ='user'


select GoodId From Goods where GoodName = 'goodname' and GoodType = 'goodType'
select GoodId From Goods where GoodName = 'goodname' and GoodType = 'goodType'

update Users Set admin = 'true'  where Login = 'userr'