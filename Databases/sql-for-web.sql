use master
go

drop database if exists QL_dichVuNauTiecLanHue
go

create database QL_dichVuNauTiecLanHue
go

use QL_dichVuNauTiecLanHue
go

/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 18/10/2023 14:20:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 18/10/2023 14:20:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 18/10/2023 14:20:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 18/10/2023 14:20:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 18/10/2023 14:20:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 18/10/2023 14:20:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 18/10/2023 14:20:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO


/*==============================================================*/
/* Table: CUSTOMER                                              */
/*==============================================================*/
create table CUSTOMER (
   CUSTOMER_ID          int identity(1,1)                  not null,
   CUS_NAME             nvarchar(100)         null,
   PHONE_NUMBER         varchar(15)          null,
   SEX                  bit                  null,
   ADDRESS              ntext                 null,
   CITIZEN_NUMBER       char(12)          null,
   --ACCOUNT_ID           int identity(1,1)                  not null,
   constraint PK_CUSTOMER primary key (CUSTOMER_ID),
   --constraint FK_CUSTOMER_ACCOUNT_ID foreign key (ACCOUNT_ID) references ACCOUNT (ACCOUNT_ID),
)
go


/*==============================================================*/
/* Table: DISH_TYPE                                             */
/*==============================================================*/
create table DISH_TYPE (
   DISH_TYPE_ID         int identity(1,1)                  not null,
   TYPE_NAME            nvarchar(100)          not null,
   constraint PK_DISH_TYPE primary key (DISH_TYPE_ID),
)
go



/*==============================================================*/
/* Table: UNIT                                                  */
/*==============================================================*/
create table UNIT (
   UNIT_ID              int identity(1,1)                  not null,
   UNIT_NAME			nvarchar(255)	     not null,
   DESCRIPTION          nvarchar(255)        null,
   constraint PK_UNIT primary key (UNIT_ID),
)
go



/*==============================================================*/
/* Table: DISH                                                  */
/*==============================================================*/
create table DISH (
   DISH_ID              int identity(1,1)                  not null,
   DISH_NAME            nvarchar(255)         null,
   PRICE                money                not null default 0,
   DISH_TYPE_ID         int                  not null,
   UNIT_ID				int					 not null,
   constraint PK_DISH primary key (DISH_ID),
   constraint CK_DISH_PRICE check (PRICE >= 0),
   constraint PK_DISH_DISH_TYPE_ID foreign key (DISH_TYPE_ID) references DISH_TYPE (DISH_TYPE_ID),
   constraint PK_DISH_UNIT_ID foreign key (UNIT_ID) references UNIT (UNIT_ID),
)
go


/*==============================================================*/
/* Table: PARTY_TYPE                                            */
/*==============================================================*/
create table PARTY_TYPE (
   PARTY_TYPE_ID        int identity(1,1)                  not null,
   NAME                 nvarchar(200)         not null,
   constraint PK_PARTY_TYPE primary key (PARTY_TYPE_ID),
)
go



/*==============================================================*/
/* Table: PARTY                                                 */
/*==============================================================*/
create table PARTY (
   PARTY_ID             int identity(1,1)                  not null,
   PARTY_NAME           ntext                not null,
   QUANTITY             int                  null, -- Số lượng bàn
   DATE                 date             not null,
   TIME                 time             null,
   LOCATION             ntext                 null,
   NOTE                 ntext                 null,
   STATUS               nvarchar(255)         null,
   HAS_MENU             bit					 not null default (0),
   PARTY_TYPE_ID        int                  not null,
   CUSTOMER_ID          int                  not null,
   constraint PK_PARTY primary key (PARTY_ID),
   constraint CK_PARTY_DATE check (DATE > getdate()),
   constraint FK_PARTY_PARTY_TYPE_ID foreign key (PARTY_TYPE_ID) references PARTY_TYPE (PARTY_TYPE_ID),
   constraint FK_PARTY_CUSTOMER_ID foreign key (CUSTOMER_ID) references CUSTOMER (CUSTOMER_ID)
)
go


