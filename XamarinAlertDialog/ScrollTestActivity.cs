using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using Android.Views;
using Java.Lang;

namespace XamarinAlertDialog
{
    [Activity(Label = "ScrollTest")]
    public class ScrollTestActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ScrollTest);
        }
    }
}