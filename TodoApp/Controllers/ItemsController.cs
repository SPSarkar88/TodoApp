using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Util;

namespace TodoApp.Controllers
{
    public class ItemsController : Controller
    {

        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService ;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            ItemResults<Item> items = await _itemService.GetItems();
            return View(items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            Item item = await _itemService.GetItem(id);
            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {
            if(! ModelState.IsValid) return View(item);
            var isInsertSucess = await _itemService.InsertItem(item);

            return isInsertSucess ? RedirectToAction(nameof(Index)) : View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _itemService.GetItem(id) ;
            if(item!= null)
            {
                return View(item);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Item item)
        {
            if (!ModelState.IsValid) { return View(item); }
            if (item == null && item.Id == Guid.Empty){return View(item);}
            var isUpdateSucess = await _itemService.UpdateItem(item);
            return isUpdateSucess ? RedirectToAction(nameof(Index)): View(item);

        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _itemService.GetItem(id);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deleteResult = await _itemService.DeleteItem(id);
            return deleteResult ? RedirectToAction(nameof(Index)): RedirectToAction(nameof(Delete), id);
        }
    }
}
