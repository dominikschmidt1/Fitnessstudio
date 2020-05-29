
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/28/2020 10:09:22
-- Generated from EDMX file: C:\Users\I2CM Developer\Source\Repos\Fitnessstudio\FitnessstudioLib\FitnessstudioModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Fitnessstudio];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_VerfuegtUeber_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VerfuegtUeber] DROP CONSTRAINT [FK_VerfuegtUeber_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_VerfuegtUeber_Leistung]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VerfuegtUeber] DROP CONSTRAINT [FK_VerfuegtUeber_Leistung];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[LeistungSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeistungSet];
GO
IF OBJECT_ID(N'[dbo].[VerfuegtUeber]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VerfuegtUeber];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nachname] nvarchar(max)  NOT NULL,
    [Vorname] nvarchar(max)  NOT NULL,
    [Wohnort] nvarchar(max)  NOT NULL,
    [Bank] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [RoleStaff] bit  NOT NULL,
    [RoleMember] bit  NOT NULL
);
GO

-- Creating table 'LeistungSet'
CREATE TABLE [dbo].[LeistungSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Bezeichnung] nvarchar(max)  NOT NULL,
    [Preis] int  NOT NULL
);
GO

-- Creating table 'VerfuegtUeber'
CREATE TABLE [dbo].[VerfuegtUeber] (
    [GehoertZu_Id] int  NOT NULL,
    [VerfuegtUeber_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LeistungSet'
ALTER TABLE [dbo].[LeistungSet]
ADD CONSTRAINT [PK_LeistungSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [GehoertZu_Id], [VerfuegtUeber_Id] in table 'VerfuegtUeber'
ALTER TABLE [dbo].[VerfuegtUeber]
ADD CONSTRAINT [PK_VerfuegtUeber]
    PRIMARY KEY CLUSTERED ([GehoertZu_Id], [VerfuegtUeber_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GehoertZu_Id] in table 'VerfuegtUeber'
ALTER TABLE [dbo].[VerfuegtUeber]
ADD CONSTRAINT [FK_VerfuegtUeber_Person]
    FOREIGN KEY ([GehoertZu_Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [VerfuegtUeber_Id] in table 'VerfuegtUeber'
ALTER TABLE [dbo].[VerfuegtUeber]
ADD CONSTRAINT [FK_VerfuegtUeber_Leistung]
    FOREIGN KEY ([VerfuegtUeber_Id])
    REFERENCES [dbo].[LeistungSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VerfuegtUeber_Leistung'
CREATE INDEX [IX_FK_VerfuegtUeber_Leistung]
ON [dbo].[VerfuegtUeber]
    ([VerfuegtUeber_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------