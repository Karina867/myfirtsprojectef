using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace mywebapp.Models;

public class Category
{
  // [Key]
  public Guid CategoryId { get; set; }

  // [Required]
  // [MaxLength(150)]
  public string Name { get; set; }
  public string Description { get; set; }
  public int Pound { get; set; }
  
  [JsonIgnore]
  public virtual ICollection<Tasks> Tasks { get; set; }
}