using Android.App;
using Android.OS;
using AndroidX.AppCompat.Widget;
using Android.Widget;
using static Android.Content.Res.Resources;
using System.Reflection.Emit;
using AndroidX.AppCompat.App;
using PhonesDB;
using Warehouse.DB;
using System;
using System.Linq;
using Android.Content;

namespace Warehouse
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private WarehousesDBHelper _dBHelper;
        private bool _filterApplied = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            InitDB();
            DisplayLowQuantityProducts();

            var contactsButton = FindViewById<Button>(Resource.Id.contactsButton);
            contactsButton.Click += ContactsButton_Click;

            var filterProductsButton = FindViewById<Button>(Resource.Id.filterProducts);
            filterProductsButton.Click += FilterProductsButton_Click;

            var warehousesButton = FindViewById<Button>(Resource.Id.warehousesButton);
            warehousesButton.Click += WarehousesButton_Click;

            Button priceButton = FindViewById<Button>(Resource.Id.priceButton);
            priceButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(PriceActivity));
                StartActivity(intent);
            };
            Button authorButton = FindViewById<Button>(Resource.Id.authorButton);
            authorButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(AuthorActivity));
                StartActivity(intent);
            };

        }

        private void InitDB()
        {
            _dBHelper = new WarehousesDBHelper(this);
        }

        private void DisplayLowQuantityProducts()
        {
            
        }

        private void ContactsButton_Click(object sender, EventArgs e)
        {
           
            StartActivity(typeof(ContactsActivity));
        }

        private void FilterProductsButton_Click(object sender, EventArgs e)
        {
           
            StartActivity(typeof(WarehousesActivity));
        }

        private void WarehousesButton_Click(object sender, EventArgs e)
        {
            
            StartActivity(typeof(BuildRouteActivity));
        }
    }
}