using Android.Content;
using Android.Views;
using Android.Widget;
using PhonesDB;
using System.Collections.Generic;
using Warehouse.DB;

namespace Warehouse
{
    public class ProductAdapter : BaseAdapter<ProductEntity>
    {
        private readonly Context context;
        private readonly List<ProductEntity> products;

        public ProductAdapter(Context context, List<ProductEntity> products)
        {
            this.context = context;
            this.products = products;
        }

        public override int Count => products.Count;

        public override long GetItemId(int position) => position;

        public override ProductEntity this[int position] => products[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.product_list_item, null, false);
            }

            var productNameTextView = view.FindViewById<TextView>(Resource.Id.productNameTextView);
            var priceTextView = view.FindViewById<TextView>(Resource.Id.priceTextView);
            var quantityTextView = view.FindViewById<TextView>(Resource.Id.quantityTextView);


            var product = products[position];
            productNameTextView.Text = product.ProductName;
            priceTextView.Text = $"Price: {product.Price:C}";
            quantityTextView.Text = $"Quantity: {product.Quantity}";

            return view;
        }
    }
}
