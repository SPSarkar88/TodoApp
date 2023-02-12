namespace TodoApp.Repository.SpNames
{
    public static class SPNames
    {
        public readonly static string UPSERT_ITEM = "dbo.usp_upsertItem";
        public readonly static string DELETE_ITEM = "dbo.usp_deleteItem";
        public readonly static string GET_ITEMS = "dbo.usp_getItems";
        public readonly static string GET_ITEM = "dbo.usp_getItemById";
        public readonly static string CHANGE_STATUS_OF_ITEM = "dbo.usp_changeStatusItem";
    }
}
