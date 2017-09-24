using System;

namespace WebscopeTest.Models
{
    public class TaskItem {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EndTime { get; set; }
    }
}
