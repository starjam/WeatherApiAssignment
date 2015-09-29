using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Net;

namespace weatherApi
{
    // BaseAdapter<Entry> gets data from CitylistActivity.cs 
    //public class DataAdapter : BaseAdapter<Entry>
    //{

    //    List<Entry> items;

    //    Activity context;
    //    public DataAdapter(Activity context, List<Entry> items)
    //        : base()
    //    {
    //        this.context = context;
    //        this.items = items;
    //    }
    //    public override long GetItemId(int position)
    //    {
    //        return position;
    //    }
    //    public override Entry this[int position]
    //    {
    //        get { return items[position]; }
    //    }
    //    public override int Count
    //    {
    //        get { return items.Count; }
    //    }
    //    public override View GetView(int position, View convertView, ViewGroup parent)
    //    {
    //        var item = items[position];
    //        View view = convertView;
    //        if (view == null) // no view to re-use, create new
    //            view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);
    //        view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Name;
    //        view.FindViewById<TextView>(Resource.Id.Text2).Text = item.Address;
    //        //if (item.Tags != null) // 
    //        //{
    //        //    var imageBitmap = GetImageBitmapFromUrl(item.Tags.ToString());
    //        //    view.FindViewById<ImageView>(Resource.Id.Image).SetImageBitmap(imageBitmap);
    //        //}
    //        return view;
    //    }
    //    private Bitmap GetImageBitmapFromUrl(string url)
    //    {
    //        Bitmap imageBitmap = null;
    //        if (!(url == "null"))
    //            using (var webClient = new WebClient())
    //            {
    //                var imageBytes = webClient.DownloadData(url);
    //                if (imageBytes != null && imageBytes.Length > 0)
    //                {
    //                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
    //                }
    //            }

    //        return imageBitmap;
    //    }

    //}
}