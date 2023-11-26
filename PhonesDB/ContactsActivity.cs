using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using PhonesDB;
using System;

namespace Warehouse
{
    [Activity(Label = "ContactsActivity")]
    public class ContactsActivity : Activity
    {
        private CheckBox filterCheckBox;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_contacts);

            filterCheckBox = FindViewById<CheckBox>(Resource.Id.filterCheckBox);
            filterCheckBox.CheckedChange += (sender, e) =>
            {
                GetContacts(); 
            };

            Button backButton = FindViewById<Button>(Resource.Id.backHome);
            backButton.Click += (sender, e) =>
            {
                this.Finish();
            };

            CheckPermission("android.permission.READ_CONTACTS", 100);
            GetContacts();
        }

        private void GetContacts()
        {
            string selection = null;
            string[] selectionArgs = null;

            if (filterCheckBox.Checked)
            {
                selection = ContactsContract.CommonDataKinds.StructuredName.DisplayName + " LIKE ?";
                selectionArgs = new string[] { "%анч%" };
            }

            var cursor = ContentResolver.Query(
                ContactsContract.Data.ContentUri,
                null,
                selection,
                selectionArgs,
                ContactsContract.CommonDataKinds.StructuredName.GivenName + " ASC"
            );
            StartManagingCursor(cursor);

            String[] data = { ContactsContract.CommonDataKinds.StructuredName.GivenName, ContactsContract.CommonDataKinds.Email.Address };
            int[] to = { Resource.Id.contactName, Resource.Id.contactEmail };

            SimpleCursorAdapter adapter = new SimpleCursorAdapter(this, Resource.Layout.contact_item, cursor, data, to, CursorAdapterFlags.None);
            var listView = FindViewById<ListView>(Resource.Id.contacts_list);
            listView.Adapter = adapter;
        }


        public void CheckPermission(String permission, int requestCode)
        {
            if (ContextCompat.CheckSelfPermission(this, permission) == Android.Content.PM.Permission.Denied)
            {
                ActivityCompat.RequestPermissions(this, new String[] { permission }, requestCode);
            }
            else
            {
                Toast.MakeText(this, "Permission already granted", ToastLength.Short).Show();
            }
        }
    }
}
