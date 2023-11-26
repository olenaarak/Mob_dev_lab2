using Android.App;
using Android.OS;
using Android.Widget;
using PhonesDB;

namespace Warehouse
{
    [Activity(Label = "AuthorActivity")]
    public class AuthorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_author);

            var authorNameTextView = FindViewById<TextView>(Resource.Id.authorName);
            authorNameTextView.Text = "Автор: Рак Олена";

            var authorImageView = FindViewById<ImageView>(Resource.Id.authorImage);
            authorImageView.SetImageResource(Resource.Drawable.author_image);
        }
    }
}
