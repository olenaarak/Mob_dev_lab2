using Android.App;
using Android.OS;
using Android.Widget;
using PhonesDB;
using System.Collections.Generic;
using System.Linq;
using Warehouse.DB;

namespace Warehouse
{
    [Activity(Label = "WarehousesActivity")]
    public class WarehousesActivity : Activity
    {
        private WarehousesDBHelper _dBHelper;
        private List<ProductEntity> products;
        private ListView productList;
        private CheckBox filterCheckBoxProducts;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_listview);

            Button backButton = FindViewById<Button>(Resource.Id.backHome);
            backButton.Click += (sender, e) =>
            {
                Finish();
            };

            InitDB();
            InitProductList();
        }

        private void InitDB()
        {
            _dBHelper = new WarehousesDBHelper(this);
            _dBHelper.OnUpgrade(_dBHelper.WritableDatabase, 1, 2);
        }

        private void InitProductList()
        {
            products = _dBHelper.GetAllProducts();
            productList = FindViewById<ListView>(Resource.Id.product_list);

            var customAdapter = new ProductAdapter(this, products);
            productList.Adapter = customAdapter;

            productList.ChoiceMode = ChoiceMode.Multiple;

            filterCheckBoxProducts = FindViewById<CheckBox>(Resource.Id.filterCheckBoxProducts);
            filterCheckBoxProducts.CheckedChange += (sender, e) =>
            {
                ApplyFilter();
            };
        }

        private void ApplyFilter()
        {
            bool showAllProducts = filterCheckBoxProducts.Checked;
            var filteredProducts = showAllProducts ? products.Where(p => p.Quantity < 5).ToList() : products;

            var customAdapter = new ProductAdapter(this, filteredProducts);
            productList.Adapter = customAdapter;
        }
    }
}
