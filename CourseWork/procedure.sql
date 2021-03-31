CREATE PROCEDURE [dbo].[AddUser]

	@firstName nvarchar(20) ,
	@lastName nvarchar(20),
	@login nvarchar(20),
	@email nvarchar(20),
	@organization nvarchar(20),
	@password nvarchar(20) ,
	@UserId int ,
	@Admin bit 
AS
    INSERT INTO Users (FirstName, LastName,Login,Email,Organization,Password)
    VALUES (@firstName, @lastName, @login,@email,@organization,@password)
   set @Admin = 0;
    SET @UserId=SCOPE_IDENTITY()
GO

drop procedure AddUser