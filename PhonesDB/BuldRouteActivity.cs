using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using PhonesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.DB;

namespace Warehouse
{
    [Activity(Label = "BuildRouteActivity")]
    public class BuildRouteActivity : Activity
    {
        private WarehousesDBHelper _dBHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_warehouses);

            Button backButton = FindViewById<Button>(Resource.Id.backHome);
            backButton.Click += (sender, e) =>
            {
                Finish();
            };

            EditText addressInput = FindViewById<EditText>(Resource.Id.addressInput);
            Button buildRouteButton = FindViewById<Button>(Resource.Id.buildRouteButton);
            buildRouteButton.Click += (sender, e) =>
            {
                string enteredAddress = addressInput.Text;
                BuildRouteToCity(enteredAddress);
            };
        }

        private void InitDB()
        {
            _dBHelper = new WarehousesDBHelper(this);
            _dBHelper.OnUpgrade(_dBHelper.WritableDatabase, 1, 2);
        }

        private void BuildRouteToCity(string address)
        {
            var joinedAddress = string.Join('+', address.Split(' '));
            var mapIntent = new Intent(Android.Content.Intent.ActionView, Android.Net.Uri.Parse("google.navigation:q=" + joinedAddress));
            StartActivity(mapIntent);
        }

    }
}