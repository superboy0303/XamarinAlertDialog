using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using Android.Views;
using Java.Lang;
using static Android.Views.View;

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
            User[] users = new User[] {
                new User(22, "张三", "男"),
                new User(33, "李四", "男"),
                new User(27, "王五", "女"),
                new User(29, "小董", "男")
            };
            UserAdapter userAdapter = new UserAdapter(this, users);
            listView1.Adapter = userAdapter;
        }
    }

    public class UserAdapter : BaseAdapter<User>
    {
        private User[] items;
        private Activity activity;

        public UserAdapter(Activity context, User[] values): base()
        {
            activity = context;
            items = values;
        }
        
        public override User this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = convertView;
            if (v == null)
            {
                v = activity.LayoutInflater.Inflate(Resource.Layout.PartListViewTest,null);
            }
            v.FindViewById<TextView>(Resource.Id.name).Text = items[position].Name();
            v.FindViewById<TextView>(Resource.Id.age).Text = items[position].Age();
            v.FindViewById<TextView>(Resource.Id.sex).Text = items[position].Sex();
            return v;
        }
    }

    public class User : Java.Lang.Object
    {
        private int mAge;
        private string mName;
        private string mSex;

        public User(int age, string name, string sex)
        {
            this.mAge = age;
            this.mName = name;
            this.mSex = sex;
        }

        public string Name()
        {
            return this.mName;
        }

        public string Age()
        {
            return this.mAge + "";
        }

        public string Sex()
        {
            return this.mSex;
        }
    }
}