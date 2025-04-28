using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProject.DAL.Models
{
    public class StudentAnswer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExperimentId { get; set; }

        [ForeignKey("ExperimentId")]
        public Experiment Experiment { get; set; } = null!;

        [Required]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; } = null!;

        [Required]
        public string Answer { get; set; } = string.Empty;
    }
}
