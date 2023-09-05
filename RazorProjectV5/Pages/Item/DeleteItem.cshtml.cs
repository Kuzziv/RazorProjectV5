using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog;
using RazorProjectV5.Services.Interfaces;

namespace RazorProjectV5.Pages.Item
{
    public class DeleteItemModel : PageModel
    {
        private IItemService _itemService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [BindProperty]
        public Models.Item Item { get; set; }

        public DeleteItemModel(IItemService itemService)
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
                }
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Item not found with ID: {ItemId}", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving the item.";
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                var deletedItem = _itemService.DeleteItem(Item.Id);

                if (deletedItem == null)
                {
                    // Log a warning if the item wasn't found for deletion.
                    Logger.Warn("Item not found with ID: {ItemId}", Item.Id);
                    TempData["ErrorMessage"] = "Item not found for deletion.";
                }
                else
                {
                    // Log an information message for a successful deletion.
                    Logger.Info("Item with ID {ItemId} deleted successfully.", Item.Id);
                    return RedirectToPage("/Item/GetAllItems"); // Redirect to the item list page
                }
            }
            catch (Exception ex)
            {
                // Log an error for any unexpected exception.
                Logger.Error(ex, "An error occurred while deleting an item with ID: {ItemId}", Item.Id);
                TempData["ErrorMessage"] = "An error occurred while deleting the item.";
            }

            // Return the page, displaying any error messages in the TempData.
            return RedirectToPage("/Item/DeleteItem", new { id = Item.Id }); // Redirect back to delete confirmation page
        }
    }
}
