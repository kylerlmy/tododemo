using System;

namespace AspNetCore.ToDo.Models
{
    public class TodoItem
    {
        public string OwnerId { get; set; }
        public Guid Id { get; set; }
        public bool IsDone { get; set; }
        public string Title { get; set; }
        public DateTimeOffset? DueAt { get; set; }
    }
}