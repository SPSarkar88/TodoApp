IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'dbo.usp_upsertItem') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE dbo.usp_upsertItem
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.usp_upsertItem
(
	@Id UNIQUEIDENTIFIER = NULL,
	@ItemName VARCHAR(200),
	@ItemDescription VARCHAR(MAX) = NULL
)
AS 
BEGIN
	BEGIN TRY 
		BEGIN TRANSACTION UPSERT
			IF @Id IS NULL BEGIN
				INSERT INTO	dbo.TblTodo (
					ID, 
					Item, 
					ItemDescription,
					CreatedDate, 
					UpdatedDate, 
					CompletedDate, 
					[Status], 
					IsDeleted
				) VALUES 
				(
					NEWID(),
					@ItemName,
					ISNULL(@ItemDescription,NULL),
					GETDATE(),
					GETDATE(),
					NULL,
					0, 
					1
				)
			END
			ELSE
			BEGIN
				UPDATE TblTodo SET  
				Item = @ItemName, 
				ItemDescription = ISNULL(@ItemDescription,NULL), 
				UpdatedDate = GETDATE()
				WHERE ID = @Id
			END
		COMMIT TRANSACTION UPSERT
	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
		BEGIN
			ROLLBACK TRANSACTION UPSERT
		END
	END CATCH
END
GO