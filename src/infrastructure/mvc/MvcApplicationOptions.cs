using System;
using System.Collections.Generic;
using System.IO;

namespace infrastructure.mvc
{
    public class MvcApplicationOptions
    {
        public bool GenerateKey
        {
            get;
            set;
        } = false;


        public DirectoryInfo KeyDirectory
        {
            get;
            set;
        }

        public TimeSpan KeyExpires
        {
            get;
            set;
        } = new TimeSpan(10, 0, 0, 0);


        public List<MvcAuthorizeOptions> AuthorizationSchemes
        {
            get;
            set;
        } = new List<MvcAuthorizeOptions>();
    }
}