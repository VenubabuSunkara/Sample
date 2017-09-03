
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/08/2017 10:49:31
-- Generated from EDMX file: D:\TransForcePortalManagement\TFWebPortalAPI\Entitys\TFEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TFPM];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CustomerDOTNumbers_CustomerProfiles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerDOTNumbers] DROP CONSTRAINT [FK_CustomerDOTNumbers_CustomerProfiles];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_Location_Configuration_CustomerProfiles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Location_Configuration] DROP CONSTRAINT [FK_Location_Configuration_CustomerProfiles];
GO
IF OBJECT_ID(N'[dbo].[FK_Location_CustomerProfiles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Location] DROP CONSTRAINT [FK_Location_CustomerProfiles];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationMetadata_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationMetadata] DROP CONSTRAINT [FK_LocationMetadata_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationMetadata_FieldType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationMetadata] DROP CONSTRAINT [FK_LocationMetadata_FieldType];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationMetadata_Location]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationMetadata] DROP CONSTRAINT [FK_LocationMetadata_Location];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationMetadata_Location_Configuration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationMetadata] DROP CONSTRAINT [FK_LocationMetadata_Location_Configuration];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[CustomerDOTNumbers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerDOTNumbers];
GO
IF OBJECT_ID(N'[dbo].[CustomerProfiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerProfiles];
GO
IF OBJECT_ID(N'[dbo].[Location]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Location];
GO
IF OBJECT_ID(N'[dbo].[Location_Configuration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Location_Configuration];
GO
IF OBJECT_ID(N'[dbo].[LocationFieldType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LocationFieldType];
GO
IF OBJECT_ID(N'[dbo].[LocationMetadata]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LocationMetadata];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Title] nvarchar(10)  NULL,
    [AccountNumber] nvarchar(10)  NULL,
    [IsActive] bit  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [CreatedBy] nvarchar(128)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(128)  NULL,
    [ModifiedOn] datetime  NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'CustomerDOTNumbers'
CREATE TABLE [dbo].[CustomerDOTNumbers] (
    [ItemID] int IDENTITY(1,1) NOT NULL,
    [CustomerID] int  NOT NULL,
    [DOTNumber] nvarchar(128)  NOT NULL,
    [DOTTag] nvarchar(500)  NULL,
    [IsActive] bit  NOT NULL,
    [CreatedBy] nvarchar(128)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(128)  NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'CustomerProfiles'
CREATE TABLE [dbo].[CustomerProfiles] (
    [CompanyID] int IDENTITY(1,1) NOT NULL,
    [CustomerName] nvarchar(75)  NOT NULL,
    [AccountNumber] nvarchar(10)  NULL,
    [AccountOwnerName] nvarchar(50)  NOT NULL,
    [AccountOwnerEmail] nvarchar(50)  NOT NULL,
    [CompanyLogo] nvarchar(max)  NULL,
    [AddressLine1] nvarchar(500)  NULL,
    [AddressLine2] nvarchar(500)  NULL,
    [City] nvarchar(50)  NULL,
    [State] nvarchar(50)  NULL,
    [ZipCode] nvarchar(50)  NULL,
    [CreatedBy] nvarchar(128)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(128)  NULL,
    [ModifiedOn] datetime  NULL,
    [isActive] bit  NULL
);
GO

-- Creating table 'Location_Configuration'
CREATE TABLE [dbo].[Location_Configuration] (
    [LocConfigID] int IDENTITY(1,1) NOT NULL,
    [CompanyID] int  NOT NULL,
    [Key] nvarchar(50)  NOT NULL,
    [Value] nvarchar(50)  NOT NULL,
    [Relation] int  NOT NULL,
    [CreatedBy] nvarchar(128)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(128)  NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'Location'
CREATE TABLE [dbo].[Location] (
    [LocId] int IDENTITY(1,1) NOT NULL,
    [LocName] nvarchar(64)  NULL,
    [CompanyID] int  NOT NULL
);
GO

-- Creating table 'LocationFieldType'
CREATE TABLE [dbo].[LocationFieldType] (
    [LocFID] int IDENTITY(1,1) NOT NULL,
    [FieldType] nvarchar(50)  NULL
);
GO

-- Creating table 'LocationMetadata'
CREATE TABLE [dbo].[LocationMetadata] (
    [LocMID] int IDENTITY(1,1) NOT NULL,
    [CustomerID] int  NOT NULL,
    [LocConfigID] int  NOT NULL,
    [Key] nvarchar(50)  NOT NULL,
    [Value] nvarchar(50)  NOT NULL,
    [FieldType] int  NOT NULL,
    [Tags] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(128)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(128)  NULL,
    [ModifiedOn] datetime  NULL,
    [LocId] int  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [RoleId] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ItemID] in table 'CustomerDOTNumbers'
ALTER TABLE [dbo].[CustomerDOTNumbers]
ADD CONSTRAINT [PK_CustomerDOTNumbers]
    PRIMARY KEY CLUSTERED ([ItemID] ASC);
GO

-- Creating primary key on [CompanyID] in table 'CustomerProfiles'
ALTER TABLE [dbo].[CustomerProfiles]
ADD CONSTRAINT [PK_CustomerProfiles]
    PRIMARY KEY CLUSTERED ([CompanyID] ASC);
GO

-- Creating primary key on [LocConfigID] in table 'Location_Configuration'
ALTER TABLE [dbo].[Location_Configuration]
ADD CONSTRAINT [PK_Location_Configuration]
    PRIMARY KEY CLUSTERED ([LocConfigID] ASC);
GO

-- Creating primary key on [LocId] in table 'Location'
ALTER TABLE [dbo].[Location]
ADD CONSTRAINT [PK_Location]
    PRIMARY KEY CLUSTERED ([LocId] ASC);
GO

-- Creating primary key on [LocFID] in table 'LocationFieldType'
ALTER TABLE [dbo].[LocationFieldType]
ADD CONSTRAINT [PK_LocationFieldType]
    PRIMARY KEY CLUSTERED ([LocFID] ASC);
GO

-- Creating primary key on [LocMID] in table 'LocationMetadata'
ALTER TABLE [dbo].[LocationMetadata]
ADD CONSTRAINT [PK_LocationMetadata]
    PRIMARY KEY CLUSTERED ([LocMID] ASC);
GO

-- Creating primary key on [RoleId], [UserId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([RoleId], [UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [CustomerID] in table 'CustomerDOTNumbers'
ALTER TABLE [dbo].[CustomerDOTNumbers]
ADD CONSTRAINT [FK_CustomerDOTNumbers_CustomerProfiles]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[CustomerProfiles]
        ([CompanyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerDOTNumbers_CustomerProfiles'
CREATE INDEX [IX_FK_CustomerDOTNumbers_CustomerProfiles]
ON [dbo].[CustomerDOTNumbers]
    ([CustomerID]);
GO

-- Creating foreign key on [CompanyID] in table 'Location_Configuration'
ALTER TABLE [dbo].[Location_Configuration]
ADD CONSTRAINT [FK_Location_Configuration_CustomerProfiles]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[CustomerProfiles]
        ([CompanyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Location_Configuration_CustomerProfiles'
CREATE INDEX [IX_FK_Location_Configuration_CustomerProfiles]
ON [dbo].[Location_Configuration]
    ([CompanyID]);
GO

-- Creating foreign key on [RoleId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUser'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUser]
ON [dbo].[AspNetUserRoles]
    ([UserId]);
GO

-- Creating foreign key on [CompanyID] in table 'Location'
ALTER TABLE [dbo].[Location]
ADD CONSTRAINT [FK_Location_CustomerProfiles]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[CustomerProfiles]
        ([CompanyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Location_CustomerProfiles'
CREATE INDEX [IX_FK_Location_CustomerProfiles]
ON [dbo].[Location]
    ([CompanyID]);
GO

-- Creating foreign key on [CustomerID] in table 'LocationMetadata'
ALTER TABLE [dbo].[LocationMetadata]
ADD CONSTRAINT [FK_LocationMetadata_Customer]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[CustomerProfiles]
        ([CompanyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMetadata_Customer'
CREATE INDEX [IX_FK_LocationMetadata_Customer]
ON [dbo].[LocationMetadata]
    ([CustomerID]);
GO

-- Creating foreign key on [LocId] in table 'LocationMetadata'
ALTER TABLE [dbo].[LocationMetadata]
ADD CONSTRAINT [FK_LocationMetadata_Location]
    FOREIGN KEY ([LocId])
    REFERENCES [dbo].[Location]
        ([LocId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMetadata_Location'
CREATE INDEX [IX_FK_LocationMetadata_Location]
ON [dbo].[LocationMetadata]
    ([LocId]);
GO

-- Creating foreign key on [LocConfigID] in table 'LocationMetadata'
ALTER TABLE [dbo].[LocationMetadata]
ADD CONSTRAINT [FK_LocationMetadata_Location_Configuration]
    FOREIGN KEY ([LocConfigID])
    REFERENCES [dbo].[Location_Configuration]
        ([LocConfigID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMetadata_Location_Configuration'
CREATE INDEX [IX_FK_LocationMetadata_Location_Configuration]
ON [dbo].[LocationMetadata]
    ([LocConfigID]);
GO

-- Creating foreign key on [FieldType] in table 'LocationMetadata'
ALTER TABLE [dbo].[LocationMetadata]
ADD CONSTRAINT [FK_LocationMetadata_FieldType]
    FOREIGN KEY ([FieldType])
    REFERENCES [dbo].[LocationFieldType]
        ([LocFID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMetadata_FieldType'
CREATE INDEX [IX_FK_LocationMetadata_FieldType]
ON [dbo].[LocationMetadata]
    ([FieldType]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------