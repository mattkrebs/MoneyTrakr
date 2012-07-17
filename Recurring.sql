
/****** Object:  Table [dbo].[Recurring]    Script Date: 07/12/2012 17:04:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Recurring](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[Automatic] [bit] NOT NULL,
	[RecurWeekly] [bit] NOT NULL,
	[RecurBiWeekly] [bit] NOT NULL,
	[RecurMontly] [bit] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Recurring] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

