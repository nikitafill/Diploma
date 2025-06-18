namespace DiplomaProject.DAL.Models
{
    public class RingRadius
    {
        public int Id { get; set; }
        public int VideoAnalysisId { get; set; }
        public float RadiusCm { get; set; }
        public VideoAnalysis VideoAnalysis { get; set; }
    }
}
