using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;

namespace DiplomaProject.DAL.Repositories
{
    public class ExperimentParametersRepository : GenericRepository<ExperimentParameters>, IExperimentParametersRepository
    {
        public ExperimentParametersRepository(ApplicationDbContext context) : base(context) { }

    }
}
