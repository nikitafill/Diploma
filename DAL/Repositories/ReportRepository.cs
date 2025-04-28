using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.DAL.Repositories
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext context) : base(context) { }
    }

}
