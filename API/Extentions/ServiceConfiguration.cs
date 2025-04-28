using DiplomaProject.DAL.Repositories.Interfaces;
using DiplomaProject.DAL.Repositories;
using AutoMapper;

namespace DiplomaProject.API.Extentions
{
    public static class ServiceConfiguration
    {
        /*public static void AddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }*/
        public static void RegisterDALDependencies(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IExperimentRepository, ExperimentRepository>()
                .AddScoped<IExperimentResultRepository, ExperimentResultRepository>()
                .AddScoped<IStudentAnswerRepository, StudentAnswerRepository>()
                .AddScoped<IReportRepository, ReportRepository>()
                .AddScoped<IQuestionRepository, QuestionRepository>()
                .AddScoped<IExperimentParametersRepository, ExperimentParametersRepository>();
        }
    }
}
