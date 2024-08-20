using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.models {
    public class Task {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}