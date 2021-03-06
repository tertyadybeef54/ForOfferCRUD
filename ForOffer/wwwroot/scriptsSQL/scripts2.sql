USE [ForOffer]
GO
/****** Object:  Table [dbo].[mf_general_notes_offer]    Script Date: 9/9/2021 17:33:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mf_general_notes_offer](
	[general_notes_offer_id] [int] IDENTITY(1,1) NOT NULL,
	[general_notes_text] [varchar](max) NOT NULL,
	[general_notes_services_id] [int] NOT NULL,
	[reg_state] [int] NOT NULL,
 CONSTRAINT [PK_mf_general_notes_offer] PRIMARY KEY CLUSTERED 
(
	[general_notes_offer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mf_general_observations_offer]    Script Date: 9/9/2021 17:33:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mf_general_observations_offer](
	[general_observation_offer_id] [int] IDENTITY(1,1) NOT NULL,
	[general_observation_text] [varchar](max) NOT NULL,
	[general_observation_services_id] [int] NOT NULL,
	[reg_state] [int] NOT NULL,
 CONSTRAINT [PK_mf_general_observations_offer] PRIMARY KEY CLUSTERED 
(
	[general_observation_offer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mf_services_place]    Script Date: 9/9/2021 17:33:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mf_services_place](
	[services_place_id] [int] IDENTITY(1,1) NOT NULL,
	[services_places] [varchar](50) NOT NULL,
 CONSTRAINT [PK_mf_services_place] PRIMARY KEY CLUSTERED 
(
	[services_place_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[mf_general_notes_offer]  WITH CHECK ADD  CONSTRAINT [FK_mf_general_notes_offer_mf_services_place] FOREIGN KEY([general_notes_services_id])
REFERENCES [dbo].[mf_services_place] ([services_place_id])
GO
ALTER TABLE [dbo].[mf_general_notes_offer] CHECK CONSTRAINT [FK_mf_general_notes_offer_mf_services_place]
GO
ALTER TABLE [dbo].[mf_general_observations_offer]  WITH CHECK ADD  CONSTRAINT [FK_mf_general_observations_offer_mf_services_place] FOREIGN KEY([general_observation_services_id])
REFERENCES [dbo].[mf_services_place] ([services_place_id])
GO
ALTER TABLE [dbo].[mf_general_observations_offer] CHECK CONSTRAINT [FK_mf_general_observations_offer_mf_services_place]
GO
