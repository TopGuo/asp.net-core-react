using System;
using System.Linq;

namespace infrastructure.action
{
    /// <summary>
    /// action特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionAttribute : Attribute
    {
        public ActionAttribute(string name, ActionType parent, int order = 0)
        {
            this.Name = name;
            this.Parent = parent;
            this.Order = order;
        }

        public string Name { get; }
        public ActionType Parent { get; }
        public int Order { get; }
        public string Icon { get; set; } = "glyphicon-asterisk";
        public string ParentId
        {
            get
            {
                var p = ParentAtions.ParentAtionsList.SingleOrDefault(t => t.ActionType == Parent);
                if (p == null)
                {
                    return null;
                }
                return p.Id;
            }
        }
        public string ParentName
        {
            get
            {
                var p = ParentAtions.ParentAtionsList.SingleOrDefault(t => t.ActionType == Parent);
                if (p == null)
                {
                    return string.Empty;
                }
                return p.Name;
            }
        }
    }
}