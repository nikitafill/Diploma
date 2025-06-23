namespace DiplomaProject.DAL.Models
{
    public class ExperimentGroupQuestion
    {
        public int Id { get; set; }
        public int ExperimentGroupId { get; set; }
        public ExperimentGroup ExperimentGroup { get; set; } = null!;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
