using RazorProjectV5.Models;

namespace RazorProjectV5.Services.Interfaces
{
    public interface IItemService
    {

        List<Item> GetItems();

        Item GetItem(int id);

        void AddItem(Item item);

        Item DeleteItem(int? itemId);

        void UpdateItem(Item item);

        IEnumerable<Item> SearchItem(string searchItem);

        IEnumerable<Item> PriceFilter(double maxPrice, double minPrice);


    }
}
