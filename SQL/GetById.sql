IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'dbo.usp_getItemById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE dbo.usp_getItemById
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.usp_getItemById
(
	@Id UNIQUEIDENTIFIER
)
-- WITH ENCRYPTION, RECOMPILE, EXECUTE AS CALLER|SELF|OWNER| 'user_name'
AS BEGIN
	SET NOCOUNT ON;
	SELECT tt.ID, tt.Item, tt.ItemDescription, tt.CreatedDate, tt.UpdatedDate, tt.CompletedDate, tt.Status 
	FROM TblTodo tt 
	WHERE tt.ID = @Id AND tt.IsDeleted <> 0
END
GO