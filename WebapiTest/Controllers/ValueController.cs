using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebapiTest.Controllers
{
    public class ValueController : Controller
    {
        // GET: Value
        public JsonResult Index()
        {
            return Json(new List<User>()
            {
                new User(22, "张三", "男"),
                new User(33, "李四", "男"),
                new User(27, "王五", "女"),
                new User(29, "小董", "男")
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 得到文件夹名称
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDirectoryNames()
        {
            string[] ary = Directory.GetDirectories(Server.MapPath("~/Images/"));
            string[] directorynameAry = ary.Select(item => Path.GetFileName(item)).ToArray();
            List<DirectoryNameInfo> infos = new List<DirectoryNameInfo>();
            foreach (string directory in directorynameAry)
            {
                string[] tempAry = directory.Split('_');
                infos.Add(new DirectoryNameInfo() { name = tempAry[1], id = int.Parse(tempAry[0]) });
            }
            infos = infos.OrderByDescending(item => item.id).ToList();
            return Json(infos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDirectoryImages()
        {
            string id = Request.QueryString["id"];
            string[] ary = Directory.GetDirectories(Server.MapPath("~/Images/"), string.Format("{0}_*", id));
            if (ary.Length == 0)
            {
                return Json(new { result = "id有误" }, JsonRequestBehavior.AllowGet);
            }
            string[] files = Directory.GetFiles(ary[0], "*.jpg");
            files = files.Select(item => "Images/" + Path.GetFileName(ary[0]) + "/" + Path.GetFileName(item)).ToArray();
            return Json(files, JsonRequestBehavior.AllowGet);
        }
    }

    public class DirectoryNameInfo
    {
        public string name { set; get; }
        public int id { set; get; }
    }

    public class User
    {
        public User(int age, string name, string sex)
        {
            this.age = age;
            this.name = name;
            this.sex = sex;
        }

        public string name { get; set; }
        public int age { get; set; }
        public string sex { get; set; }
    }

}