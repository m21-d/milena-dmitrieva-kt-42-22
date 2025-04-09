using MilenaDmitrievaKt_42_22.Interfaces;
using MilenaDmitrievaKt_42_22.Interfaces.TeachersInterfaces;

namespace MilenaDmitrievaKt_42_22.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ICafedraService, CafedraService>();
            return services;
        }
    }
}
