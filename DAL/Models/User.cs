using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaProject.DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Student"; // Student, Teacher, Admin

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Связь: Один пользователь → много экспериментов
        public ICollection<Experiment> Experiments { get; set; } = new List<Experiment>();
    }
}
