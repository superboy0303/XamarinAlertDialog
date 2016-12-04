using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FatchWebImage
{
    class Program
    {
        private static string _rootSrc = "http://tnfs.tngou.net/image"; // "http://tnfs.tngou.net/img";

        /// <summary>
        /// 抓去网站图片，存入本地照片文件夹
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("正在开始处理照片下载...");
            string[] pathAry = Directory.GetDirectories("Images/");
            foreach (string path in pathAry)
            {
                Console.WriteLine("正在保存图片到文件夹：" + path);
                string directoryName = Path.GetDirectoryName(path);
                //获取相应的id
                string id = directoryName.Split('_')[0];

                //目前暂时一次取30个地址
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                var result = webClient.DownloadString(string.Format("http://www.tngou.net/tnfs/api/list?id={0}&page=1&rows=30", id));
                webClient.Dispose();

                //透过JSON.net 反序列化为User对象
                ImageApiData apidata = JsonConvert.DeserializeObject<ImageApiData>(result);
                List<string> imgLst = new List<string>();
                if (apidata.status)
                {
                    imgLst = apidata.tngou.Select(item => _rootSrc + item.img).ToList();
                }
                if (imgLst.Count != 0)
                {
                    try
                    {
                        //保存图片到指定的文件夹
                        //Parallel.ForEach(imgLst, (filePath =>
                        //string filepath = Server.MapPath("xls/" + filename);
                        //try
                        //{

                        //    string newfilename = "c:\\" + filename;//newfilename为存放本地的文件路径
                        //    DownLoadFile(filepath, newfilename);
                        //}
                        //catch (System.Exception ee)
                        //{
                        //    string dd = ee.ToString();
                        //    Response.Write("文件无法下载");
                        //}

                        //foreach (string filePath in imgLst)
                        Parallel.ForEach(imgLst, (filePath =>
                        {
                            HttpWebRequest request = WebRequest.Create(filePath) as HttpWebRequest;
                            request.Method = "GET";
                            request.Timeout = 30000;
                            request.AllowAutoRedirect = true;
                            request.ContentType = "image/bmp";
                            request.UserAgent = "Mozilla/5.0 (Windows NT 5.2; rv:11.0) Gecko/20100101 Firefox/11.0";
                            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                            string saveFilePath = path +"\\" + Path.GetFileName(filePath);
                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            {
                                StreamReader sr = new StreamReader(response.GetResponseStream());
                                Bitmap sourcebm = new Bitmap(sr.BaseStream);
                                sr.Close();
                                sourcebm.Save(saveFilePath);     //filename 保存地址
                                Console.WriteLine("已保存图片文件："  + saveFilePath);
                            }

                            //WebRequest request = WebRequest.Create(filePath);
                            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            //Stream stream = response.GetResponseStream();
                            //string filename = Path.GetFileName(filePath);
                            //var fs = File.Create(path + "\\" + filename);
                            //int count = 0;
                            //do
                            //{
                            //    var buffer = new byte[4096];
                            //    count = stream.Read(buffer, 0, buffer.Length);
                            //    fs.Write(buffer, 0, count);
                            //} while (count > 0);
                            //Console.WriteLine("已保存图片文件：" + path + "\\" + filename);
                            //stream.Close();
                            //response.Close();
                            //response.Dispose();
                        //}
                        }));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                Console.WriteLine("已处理完文件夹：" + path);
            }
        }
    }

    public class ImageApiData
    {
        public bool status { set; get; }

        public int total { set; get; }

        public List<Tngou> tngou { set; get; }
    }

    public class Tngou
    {
        public string img { get; set; }
        public string title { get; set; }
        public long time { get; set; }
    }

}
