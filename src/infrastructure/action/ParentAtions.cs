using System;
using System.Collections.Generic;
using infrastructure.extensions;

namespace infrastructure.action
{
    public class ParentAtions
    {
        readonly string guid = Guid.Empty.ToString();
        public ParentAtions(ActionType actionType)
        {
            ActionType = actionType;
        }
        public ParentAtions(ActionType actionType, string url)
        {
            ActionType = actionType;
            Url = url;
        }
        /// <summary>
        /// 编号-重组一个和菜单相关的guid
        /// </summary>
        public string Id
        {
            get
            {
                var id = ((int)ActionType).ToString();
                return Guid.Parse($"{guid.Substring(0, guid.Length - id.Length)}{id}").ToString();
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get { return ActionType.GetEnumToString(); } }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; } = "";
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get { return (int)ActionType; } }

        static List<ParentAtions> parentActions = new List<ParentAtions>();
        internal ActionType ActionType { get; }

        /// <summary>
        /// 获取或初始化父菜单
        /// </summary>
        public static List<ParentAtions> ParentAtionsList
        {
            get
            {
                if (parentActions.Count == 0)
                {
                    foreach (var type in JsonExtension.GetEnumToString<ActionType>())
                    {
                        parentActions.Add(new ParentAtions((ActionType)type.Item1));
                    }
                }
                return parentActions;
            }
        }
    }
}