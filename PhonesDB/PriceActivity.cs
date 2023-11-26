using Android.App;
using Android.OS;
using Android.Widget;
using PhonesDB;
using System.Linq;
using Warehouse.DB;

namespace Warehouse
{
    [Activity(Label = "PriceActivity")]
    public class PriceActivity : Activity
    {
        private WarehousesDBHelper _dBHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_price);

            _dBHelper = new WarehousesDBHelper(this);
            _dBHelper.OnUpgrade(_dBHelper.WritableDatabase, 1, 2);

            var products = _dBHelper.GetAllProducts();
            var minPrice = products.Min(p => p.Price);
            var maxPrice = products.Max(p => p.Price);

            var minPriceTextView = FindViewById<TextView>(Resource.Id.minPrice);
            minPriceTextView.Text = $"Мінімальна ціна: {minPrice}";

            var maxPriceTextView = FindViewById<TextView>(Resource.Id.maxPrice);
            maxPriceTextView.Text = $"Максимальна ціна: {maxPrice}";
        }
    }
}
