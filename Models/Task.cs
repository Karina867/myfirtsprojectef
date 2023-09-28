using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mywebapp.Models;
public class Task
{

    // [Key]
    public Guid TaskId { get; set; }

    [ForeignKey("CategoryId")]
    public Guid CategoryId { get; set; }
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority PriorityTask { get; set; }
    public DateTime InsertDate { get; set; }
    public Category Category { get; set; }

    [NotMapped]
    public string Resume { get; set; }
}

public enum Priority
{
    Low,
    Mid,
    Hight
}