/*==============================================================*/
/* Table: INVOICE                                               */
/*==============================================================*/
create table INVOICE (
   INVOICE_ID           int identity(1,1)                  not null,
   INVOICE_DATE         datetime             not null default getdate(),
   PAYMENT_TIME         datetime             null,
   TOTAL_PRICE          money                null,
   DEPOSIT              money                null,
   TOTAL                money                null,
   PARTY_ID             int                  not null,
   constraint PK_INVOICE primary key (INVOICE_ID),
   constraint CK_INVOICE_TOTAL_PRICE check (TOTAL_PRICE >= 0),
   constraint CK_INVOICE_DEPOSIT check (DEPOSIT >= 0),
   constraint CK_INVOICE_TOTAL check (TOTAL >= 0),
   constraint FK_INVOICE_PARTY_ID foreign key (PARTY_ID) references PARTY (PARTY_ID),
)
go




/*==============================================================*/
/* Table: DETAIL_INVOICE                                        */
/*==============================================================*/
create table DETAIL_INVOICE (
   DETAIL_INVOICE_ID    int identity(1,1)                  not null,
   NUMBER               int                  null,
   PRICE				money				 not null,
   AMOUNT               money                not null default 0,
   DISH_ID              int                  not null,
   INVOICE_ID           int                  not null,
   constraint PK_DETAIL_INVOICE primary key (DETAIL_INVOICE_ID),
   constraint CK_DETAIL_INVOICE check (AMOUNT >= 0),
   constraint FK_DETAIL_INVOICE_DISH_ID foreign key (DISH_ID) references DISH (DISH_ID),
   constraint FK_DETAIL_INVOICE_INVOICE_ID foreign key (INVOICE_ID) references INVOICE (INVOICE_ID),
)
go



/*==============================================================*/
/* Table: STAFF_TYPE                                            */
/*==============================================================*/
create table STAFF_TYPE (
   STAFF_TYPE_ID        int identity(1,1)                  not null,
   NAME                 nvarchar(50)          null,
   constraint PK_STAFF_TYPE primary key (STAFF_TYPE_ID),
)
go


/*==============================================================*/
/* Table: STAFF                                                 */
/*==============================================================*/
create table STAFF (
   STAFF_ID             int identity(1,1)                  not null,
   STAFF_NAME           nvarchar(100)         null,
   PHONE_NUMBER         nvarchar(15)          null,
   SEX                  bit                  null,
   ADDRESS              ntext                 null,
   CITIZEN_NUMBER       nvarchar(12)          null,
   STAFF_TYPE_ID        int                  not null,
   USERS_ID             nvarchar(450)         null,
   constraint PK_STAFF primary key (STAFF_ID),
   constraint FK_STAFF_STAFF_TYPE_ID foreign key (STAFF_TYPE_ID) references STAFF_TYPE (STAFF_TYPE_ID),
   constraint FK_STAFF_USERS_ID foreign key (USERS_ID) references ASPNETUSERS (Id),
)
go



/*==============================================================*/
/* Table: WORK                                                  */
/*==============================================================*/
create table WORK (
   STAFF_ID             int                  not null,
   PARTY_ID             int                  not null,
   SALARY               money                null default 0,
   constraint PK_WORK primary key (STAFF_ID, PARTY_ID),
   constraint PK_WORK_SLARY check (SALARY >= 0),
   constraint FK_WORK_STAFF_ID foreign key (STAFF_ID) references STAFF (STAFF_ID),
   constraint FK_WORK_PARTY_ID foreign key (PARTY_ID) references PARTY (PARTY_ID),
)
go



-----------------------------------------------------------
--/*==============================================================*/
--/* Phân quyền: Admin                                            */
--/*==============================================================*/
--drop login LH_Admin
--GO

