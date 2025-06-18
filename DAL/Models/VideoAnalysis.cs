namespace DiplomaProject.DAL.Models
{
    public class VideoAnalysis
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public ExperimentGroup Group { get; set; }
        public string ExtractedFramePath { get; set; }
        public float PixelsPerCm { get; set; }
        public float CentreX { get; set; }
        public float CentreY { get; set; }

        public List<RingRadius> Radii { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
