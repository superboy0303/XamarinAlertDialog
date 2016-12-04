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

        private string rootSrc = "http://tnfs.tngou.net/img/";

        
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