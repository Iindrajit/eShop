using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.Configure<ApiBehaviorOptions>(option => {
                option.InvalidModelStateResponseFactory = actionContext => 
                {
                    var validationErrors = actionContext.ModelState
                                            .Where(e => e.Value.Errors.Count > 0)
                                            .SelectMany(e => e.Value.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse
                    {
                        Errors = validationErrors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            
            return services;
        }
    }
}