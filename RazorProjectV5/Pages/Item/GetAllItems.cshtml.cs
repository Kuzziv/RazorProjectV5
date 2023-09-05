using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog;
using RazorProjectV5.Services.Interfaces;

namespace RazorProjectV5.Pages.Item
{
    public class GetAllItemsModel : PageModel
    {
        private IItemService _itemService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public List<Models.Item> Items { get; set; }

        [BindProperty]
        public string SearchString { get; set; }
        [BindProperty]
        public double MaxPrice { get; set; }
        [BindProperty]
        public double MinPrice { get; set; }

        public GetAllItemsModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        public void OnGet()
        {
            try
            {
                Items = _itemService.GetItems();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "An error occurred while retrieving items in OnGet");
                ModelState.AddModelError(string.Empty, "Oops, something went wrong. Please try again later.");
            }
        }


        public IActionResult OnPostSearch()
        {
            try
            {
                if (_itemService.SearchItem(SearchString).ToList().Count == 0)
                {
                    Items = _itemService.GetItems();
                    throw new Exception("No items is found.");
                }
                else
                {
                    Items = _itemService.SearchItem(SearchString).ToList();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error occurred while searching items.");
                ModelState.AddModelError(string.Empty, "Oops, something went wrong. Please try again later.");
            }
            return Page();
        }


        public IActionResult OnPostPriceFilter()
        {
            try
            {
                Items = _itemService.PriceFilter(MaxPrice, MinPrice).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error occurred while filtering items by price.");
                ModelState.AddModelError(string.Empty, "Oops, something went wrong. Please try again later.");
            }
            return Page();
        }

    }
}
