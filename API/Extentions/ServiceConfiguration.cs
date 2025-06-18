using DiplomaProject.DAL.Repositories.Interfaces;
using DiplomaProject.DAL.Repositories;
using DiplomaProject.BLL.Services;
using DiplomaProject.BLL.Services.Interfaces;

namespace DiplomaProject.API.Extentions
{
    public static class ServiceConfiguration
    {
        public static void RegisterBLLDependencies(this IServiceCollection services)
        {
            services
                .AddScoped<IExperimentService, ExperimentService>()
                .AddScoped<IQuestionService, QuestionService>()
                .AddScoped<IExperimentGroupService, ExperimentGroupService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IStudentAnswerService, StudentAnswerService>();
        }
        public static void RegisterDALDependencies(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IStudentAnswerRepository, StudentAnswerRepository>()
                .AddScoped<IQuestionRepository, QuestionRepository>()
                .AddScoped<IExperimentRepository, ExperimentRepository>()
                .AddScoped<IExperimentGroupRepository, ExperimentGroupRepository>();
        }
    }
}
