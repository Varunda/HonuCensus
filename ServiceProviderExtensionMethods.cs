using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honu_census {

    public static class ServiceProviderExtensionMethods {

        public static IServiceCollection AddHonuCensus(this IServiceCollection services, Action<HonuCensus> config) {
            services.AddSingleton<HonuCensus>();

            return services;
        }

    }
}
