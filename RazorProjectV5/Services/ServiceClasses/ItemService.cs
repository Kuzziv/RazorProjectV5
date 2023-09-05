using NLog;
using RazorProjectV5.Models;
using RazorProjectV5.Services.Interfaces;

namespace RazorProjectV5.Services.ServiceClasses
{
    public class ItemService : IItemService
    {
        private List<Item> _items { get; set; }
        private ILogger<ItemService> _logger; // Add a private ILogger field
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger(); // NLog Logger instance
        private IJsonFileService<Item> _IjsonFileService;

        public ItemService(ILogger<ItemService> logger, IJsonFileService<Item> jsonFileService)
        {
            _items = MockData.MockItem.GetAllItemsModel();
            //_items = _IjsonFileService.GetAllAsync().Result.ToList();
            _logger = logger;
            _IjsonFileService = jsonFileService;
        }

        public void AddItem(Item item)
        {
            try
            {
                _items.Add(item);
                _IjsonFileService.SaveAsync(_items);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error occurred while adding an item.");
                throw;
            }
        }

        public Item DeleteItem(int? itemId)
        {
            try
            {
                foreach (Item item in _items)
                {
                    if (item.Id == itemId)
                    {
                        _items.Remove(item);
                        _IjsonFileService.SaveAsync(_items);
                        return item;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an item.");
                throw;
            }
        }

        public Item GetItem(int id)
        {
            try
            {
                foreach (Item item in _items)
                {
                    if (item.Id == id)
                    {
                        return item;
                    }
                }
                throw new ArgumentException($"Item with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving an item.");
                throw;
            }
        }

        public List<Item> GetItems()
        {
            try
            {
                //return null;
                //return MockData.MockItem.GetAllItemsModel();
                return _IjsonFileService.GetAllAsync().Result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting items.");
                throw;
            }
        }

        public void UpdateItem(Item nitem)
        {
            try
            {
                if (nitem != null)
                {
                    foreach (Item oItem in _items)
                    {
                        if (nitem.Id == oItem.Id)
                        {
                            oItem.Name = nitem.Name;
                            oItem.Price = nitem.Price;
                        }
                    }
                    _IjsonFileService.SaveAsync(_items);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating an item.");
                throw;
            }
        }

        public IEnumerable<Item> SearchItem(string searchItem)
        {
            try
            {
                List<Item> result = new List<Item>();

                foreach (Item item in _items)
                {
                    if (item.Name.ToLower().Contains(searchItem.ToLower()))
                    {
                        result.Add(item);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for items.");
                throw;
            }
        }

        public IEnumerable<Item> PriceFilter(double maxPrice, double minPrice = 0)
        {
            try
            {
                List<Item> result = new List<Item>();

                foreach (Item item in _items)
                {
                    if ((minPrice == 0 && item.Price <= maxPrice) || (maxPrice == 0 && item.Price >= minPrice) || (item.Price >= minPrice && item.Price <= maxPrice))
                    {
                        result.Add(item);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while filtering items by price.");
                throw;
            }
        }
    }
}
