using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProject.DAL.Models
{
    public class ExperimentParameters
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExperimentId { get; set; }

        [ForeignKey("ExperimentId")]
        public Experiment Experiment { get; set; } = null!;

        [Required]
        public double Wavelength { get; set; } // Длина волны

        [Required]
        public double RefractiveIndex { get; set; } // Показатель преломления

        [Required]
        public double Radius { get; set; } // Радиус линзы

        [Required]
        public double Pressure { get; set; } // Давление (если моделируется)
    }
}
