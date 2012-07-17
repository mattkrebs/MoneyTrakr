
/****** Object:  Table [dbo].[User]    Script Date: 07/12/2012 17:04:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[ID] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[AccountID] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

Server=107d983c-033c-448d-8b99-a090017367f8.sqlserver.sequelizer.com;Database=db107d983c033c448d8b99a090017367f8;User ID=dnjqajdmneiuncvj;Password=YCMHgP5T7gAZ877HdWKZ8xcbfdxTgmwmkcURTTgBKKQ34qEHkTTSC8wAALCcqjMU;