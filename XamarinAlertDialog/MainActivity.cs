using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using Android.Views;
using Java.Lang;

namespace XamarinAlertDialog
{
    [Activity(Label = "AlertDialogTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //这个效果就是登陆或者其他的加载提示框，如果这里用 ProgressDialog.Builder 也是可以，但是要自定义显示信息，包括图片信息等等。
            ProgressDialog p_dialog = new ProgressDialog(this);
            p_dialog.SetMessage("正在加载……");
            p_dialog.Show();

            //等加载完成后再让其消失
            Thread newThread = new Thread(() =>
            {
                Thread.Sleep(2000);  //模拟加载数据，当前进程延迟2秒

                Button btn = FindViewById<Button>(Resource.Id.button1);
                btn.Click += Btn_Click;

                Button btn2 = FindViewById<Button>(Resource.Id.button2);
                btn2.Click += Btn2_Click;

                Button btn3 = FindViewById<Button>(Resource.Id.button3);
                btn3.Click += Btn3_Click;

                Button btn4 = FindViewById<Button>(Resource.Id.button4);
                btn4.Click += Btn4_Click;

                Button btn5 = FindViewById<Button>(Resource.Id.button5);
                btn5.Click += Btn5_Click;

                Button btn6 = FindViewById<Button>(Resource.Id.button6);
                btn6.Click += Btn6_Click;

                // cancel和dismiss方法本质都是一样的，都是从屏幕中删除Dialog,唯一的区别是  
                // 调用cancel方法会回调DialogInterface.OnCancelListener如果注册的话,dismiss方法不会回掉  
                p_dialog.Cancel();
            });
            newThread.Start();
        }

        /// <summary>
        /// 测试ListView相关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn6_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetClass(BaseContext, typeof(ListViewTestActivity));
            StartActivity(intent);
        }

        /// <summary>
        /// 滚动条相关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn5_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetClass(BaseContext, typeof(ScrollTestActivity));
            StartActivity(intent);
        }

        /// <summary>
        /// 列表对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn4_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder ad_build = new AlertDialog.Builder(this);
            string[] array = new string[] { "语文", "数学", "英语" };
            int selectIndex = 0;  //默认是第一项
            ad_build.SetItems(array, new EventHandler<DialogClickEventArgs>((object o2, DialogClickEventArgs e2) =>
            {
                selectIndex = e2.Which; //可捕获用户选中的值
                //MessageBoxShow("你最终选择的是" + array[e2.Which]);
                MessageBoxShowWithPic("你最终选择的是" + array[e2.Which]);
            }));
            ad_build.SetIcon(Android.Resource.Drawable.StatSysWarning);
            ad_build.Show();
        }

        /// <summary>
        /// 多选对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn3_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder ad_build = new AlertDialog.Builder(this);
            string[] array = new string[] { "语文", "数学", "英语" };
            bool[] resultArray = new bool[] { false, false, false };

            ad_build.SetMultiChoiceItems(array, resultArray, new EventHandler<DialogMultiChoiceClickEventArgs>((object o2, DialogMultiChoiceClickEventArgs e2) =>
            {
                resultArray[e2.Which] = e2.IsChecked;
                MessageBoxShow("你选择的是" + array[e2.Which]);
            }));
            ad_build.SetNegativeButton("确定", new System.EventHandler<DialogClickEventArgs>((object o2, DialogClickEventArgs e2) =>
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < resultArray.Length; i++)
                {
                    if (resultArray[i])
                    {
                        sb.Append(array[i]);
                        if (i != resultArray.Length - 1)
                        {
                            sb.Append(",");
                        }
                    }
                }
                MessageBoxShow("你最终选择的有：" + sb.ToString());
            }));
            ad_build.SetPositiveButton("取消", new System.EventHandler<DialogClickEventArgs>((object o3, DialogClickEventArgs e3) =>
            {
                MessageBoxShow("已取消");
            }));
            ad_build.SetIcon(Android.Resource.Drawable.StatSysWarning);
            ad_build.Show();
        }

        /// <summary>
        /// 单选对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn2_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder ad_build = new AlertDialog.Builder(this);
            string[] array = new string[] { "语文", "数学", "英语" };
            int selectIndex = 0;  //默认是第一项

            ad_build.SetSingleChoiceItems(array, 0, new EventHandler<DialogClickEventArgs>((object o2, DialogClickEventArgs e2) =>
            {
                selectIndex = e2.Which; //可捕获用户选中的值
                MessageBoxShow("你选择的是" + array[e2.Which]);
            }));
            ad_build.SetNegativeButton("确定", new System.EventHandler<DialogClickEventArgs>((object o2, DialogClickEventArgs e2) =>
            {
                //直接获取，e2.Which会出现-2，所以利用上面的结果来赋值
                //MessageBoxShow("你选择的是" + array[e2.Which]);  
                MessageBoxShow("你最终选择的是：" + array[selectIndex]);
            }));
            ad_build.SetPositiveButton("取消", new System.EventHandler<DialogClickEventArgs>((object o3, DialogClickEventArgs e3) =>
            {
                MessageBoxShow("已取消");
            }));
            ad_build.SetIcon(Android.Resource.Drawable.StatSysWarning);
            ad_build.Show();
        }


        /// <summary>
        /// 弹出常规对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Click(object sender, System.EventArgs e)
        {
            AlertDialog.Builder ad_build = new AlertDialog.Builder(this)
                             .SetTitle("提示")
                             .SetMessage("你确定要删除吗？");

            //当然你也可以new一个类的对象来处理点击事件，这里我觉得没有必要
            ad_build.SetNegativeButton("确定", new System.EventHandler<Android.Content.DialogClickEventArgs>((object o2, DialogClickEventArgs e2) =>
                {
                    MessageBoxShow("已确定");
                }
            ));
            ad_build.SetPositiveButton("取消", new System.EventHandler<Android.Content.DialogClickEventArgs>((object o3, DialogClickEventArgs e3) =>
                {
                    MessageBoxShow("已取消");
                }
            ));
            ad_build.SetIcon(Android.Resource.Drawable.StatSysWarning);
            ad_build.Show();
        }

        private void MessageBoxShow(string str)
        {
            var item = Toast.MakeText(this, str, ToastLength.Short);

            //设置垂直水平居中
            //item.SetGravity(GravityFlags.CenterHorizontal | GravityFlags.CenterVertical, 0, 0);   //位或运算
            item.SetGravity(GravityFlags.Center, 0, 0);
            item.Show();
        }

        private void MessageBoxShowWithPic(string str)
        {
            var item = Toast.MakeText(this, str, ToastLength.Short);
            item.SetGravity(GravityFlags.Center, 0, 0);

            //创建一个图片视图
            ImageView iv = new ImageView(this);
            iv.SetImageResource(Android.Resource.Drawable.StatSysWarning);   //安卓自带的图片

            //得到Toast布局（强制改变为线型布局）
            LinearLayout toastView = (LinearLayout)item.View;
            //设置内容显示位置
            toastView.SetGravity(GravityFlags.Center);
            //设置布局的方向
            toastView.Orientation = Orientation.Horizontal;

            //给布局添加一个视图，并添加到前面
            toastView.AddView(iv, 0);

            //取得文字，让图片居中文字对齐
            TextView textView = toastView.GetChildAt(1) as TextView;
            textView.Gravity = GravityFlags.CenterVertical;  //文字本身
            LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
            p.Gravity = GravityFlags.Center;   //对外容器也居中
            textView.LayoutParameters = p;

            //显示Toast
            item.Show();
        }
    }
}

