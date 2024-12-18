USE [Career]
GO
/****** Object:  Table [dbo].[tbl_ApplyJob]    Script Date: 10/29/2024 10:30:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ApplyJob](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[IDUser] [bigint] NOT NULL,
	[IDJob] [bigint] NOT NULL,
	[AppliedDate] [date] NULL,
	[Name] [nvarchar](200) NULL,
	[Mail] [char](50) NULL,
	[CV] [ntext] NULL,
	[CoverLetter] [ntext] NULL,
	[ApplyStatus] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Category]    Script Date: 10/29/2024 10:30:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Category](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[image] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Company]    Script Date: 10/29/2024 10:30:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Company](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [ntext] NULL,
	[Avt] [ntext] NULL,
	[PhoneNumber] [char](10) NULL,
	[Location] [nvarchar](200) NULL,
	[Email] [char](50) NULL,
	[PassWord] [char](100) NULL,
	[CreatedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_CV]    Script Date: 10/29/2024 10:30:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CV](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[IDUser] [bigint] NOT NULL,
	[CreationDate] [date] NULL,
	[FileCV] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Job]    Script Date: 10/29/2024 10:30:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Job](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Detail] [ntext] NULL,
	[IDCompany] [bigint] NOT NULL,
	[Requirement] [ntext] NULL,
	[Description] [ntext] NULL,
	[Benefit] [ntext] NULL,
	[Offer] [float] NULL,
	[Industry] [nvarchar](200) NULL,
	[CreationDate] [date] NULL,
	[LimitDate] [date] NULL,
	[Total] [int] NULL,
	[Type] [nvarchar](100) NULL,
	[Sex] [varchar](20) NULL,
	[Location] [nvarchar](250) NULL,
	[IDCategory] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_User]    Script Date: 10/29/2024 10:30:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[DoB] [date] NULL,
	[Major] [ntext] NOT NULL,
	[JobCity] [ntext] NULL,
	[ProfileUser] [ntext] NULL,
	[Skill] [ntext] NULL,
	[Expected] [float] NULL,
	[Experiences] [int] NULL,
	[Position] [nvarchar](200) NULL,
	[Email] [char](50) NULL,
	[PassWord] [char](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tbl_ApplyJob]  WITH CHECK ADD  CONSTRAINT [fk_apply_job] FOREIGN KEY([IDJob])
REFERENCES [dbo].[tbl_Job] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_ApplyJob] CHECK CONSTRAINT [fk_apply_job]
GO
ALTER TABLE [dbo].[tbl_ApplyJob]  WITH CHECK ADD  CONSTRAINT [fk_apply_user] FOREIGN KEY([IDUser])
REFERENCES [dbo].[tbl_User] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_ApplyJob] CHECK CONSTRAINT [fk_apply_user]
GO
ALTER TABLE [dbo].[tbl_CV]  WITH CHECK ADD  CONSTRAINT [fk_cv_cv] FOREIGN KEY([IDUser])
REFERENCES [dbo].[tbl_User] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_CV] CHECK CONSTRAINT [fk_cv_cv]
GO
ALTER TABLE [dbo].[tbl_Job]  WITH CHECK ADD  CONSTRAINT [fk_job_cat] FOREIGN KEY([IDCategory])
REFERENCES [dbo].[tbl_Category] ([ID])
GO
ALTER TABLE [dbo].[tbl_Job] CHECK CONSTRAINT [fk_job_cat]
GO
ALTER TABLE [dbo].[tbl_Job]  WITH CHECK ADD  CONSTRAINT [fk_job_company] FOREIGN KEY([IDCompany])
REFERENCES [dbo].[tbl_Company] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Job] CHECK CONSTRAINT [fk_job_company]
GO
