IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Discount] (
    [Id] bigint NOT NULL,
    [Brand] nvarchar(max) NOT NULL,
    [Barcode] nvarchar(max) NOT NULL,
    [ProductName] nvarchar(max) NOT NULL,
    [OriginalPrice] decimal(18,2) NOT NULL,
    [DiscountedPrice] int NOT NULL,
    [ImagePath] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Discount] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241116153337_Init', N'8.0.10');
GO

COMMIT;
GO

