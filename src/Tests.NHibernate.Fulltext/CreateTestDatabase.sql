CREATE DATABASE [NHFullTextTest] CONTAINMENT = NONE 
GO

USE [NHFullTextTest]
GO

CREATE FULLTEXT CATALOG [NHFullTextTestCatalog]WITH ACCENT_SENSITIVITY = ON
AS DEFAULT
GO

CREATE TABLE [dbo].[Content](
	[Pagename] [nvarchar](20) NOT NULL,
	[Url] [nvarchar](30) NOT NULL,
	[Description] [text] NULL,
	[Keywords] [varchar](4000) NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Pagename] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  FullTextIndex     Script Date: 15.01.2015 23:36:08 ******/
CREATE FULLTEXT INDEX ON [dbo].[Content](
[Description] LANGUAGE 'English', 
[Keywords] LANGUAGE 'English', 
[Pagename] LANGUAGE 'English', 
[Url] LANGUAGE 'English')
KEY INDEX [PK_Content]ON ([NHFullTextTestCatalog], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

GO

INSERT content values ('home.asp','home.aspx','This is the home page','home,SQL')
GO
INSERT content values ('pagetwo.asp','/page2/pagetwo.aspx','NHibernate is great','second')
GO
INSERT content values ('pagethree.asp','/page3/pagethree.aspx','NHibernate.Spatial is the greatest','third')
GO 