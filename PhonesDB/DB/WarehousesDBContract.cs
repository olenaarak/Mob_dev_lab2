using Android.Provider;

namespace Warehouse.DB
{
    public class WarehousesDBContract
    {
        public class Warehouse : IBaseColumns
        {
            public const string TableName = "Warehouse";

            public const string ProductName = "ProductName";

            public const string Price = "Price";

            public const string Quantity = "Quantity";

            public static readonly ProductEntity[] Data = new ProductEntity[]
            {
                new ProductEntity
                {
                    ProductName = "Smartphone",
                    Price = 100.0,
                    Quantity = 8,
                },
                new ProductEntity
                {
                    ProductName = "Laptop",
                    Price = 150.0,
                    Quantity = 3,
                },
                new ProductEntity
                {
                    ProductName = "Refrigerator",
                    Price = 15000.0,
                    Quantity = 2,
                },
                new ProductEntity
                {
                    ProductName = "Headphones", 
                    Price = 200.0,
                    Quantity = 5,
                },
                new ProductEntity
                {
                    ProductName = "Coffee Maker", 
                    Price = 120.0,
                    Quantity = 10,
                },
                // Add more products as needed
            };
        }
    }
}
