IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'dbo.usp_getItems') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE dbo.usp_getItems
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.usp_getItems
(
	@IsActive BIT = NULL,
	@Total INT OUT
)
-- WITH ENCRYPTION, RECOMPILE, EXECUTE AS CALLER|SELF|OWNER| 'user_name'
AS BEGIN
	
	SELECT tt.ID, tt.Item, tt.ItemDescription, tt.CreatedDate, tt.UpdatedDate, tt.CompletedDate, tt.Status, tt.IsDeleted
	FROM TblTodo tt 
	WHERE tt.IsDeleted = ISNULL(@IsActive,1)
	SELECT @Total = @@ROWCOUNT

END
GO