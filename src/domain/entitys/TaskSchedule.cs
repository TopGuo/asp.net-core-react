using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class TaskSchedule
    {
        public string Id { get; set; }
        public string JobGroup { get; set; }
        public string JobName { get; set; }
        public string CronExpress { get; set; }
        public DateTime StarRunTime { get; set; }
        public DateTime? EndRunTime { get; set; }
        public DateTime? NextRunTime { get; set; }
        public int RunStatus { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string CreateAuthr { get; set; }
    }
}
