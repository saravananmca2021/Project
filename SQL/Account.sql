USE [Bank]
GO

ALTER TABLE [dbo].[Account] DROP CONSTRAINT [FK__Account__Custome__5441852A]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 08-02-2023 02:11:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Account]') AND type in (N'U'))
DROP TABLE [dbo].[Account]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 08-02-2023 02:11:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumberGenerated] [nvarchar](90) NULL,
	[DateCreated] [datetime] NULL,
	[DateLastUpdated] [datetime] NULL,
	[CurrentAccountBalance] [numeric](18, 0) NULL,
	[CustomerID] [numeric](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[User] ([CustomerID])
GO


