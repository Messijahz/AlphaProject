CREATE TABLE [Users] (
  [UserId] VARCHAR(36) PRIMARY KEY,
  [Username] VARCHAR(100) UNIQUE,
  [Email] VARCHAR(255) UNIQUE,
  [PasswordHash] TEXT,
  [CreatedAt] TIMESTAMP NOT NULL,
  [IsDeleted] BOOLEAN DEFAULT (false)
)
GO

CREATE TABLE [Roles] (
  [RoleId] VARCHAR(36) PRIMARY KEY,
  [RoleName] VARCHAR(50) UNIQUE
)
GO

CREATE TABLE [UserRoles] (
  [UserId] VARCHAR(36),
  [RoleId] VARCHAR(36)
)
GO

CREATE TABLE [Projects] (
  [ProjectId] VARCHAR(36) PRIMARY KEY,
  [Name] VARCHAR(255),
  [CustomerId] VARCHAR(36),
  [Description] TEXT,
  [StartDate] DATE,
  [EndDate] DATE,
  [Budget] DECIMAL(18,2),
  [StatusId] VARCHAR(36),
  [ProjectManagerId] VARCHAR(36),
  [CreatedBy] VARCHAR(36),
  [CreatedAt] TIMESTAMP NOT NULL,
  [IsDeleted] BOOLEAN DEFAULT (false)
)
GO

CREATE TABLE [Customers] (
  [CustomerId] VARCHAR(36) PRIMARY KEY,
  [CustomerName] VARCHAR(255),
  [ContactPerson] VARCHAR(255),
  [FirstName] VARCHAR(100),
  [LastName] VARCHAR(100),
  [Email] VARCHAR(255),
  [PhoneNumber] VARCHAR(20),
  [StreetAddress] VARCHAR(255),
  [PostAddress] VARCHAR(255),
  [PostCode] VARCHAR(20),
  [City] VARCHAR(100),
  [ProjectTitle] VARCHAR(255),
  [IsDeleted] BOOLEAN DEFAULT (false)
)
GO

CREATE TABLE [Status] (
  [StatusId] VARCHAR(36) PRIMARY KEY,
  [StatusName] VARCHAR(50) DEFAULT 'New'
)
GO

CREATE TABLE [ProjectManagers] (
  [ProjectManagerId] VARCHAR(36) PRIMARY KEY,
  [UserId] VARCHAR(36),
  [FirstName] VARCHAR(100),
  [LastName] VARCHAR(100),
  [Email] VARCHAR(255)
)
GO

CREATE TABLE [ProjectMembers] (
  [MemberId] VARCHAR(36) PRIMARY KEY,
  [ProjectId] VARCHAR(36),
  [FirstName] VARCHAR(100),
  [LastName] VARCHAR(100),
  [Email] VARCHAR(255),
  [PhoneNumber] VARCHAR(20),
  [JobTitle] VARCHAR(255),
  [StreetAddress] VARCHAR(255),
  [PostAddress] VARCHAR(255),
  [PostCode] VARCHAR(20),
  [City] VARCHAR(100),
  [DateOfBirth] DATE,
  [IsDeleted] BOOLEAN DEFAULT (false)
)
GO

CREATE TABLE [ProjectImages] (
  [ImageId] VARCHAR(36) PRIMARY KEY,
  [ProjectId] VARCHAR(36),
  [ImageUrl] TEXT,
  [UploadedBy] VARCHAR(36),
  [UploadedAt] TIMESTAMP NOT NULL
)
GO

CREATE TABLE [Notifications] (
  [NotificationId] VARCHAR(36) PRIMARY KEY,
  [UserId] VARCHAR(36),
  [ProjectId] VARCHAR(36),
  [Message] TEXT,
  [CreatedAt] TIMESTAMP NOT NULL,
  [IsRead] BOOLEAN DEFAULT (false)
)
GO

CREATE TABLE [Messages] (
  [MessageId] VARCHAR(36) PRIMARY KEY,
  [SenderId] VARCHAR(36),
  [ReceiverId] VARCHAR(36),
  [Content] TEXT,
  [SentAt] TIMESTAMP NOT NULL,
  [IsRead] BOOLEAN DEFAULT (false)
)
GO

CREATE TABLE [ChangeLogs] (
  [LogId] VARCHAR(36) PRIMARY KEY,
  [UserId] VARCHAR(36),
  [ProjectId] VARCHAR(36),
  [ActionType] VARCHAR(50),
  [Timestamp] TIMESTAMP NOT NULL,
  [OldData] JSON,
  [NewData] JSON
)
GO

CREATE TABLE [OAuthLogins] (
  [UserId] VARCHAR(36),
  [Provider] VARCHAR(50),
  [ProviderKey] VARCHAR(255) UNIQUE
)
GO

CREATE TABLE [UserSettings] (
  [UserId] VARCHAR(36),
  [ThemePreference] VARCHAR(50),
  [CookieConsent] BOOLEAN DEFAULT (false)
)
GO

CREATE UNIQUE INDEX [Users_index_0] ON [Users] ("Email")
GO

CREATE INDEX [Projects_index_1] ON [Projects] ("Name")
GO

CREATE INDEX [Customers_index_2] ON [Customers] ("CustomerName")
GO

CREATE INDEX [ProjectManagers_index_3] ON [ProjectManagers] ("FirstName", "LastName")
GO

CREATE INDEX [ProjectMembers_index_4] ON [ProjectMembers] ("FirstName", "LastName", "Email")
GO

ALTER TABLE [UserRoles] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [UserRoles] ADD FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId])
GO

ALTER TABLE [Projects] ADD FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId])
GO

ALTER TABLE [Projects] ADD FOREIGN KEY ([StatusId]) REFERENCES [Status] ([StatusId])
GO

ALTER TABLE [Projects] ADD FOREIGN KEY ([ProjectManagerId]) REFERENCES [ProjectManagers] ([ProjectManagerId])
GO

ALTER TABLE [Projects] ADD FOREIGN KEY ([CreatedBy]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [ProjectManagers] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [ProjectMembers] ADD FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
GO

ALTER TABLE [ProjectImages] ADD FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
GO

ALTER TABLE [ProjectImages] ADD FOREIGN KEY ([UploadedBy]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [Notifications] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [Notifications] ADD FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
GO

ALTER TABLE [Messages] ADD FOREIGN KEY ([SenderId]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [Messages] ADD FOREIGN KEY ([ReceiverId]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [ChangeLogs] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [ChangeLogs] ADD FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
GO

ALTER TABLE [OAuthLogins] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [UserSettings] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO
