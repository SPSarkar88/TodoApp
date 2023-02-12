using TodoApp.Models;
using TodoApp.Util;

namespace TodoApp.Repository
{
    public interface IItemRepository: IDisposable
    {
        public Task<bool> UpsertItem(Item item);
        public Task<ItemResults<Item>> GetItems();
        public Task<Item> GetItem(Guid id);
        public bool ChanngeStatusOfTask(Status status);
        public Task<bool> DeleteItem(Guid id);
    }

    
}
