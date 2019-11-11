using System;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace infrastructure.utils
{
    public class ImageHandlerUtil
    {
        static Regex reg1 = new Regex("%2B", RegexOptions.IgnoreCase);
        static Regex reg2 = new Regex("%2F", RegexOptions.IgnoreCase);
        static Regex reg3 = new Regex("%3D", RegexOptions.IgnoreCase);
        static Regex reg4 = new Regex("(data:([^;]*);base64,)", RegexOptions.IgnoreCase);

        public static string PraseBase64(string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                return string.Empty;
            }
            var newBase64 = reg1.Replace(base64, "+");
            newBase64 = reg2.Replace(newBase64, "/");
            newBase64 = reg3.Replace(newBase64, "=");
            return reg4.Replace(newBase64, "");
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="base64">base64</param>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        /// <returns>相对url fileName</returns>
        public static string SaveBase64Image(string base64, string fileName, string path)
        {
            if (string.IsNullOrWhiteSpace(base64) || string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }
            var di = PathUtil.MapPath(path);
            if (!Directory.Exists(di))
            {
                Directory.CreateDirectory(di);
            }
            var ext = System.IO.Path.GetExtension(fileName);
            var bytes = Convert.FromBase64String(PraseBase64(base64));
            var str = $"{Guid.NewGuid()}{ext}";
            File.WriteAllBytes(System.IO.Path.Combine(di, str), bytes);
            return PathUtil.Combine(path, str);
        }

        /// <summary>
        /// 保存base64 图片
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="fileName">文件名</param>
        /// <param name="physicalPath">物理路径</param>
        /// <param name="virtualRootPath"></param>
        /// <returns>保存的文件名</returns>
        public static string SaveImage(string base64, string fileName, string physicalPath, string virtualRootPath)
        {
            if (string.IsNullOrWhiteSpace(base64) || string.IsNullOrEmpty(fileName))
            {
                throw new NullReferenceException($"null:{nameof(base64)}/{nameof(fileName)}");
            }
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }
            var ext = System.IO.Path.GetExtension(fileName);
            var bytes = Base64ToBytes(base64);//Convert.FromBase64String(PraseBase64(base64));
            var str = $"{Guid.NewGuid()}{ext}";
            File.WriteAllBytes(physicalPath, bytes);
            return PathUtil.Combine(virtualRootPath, str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static byte[] Base64ToBytes(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
            {
                throw new NullReferenceException($"base64 file is null:{nameof(base64)}");
            }
            var bytes = Convert.FromBase64String(PraseBase64(base64));
            return bytes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes">图片byte文件流</param>
        /// <param name="waterMarks"></param>
        /// <param name="filePath">图片保存物理路径</param>
        public static void WaterMarks(byte[] bytes, string waterMarks, string filePath)
        {
            MemoryStream memoryStream = new MemoryStream(bytes);
            using (Image image = Image.FromStream(memoryStream))
            {
                using (Bitmap bitmap = new Bitmap(image.Width, image.Height))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.Clear(Color.Transparent);
                        graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        graphics.DrawString(waterMarks, new Font("AdobeHeitiStd-Regular", 20, FontStyle.Italic | FontStyle.Bold), Brushes.Red, new PointF(image.Width / 2, image.Height - image.Height / 8), stringFormat);
                        bitmap.Save(filePath, ImageFormat.Png);
                    }
                }
            }
        }
        /// <summary>
        /// Shrink Image
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static byte[] ShrinkImage(byte[] bytes, int max = 0)
        {
            if (bytes.Length < 1024 * 100)
            {
                return bytes;
            }
            if (max < 1)
            {
                max = 1024;
            }
            MemoryStream ms = new MemoryStream(bytes);
            int width = max;
            int height = max;
            using (Image image = Image.FromStream(ms))
            {
                if (max == 0)
                {
                    width = image.Height;
                    width = image.Width;
                }
                else
                {
                    //设定宽高比
                    if (image.Width > image.Height)
                    {
                        if (image.Width < max)
                        {
                            width = image.Width;
                        }
                        height = (width * image.Height) / image.Width;
                    }
                    else
                    {
                        if (image.Height < max)
                        {
                            height = image.Height;
                        }
                        width = (height * image.Width) / image.Height;
                    }
                }
                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.Clear(Color.Transparent);

                        graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0,
                            image.Width, image.Height, GraphicsUnit.Pixel);
                        StringFormat strFormat = new StringFormat();
                        strFormat.Alignment = StringAlignment.Center;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            bitmap.Save(stream, ImageFormat.Jpeg);
                            var _newBytes = new byte[stream.Length];
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.Read(_newBytes, 0, _newBytes.Length);
                            return _newBytes;
                        }
                    }
                }
            }
        }
    }
}