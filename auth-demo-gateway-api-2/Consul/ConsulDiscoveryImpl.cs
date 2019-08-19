using System.Threading;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Hosting;

namespace auth_demo_gateway_api_2.Consul
{
    public class ServiceDiscoveryImpl : IHostedService
    {
        private readonly IConsulClient _client;
        private readonly auth_demo_gateway_api_2.Consul.ConsulConfig _config;
        private string _registrationId;

        public ServiceDiscoveryImpl(IConsulClient client, auth_demo_gateway_api_2.Consul.ConsulConfig config)
        {
            _client = client;
            _config = config;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _registrationId = $"{_config.ServiceName}";
            var registration = new AgentServiceRegistration
            {
                ID = _registrationId,
                Name = _config.ServiceName,
                Address = _config.ServiceAddress.Host,
                Port = _config.ServiceAddress.Port
            };
            await _client.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await _client.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}
