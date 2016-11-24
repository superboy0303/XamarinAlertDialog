using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using Android.Views;
using Java.Lang;

namespace XamarinAlertDialog
{
    [Activity(Label = "ListViewTest")]
    public class ListViewTestActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListViewTest);

            var listView1 = FindViewById<ListView>(Resource.Id.listView1);
            var listViewData = new string[] { "字符1" , "字符2",  "字符3" };
            listView1.Adapter = new ArrayAdapter<string>(BaseContext, Resource.Id, listViewData);
        }
    }
}