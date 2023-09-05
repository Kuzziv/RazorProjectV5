using RazorProjectV5.Models;

namespace RazorProjectV5.MockData
{
    public class MockItem
    {

        public static List<Item> Items = new List<Item>()
        {
            new Item(1, "Panties", 25),
            new Item(2, "Hoodie", 50),
            new Item(3, "Boots", 35)
        };


        public static List<Item> GetAllItemsModel()
        {
            return Items;
        }



    }
}
