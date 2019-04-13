SET IDENTITY_INSERT [dbo].[Patient]  ON

GO
INSERT INTO [dbo].[Patient] ([Id],[FirstName],[LastName],[Email],[Phone],[Address],[DentistId]) VALUES (1,'Liz', 'Laughlin', 'liz@gmail.com', '4388782318', 'Rue Guy', 2);

GO
INSERT INTO [dbo].[Patient] ([Id],[FirstName],[LastName],[Email],[Phone],[Address],[DentistId]) VALUES (2,'Joshua', 'Gordan', 'josh@gmail.com', '4388782318', 'Grand Blvd', 3);

GO
INSERT INTO [dbo].[Patient] ([Id],[FirstName],[LastName],[Email],[Phone],[Address],[DentistId]) VALUES (3,'Jessy', 'Rosen', 'jessy@gmail.com', '4388782318', 'Rue Prince', 4);

GO
SET IDENTITY_INSERT [dbo].[Patient]  OFF