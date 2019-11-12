using System;

namespace infrastructure.action
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class MenuActionAttribute : Attribute
    {
        public MenuActionAttribute(string parentName)
        {
            ParentName = parentName;
        }
        public string ParentName { get; }
    }
}