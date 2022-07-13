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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE TABLE [CategoryModels] (
        [CategoryID] int NOT NULL IDENTITY,
        [CategoryName] nvarchar(max) NULL,
        CONSTRAINT [PK_CategoryModels] PRIMARY KEY ([CategoryID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE TABLE [Roles] (
        [RoleID] int NOT NULL IDENTITY,
        [RoleName] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE TABLE [Foods] (
        [FoodID] int NOT NULL IDENTITY,
        [FoodName] varchar(50) NOT NULL,
        [CategoryID] int NOT NULL,
        [FoodAmout] int NOT NULL,
        [FoodPrice] int NOT NULL,
        [FoodImage] varchar(255) NOT NULL,
        [CreateDate] datetime2 NOT NULL,
        [FoodNote] nvarchar(300) NULL,
        [IsDelete] bit NOT NULL,
        CONSTRAINT [PK_Foods] PRIMARY KEY ([FoodID]),
        CONSTRAINT [FK_Foods_CategoryModels_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [CategoryModels] ([CategoryID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE TABLE [Users] (
        [UserID] int NOT NULL IDENTITY,
        [UserFullName] nvarchar(55) NOT NULL,
        [Email] varchar(50) NOT NULL,
        [UserPassWord] varchar(50) NOT NULL,
        [Gender] int NOT NULL,
        [UserBirthDay] datetime2 NOT NULL,
        [UsersPhone] varchar(15) NOT NULL,
        [UserAddress] nvarchar(255) NOT NULL,
        [RoleID] int NOT NULL,
        [IsDelete] bit NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserID]),
        CONSTRAINT [FK_Users_Roles_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Roles] ([RoleID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE TABLE [Carts] (
        [CartId] int NOT NULL IDENTITY,
        [Note] nvarchar(255) NULL,
        [TotalPrice] float NOT NULL,
        [OrderDate] datetime2 NOT NULL,
        [UserID] int NOT NULL,
        CONSTRAINT [PK_Carts] PRIMARY KEY ([CartId]),
        CONSTRAINT [FK_Carts_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE TABLE [CartDetails] (
        [CartDetailID] int NOT NULL IDENTITY,
        [ShipDate] datetime2 NOT NULL,
        [TotalPrice] real NOT NULL,
        [Quantity] int NOT NULL,
        [CartID] int NOT NULL,
        [FoodID] int NOT NULL,
        CONSTRAINT [PK_CartDetails] PRIMARY KEY ([CartDetailID]),
        CONSTRAINT [FK_CartDetails_Carts_CartID] FOREIGN KEY ([CartID]) REFERENCES [Carts] ([CartId]) ON DELETE CASCADE,
        CONSTRAINT [FK_CartDetails_Foods_FoodID] FOREIGN KEY ([FoodID]) REFERENCES [Foods] ([FoodID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE INDEX [IX_CartDetails_CartID] ON [CartDetails] ([CartID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE INDEX [IX_CartDetails_FoodID] ON [CartDetails] ([FoodID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE INDEX [IX_Carts_UserID] ON [Carts] ([UserID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE INDEX [IX_Foods_CategoryID] ON [Foods] ([CategoryID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    CREATE INDEX [IX_Users_RoleID] ON [Users] ([RoleID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220618113430_dbbbb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220618113430_dbbbb', N'5.0.17');
END;
GO

COMMIT;
GO

