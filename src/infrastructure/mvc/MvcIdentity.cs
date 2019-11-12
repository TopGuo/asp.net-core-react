using System;
using System.Collections.Generic;
using System.Security.Principal;
using infrastructure.utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace infrastructure.mvc
{
    public class MvcIdentity : IIdentity
    {
        public const string AUTHENTICATION_TYPE = ".NetCoreIdentity";
        public static MvcIdentity Instance = new MvcIdentity();
        [JsonIgnore]
        public string AuthenticationType { get; private set; }
        [JsonIgnore]
        public int TimeZoneOffset
        {
            get
            {
                string cookie = CookieUtil.GetCookie(CookieUtil.ClientTimeCookieName, true);
                if (!string.IsNullOrEmpty(cookie))
                {
                    int.TryParse(cookie.Trim(), out int result);
                    return result;
                }
                return 0;
            }
        }
        [JsonProperty]
        public string Id
        {
            get;
            protected set;
        }
        [JsonProperty]
        public string Name
        {
            get;
            protected set;
        }
        [JsonIgnore]
        public bool IsAuthenticated
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    if (!string.IsNullOrWhiteSpace(Id))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
        [JsonProperty]
        public Guid Token
        {
            get;
            protected set;
        }

        [JsonProperty]
        public string UserName
        {
            get;
            protected set;
        }

        [JsonProperty]
        public string Email
        {
            get;
            protected set;
        }

        /// <summary>
        /// employee role id
        /// </summary>
        [JsonProperty]
        public int RoleId
        {
            get;
            protected set;
        }

        [JsonProperty]
        public DateTime? LastLogin
        {
            get;
            protected set;
        }

        public virtual object Data
        {
            get;
            set;
        }
        protected MvcIdentity()
        {
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">can same as UserName</param>
        /// <param name="userName"></param>
        /// <param name="roleId"></param>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <param name="lastLogin"></param>
        public MvcIdentity(string id, string name, string userName, string email = null, int? roleId = null, Guid? token = null, DateTime? lastLogin = null, object data = null)
        {
            Id = id;
            Name = name;
            UserName = (userName ?? name);
            RoleId = (roleId ?? (-100));
            Email = email;
            Token = (token ?? Guid.Empty);
            LastLogin = lastLogin;
            if (data != null)
            {
                Data = data;
            }
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(Id))
            {
                AuthenticationType = AUTHENTICATION_TYPE;
            }
        }
        public virtual T GetData<T>()
        {
            if (Data != null)
            {
                return ((JToken)(Data as JContainer)).ToObject<T>();
            }
            return default(T);
        }

        public virtual List<T> GetDataList<T>()
        {
            if (Data != null)
            {
                return ((JToken)(Data as JContainer)).ToObject<List<T>>();
            }
            return new List<T>();
        }

        public MvcPrincipal GetPrincipal()
        {
            if (IsAuthenticated)
            {
                AuthenticationType = AUTHENTICATION_TYPE;
            }
            return new MvcPrincipal(this);
        }

        public void Login(string scheme, Action<CookieOptions> options = null)
        {
            CookieUtil.AppendCookie(scheme, DataProtectionUtil.Protect(JsonConvert.SerializeObject(this)), true, options);
        }

        public void Logout(string scheme)
        {
            CookieUtil.RemoveCookie(scheme, true, "");
        }
    }
}