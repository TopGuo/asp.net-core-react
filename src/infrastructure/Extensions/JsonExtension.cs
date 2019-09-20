using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace infrastructure.extensions
{
    public static class JsonExtension
    {
        /// <summary>
        /// set default config
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerSettings DefaultJsonSettings()
        {
            try
            {
                IOptions<MvcJsonOptions> val = ServiceExtension.Get<IOptions<MvcJsonOptions>>();
                if (val != null)
                {
                    JsonSerializerSettings val2 = val.Value.SerializerSettings;
                    return val2;
                }
                throw new KeyNotFoundException("MvcJsonOptions.SerializerSettings");
            }
            catch
            {
                JsonSerializerSettings val3 = new JsonSerializerSettings();
                val3.NullValueHandling = NullValueHandling.Ignore;
                val3.DateFormatString = "yyyy/MM/dd HH:mm";
                // val3.ContractResolver= ;
                val3.Formatting = Newtonsoft.Json.Formatting.None;
                val3.MissingMemberHandling = MissingMemberHandling.Ignore;
                val3.MaxDepth = 32;
                val3.TypeNameHandling = TypeNameHandling.None;
                val3.Culture = new CultureInfo("en-us");
                val3.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return val3;
            }
        }
        public static string GetJson(this object obj)
        {
            return obj.GetJson((jsonSetting) => { });
        }
        public static string GetJson(this object obj, Action<JsonSerializerSettings> next)
        {
            if (obj == null) return "{}";
            JsonSerializerSettings val = DefaultJsonSettings();
            next(val);
            return JsonConvert.SerializeObject(obj, val);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="jpath">select json from json</param>
        /// <returns></returns>
        public static object GetModel(this string input, string jpath = "")
        {
            if (string.IsNullOrEmpty(input)) return null;
            input = input.FormatJson();
            JsonSerializerSettings val = DefaultJsonSettings();
            object obj = JsonConvert.DeserializeObject(input, val);
            if (!string.IsNullOrEmpty(jpath))
            {
                JArray jVal;
                if ((jVal = (obj as JArray)) != null)
                {
                    return jVal.First.SelectToken(jpath);
                }
                JObject jObj;
                if ((jObj = (obj as JObject)) != null)
                {
                    return jObj.SelectToken(jpath);
                }
                throw new Exception($"model is {obj.GetType().Name} jpath error");
            }
            return obj;
        }
        public static T GetModel<T>(this string input, string jpath = "")
        {
            if (string.IsNullOrWhiteSpace(input)) return default(T);
            input = input.FormatJson();
            JsonSerializerSettings jsonSerializerSettings = DefaultJsonSettings();
            if (string.IsNullOrEmpty(jpath))
            {
                if (!input.StartsWith("["))
                {
                    return JsonConvert.DeserializeObject<T>(input, jsonSerializerSettings);
                }
                return JsonConvert.DeserializeObject<List<T>>(input, jsonSerializerSettings).FirstOrDefault();
            }
            var obj = JsonConvert.DeserializeObject(input);
            JArray jArray;
            if ((jArray = (obj as JArray)) != null)
            {
                return jArray.First.SelectToken(jpath).ToObject<T>();
            }
            JObject jObject;
            if ((jObject = (obj as JObject)) != null)
            {
                return jObject.SelectToken(jpath).ToObject<T>();
            }
            throw new Exception($"jtoken is {obj.GetType().Name} error");
        }
        public static List<T> GetModelList<T>(this string input, string jpath)
        {
            if (string.IsNullOrWhiteSpace(input)) return new List<T>();
            input = input.FormatJson();
            JsonSerializerSettings jsonSerializerSettings = DefaultJsonSettings();
            if (string.IsNullOrEmpty(jpath))
            {
                if (!input.StartsWith("["))
                {
                    return new List<T> { JsonConvert.DeserializeObject<T>(input, jsonSerializerSettings) };
                }
                return JsonConvert.DeserializeObject<List<T>>(input, jsonSerializerSettings);
            }
            object obj = JsonConvert.DeserializeObject(input);
            JArray jArray;
            List<T> list = new List<T>();
            if ((jArray = (obj as JArray)) != null)
            {
                jArray.ToList().ForEach((t) =>
                {
                    list.Add(t.SelectToken(jpath).ToObject<T>());
                });
                return list;
            }
            JObject jObject;
            if ((jObject = (obj as JObject)) != null)
            {
                list.Add(jObject.SelectToken(jpath).ToObject<T>());
                return list;
            }
            throw new Exception($"jtoken is {obj.GetType().Name}");
        }
        public static string GetEnumToString(this Enum _enum)
        {
            if (_enum == null)
            {
                return string.Empty;
            }
            Type type = _enum.GetType();
            string name = Enum.GetName(type, _enum);
            if (!string.IsNullOrEmpty(name))
            {
                DescriptionAttribute customAttribute = (type.GetField(name)).GetCustomAttribute<DescriptionAttribute>();
                if (customAttribute != null)
                {
                    return customAttribute.Description;
                }
                return name;
            }
            List<FieldInfo> list = (type.GetFields()).Where(t => t.FieldType == type).ToList();
            List<string> description = new List<string>();
            list.ForEach((FieldInfo t) =>
            {
                if (_enum.HasFlag((Enum)t.GetValue(_enum)))
                {
                    DescriptionAttribute customAttribute2 = t.GetCustomAttribute<DescriptionAttribute>();
                    if (customAttribute2 != null && !string.IsNullOrEmpty(customAttribute2.Description))
                    {
                        description.Add(customAttribute2.Description);
                    }
                    else
                    {
                        description.Add(t.Name);
                    }
                }
            });
            return string.Join(",", description);
        }
        public static string FormatJson(this string json)
        {
            if (string.IsNullOrEmpty(json)) return json;
            json = json.Trim('\n', '\r', '\t', ' ');
            if (!json.StartsWith("<html", StringComparison.OrdinalIgnoreCase) && !json.StartsWith("<!DOCTYPE", StringComparison.Ordinal))
            {
                if (json.StartsWith("<"))
                {
                    return json.FormatXmlToJson();
                }
                if (!json.StartsWith("[") && !json.StartsWith("{"))
                {
                    return JsonConvert.SerializeObject(new Dictionary<string, string>
                    {
                        {
                            "json",
                            json
                        }
                    });
                }
                return json;
            }
            return JsonConvert.SerializeObject(new Dictionary<string, string>{
                {
                    "html",json
                }
            });
        }
        public static string FormatXmlToJson(this string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            string text = JsonConvert.SerializeXmlNode(xmlDocument, 0, true);
            return text;
        }
    }
}