using System;
using System.IO;
using domain.configs;
using domain.enums;
using domain.models;
using domain.repository;
using infrastructure.extensions;
using infrastructure.utils;
using Microsoft.AspNetCore.Mvc;

namespace upload.Controllers
{
    public class ReqModel
    {
        public string Base64 { get; set; }
        public string FileName { get; set; }
        public string Sign { get; set; }
        public string WaterMarks { get; set; }
    }
    [Route("api/[controller]/[action]")]
    public class UploadController : Controller
    {
        [HttpPost]
        public MyResult<UploadModel> Image([FromBody]FileUploadModel model)
        {
            MyResult<UploadModel> result = new MyResult<UploadModel>();
            var ext = Path.GetExtension(model.FileName);
            if (string.IsNullOrEmpty(ext))
            {
                ext = ".png";
            }
            if (string.IsNullOrEmpty(model.Id))
            {
                return result.SetError("Id 不能为空");
            }
            var fileName = $"{model.Type}_{DateTime.Now.ToString("yyyyMMddHHmmssffffff")}_{ext}";
            var virtualPath = PathUtil.Combine(Enum.GetName(typeof(FileType), model.Type), model.Id.ToString(), fileName);
            var reqUrl = PathUtil.CombineWithRoot("api/Upload/Base64File");
            model.Picture = Convert.ToBase64String(ImageHandlerUtil.ShrinkImage(ImageHandlerUtil.Base64ToBytes(model.Picture)));
            string rep = HttpUtil.PostString(reqUrl, new { base64 = model.Picture, fileName = virtualPath, waterMarks = model.WaterMarks }.GetJson(), "application/json");
            var repModel = rep.GetModel<MyResult<UploadModel>>();
            result.Data = repModel.Data;
            return result;
        }
        #region Base64File
        [HttpPost]
        public MyResult<UploadModel> Base64File([FromBody]ReqModel model)
        {
            MyResult<UploadModel> result = new MyResult<UploadModel>();
            if (string.IsNullOrEmpty(model.Base64) || string.IsNullOrEmpty(model.FileName))
            {
                var di = PathUtil.MapPath(Constants.DefaultHeadPicture);
                LogUtil<UploadController>.Info("di" + di);
                FileInfo fileInfo = new FileInfo(di);
                UploadModel model1 = new UploadModel
                {
                    FileName = fileInfo.Name,
                    FullName = fileInfo.FullName,
                    Extension = fileInfo.Extension,
                    Length = fileInfo.Length,
                    VirtualPath = model.FileName ?? Constants.DefaultHeadPicture,
                    FullVirtualPath = PathUtil.CombineWithRoot(model.FileName ?? Constants.DefaultHeadPicture)
                };
                result.Data = model1;
            }
            else
            {
                var filePath = PathUtil.MapPath(model.FileName);
                FileInfo fileInfo = new FileInfo(filePath);
                var bytes = ImageHandlerUtil.Base64ToBytes(model.Base64);
                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                    if (!string.IsNullOrEmpty(model.WaterMarks))
                    {
                        ImageHandlerUtil.WaterMarks(bytes, model.WaterMarks, filePath);
                    }
                    if (!fileInfo.Exists)
                    {
                        using (var fs = fileInfo.Create())
                        {
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Flush(true);
                            fileInfo.Refresh();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.WaterMarks))
                    {
                        ImageHandlerUtil.WaterMarks(bytes, model.WaterMarks, filePath);
                    }
                    if (!fileInfo.Exists)
                    {
                        using (var fs = fileInfo.Create())
                        {
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Flush(true);
                            fileInfo.Refresh();
                        }
                    }
                }
                UploadModel model2 = new UploadModel
                {
                    FileName = fileInfo.Name,
                    FullName = fileInfo.FullName,
                    Extension = fileInfo.Extension,
                    Length = fileInfo.Length,
                    VirtualPath = model.FileName,
                    FullVirtualPath = PathUtil.CombineWithRoot(model.FileName),
                };
                result.Data = model2;
            }
            return result;
        }
        #endregion


    }
}