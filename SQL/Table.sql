
CREATE TABLE dbo.TblTodo
(
	ID UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	Item VARCHAR(200) NOT NULL,
	ItemDescription VARCHAR(MAX) NOT NULL, 
	CreatedDate DATETIME NOT NULL,
	UpdatedDate DATETIME NOT NULL DEFAULT GETDATE(),
	CompletedDate DATETIME NULL,
	[Status] TINYINT NOT NULL DEFAULT 0, -- 0 incomplete, 1 Complete, 2 Cancelled
	IsDeleted BIT NOT NULL DEFAULT 0, -- 0 deleted, 1 not deleted
	CONSTRAINT PK_TodoTbl_ID PRIMARY KEY (ID)
)
GO

CREATE TABLE DB_Errors
(
	ErrorID        INT IDENTITY(1, 1),
	UserName       VARCHAR(100),
	ErrorNumber    INT,
	ErrorState     INT,
	ErrorSeverity  INT,
	ErrorLine      INT,
	ErrorProcedure VARCHAR(MAX),
	ErrorMessage   VARCHAR(MAX),
	ErrorDateTime  DATETIME
)
GO