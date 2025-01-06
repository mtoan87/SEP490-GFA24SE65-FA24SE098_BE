CREATE DATABASE SOSChildrenVillageDB
use SOSChildrenVillageDB
GO
/****** Object:  Table [dbo].[AcademicReport]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicReport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Diploma] [nvarchar](255) NULL,
	[SchoolLevel] [nvarchar](255) NULL,
	[Child_Id] [varchar](100) NULL,
	[School_Id] [varchar](100) NULL,
	[GPA] [decimal](18, 2) NULL,
	[SchoolReport] [nvarchar](max) NULL,
	[Semester] [nvarchar](50) NULL,
	[AcademicYear] [varchar](100) NULL,
	[Remarks] [nvarchar](max) NULL,
	[Achievement] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NULL,
	[Class] [nvarchar](50) NULL,
	[Feedback] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AcademicReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Activity_Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Address] [nvarchar](500) NULL,
	[Village_Id] [varchar](100) NULL,
	[ActivityType] [nvarchar](50) NULL,
	[TargetAudience] [nvarchar](255) NULL,
	[Organizer] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Event_Id] [int] NULL,
	[Budget] [decimal](18, 2) NULL,
	[Feedback] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
)
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[House_id] [varchar](100) NULL,
	[UserAccount_Id] [varchar](100) NULL,
	[BookingSlot_Id] [int] NULL,
	[Visitday] [date] NULL,
	[Status] [nvarchar](100) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingSlot]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingSlot](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Start_time] [time](7) NULL,
	[End_time] [time](7) NULL,
	[Status] [nvarchar](100) NULL,
	[Slot_time] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Child]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Child](
	[Id] [varchar](100) NOT NULL,
	[Child_Name] [nvarchar](100) NULL,
	[Health_Status] [nvarchar](100) NULL,
	[House_Id] [varchar](100) NULL,
	[School_Id] [varchar](100) NULL,
	[FacilitiesWallet_Id] [int] NULL,
	[SystemWallet_Id] [int] NULL,
	[FoodStuffWallet_Id] [int] NULL,
	[HealthWallet_Id] [int] NULL,
	[NecessitiesWallet_Id] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[CurrentAmount] [decimal](18, 2) NULL,
	[AmountLimit] [decimal](18, 2) NULL,
	[Gender] [nvarchar](100) NULL,
	[Dob] [datetime] NOT NULL,
	[Status] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) 
GO
/****** Object:  Table [dbo].[ChildNeeds]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChildNeeds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Child_Id] [varchar](100) NOT NULL,
	[NeedDescription] [nvarchar](max) NULL,
	[NeedType] [nvarchar](255) NULL,
	[Priority] [nvarchar](20) NULL,
	[FulfilledDate] [datetime] NULL,
	[Remarks] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChildProgress]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChildProgress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Child_Id] [varchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Date] [datetime] NULL,
	[Category] [nvarchar](50) NULL,
	[Event_Id] [int] NULL,
	[Activity_Id] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Donation]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserAccount_Id] [varchar](100) NULL,
	[User_Name] [nvarchar](200) NULL,
	[User_Email] [nvarchar](200) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](300) NULL,
	[Event_Id] [int] NULL,
	[Child_Id] [varchar](100) NULL,
	[FacilitiesWallet_Id] [int] NULL,
	[SystemWallet_Id] [int] NULL,
	[FoodStuffWallet_Id] [int] NULL,
	[HealthWallet_Id] [int] NULL,
	[NecessitiesWallet_Id] [int] NULL,
	[Donation_Type] [nvarchar](200) NULL,
	[Date_Time] [datetime] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[EventCode] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[FacilitiesWallet_Id] [int] NULL,
	[FoodStuffWallet_Id] [int] NULL,
	[SystemWallet_Id] [int] NULL,
	[HealthWallet_Id] [int] NULL,
	[NecessitiesWallet_Id] [int] NULL,
	[EventCode] [nvarchar](200) NULL,
	[Amount] [decimal](18, 2) NULL,
	[CurrentAmount] [decimal](18, 2) NULL,
	[AmountLimit] [decimal](18, 2) NULL,
	[Status] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Village_Id] [varchar](100) NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expense]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expense](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Expense_Amount] [decimal](18, 2) NULL,
	[Description] [nvarchar](500) NULL,
	[Expenseday] [datetime] NULL,
	[Status] [nvarchar](100) NULL,
	[SystemWallet_Id] [int] NULL,
	[ExpenseType] [nvarchar](100) NULL,
	[FacilitiesWallet_Id] [int] NULL,
	[FoodStuffWallet_Id] [int] NULL,
	[HealthWallet_Id] [int] NULL,
	[NecessitiesWallet_Id] [int] NULL,
	[RequestedBy] [varchar](100) NULL,
	[ApprovedBy] [varchar](100) NULL,
	[House_Id] [varchar](100) NULL,
	[Child_Id] [varchar](100) NULL,
	[Event_Id] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacilitiesWallet]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacilitiesWallet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Budget] [decimal](18, 2) NULL,
	[UserAccount_Id] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodStuffWallet]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodStuffWallet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Budget] [decimal](18, 2) NULL,
	[UserAccount_Id] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthReport]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthReport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Child_Id] [varchar](100) NULL,
	[Nutritional_Status] [nvarchar](100) NULL,
	[Medical_History] [nvarchar](100) NULL,
	[Vaccination_Status] [nvarchar](100) NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
	[Checkup_Date] [datetime] NULL,
	[Doctor_Name] [nvarchar](255) NULL,
	[Recommendations] [nvarchar](max) NULL,
	[Health_Status] [nvarchar](50) NULL,
	[Follow_Up_Date] [datetime] NULL,
	[Illnesses] [nvarchar](max) NULL,
	[Allergies] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HealthReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthWallet]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthWallet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Budget] [decimal](18, 2) NULL,
	[UserAccount_Id] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[House]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[House](
	[Id] [varchar](100) NOT NULL,
	[House_Name] [nvarchar](100) NOT NULL,
	[House_Number] [int] NULL,
	[Expense_Amount] [decimal](18, 2) NULL,
	[Location] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[House_Member] [int] NULL,
	[CurrentMembers] [int] NULL,
	[House_Owner] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[UserAccount_Id] [varchar](100) NULL,
	[Village_Id] [varchar](100) NULL,
	[FoundationDate] [datetime] NOT NULL,
	[LastInspectionDate] [datetime] NULL,
	[MaintenanceStatus] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UrlPath] [nvarchar](max) NULL,
	[Child_Id] [varchar](100) NULL,
	[House_Id] [varchar](100) NULL,
	[Village_Id] [varchar](100) NULL,
	[Event_Id] [int] NULL,
	[UserAccount_Id] [varchar](100) NULL,
	[Activity_Id] [int] NULL,
	[Inventory_Id] [int] NULL,
	[School_Id] [varchar](100) NULL,
	[HealthReport_Id] [int] NULL,
	[AcademicReport_Id] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Income]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Income](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Donation_Id] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[SystemWallet_Id] [int] NULL,
	[FacilitiesWallet_Id] [int] NULL,
	[FoodStuffWallet_Id] [int] NULL,
	[HealthWallet_Id] [int] NULL,
	[NecessitiesWallet_Id] [int] NULL,
	[Receiveday] [datetime] NOT NULL,
	[Status] [nvarchar](100) NULL,
	[UserAccount_Id] [varchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Item_Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Quantity] [int] NOT NULL,
	[Purpose] [nvarchar](max) NULL,
	[BelongsTo] [varchar](100) NOT NULL,
	[BelongsToId] [varchar](100) NOT NULL,
	[PurchaseDate] [datetime] NULL,
	[LastInspectionDate] [datetime] NULL,
	[MaintenanceStatus] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NecessitiesWallet]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NecessitiesWallet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Budget] [decimal](18, 2) NULL,
	[UserAccount_Id] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Payment_Method] [nvarchar](100) NULL,
	[Date_Time] [datetime] NULL,
	[Donation_Id] [int] NULL,
	[Status] [nvarchar](100) NULL,
	[Amount] [decimal](18, 2) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role_Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[School]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[School](
	[Id] [varchar](100) NOT NULL,
	[SchoolName] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[SchoolType] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_School] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectDetails]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AcademicReportId] [int] NOT NULL,
	[SubjectName] [nvarchar](255) NULL,
	[Score] [decimal](5, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_SubjectDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemWallet]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemWallet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Budget] [decimal](18, 2) NULL,
	[UserAccount_Id] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemWallet_Id] [int] NULL,
	[FacilitiesWallet_Id] [int] NULL,
	[FoodStuffWallet_Id] [int] NULL,
	[HealthWallet_Id] [int] NULL,
	[NecessitiesWallet_Id] [int] NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Date_Time] [datetime] NULL,
	[Status] [nvarchar](200) NULL,
	[UserAccount_Id] [varchar](100) NULL,
	[Donation_Id] [int] NULL,
	[Income_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferHistory]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Child_Id] [varchar](100) NOT NULL,
	[FromHouseID] [varchar](100) NOT NULL,
	[ToHouseID] [varchar](100) NOT NULL,
	[TransferDate] [datetime] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[RejectionReason] [nvarchar](max) NULL,
	[HandledBy] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferRequest]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Child_Id] [varchar](100) NOT NULL,
	[FromHouseID] [varchar](100) NOT NULL,
	[ToHouseID] [varchar](100) NULL,
	[RequestDate] [datetime] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[DirectorNote] [nvarchar](max) NULL,
	[RequestReason] [nvarchar](max) NULL,
	[ApprovedBy] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[Id] [varchar](100) NOT NULL,
	[User_Name] [nvarchar](200) NULL,
	[User_Email] [nvarchar](200) NULL,
	[Password] [nvarchar](200) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](300) NULL,
	[Dob] [datetime] NULL,
	[Gender] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[Role_Id] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Village]    Script Date: 1/4/2025 1:24:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Village](
	[Id] [varchar](100) NOT NULL,
	[Village_Name] [nvarchar](200) NULL,
	[EstablishedDate] [datetime] NULL,
	[Expense_Amount] [decimal](18, 2) NULL,
	[Area] [float] NULL,
	[TotalHouses] [int] NULL,
	[TotalChildren] [int] NULL,
	[ContactNumber] [nvarchar](200) NULL,
	[Location] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [nvarchar](100) NULL,
	[UserAccount_Id] [varchar](100) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AcademicReport] ADD  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[AcademicReport] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Activity] ADD  DEFAULT ('Planned') FOR [Status]
GO
ALTER TABLE [dbo].[Activity] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Activity] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Booking] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[BookingSlot] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Child] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ChildNeeds] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[ChildNeeds] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ChildNeeds] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ChildProgress] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ChildProgress] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Donation] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Expense] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[HealthReport] ADD  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[HealthReport] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[House] ADD  DEFAULT ((0)) FOR [CurrentMembers]
GO
ALTER TABLE [dbo].[House] ADD  DEFAULT ('Good') FOR [MaintenanceStatus]
GO
ALTER TABLE [dbo].[House] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Image] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Income] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Inventory] ADD  DEFAULT ('Good') FOR [MaintenanceStatus]
GO
ALTER TABLE [dbo].[Inventory] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Inventory] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[School] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[School] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[SubjectDetails] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SubjectDetails] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TransferHistory] ADD  DEFAULT (getdate()) FOR [TransferDate]
GO
ALTER TABLE [dbo].[TransferHistory] ADD  DEFAULT ('Completed') FOR [Status]
GO
ALTER TABLE [dbo].[TransferHistory] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TransferHistory] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TransferRequest] ADD  DEFAULT (getdate()) FOR [RequestDate]
GO
ALTER TABLE [dbo].[TransferRequest] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[TransferRequest] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TransferRequest] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserAccount] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Village] ADD  DEFAULT ('0') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AcademicReport]  WITH CHECK ADD  CONSTRAINT [FK_AcademicReport_Child] FOREIGN KEY([Child_Id])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[AcademicReport] CHECK CONSTRAINT [FK_AcademicReport_Child]
GO
ALTER TABLE [dbo].[AcademicReport]  WITH CHECK ADD  CONSTRAINT [FK_AcademicReport_School] FOREIGN KEY([School_Id])
REFERENCES [dbo].[School] ([Id])
GO
ALTER TABLE [dbo].[AcademicReport] CHECK CONSTRAINT [FK_AcademicReport_School]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Village] FOREIGN KEY([Village_Id])
REFERENCES [dbo].[Village] ([Id])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_Village]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([House_id])
REFERENCES [dbo].[House] ([Id])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD FOREIGN KEY([FacilitiesWallet_Id])
REFERENCES [dbo].[FacilitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD FOREIGN KEY([FoodStuffWallet_Id])
REFERENCES [dbo].[FoodStuffWallet] ([Id])
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD FOREIGN KEY([HealthWallet_Id])
REFERENCES [dbo].[HealthWallet] ([Id])
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD FOREIGN KEY([House_Id])
REFERENCES [dbo].[House] ([Id])
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD FOREIGN KEY([NecessitiesWallet_Id])
REFERENCES [dbo].[NecessitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD FOREIGN KEY([School_Id])
REFERENCES [dbo].[School] ([Id])
GO
ALTER TABLE [dbo].[Child]  WITH CHECK ADD FOREIGN KEY([SystemWallet_Id])
REFERENCES [dbo].[SystemWallet] ([Id])
GO
ALTER TABLE [dbo].[ChildNeeds]  WITH CHECK ADD FOREIGN KEY([Child_Id])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[ChildProgress]  WITH CHECK ADD FOREIGN KEY([Activity_Id])
REFERENCES [dbo].[Activity] ([Id])
GO
ALTER TABLE [dbo].[ChildProgress]  WITH CHECK ADD FOREIGN KEY([Child_Id])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[ChildProgress]  WITH CHECK ADD FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([Child_Id])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([FacilitiesWallet_Id])
REFERENCES [dbo].[FacilitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([FoodStuffWallet_Id])
REFERENCES [dbo].[FoodStuffWallet] ([Id])
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([HealthWallet_Id])
REFERENCES [dbo].[HealthWallet] ([Id])
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([NecessitiesWallet_Id])
REFERENCES [dbo].[NecessitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([SystemWallet_Id])
REFERENCES [dbo].[SystemWallet] ([Id])
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD FOREIGN KEY([FacilitiesWallet_Id])
REFERENCES [dbo].[FacilitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD FOREIGN KEY([FoodStuffWallet_Id])
REFERENCES [dbo].[FoodStuffWallet] ([Id])
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD FOREIGN KEY([HealthWallet_Id])
REFERENCES [dbo].[HealthWallet] ([Id])
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD FOREIGN KEY([NecessitiesWallet_Id])
REFERENCES [dbo].[NecessitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD FOREIGN KEY([SystemWallet_Id])
REFERENCES [dbo].[SystemWallet] ([Id])
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Village] FOREIGN KEY([Village_Id])
REFERENCES [dbo].[Village] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Village]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD FOREIGN KEY([Child_Id])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD FOREIGN KEY([FacilitiesWallet_Id])
REFERENCES [dbo].[FacilitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD FOREIGN KEY([FoodStuffWallet_Id])
REFERENCES [dbo].[FoodStuffWallet] ([Id])
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD FOREIGN KEY([HealthWallet_Id])
REFERENCES [dbo].[HealthWallet] ([Id])
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD FOREIGN KEY([House_Id])
REFERENCES [dbo].[House] ([Id])
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD FOREIGN KEY([NecessitiesWallet_Id])
REFERENCES [dbo].[NecessitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[FacilitiesWallet]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[FoodStuffWallet]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[HealthReport]  WITH CHECK ADD  CONSTRAINT [FK_HealthReport_Child] FOREIGN KEY([Child_Id])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[HealthReport] CHECK CONSTRAINT [FK_HealthReport_Child]
GO
ALTER TABLE [dbo].[HealthWallet]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[House]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[House]  WITH CHECK ADD FOREIGN KEY([Village_Id])
REFERENCES [dbo].[Village] ([Id])
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_AcademicReport] FOREIGN KEY([AcademicReport_Id])
REFERENCES [dbo].[AcademicReport] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_AcademicReport]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_Activity] FOREIGN KEY([Activity_Id])
REFERENCES [dbo].[Activity] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_Activity]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_Child] FOREIGN KEY([Child_Id])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_Child]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_Event] FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_Event]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_HealthReport] FOREIGN KEY([HealthReport_Id])
REFERENCES [dbo].[HealthReport] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_HealthReport]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_House] FOREIGN KEY([House_Id])
REFERENCES [dbo].[House] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_House]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_Inventory] FOREIGN KEY([Inventory_Id])
REFERENCES [dbo].[Inventory] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_Inventory]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_School] FOREIGN KEY([School_Id])
REFERENCES [dbo].[School] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_School]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_UserAccount] FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_UserAccount]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_Village] FOREIGN KEY([Village_Id])
REFERENCES [dbo].[Village] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_Village]
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([Donation_Id])
REFERENCES [dbo].[Donation] ([Id])
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([FacilitiesWallet_Id])
REFERENCES [dbo].[FacilitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([FoodStuffWallet_Id])
REFERENCES [dbo].[FoodStuffWallet] ([Id])
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([HealthWallet_Id])
REFERENCES [dbo].[HealthWallet] ([Id])
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([NecessitiesWallet_Id])
REFERENCES [dbo].[NecessitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([SystemWallet_Id])
REFERENCES [dbo].[SystemWallet] ([Id])
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([SystemWallet_Id])
REFERENCES [dbo].[SystemWallet] ([Id])
GO
ALTER TABLE [dbo].[Income]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[NecessitiesWallet]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([Donation_Id])
REFERENCES [dbo].[Donation] ([Id])
GO
ALTER TABLE [dbo].[SubjectDetails]  WITH CHECK ADD  CONSTRAINT [FK_SubjectDetails_AcademicReport] FOREIGN KEY([AcademicReportId])
REFERENCES [dbo].[AcademicReport] ([Id])
GO
ALTER TABLE [dbo].[SubjectDetails] CHECK CONSTRAINT [FK_SubjectDetails_AcademicReport]
GO
ALTER TABLE [dbo].[SystemWallet]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([Donation_Id])
REFERENCES [dbo].[Donation] ([Id])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([FacilitiesWallet_Id])
REFERENCES [dbo].[FacilitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([FoodStuffWallet_Id])
REFERENCES [dbo].[FoodStuffWallet] ([Id])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([HealthWallet_Id])
REFERENCES [dbo].[HealthWallet] ([Id])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([Income_Id])
REFERENCES [dbo].[Income] ([Id])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([NecessitiesWallet_Id])
REFERENCES [dbo].[NecessitiesWallet] ([Id])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([SystemWallet_Id])
REFERENCES [dbo].[SystemWallet] ([Id])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
ALTER TABLE [dbo].[TransferHistory]  WITH CHECK ADD FOREIGN KEY([Child_Id])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[TransferHistory]  WITH CHECK ADD FOREIGN KEY([FromHouseID])
REFERENCES [dbo].[House] ([Id])
GO
ALTER TABLE [dbo].[TransferHistory]  WITH CHECK ADD FOREIGN KEY([ToHouseID])
REFERENCES [dbo].[House] ([Id])
GO
ALTER TABLE [dbo].[TransferRequest]  WITH CHECK ADD FOREIGN KEY([Child_Id])
REFERENCES [dbo].[Child] ([Id])
GO
ALTER TABLE [dbo].[TransferRequest]  WITH CHECK ADD FOREIGN KEY([FromHouseID])
REFERENCES [dbo].[House] ([Id])
GO
ALTER TABLE [dbo].[TransferRequest]  WITH CHECK ADD FOREIGN KEY([ToHouseID])
REFERENCES [dbo].[House] ([Id])
GO
ALTER TABLE [dbo].[UserAccount]  WITH CHECK ADD FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Village]  WITH CHECK ADD FOREIGN KEY([UserAccount_Id])
REFERENCES [dbo].[UserAccount] ([Id])
GO
CREATE TRIGGER trg_CheckBelongsTo
ON [dbo].[Inventory]
AFTER INSERT, UPDATE
AS
BEGIN
    -- Check nếu BelongsTo là 'Village', BelongsToId nó sẽ check ID trong bảng Village có tồn tại không
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE BelongsTo = 'Village'
          AND NOT EXISTS (SELECT 1 FROM Village WHERE Village.Id = inserted.BelongsToId)
    )
    BEGIN
        RAISERROR ('BelongsToId does not exist in Village table!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Check nếu BelongsTo là 'House', BelongsToId nó sẽ check ID trong bảng House có tồn tại không
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE BelongsTo = 'House'
          AND NOT EXISTS (SELECT 1 FROM House WHERE House.Id = inserted.BelongsToId)
    )
    BEGIN
        RAISERROR ('BelongsToId does not exist in the House table!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
INSERT INTO Role (Role_Name)
VALUES 
('Admin'),
('Sponsor'),
('HouseMother'),
('Accountant'),
('Donor'),
('Director');


-- Insert into UserAccount table
INSERT INTO UserAccount (Id, User_Name, User_Email, Password, Phone, Address, Dob, Gender, Country, Role_Id, IsDeleted, Status, CreatedDate) VALUES 
('UA001', 'Admin', 'admin@gmail.com', 'Vn872002@', '1234567890', '123 Main St', '1985-01-15', 'Male', 'USA', 1, 0,'Active',GETDATE()),
('UA002', 'Accountant', 'accountant@gmail.com', '123', '9876543210', '456 Oak Ave', '1990-04-22', 'Female', 'Canada', 3, 0,'Active',GETDATE()),
('UA003', 'Donor', 'donor@gmail.com', '123', '1112223333', '789 Pine Rd', '1978-07-30', 'Male', 'UK', 4, 0,'Active',GETDATE()),
('UA004', 'HouseMother1', 'housemother1@gmail.com', '123', '4445556666', '101 Maple St', '1989-03-12', 'Female', 'Australia', 3, 0,'Active',GETDATE()),
('UA005', 'HouseMother2', 'housemother2@gmail.com', '123', '7778889999', '202 Elm St', '1992-09-25', 'Male', 'New Zealand', 3, 0,'Active',GETDATE()),
('UA006', 'HouseMother3', 'housemother3@gmail.com', '123', '7778889999', '202 Noah St', '1991-11-25', 'Male', 'New Gate', 3, 0,'Active',GETDATE()),
('UA007', 'HouseMother4', 'housemother4@gmail.com', '123', '7778889999', '202 Sub St', '1992-09-25', 'Female', 'Operation Zone', 3, 0,'Active',GETDATE()),
('UA008', 'HouseMother5', 'housemother5@gmail.com', '123', '7778889999', '202 Marina Town', '1992-09-25', 'Male', 'New Zealand', 3, 0,'Active',GETDATE()),
('UA009', 'Sponsor1', 'sponsor1@gmail.com', '123', '7778889999', '112 FPT Town', '1992-09-25', 'Male', 'New Warcry', 2, 0,'Active',GETDATE()),
('UA010', 'Sponsor2', 'sponsor2@gmail.com', '123', '7778889999', '112 Hai Noi Town', '1992-09-25', 'Male', 'Viet Nam', 2, 0,'Active',GETDATE()),
('UA011', 'Sponsor3', 'sponsor3@gmail.com', '123', '7778889999', '1312 Yen Lang', '1992-09-25', 'FeMale', 'Trung Nam', 2, 0,'Active',GETDATE()),
('UA012', 'Sponsor4', 'sponsor4@gmail.com', '123', '7778889999', '112 Hang Dau', '1992-09-25', 'FeMale', 'Quoc Tu', 2, 0,'Active',GETDATE()),
('UA013', 'Sponsor5', 'sponsor5@gmail.com', '123', '7778889999', '112 Hoa Binh', '1992-09-25', 'Male', 'Thai Lan', 2, 0,'Active',GETDATE()),
('UA014', 'Director1', 'director1@gmail.com', '123', '7778889999', '112 Hoa Thang', '1972-01-25', 'Male', 'Singapore', 6, 0,'Active',GETDATE()),
('UA015', 'Director2', 'director2@gmail.com', '123', '7778889999', '112 Hoa Thang', '1972-01-25', 'Male', 'Singapore', 6, 0,'Active',GETDATE()),
('UA016', 'Director3', 'director3@gmail.com', '123', '7778889999', '112 Hoa Thang', '1972-01-25', 'Male', 'Singapore', 6, 0,'Active',GETDATE()),
('UA017', 'Director4', 'director4@gmail.com', '123', '7778889999', '112 Hoa Thang', '1972-01-25', 'Male', 'Singapore', 6, 0,'Active',GETDATE()),
('UA018', 'Director5', 'director5@gmail.com', '123', '7778889999', '112 Hoa Thang', '1972-01-25', 'Male', 'Singapore', 6, 0,'Active',GETDATE());

Insert into FacilitiesWallet(Budget, UserAccount_Id)
Values
(1000000000,'UA001');
Insert into SystemWallet(Budget, UserAccount_Id)
Values
(1000000000,'UA001');
Insert into NecessitiesWallet(Budget, UserAccount_Id)
Values
(1000000000,'UA001');
Insert into FoodStuffWallet(Budget, UserAccount_Id)
Values
(1000000000,'UA001');
Insert into HealthWallet(Budget, UserAccount_Id)
Values
(1000000000,'UA001');
-- Insert into Village table
INSERT INTO Village (Id, Village_Name, Location, Description, UserAccount_Id, IsDeleted, Status,CreatedDate) VALUES 
('V001', 'Greenwood Village', 'Valley', 'A small, peaceful village.', 'UA014', 0,'Active',GETDATE()),
('V002', 'Oakwood Village', 'Hillside', 'Village near the hills.', 'UA015', 0,'Active',GETDATE()),
('V003', 'Maplewood Village', 'Riverside', 'Village by the river.', 'UA016', 0,'Active',GETDATE()),
('V004', 'Pinewood Village', 'Forest', 'A village surrounded by pine trees.', 'UA017', 0,'Active',GETDATE()),
('V005', 'Willow Creek Village', 'Creekside', 'A village near a flowing creek.', 'UA018', 0,'Active',GETDATE());
INSERT INTO [Event] (Name, Description, StartTime, EndTime, Status, CreatedDate, ModifiedDate, IsDeleted, Amount,CurrentAmount,AmountLimit,  FacilitiesWallet_Id,FoodStuffWallet_Id,NecessitiesWallet_Id,SystemWallet_Id,HealthWallet_Id,Village_Id,EventCode) 
VALUES 
('Children Day Event', 'A fun event for all children in the village', '2024-12-01 09:00:00', '2024-12-01 17:00:00', 'Scheduled', '2024-09-27 10:00:00', '2024-09-27 10:00:00', 0, 1000.00,1000.00,100000000, 1,NULL,NULL,NULL,NULL ,'V001','E001'),
('Christmas Celebration', 'A joyful Christmas event for children and sponsors', '2024-12-25 14:00:00', '2024-12-25 20:00:00', 'Scheduled', '2024-09-27 10:00:00', '2024-09-27 10:00:00', 0, 1500.00,1500.00,100000000, NULL,1,NULL,NULL,NULL,'V002','E002'),
('Education Support Workshop', 'A workshop to help children with learning strategies', '2024-11-15 10:00:00', '2024-11-15 15:00:00', 'Scheduled', '2024-09-27 10:00:00', '2024-09-27 10:00:00', 0, 500.00,500.00,50000000.00, NULL,NULL,1,NULL,NULL,'V003','E003'),
('System Wallet Event', 'A workshop to help children', '2024-11-15 10:00:00', '2024-11-15 15:00:00', 'Scheduled', '2024-09-27 10:00:00', '2024-09-27 10:00:00', 0, 500.00,500.00,500000000.00, NULL,NULL,NULL,1,NULL,'V004','E004'),
('Support Children Health', 'Health help children', '2024-11-15 10:00:00', '2024-11-15 15:00:00', 'Scheduled', '2024-09-27 10:00:00', '2024-09-27 10:00:00', 0, 500.00,500.00,50000000.00,NULL,NULL,NULL,NULL,1,'V005','E005');
-- Insert into House table
INSERT INTO House (Id, House_Name, House_Number, Location, Description, House_Member, CurrentMembers, House_Owner, Status, UserAccount_Id, Village_Id, FoundationDate, MaintenanceStatus, IsDeleted, CreatedDate
) VALUES
-- House 1
('H001', 'House 1', '101', 'North Street', 'House for orphans with a nice garden.', 5, 3, 'HouseMother1', 'Active', 'UA008', 'V001', '2010-01-01', 'Good', 0, GETDATE()),
-- House 2
('H002', 'House 2', '102', 'East Street', 'A cozy house near the playground.', 6, 5, 'HouseMother2', 'Active', 'UA004', 'V001', '2011-03-15', 'Good', 0, GETDATE()),
-- House 3
('H003', 'House 3', '103', 'West Street', 'A small house with a great view of the river.', 7, 6, 'HouseMother3', 'Active', 'UA005', 'V001', '2012-06-20', 'Good', 0, GETDATE()),
-- House 4
('H004', 'House 4', '104', 'South Street', 'House with easy access to local school.', 4, 4, 'HouseMother4', 'Active', 'UA006', 'V005', '2013-09-10', 'Good' , 0, GETDATE()),
-- House 5
('H005', 'House 5', '105', 'Central Avenue', 'Large house with several rooms for children.', 8, 7, 'HouseMother5', 'Active', 'UA007', 'V005', '2014-12-01', 'Good',  0, GETDATE()),
-- House 6
('H006', 'House 6', '106', 'North Street', 'House close to the local clinic.', 5, 5, 'HouseMother1', 'Active', 'UA007', 'V005', '2015-02-11', 'Good',  0, GETDATE()),
-- House 7
('H007', 'House 7', '107', 'East Street', 'A house near the village market.', 6, 6, 'HouseMother2', 'Active', 'UA008', 'V002', '2016-04-25', 'Good', 0, GETDATE()),
-- House 8
('H008', 'House 8', '108', 'West Street', 'House with a backyard garden.', 7, 6, 'HouseMother3', 'Active', 'UA004', 'V002', '2017-07-13', 'Good',  0, GETDATE()),
-- House 9
('H009', 'House 9', '109', 'South Street', 'House near the community hall.', 4, 3, 'HouseMother4', 'Active', 'UA005', 'V002', '2018-09-30', 'Good',  0, GETDATE()),
-- House 10
('H010', 'House 10', '110', 'Central Avenue', 'House with large play area for kids.', 8, 8, 'HouseMother5', 'Active', 'UA006', 'V003', '2019-11-20', 'Good', 0, GETDATE()),
-- House 11
('H011', 'House 11', '111', 'North Street', 'House near the church.', 5, 4, 'HouseMother1', 'Active', 'UA008', 'V003', '2020-02-05', 'Good', 0, GETDATE()),
-- House 12
('H012', 'House 12', '112', 'East Street', 'House with a front porch.', 6, 5, 'HouseMother2', 'Active', 'UA005', 'V003', '2021-05-18', 'Good',  0, GETDATE()),
-- House 13
('H013', 'House 13', '113', 'West Street', 'House near the village entrance.', 7, 6, 'HouseMother3', 'Active', 'UA004', 'V004', '2022-07-10', 'Good',  0, GETDATE()),
-- House 14
('H014', 'House 14', '114', 'South Street', 'House near the sports ground.', 4, 4, 'HouseMother4', 'Active', 'UA007', 'V004', '2023-09-30', 'Good',  0, GETDATE()),
-- House 15
('H015', 'House 15', '115', 'Central Avenue', 'House with a small library.', 8, 8, 'HouseMother5', 'Active', 'UA006', 'V004', '2024-01-01', 'Good', 0, GETDATE());

INSERT INTO [dbo].[School] 
    ([Id], [SchoolName], [Address], [SchoolType], [PhoneNumber], [Email], [IsDeleted], [CreatedBy], [CreatedDate])
VALUES
-- School 1
('SCH001', 'Greenfield Primary School', '123 Greenfield Street, City A', 'Primary', '0123456789', 'greenfield@school.edu', 0, NULL, GETDATE()),

-- School 2
('SCH002', 'Riverdale Middle School', '456 Riverdale Avenue, City B', 'Middle', '0987654321', 'riverdale@school.edu', 0, NULL, GETDATE()),

-- School 3
('SCH003', 'Hillside High School', '789 Hillside Road, City C', 'High', '0112233445', 'hillside@school.edu', 0, NULL, GETDATE()),

-- School 4
('SCH004', 'Bright Future Academy', '101 Bright Future Lane, City D', 'Primary', '0223344556', 'brightfuture@academy.edu', 0, NULL, GETDATE()),

-- School 5
('SCH005', 'Sunrise International School', '202 Sunrise Boulevard, City E', 'International', '0334455667', 'sunrise@school.edu', 0, NULL, GETDATE());
INSERT INTO Child (Id, Child_Name, Health_Status, House_Id, School_Id, Gender, Dob, IsDeleted,status,CreatedDate) VALUES 
('C001', 'Alice Johnson', 'Good', 'H001', 'SCH001', 'Female', '2012-05-10',  0,'Active',GETDATE()),
('C002', 'Bob Smith', 'Excellent', 'H001', 'SCH001', 'Male', '2011-11-12',  0,'Active',GETDATE()),
('C003', 'Cathy Brown', 'Average', 'H001', 'SCH001', 'Female', '2010-02-20', 0,'Active',GETDATE()),
('C004', 'Daniel Wilson', 'Good', 'H001', 'SCH001', 'Male', '2013-08-05',  0,'Active',GETDATE()),
('C005', 'Eva Adams', 'Good', 'H001', 'SCH001', 'Female', '2012-03-15',  0,'Active',GETDATE()),

('C006', 'Frank Miller', 'Excellent', 'H002', 'SCH002', 'Male', '2011-07-22', 0,'Active',GETDATE()),
('C007', 'Grace Lee', 'Good', 'H002', 'SCH002', 'Female', '2012-01-14',  0,'Active',GETDATE()),
('C008', 'Henry Clark', 'Average', 'H002', 'SCH002', 'Male', '2010-10-30',  0,'Active',GETDATE()),
('C009', 'Ivy Davis', 'Good', 'H002', 'SCH002', 'Female', '2013-06-25',  0,'Active',GETDATE()),
('C010', 'Jack Thomas', 'Good', 'H002', 'SCH002', 'Male', '2012-09-18',  0,'Active',GETDATE()),

('C011', 'Katie Harris', 'Excellent', 'H003', 'SCH003', 'Female', '2011-03-03',  0,'Active',GETDATE()),
('C012', 'Liam Walker', 'Good', 'H003', 'SCH003', 'Male', '2012-11-01',  0,'Active',GETDATE()),
('C013', 'Mia Young', 'Average', 'H003', 'SCH003', 'Female', '2010-12-28',  0,'Active',GETDATE()),
('C014', 'Noah Martinez', 'Good', 'H003', 'SCH003', 'Male', '2013-04-19',  0,'Active',GETDATE()),
('C015', 'Olivia Gonzalez', 'Good', 'H003', 'SCH003', 'Female', '2012-02-23',  0,'Active',GETDATE()),

('C016', 'Peter Anderson', 'Excellent', 'H004', 'SCH004', 'Male', '2011-06-15',  0,'Active',GETDATE()),
('C017', 'Quinn Lopez', 'Good', 'H004', 'SCH004', 'Female', '2012-05-08',  0,'Active',GETDATE()),
('C018', 'Ryan Wright', 'Average', 'H004', 'SCH004', 'Male', '2010-03-27',  0,'Active',GETDATE()),
('C019', 'Sophia King', 'Good', 'H004', 'SCH004', 'Female', '2013-09-09',  0,'Active',GETDATE()),
('C020', 'Thomas Scott', 'Good', 'H004', 'SCH004', 'Male', '2012-07-30',  0,'Active',GETDATE()),

('C021', 'Ursula Green', 'Excellent', 'H005', 'SCH005', 'Female', '2011-10-20',  0,'Active',GETDATE()),
('C022', 'Victor Baker', 'Good', 'H005', 'SCH005', 'Male', '2012-03-17',  0,'Active',GETDATE()),
('C023', 'Wendy Nelson', 'Average', 'H005', 'SCH005', 'Female', '2010-11-11',  0,'Active',GETDATE()),
('C024', 'Xander Hill', 'Good', 'H005', 'SCH005', 'Male', '2013-05-02',  0,'Active',GETDATE()),
('C025', 'Yara Torres', 'Good', 'H005', 'SCH005', 'Female', '2012-08-07', 0,'Active',GETDATE()),

('C026', 'Zachary Rivera', 'Excellent', 'H006', 'SCH001', 'Male', '2011-04-10',  0,'Active',GETDATE()),
('C027', 'Abby Wood', 'Good', 'H006', 'SCH001', 'Female', '2012-11-05',  0,'Active',GETDATE()),
('C028', 'Bobby Evans', 'Average', 'H006', 'SCH001', 'Male', '2010-09-15',  0,'Active',GETDATE()),
('C029', 'Clara Brooks', 'Good', 'H006', 'SCH001', 'Female', '2013-01-19',  0,'Active',GETDATE()),
('C030', 'Dylan Powell', 'Good', 'H006', 'SCH001', 'Male', '2012-06-11',  0,'Active',GETDATE());

INSERT INTO Child (Id, Child_Name, Health_Status, House_Id, School_Id, Gender, Dob, IsDeleted,status,Amount,CurrentAmount,AmountLimit, FacilitiesWallet_Id,FoodStuffWallet_Id,NecessitiesWallet_Id,SystemWallet_Id,HealthWallet_Id,CreatedDate) VALUES 
('C031', 'Alice Johnson', 'Bad', 'H001', 'SCH001', 'Female', '2012-05-10',  0,'Active',1000.00,1000.00,100000000,NULL,1,NULL,NULL,NULL,GETDATE()),
('C032', 'Bob Smith', 'Bad', 'H001', 'SCH002', 'Male', '2011-11-12',  0,'Active',1000.00,1000.00,100000000,1,NULL,NULL,NULL,NULL,GETDATE()),
('C033', 'Cathy Brown', 'Bad', 'H001', 'SCH003', 'Female', '2010-02-20', 0,'Active',1000.00,1000.00,100000000,NULL,NULL,1,NULL,NULL,GETDATE()),
('C034', 'Daniel Wilson', 'Bad', 'H001', 'SCH004', 'Male', '2013-08-05',  0,'Active',1000.00,1000.00,100000000,1,NULL,NULL,NULL,NULL,GETDATE()),
('C035', 'Eva Adams', 'Bad', 'H001', 'SCH005', 'Female', '2012-03-15',  0,'Active',1000.00,1000.00,100000000,NULL,NULL,NULL,NULL,1,GETDATE()),

('C036', 'Frank Miller', 'Bad', 'H002', 'SCH005', 'Male', '2011-07-22', 0,'Active',1000.00,1000.00,100000000,1,NULL,NULL,NULL,NULL,GETDATE()),
('C037', 'Grace Lee', 'Bad', 'H002', 'SCH004', 'Female', '2012-01-14',  0,'Active',1000.00,1000.00,100000000,NULL,1,NULL,NULL,NULL,GETDATE()),
('C038', 'Henry Clark', 'Bad', 'H002', 'SCH003', 'Male', '2010-10-30',  0,'Active',1000.00,1000.00,100000000,NULL,NULL,1,NULL,NULL,GETDATE()),
('C039', 'Ivy Davis', 'Bad', 'H002', 'SCH002', 'Female', '2013-06-25',  0,'Active',1000.00,1000.00,100000000,NULL,NULL,NULL,1,NULL,GETDATE()),
('C040', 'Jack Thomas', 'Bad', 'H002', 'SCH001', 'Male', '2012-09-18',  0,'Active',1000.00,1000.00,100000000,NULL,NULL,NULL,NULL,1,GETDATE());
INSERT INTO BookingSlot (Start_Time, End_Time, Status, Slot_Time,CreatedDate) VALUES 
('09:00:00', '10:00:00', 'Available', 60,GETDATE()),
('10:00:00', '11:00:00', 'Available', 60,GETDATE()),
('11:00:00', '12:00:00', 'Available', 60,GETDATE()),
('13:00:00', '14:00:00', 'Available', 60,GETDATE()),
('14:00:00', '15:00:00', 'Available', 60,GETDATE()),
('15:00:00', '16:00:00', 'Available', 60,GETDATE());

INSERT INTO Booking (House_Id, UserAccount_Id, BookingSlot_Id, Visitday, Status,CreatedDate) VALUES 
('H001', 'UA009', 1, '2024-09-01', 'Confirmed',GETDATE()),
('H002', 'UA010', 2, '2024-09-01', 'Confirmed',GETDATE()),
('H003', 'UA011', 3, '2024-09-01', 'Pending',GETDATE()),
('H004', 'UA012', 4, '2024-09-01', 'Confirmed',GETDATE()),
('H005', 'UA013', 5, '2024-09-01', 'Cancelled',GETDATE());


INSERT INTO Donation (UserAccount_Id, Donation_Type, Date_Time, Amount, Description, Status,CreatedDate,FacilitiesWallet_Id,FoodStuffWallet_Id,NecessitiesWallet_Id,SystemWallet_Id,HealthWallet_Id,Child_Id,Event_Id,EventCode) VALUES 
('UA009', 'Wallet', '2024-01-10 10:00:00', 500.00, 'Donation for Facilities Wallet', 'Paid',GETDATE(),1,NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('UA010', 'Event', '2024-02-15 11:30:00', 750.00, 'Donation for Food Wallet', 'Paid',GETDATE(),NULL,1,NULL,NULL,NULL,NULL,1,'E001'),
('UA011', 'Wallet', '2024-03-20 12:00:00', 1200.00, 'Donation for Necessities Wallet', 'Paid',GETDATE(),NULL,NULL,1,NULL,NULL,NULL,NULL,NULL),
('UA012', 'Child', '2024-04-25 14:00:00', 300.00, 'Donation for System Wallet', 'Paid',GETDATE(),NULL,NULL,NULL,1,NULL,'C030',NULL,NULL),
('UA013', 'Wallet', '2024-05-30 16:00:00', 800.00, 'Donation for Health Wallet', 'Paid',GETDATE(),NULL,NULL,NULL,NULL,1,NULL,NULL,NULL);

-- Insert into Payment table
INSERT INTO Payment (Payment_Method, Date_Time, Donation_Id, Status, Amount,CreatedDate) VALUES 
('Credit Card', '2024-01-15 09:00:00', 1, 'Paid', 500.00,GETDATE()),  -- Payment by Sponsor1
('Bank Transfer', '2024-02-18 10:30:00', 2, 'Paid', 750.00,GETDATE()),  -- Payment by Sponsor2
('PayPal', '2024-03-21 12:00:00', 3,'Paid',  1200.00,GETDATE()),       -- Payment by Sponsor3
('Debit Card', '2024-04-22 14:30:00',4, 'Paid', 300.00,GETDATE()),   -- Payment by Sponsor4
('Wire Transfer', '2024-05-23 16:45:00',  5,'Paid', 800.00,GETDATE()); 

-- Insert into Income table
INSERT INTO Income (Donation_Id, Receiveday, UserAccount_Id, IsDeleted, Status,CreatedDate,FacilitiesWallet_Id,FoodStuffWallet_Id,NecessitiesWallet_Id,SystemWallet_Id,HealthWallet_Id) VALUES 
(1, '2024-01-02 11:00:00', 'UA009',  0,'Completed',GETDATE(),1,NULL,NULL,NULL,NULL),  -- Income from Sponsor1's donation for House1
(2, '2024-02-16 10:30:00', 'UA010',  0,'Completed',GETDATE(),NULL,NULL,1,NULL,NULL),  -- Income from Sponsor2's donation for House2
(3, '2024-03-11 12:45:00', 'UA011',  0,'Completed',GETDATE(),NULL,1,NULL,NULL,NULL),  -- Income from Sponsor3's donation for House3
(4, '2024-04-06 14:00:00', 'UA012',  0,'Completed',GETDATE(),NULL,NULL,NULL,1,NULL),  -- Income from Sponsor4's donation for House4
(5, '2024-05-21 16:15:00', 'UA013',  0,'Completed',GETDATE(),NULL,NULL,NULL,NULL,1);  -- Income from Sponsor5's donation for House5

-- Insert into Expense table
INSERT INTO Expense (Expense_Amount, Description, Expenseday, House_Id, IsDeleted, Status,CreatedDate,FacilitiesWallet_Id,FoodStuffWallet_Id,NecessitiesWallet_Id,SystemWallet_Id,HealthWallet_Id) VALUES 
(150.00, 'Purchase of cleaning supplies', '2024-01-10 09:30:00', 'H001', 0,'Approved',GETDATE(),NULL,1,NULL,NULL,NULL),  -- Expense for House1
(200.00, 'Repair and maintenance', '2024-02-12 11:15:00', 'H002', 0,'Approved',GETDATE(),1,NULL,NULL,NULL,NULL),         -- Expense for House2
(300.00, 'Food and groceries', '2024-03-14 15:45:00', 'H003', 0,'Approved',GETDATE(),NULL,NULL,NULL,1,NULL),             -- Expense for House3
(250.00, 'Utilities payment', '2024-04-20 10:30:00', 'H004', 0,'Approved',GETDATE(),NULL,NULL,NULL,1,NULL),              -- Expense for House4
(180.00, 'Educational materials', '2024-05-25 14:00:00', 'H005', 0,'Approved',GETDATE(),NULL,NULL,NULL,NULL,1);



-- Insert into Child table


INSERT INTO [dbo].[HealthReport] 
    ([Child_Id], [Nutritional_Status], [Medical_History], [Vaccination_Status], 
     [Weight], [Height], [Checkup_Date], [Doctor_Name], [Recommendations], 
     [Health_Status], [Follow_Up_Date], [Illnesses], [Allergies], [Status], [IsDeleted], [CreatedBy], [CreatedDate])
VALUES
-- Health Report 1
('C001', 'Good', 'No significant issues', 'Up to date', 
 20.5, 120.0, '2024-11-01', 'Dr. Jane Doe', 'Continue balanced diet and regular exercise.', 
 'Healthy', '2024-12-01', 'None', 'Peanuts', 
  'Active', 0, NULL, GETDATE()),

-- Health Report 2
('C002', 'Moderate', 'Frequent colds', 'Up to date', 
 18.0, 115.5, '2024-11-03', 'Dr. John Smith', 'Monitor closely and consider additional vitamins.', 
 'Moderate', '2024-11-30', 'Asthma', 'Dust', 
  'Active', 0, NULL, GETDATE()),

-- Health Report 3
('C003', 'Poor', 'History of malnutrition', 'Missed some doses', 
 15.0, 110.0, '2024-11-05', 'Dr. Alice Brown', 'Immediate intervention needed; schedule a follow-up.', 
 'Critical', '2024-11-20', 'Malnutrition', 'Lactose', 
  'Active', 0, NULL, GETDATE()),

-- Health Report 4
('C004', 'Good', 'No medical history', 'Up to date', 
 22.0, 125.0, '2024-11-10', 'Dr. Mark Wilson', 'Maintain current routine; no further action needed.', 
 'Healthy', '2025-01-01', 'None', 'None', 
  'Active', 0, NULL, GETDATE()),

-- Health Report 5
('C005', 'Moderate', 'Mild allergies', 'Partially completed', 
 19.5, 117.0, '2024-11-12', 'Dr. Emily White', 'Ensure all vaccinations are up to date.', 
 'Moderate', '2024-12-15', 'Eczema', 'Gluten', 
 'Active', 0, NULL, GETDATE());

INSERT INTO [dbo].[AcademicReport] 
    ([Diploma], [SchoolLevel], [Child_Id], [School_Id], [GPA], [SchoolReport], [Semester], [AcademicYear], [Remarks], 
     [Achievement], [Status], [Class], [Feedback], [IsDeleted], [CreatedBy], [CreatedDate])
VALUES
-- Academic Report 1
('Primary Education', 'Elementary', 'C001', 'SCH001', 7, 'Excellent performance across all subjects.', 'Semester 1', '2024', 
 'Very active and curious in class.', 'Best student of the semester.', 'Active', '5A', 'Needs to work on group collaboration.', 0, NULL, GETDATE()),

-- Academic Report 2
('Primary Education', 'Elementary', 'C002', 'SCH001', 6, 'Good progress with room for improvement.', 'Semester 1', '2024', 
 'Needs more attention in Science.', 'Improved in Mathematics.', 'Active', '5B', 'Encouraged to participate in extracurricular activities.', 0, NULL, GETDATE()),

-- Academic Report 3
('Secondary Education', 'Middle School', 'C003', 'SCH003', 5, 'Outstanding performance in all subjects.', 'Semester 2', '2024', 
 'Shows great leadership in group activities.', 'Science Olympiad Winner.', 'Active', '7A', 'Excels in problem-solving skills.', 0, NULL, GETDATE()),

-- Academic Report 4
('Secondary Education', 'Middle School', 'C004', 'SCH004', 8, 'Satisfactory performance, needs consistent efforts.', 'Semester 1', '2024', 
 'Active in sports but needs to focus on academics.', 'Best Sports Athlete.', 'Active', '7B', 'Requires guidance in time management.', 0, NULL, GETDATE()),

-- Academic Report 5
('Primary Education', 'Elementary', 'C005', 'SCH002', 7, 'Highly disciplined and excels in arts.', 'Semester 2', '2024', 
 'Demonstrates exceptional skills in painting.', 'Winner of National Art Competition.', 'Active', '5C', 'A role model for peers.', 0, NULL, GETDATE());

INSERT INTO [dbo].[SubjectDetails] 
    ([AcademicReportId], [SubjectName], [Score], [Remarks], [IsDeleted], [CreatedBy], [CreatedDate])
VALUES
-- Subject Details for Academic Report 1
(1, 'Mathematics', 9.5, 'Excellent understanding of concepts.', 0, NULL, GETDATE()),
(1, 'Science', 8.8, 'Shows curiosity and active participation.', 0, NULL, GETDATE()),
(1, 'English', 9.2, 'Demonstrates strong language skills.', 0, NULL, GETDATE()),

-- Subject Details for Academic Report 2
(2, 'Mathematics', 7.5, 'Good progress but needs consistent practice.', 0, NULL, GETDATE()),
(2, 'Science', 6.8, 'Requires more focus on experiments.', 0, NULL, GETDATE()),
(2, 'History', 8.0, 'Shows great interest in learning history.', 0, NULL, GETDATE()),

-- Subject Details for Academic Report 3
(3, 'Mathematics', 10.0, 'Outstanding performance.', 0, NULL, GETDATE()),
(3, 'Physics', 9.8, 'Excels in problem-solving.', 0, NULL, GETDATE()),
(3, 'Chemistry', 9.5, 'Strong understanding of chemical concepts.', 0, NULL, GETDATE()),

-- Subject Details for Academic Report 4
(4, 'Mathematics', 7.0, 'Needs improvement in algebra.', 0, NULL, GETDATE()),
(4, 'Physical Education', 9.0, 'Excellent performance in sports.', 0, NULL, GETDATE()),
(4, 'Geography', 7.5, 'Good understanding of geographic concepts.', 0, NULL, GETDATE()),

-- Subject Details for Academic Report 5
(5, 'Arts', 10.0, 'Exceptional creativity and execution.', 0, NULL, GETDATE()),
(5, 'Mathematics', 8.5, 'Good analytical skills.', 0, NULL, GETDATE()),
(5, 'English', 9.0, 'Strong command of the language.', 0, NULL, GETDATE());

INSERT INTO [dbo].[Activity] 
    ([Activity_Name], [Description], [StartDate], [EndDate], [Village_Id], [Organizer], [Status], [Event_Id], [Budget], [IsDeleted], [CreatedBy], [CreatedDate])
VALUES
    ('Tree Planting Campaign', 'A community effort to plant trees around the village.', '2025-01-10', '2025-01-10', 'V001', 'Environment Committee', 'Planned', NULL, NULL, 0, NULL, GETDATE()),
    ('Health Awareness Workshop', 'An interactive session focusing on hygiene and nutrition.', '2025-01-15', '2025-01-15', 'V002', 'Health Department', 'Planned', NULL, NULL, 0, NULL, GETDATE()),
    ('Sports Day', 'A day dedicated to various sports activities.', '2025-01-20', '2025-01-20', 'V003', 'Sports Committee', 'Planned', NULL, NULL, 0, NULL, GETDATE()),
    ('Craft Workshop', 'A creative workshop for children to learn crafts.', '2025-01-25', '2025-01-25', 'V004', 'Art Club', 'Planned', NULL, NULL, 0, NULL, GETDATE()),
    ('Fundraising Gala', 'An evening to raise funds for village projects.', '2025-01-30', '2025-01-30', 'V005', 'Fundraising Team', 'Planned', NULL, NULL, 0, NULL, GETDATE());


INSERT INTO [dbo].[Inventory] 
    ([Item_Name], [Description], [Quantity], [Purpose], [BelongsTo], [BelongsToId], [PurchaseDate], [LastInspectionDate], [MaintenanceStatus], [IsDeleted], [CreatedBy], [CreatedDate]) 
VALUES
-- Items for Village V001
('Soccer Balls', 'Standard soccer balls for village activities.', 20, 'Sports Activities', 'Village', 'V001', '2024-01-01', '2024-06-15', 'Good', 0, NULL, GETDATE()),
('Chairs', 'Plastic chairs for village meetings.', 50, 'Meetings and Events', 'Village', 'V001', '2023-12-20', '2024-06-10', 'Good', 0, NULL, GETDATE()),
('Projector', 'Multimedia projector for presentations.', 1, 'Educational Purposes', 'Village', 'V001', '2024-02-10', '2024-06-10', 'Good', 0, NULL, GETDATE()),

-- Items for House H001
('Beds', 'Single beds for children.', 5, 'Accommodation', 'House', 'H001', '2023-11-01', '2024-06-15', 'Good', 0, NULL, GETDATE()),
('Study Desks', 'Desks for children to study.', 5, 'Education', 'House', 'H001', '2023-12-15', '2024-06-20', 'Good', 0, NULL, GETDATE()),
('Cooking Utensils', 'Basic utensils for cooking.', 10, 'Daily Cooking', 'House', 'H001', '2023-10-05', '2024-06-05', 'Good', 0, NULL, GETDATE()),

-- Items for Village V002
('Volleyball Nets', 'Volleyball nets for sports events.', 2, 'Sports Activities', 'Village', 'V002', '2023-09-15', '2024-05-10', 'Good', 0, NULL, GETDATE()),
('Tables', 'Wooden tables for dining and meetings.', 10, 'Dining and Events', 'Village', 'V002', '2024-01-25', '2024-06-25', 'Good', 0, NULL, GETDATE()),

-- Items for House H002
('Mattresses', 'Comfortable mattresses for beds.', 6, 'Accommodation', 'House', 'H002', '2023-08-01', '2024-06-10', 'Good', 0, NULL, GETDATE()),
('Laptops', 'Basic laptops for children.', 2, 'Education', 'House', 'H002', '2024-02-01', '2024-06-10', 'Good', 0, NULL, GETDATE()),

-- Items for Village V003
('Books', 'Educational books for the library.', 100, 'Education', 'Village', 'V003', '2023-07-10', '2024-06-10', 'Good', 0, NULL, GETDATE()),
('Water Dispensers', 'Automatic water dispensers.', 3, 'Daily Use', 'Village', 'V003', '2024-01-01', '2024-06-05', 'Good', 0, NULL, GETDATE()),

-- Items for House H003
('Wardrobes', 'Wooden wardrobes for storing clothes.', 7, 'Accommodation', 'House', 'H003', '2023-06-01', '2024-05-15', 'Good', 0, NULL, GETDATE()),
('Plates', 'Ceramic plates for dining.', 15, 'Daily Meals', 'House', 'H003', '2023-11-20', '2024-06-20', 'Good', 0, NULL, GETDATE());

INSERT INTO [dbo].[ChildNeeds] 
    ([Child_Id], [NeedDescription], [NeedType], [Priority], [FulfilledDate], [Remarks], [Status], [IsDeleted], [CreatedDate]) 
VALUES
('C001', 'Regular health checkup needed', 'Health', 'Medium', NULL, 'Annual checkup due', 'Pending', 0, GETDATE()),
('C002', 'School supplies requirement', 'Education', 'Medium', NULL, 'Books and stationery needed', 'Pending', 0, GETDATE()),
('C003', 'Vitamin supplements needed', 'Health', 'Medium', NULL, 'For immune system boost', 'Pending', 0, GETDATE()),
('C004', 'Sports equipment', 'Basic Needs', 'Low', NULL, 'For physical activities', 'Pending', 0, GETDATE()),
('C005', 'School uniform needed', 'Basic Needs', 'Medium', NULL, 'Current uniform worn out', 'Pending', 0, GETDATE());

INSERT INTO [dbo].[ChildProgress] 
    ([Child_Id], [Description], [Date], [Category], [Event_Id], [Activity_Id], [IsDeleted], [CreatedBy], [CreatedDate]) 
VALUES
('C001', 'Completed the semester with excellent grades.', '2024-01-15', 'Education', NULL, NULL, 0, NULL, GETDATE()),
('C002', 'Improved reading and writing skills significantly.', '2024-02-10', 'Education', NULL, NULL, 0, NULL, GETDATE()),
('C003', 'Participated in the annual health check-up.', '2024-03-05', 'Health', NULL, NULL, 0, NULL, GETDATE()),
('C004', 'Vaccination completed successfully.', '2024-02-20', 'Health', NULL, NULL, 0, NULL, GETDATE()),
('C005', 'Won the first prize in the art competition.', '2024-01-30', 'Arts', NULL, NULL, 0, NULL, GETDATE());