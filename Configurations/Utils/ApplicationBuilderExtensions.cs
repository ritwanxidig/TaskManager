using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Configurations.Exceptions;

namespace Task_Manager.Configurations.Utils
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<GlobalExceptionHandler>();

    }
}