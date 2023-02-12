using TodoApp.Models;
using TodoApp.Repository;
using TodoApp.Util;

namespace TodoApp.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<bool> DeleteItem(Guid id)
        {
            var isDeleteSuccess = false;
            if (id != Guid.Empty) { isDeleteSuccess= await _itemRepository.DeleteItem(id); }
            return isDeleteSuccess;
        }

        public void Dispose()
        {
            _itemRepository.Dispose();
        }

        public async Task<Item> GetItem(Guid id)
        {
            return await _itemRepository.GetItem(id);
        }

        public async Task<ItemResults<Item>> GetItems()
        {
            return await _itemRepository.GetItems();
        }

        public async Task<bool> InsertItem(Item item)
        {
            var isInsertSucess = false;
            if (item != null) { isInsertSucess = await _itemRepository.UpsertItem(item); }
            return isInsertSucess;
        }

        public async Task<bool> UpdateItem(Item item)
        {
            var isupdateSucess = false;
            if (item != null) { isupdateSucess = await _itemRepository.UpsertItem(item); }
            return isupdateSucess;
        }
    }
}
