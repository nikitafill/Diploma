using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaProject.DAL.Models
{
    public class ExperimentGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? SourceName { get; set; }
        public string? FramesDirectory { get; set; }
        public List<VideoAnalysis>? Experiments { get; set; } = new();
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public List<ExperimentGroupQuestion> ExperimentGroupQuestions { get; set; } = new();
    }
}
