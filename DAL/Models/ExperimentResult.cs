using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProject.DAL.Models
{
    public class ExperimentResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExperimentId { get; set; }

        [ForeignKey("ExperimentId")]
        public Experiment Experiment { get; set; } = null!;

        public string? RingPatternImage { get; set; } // Путь к изображению

        public int MaxRingCount { get; set; } // Количество колец

        public string? CalculationData { get; set; } // JSON с расчетами
    }
}
