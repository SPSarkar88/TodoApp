using TodoApp.Models;
using TodoApp.Util;

namespace TodoApp.Services
{
    public interface IItemService : IDisposable
    {
        Task<Item> GetItem(Guid id);
        Task<ItemResults<Item>> GetItems();
        Task<bool> InsertItem(Item item);
        Task<bool> UpdateItem(Item item);
        Task<bool> DeleteItem(Guid id);
    }
}