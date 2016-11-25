using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using Android.Views;
using Java.Lang;
using static Android.Views.View;
using static Android.App.LauncherActivity;

namespace XamarinAlertDialog
{
    [Activity(Label = "ListViewSimpleTest")]
    public class ListViewSimpleTestActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListViewSimpleTest);

            var listView1 = FindViewById<ListView>(Resource.Id.listView1);
            var listViewData = new string[] { "小明", "小张", "小王" };
            listView1.Adapter = new ArrayAdapter<string>(BaseContext, Android.Resource.Layout.SimpleListItem1, listViewData);
            
            //点击事件
            listView1.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs e)
            {
                Toast.MakeText(BaseContext, listViewData[e.Position], ToastLength.Short).Show();
            };
        }
    }
}