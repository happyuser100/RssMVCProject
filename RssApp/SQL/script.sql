USE [RssDB]
GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 07-Jan-21 9:15:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[FavoriteID] [int] IDENTITY(1,1) NOT NULL,
	[MenuID] [int] NOT NULL,
	[MenuLink] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Favorites] PRIMARY KEY CLUSTERED 
(
	[FavoriteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuItems]    Script Date: 07-Jan-21 9:15:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItems](
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](100) NOT NULL,
	[NavURL] [nvarchar](200) NOT NULL,
	[ParentMenuID] [int] NOT NULL,
 CONSTRAINT [PK_MenuItems_1] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[MenuItems] ON 

INSERT [dbo].[MenuItems] ([MenuID], [MenuName], [NavURL], [ParentMenuID]) VALUES (1, N'Favorites', N'#', 0)
INSERT [dbo].[MenuItems] ([MenuID], [MenuName], [NavURL], [ParentMenuID]) VALUES (2, N'Feeds', N'#', 0)
INSERT [dbo].[MenuItems] ([MenuID], [MenuName], [NavURL], [ParentMenuID]) VALUES (4, N'Engadget', N'https://www.nasa.gov/rss/dyn/lg_image_of_the_day.rss', 2)
INSERT [dbo].[MenuItems] ([MenuID], [MenuName], [NavURL], [ParentMenuID]) VALUES (6, N'Msdn', N'https://www.nasa.gov/rss/dyn/armstrong_news.rss', 2)
INSERT [dbo].[MenuItems] ([MenuID], [MenuName], [NavURL], [ParentMenuID]) VALUES (7, N'101 Cookbooks', N'https://www.nasa.gov/rss/dyn/centers/stennis/news/latest_news.rss', 2)
SET IDENTITY_INSERT [dbo].[MenuItems] OFF
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Favorites_MenuItems] FOREIGN KEY([MenuID])
REFERENCES [dbo].[MenuItems] ([MenuID])
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK_Favorites_MenuItems]
GO
