using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoApp.Util;

namespace TodoApp.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Todo Name")] 
        public string? Name { get; set; }
        [Display(Name = "Todo Description")]
        public string? Description { get; set; }
        [Display(Name = "Status")]
        public Byte Status { get; set; }
        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreateDate { get; set; }
        [Required]
        [Display(Name = "Created Date")]
        public DateTime UpdatedTime { get; set; }
        [Display(Name = "Completed  Date")]
        public DateTime Completedtime { get; set; }
        public Byte ActiveNess { get; set; }
    }
}
