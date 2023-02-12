using TodoApp.Models;
using System.Data.SqlClient;
using TodoApp.Util;
using TodoApp.Repository.SpNames;
using Microsoft.SqlServer.Server;

namespace TodoApp.Repository.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private SqlCommand _command;
        private readonly string? _connectionString;

        public ItemRepository(IConfiguration configuration)
        {
            _configuration = configuration ;
            _connectionString = configuration.GetConnectionString("TodoDb");
            _connection = new SqlConnection(_connectionString);
        }
        public bool ChanngeStatusOfTask(Status status)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItem(Guid id)
        {
            _command = _connection.CreateCommand();
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = SPNames.DELETE_ITEM;

            _command.Parameters.Clear();
            _command.Parameters.AddWithValue("@Id", id);
            int result = 0; 
            try
            {
                _connection.Open();
                result = await _command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result > 0;
        }

        public async Task<ItemResults<Item>> GetItems()
        {
            _command = _connection.CreateCommand();
            _command.CommandText = SPNames.GET_ITEMS;
            _command.Parameters.AddWithValue("@IsActive", DBNull.Value);
            _command.CommandType = System.Data.CommandType.StoredProcedure;

            _command.Parameters.Add("@Total", System.Data.SqlDbType.Int);
            _command.Parameters["@Total"].Direction = System.Data.ParameterDirection.Output;

            ItemResults<Item> items = new ItemResults<Item>();
            try
            {
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Item item = new Item();
                        item.Id = Guid.Parse(dr["Id"].ToString());
                        item.Name = dr["Item"].ToString();
                        item.Description = dr["ItemDescription"].ToString();
                        item.Status = Convert.ToByte(dr["Status"]);
                        item.CreateDate = Convert.ToDateTime(dr["CreatedDate"]);
                        item.UpdatedTime = Convert.ToDateTime(dr["UpdatedDate"]);
                        if (dr["CompletedDate"] != DBNull.Value)
                            item.Completedtime = Convert.ToDateTime(dr["CompletedDate"]);
                        item.ActiveNess = Convert.ToByte(dr["IsDeleted"]);
                        items.Data.Add(item);
                    }
                }
                dr.Close();
            }
            catch (Exception)
            {
                throw;
            }
            var totalCount = _command.Parameters["@Total"].Value != DBNull.Value ? 
                Convert.ToInt32(_command.Parameters["@Total"].Value): 0;
            items.Total = totalCount; 
            return items;
        }

        public async Task<Item> GetItem(Guid id)
        {
            _command = _connection.CreateCommand();
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = SPNames.GET_ITEM;


            Item item = new Item();
            _command.Parameters.Clear();
            _command.Parameters.AddWithValue("@Id", id);
            try
            {
                await _connection.OpenAsync();
                SqlDataReader dr = await _command.ExecuteReaderAsync();
                while (dr.Read())
                {
                    item.Id = Guid.Parse(dr["Id"].ToString());
                    item.Name = dr["Item"].ToString();
                    item.Description= dr["ItemDescription"].ToString();
                    item.Status = Convert.ToByte(dr["Status"]);
                    item.CreateDate = Convert.ToDateTime(dr["CreatedDate"]);
                    item.UpdatedTime = Convert.ToDateTime(dr["UpdatedDate"]);
                    if (dr["CompletedDate"] != DBNull.Value)
                        item.Completedtime = Convert.ToDateTime(dr["CompletedDate"]);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return item;
        }

        public async Task<bool> UpsertItem(Item item)
        {
            
            _command = _connection.CreateCommand();
            _command.CommandText = SPNames.UPSERT_ITEM;
            _command.CommandType = System.Data.CommandType.StoredProcedure;

            int result = 0;
            if(item.Id == Guid.Empty)
            {
                _command.Parameters.Clear();
                _command.Parameters.AddWithValue("@Id", DBNull.Value);
                _command.Parameters.AddWithValue("@ItemName", item.Name);
                _command.Parameters.AddWithValue("@ItemDescription", item.Description);
            }
            else
            {
                _command.Parameters.Clear();
                _command.Parameters.AddWithValue("@Id", item.Id);
                _command.Parameters.AddWithValue("@ItemName", item.Name);
                _command.Parameters.AddWithValue("@ItemDescription", item.Description);
            }

            try
            {
                _connection.Open();
                result = await _command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
            return result>0;
        }

        public void Dispose()
        {
            if(_connection != null )
            {
                if(_command!= null)
                    _command.Dispose();
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
