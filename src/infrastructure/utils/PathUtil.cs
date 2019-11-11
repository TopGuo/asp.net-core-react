using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using infrastructure.extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.DotNet.PlatformAbstractions;

namespace infrastructure.utils
{
    public class PathUtil
    {
        private static PathUtil Instance = new PathUtil();

        private static Regex HttpRegex = new Regex("^(http(s)?://([^/]+))");

        private static Regex ColonRegex = new Regex("(?<!:)(//+)");

        private static Regex SlashRegex = new Regex("([\\\\]+)");

        public static string AbsoluteUrl => Instance._AbsoluteUrl;

        public static string WebPhysicalPath => Instance._WebPhysicalPath;

        public static string ContentPhysicalPath => Instance._ContentPhysicalPath;

        public static string WebVirtualPath => Instance._WebVirtualPath;

        /// <summary>
        /// 系统环境配置
        /// </summary>
        public static IHostingEnvironment HostingEnvironment => Instance._HostingEnvironment;

        protected IHostingEnvironment _HostingEnvironment
        {
            get;
        }

        protected string _AbsoluteUrl
        {
            get;
        }

        protected string _WebPhysicalPath
        {
            get;
        }

        protected string _ContentPhysicalPath
        {
            get;
        }

        protected string _WebVirtualPath
        {
            get;
        }

        public static string MapPath(string virtualPath, bool isWebRoot = true)
        {
            return Instance._MapPath(virtualPath, isWebRoot);
        }

        public static string Combine(params string[] path)
        {
            if (path.Length != 0)
            {
                string text = Instance._CombinePath(path);
                Match match = HttpRegex.Match(path[0]);
                if (match.Success)
                {
                    return ColonRegex.Replace(string.Format("{0}/{1}", match.Result("$1"), text), "/");
                }
                return text;
            }
            return Instance._WebVirtualPath;
        }

        public static string CombineWithoutRoot(params string[] path)
        {
            return Instance._CombinePath(path);
        }

        public static string CombineWithRoot(params string[] path)
        {
            if (path.Length != 0)
            {
                string arg = Instance._CombinePath(path);
                Match match = HttpRegex.Match(path[0]);
                if (match.Success)
                {
                    return ColonRegex.Replace(string.Format("{0}/{1}", match.Result("$1"), arg), "/");
                }
                return ColonRegex.Replace($"{Instance._AbsoluteUrl}/{arg}", "/");
            }
            return Instance._AbsoluteUrl;
        }

        protected PathUtil()
        {
            IHostingEnvironment val = ServiceExtension.Get<IHostingEnvironment>();
            _HostingEnvironment = val;
            _WebPhysicalPath = val.WebRootPath;
            _ContentPhysicalPath = val.ContentRootPath;
            if (ServiceExtension.HttpContext == null)
            {
                _WebVirtualPath = "/";
                _AbsoluteUrl = "/";
            }
            else
            {
                HttpRequest val2 = ServiceExtension.HttpContext.Request;
                PathString pathBase = val2.PathBase;
                _WebVirtualPath = pathBase.Value;
                if (string.IsNullOrWhiteSpace(_WebVirtualPath))
                {
                    _WebVirtualPath = "/";
                }
                string scheme = val2.Scheme;
                HostString host = val2.Host;
                string value = host.Value;
                pathBase = val2.PathBase;
                _AbsoluteUrl = $"{scheme}://{value}{pathBase.Value}/";
            }
        }

        protected string _MapPath(string virtualPath, bool isWebRoot = true)
        {
            string text = isWebRoot ? _WebPhysicalPath : _ContentPhysicalPath;
            if (string.IsNullOrWhiteSpace(virtualPath))
            {
                return text;
            }
            var isWin = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (isWin)
            {
                string input = string.Format("{0}\\{1}", text, Regex.Replace(virtualPath.Replace("~", ""), "([/]+)", "\\"));
                return SlashRegex.Replace(input, "\\");
            }
            else
            {
                return $"{text}/{virtualPath}";
            }

        }

        protected string _CombinePath(params string[] path)
        {

            if (path != null && path.Length != 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string input in path)
                {
                    stringBuilder.Append("/");
                    stringBuilder.Append(SlashRegex.Replace(HttpRegex.Replace(input, ""), "/"));
                }
                string text = ColonRegex.Replace(stringBuilder.ToString(), "/");
                if (text.IndexOf("/") != 0 && !HttpRegex.IsMatch(text))
                {
                    text = $"/{text}";
                }
                if (text.IndexOf(_WebVirtualPath) != 0 && !HttpRegex.IsMatch(path[0]))
                {
                    text = $"{_WebVirtualPath}/{text}";
                }
                return text;
            }
            return _WebVirtualPath;
        }
    }
}