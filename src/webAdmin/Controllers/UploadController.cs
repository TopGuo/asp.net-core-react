using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using domain.configs;
using infrastructure.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webAdmin.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UploadController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public object UEditor()
        {
            object returnValue = null;
            string path = this.Params("path");
            if (string.IsNullOrEmpty(path))
            {
                path = Constants.UPLOAD_TEMP_PATH;
            }
            switch (Request.Query["action"])
            {
                case "Local":
                    returnValue = uploadLocal(path);
                    break;
                case "Remote":
                    returnValue = new { list = uploadRemote(path) };
                    break;
                case "config":
                    returnValue = new { url = "url", title = "title", original = "original", state = "SUCCESS" };
                    break;
            }
            return returnValue;
        }
        public object UEditor([FromQuery]string action)
        {
            object returnValue = null;
            string path = this.Params("path");
            if (string.IsNullOrEmpty(path))
            {
                path = Constants.UPLOAD_TEMP_PATH;
            }
            switch (Request.Query["action"])
            {
                case "Local":
                    returnValue = uploadLocal(path);
                    break;
                case "Remote":
                    returnValue = new { list = uploadRemote(path) };
                    break;
                case "config":
                    returnValue = new { url = "url", title = "title", original = "original", state = "SUCCESS" };
                    break;
            }
            return returnValue;
        }
        //文件允许格式
        private readonly string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };
        //文件大小限制，单位KB
        private const int size = 1024 * 2;

        object uploadLocal(string path)
        {

            //文件上传状态,初始默认成功，可选参数{"SUCCESS","ERROR","SIZE","TYPE"}
            string state = "SUCCESS";

            string title = string.Empty;
            string oriName = string.Empty;
            string filename = string.Empty;
            string url = string.Empty;
            string currentType = string.Empty;
            //保存路径
            string uploadpath = PathUtil.MapPath(path);
            try
            {
                IFormFile uploadFile = Request.Form.Files[0];
                title = uploadFile.FileName.ToLower();
                //目录验证
                if (!Directory.Exists(uploadpath))
                {
                    Directory.CreateDirectory(uploadpath);
                }
                //格式验证
                string[] temp = uploadFile.FileName.Split('.');
                currentType = "." + temp[temp.Length - 1].ToLower();
                if (Array.IndexOf(filetype, currentType) == -1)
                {
                    state = "文件类型不正确";
                }
                else               //大小验证
                if (uploadFile.Length / 1024 > size)
                {
                    state = $"文件不能超过{size / 1024}M";
                }
                //获取图片描述
                if (Request.Query["pictitle"].Count > 0)
                {
                    if (!String.IsNullOrEmpty(Request.Query["pictitle"]))
                    {
                        title = Request.Query["pictitle"];
                    }
                }
                //获取原始文件名
                if (Request.Query["fileName"].Count > 0)
                {
                    if (!String.IsNullOrEmpty(Request.Query["fileName"]))
                    {
                        oriName = Request.Query["fileName"].ToString().Split(',')[1];
                    }
                }
                //保存图片
                if (state == "SUCCESS")
                {
                    if (System.IO.File.Exists(Path.Combine(uploadpath, title)))
                    {
                        state = "ERROR";
                    }
                    else
                    {
                        filename = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + currentType;
                        FileInfo fi = new FileInfo(Path.Combine(uploadpath, filename));
                        using (var fs = fi.OpenWrite())
                        {
                            uploadFile.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    url = PathUtil.CombineWithRoot(path, filename);
                }
            }
            catch (Exception)
            {
                state = "ERROR";
            }
            return new { url, title, original = oriName, state };
        }
        class upload
        {
            public string source { get; set; }
            public string state { get; set; }
            public string url { get; set; }
        }
        List<upload> uploadRemote(string path)
        {
            List<upload> list = new List<upload>();

            string savePath = PathUtil.MapPath(path);//保存文件地址
            //目录验证
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            string uri = System.Net.WebUtility.UrlDecode(this.Params("upfile"));
            string[] imgUrls = null;
            if (string.IsNullOrEmpty(uri))
            {
                uri = System.Net.WebUtility.UrlDecode(this.Params("upfile[]"));
                imgUrls = Regex.Split(uri, ",", RegexOptions.IgnoreCase);
            }
            else
            {
                uri = uri.Replace("&amp;", "&");
                imgUrls = Regex.Split(uri, "ue_separate_ue", RegexOptions.IgnoreCase);
            }
            for (int i = 0, len = imgUrls.Length; i < len; i++)
            {
                try
                {
                    var imgUrl = imgUrls[i];
                    var upload = new upload { source = imgUrl, state = "SUCCESS" };
                    if (!imgUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                        && !imgUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    //格式验证
                    int temp = imgUrl.LastIndexOf('.');
                    var ext = imgUrl.Substring(temp).ToLower();
                    if (Array.IndexOf(filetype, ext) == -1)
                    {
                        upload.state = "ERROR";
                        continue;
                    }
                    var stream = HttpUtil.DownBytes(imgUrl);
                    if (stream == null || stream.Length == 0)
                    {
                        continue;
                    }
                    var tmpName = $"{DateTime.Now.ToString("yyyyMMddHHmmssffffff")}_{i}{ext}";
                    //写入文件
                    using (FileStream fs = new FileStream(Path.Combine(savePath, tmpName), FileMode.CreateNew))
                    {
                        fs.Write(stream, 0, stream.Length);
                        fs.Flush();
                    }
                    upload.url = PathUtil.CombineWithRoot(path, tmpName);
                    list.Add(upload);
                }
                catch
                {

                }
            }
            return list;
        }
    }
}