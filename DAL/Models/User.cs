using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaProject.DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Студент";
        [Required]
        public string GroupName { get; set; } = string.Empty;
        public List<ExperimentGroup> ExperimentGroups { get; set; } = new ();
    }
}
