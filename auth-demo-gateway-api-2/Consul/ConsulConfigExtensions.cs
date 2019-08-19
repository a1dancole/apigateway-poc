﻿using System;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace auth_demo_gateway_api_2.Consul
{
    public static class ConsulConfigExtensions
    {
        public static auth_demo_gateway_api_2.Consul.ConsulConfig GetConsulConfig(this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var serviceConfig = new auth_demo_gateway_api_2.Consul.ConsulConfig
            {
                ServiceDiscoveryAddress = configuration.GetValue<Uri>("ServiceConfig:serviceDiscoveryAddress"),
                ServiceAddress = configuration.GetValue<Uri>("ServiceConfig:serviceAddress"),
                ServiceName = configuration.GetValue<string>("ServiceConfig:serviceName"),
                ServiceId = configuration.GetValue<string>("ServiceConfig:serviceId")
            };

            return serviceConfig;
        }

        public static void RegisterConsulServices(this IServiceCollection services, auth_demo_gateway_api_2.Consul.ConsulConfig serviceConfig)
        {
            if (serviceConfig == null)
            {
                throw new ArgumentNullException(nameof(serviceConfig));
            }

            var consulClient = CreateConsulClient(serviceConfig);

            services.AddSingleton(serviceConfig);
            services.AddSingleton<IHostedService, ServiceDiscoveryImpl>();
            services.AddSingleton<IConsulClient, ConsulClient>(provider => consulClient);
        }

        private static ConsulClient CreateConsulClient(auth_demo_gateway_api_2.Consul.ConsulConfig serviceConfig)
        {
            return new ConsulClient(config =>
            {
                config.Address = serviceConfig.ServiceDiscoveryAddress;
            });
        }
    }
}
