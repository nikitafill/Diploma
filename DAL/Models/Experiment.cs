using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaProject.DAL.Models
{
    public class Experiment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public DateTime ExperimentDate { get; set; } = DateTime.UtcNow;

        // Связь: Один эксперимент → одни параметры
        public ExperimentParameters? Parameters { get; set; }

        // Связь: Один эксперимент → один результат
        public ExperimentResult? Result { get; set; }

        // Связь: Один эксперимент → много ответов студента
        public ICollection<StudentAnswer> Answers { get; set; } = new List<StudentAnswer>();

        // Связь: Один эксперимент → один отчет
        public Report? Report { get; set; }
    }
}
