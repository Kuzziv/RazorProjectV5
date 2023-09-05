using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProjectV5.Services.Interfaces;

namespace RazorProjectV5.Pages.Item
{
    public class CreateItemModel : PageModel
    {
        private IItemService _itemService;
        private ILogger<CreateItemModel> _logger; // Add a private ILogger field

        [BindProperty]
        public Models.Item item { get; set; }

        public CreateItemModel(IItemService itemService, ILogger<CreateItemModel> logger) // Inject the ILogger
        {
            _itemService = itemService;
            _logger = logger; // Initialize the logger
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _itemService.AddItem(item);

                return RedirectToPage("./GetAllItems");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an item.");
                ModelState.AddModelError(string.Empty, "An error occurred while adding the item. Please try again later.");
                return Page(); // Or return an error view
            }
        }
    }
}