--USE master
--GO
--CREATE LOGIN LH_Admin
--	WITH PASSWORD = N'123456',
--	CHECK_EXPIRATION = OFF,
--	CHECK_POLICY = OFF,
--	DEFAULT_DATABASE = QL_dichVuNauTiecLanHue
--GO

--USE QL_dichVuNauTiecLanHue
--GO
--CREATE USER LH_Admin
--	FOR LOGIN LH_Admin
--GO

--USE QL_dichVuNauTiecLanHue
--GO
--Sp_addrolemember 'db_owner', 'LH_Admin'
--Go


--/*==============================================================*/
--/* Thêm nhóm quyền:                                             */
--/*==============================================================*/

---- Xem: DISH
--USE QL_dichVuNauTiecLanHue
--GO
--Sp_addrole 'see_dish'

--GRANT SELECT 
--ON DISH
--TO see_dish
--GO



---- Xem: PARTY
--USE QL_dichVuNauTiecLanHue
--GO
--Sp_addrole 'see_party'

--GRANT SELECT 
--ON PARTY
--TO see_party
--GO


--/*==============================================================*/
--/* Phân quyền: Staff                                            */
--/*==============================================================*/
--drop login LH_Staff
--GO

--USE master
--GO
--CREATE LOGIN LH_Staff
--	WITH PASSWORD = N'123456',
--	CHECK_EXPIRATION = OFF,
--	CHECK_POLICY = OFF,
--	DEFAULT_DATABASE = QL_dichVuNauTiecLanHue
--GO

--USE QL_dichVuNauTiecLanHue
--GO
--CREATE USER LH_Staff
--	FOR LOGIN LH_Staff
--GO

---- Thêm vào nhóm quyền
--USE QL_dichVuNauTiecLanHue
--GO
--Sp_addrolemember 'see_dish', 'LH_Staff'

--GO
--Sp_addrolemember 'see_party', 'LH_Staff'
--GO

---- Gán quyền: Xem, Cập nhật STAFF
--GRANT SELECT, UPDATE
--ON STAFF TO LH_Staff
--GO

---- Gán quyền: Thêm, Xem, Cập nhật, Xoá WORK
--GRANT INSERT, SELECT, UPDATE, DELETE
--ON WORK TO LH_Staff
--GO

--/*==============================================================*/
--/* Phân quyền: Staff                                            */
--/*==============================================================*/
--drop login LH_Guest
--GO

--USE master
--GO
--CREATE LOGIN LH_Guest
--	WITH PASSWORD = N'123456',
--	CHECK_EXPIRATION = OFF,
--	CHECK_POLICY = OFF,
--	DEFAULT_DATABASE = QL_dichVuNauTiecLanHue
--GO

--USE QL_dichVuNauTiecLanHue
--GO
--CREATE USER LH_Guest
--	FOR LOGIN LH_Guest
--GO

---- Thêm vào nhóm quyền
--USE QL_dichVuNauTiecLanHue
--GO
--Sp_addrolemember 'see_dish', 'LH_Guest'
--GO



/*==============================================================*/
/* Trigger                                                      */
/*==============================================================*/

-- Trigger: DETAIL_INVOICE


-- Trigger: DETAIL_INVOICE
--CREATE TRIGGER Trg_After_DetailInvoice_Update
--ON DETAIL_INVOICE
--AFTER UPDATE
--AS
--BEGIN
--	UPDATE INVOICE
--	SET TOTAL_PRICE = (
--		SELECT SUM(INS.AMOUNT)
--		FROM DETAIL_INVOICE DI
--		JOIN inserted INS ON INS.DETAIL_INVOICE_ID = DI.DETAIL_INVOICE_ID
--	)
--	FROM INVOICE INV
--	JOIN inserted INS ON INV.INVOICE_ID = INS.INVOICE_ID
--END