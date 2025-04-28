using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProject.DAL.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExperimentId { get; set; }

        [ForeignKey("ExperimentId")]
        public Experiment Experiment { get; set; } = null!;

        [Required]
        public string ReportText { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
