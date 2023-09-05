using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog;
using RazorProjectV5.Services.Interfaces;

namespace RazorProjectV5.Pages.Item
{
    public class EditItemModel : PageModel
    {
        private IItemService _itemService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(); // NLog Logger instance

        [BindProperty]
        public Models.Item Item { get; set; }

        public EditItemModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Item = _itemService.GetItem(id);
                if (Item == null)
                {
                    TempData["ErrorMessage"] = "Item not found.";
                    Logger.Warn("Item not found with ID: {ItemId}", id); // Log the warning
                    //return RedirectToPage("/NotFound");
                }

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving the item.";
                Logger.Error(ex, "An error occurred while retrieving the item with ID: {ItemId}", id); // Log the error
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the item. Please try again later.");
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _itemService.UpdateItem(Item);
                return RedirectToPage("GetAllItems");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the item.";
                Logger.Error(ex, "An error occurred while updating the item with ID: {ItemId}", Item?.Id); // Log the error
                ModelState.AddModelError(string.Empty, "An error occurred while updating the item. Please try again later.");
                return Page();
            }
        }
    }
}
