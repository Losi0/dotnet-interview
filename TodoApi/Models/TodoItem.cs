using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        public bool IsComplete { get; set; } = false;
        
        public long ListId { get; set; }
        
        [ForeignKey("ListId")]
        public TodoList? TodoList { get; set; }
    }
}
