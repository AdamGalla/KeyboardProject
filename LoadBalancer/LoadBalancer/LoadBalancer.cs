using LoadBalancer.Strategies;

namespace LoadBalancer;

public class LoadBalancer : ILoadBalancer
{
    private IStrategy? _strategy;
    private readonly Dictionary<Guid, Service> _services = new();

    public Guid AddService(Service service)
    {
        throw new NotImplementedException();
    }

    public IStrategy? GetActiveStrategy()
    {
        return _strategy;
    }

    public IEnumerable<Service> GetAllServices()
    {
        return _services.Values;
    }

    public Service? GetNextService()
    {
        if(_strategy is null)
        {
            return null;
        }
        return _strategy.GetNextService(GetAllServices());
    }

    public bool RemoveService(Guid serviceId)
    {
        return _services.Remove(serviceId);
    }

    public void SetActiveStrategy(IStrategy activeStrategy)
    {
        _strategy = activeStrategy;
    }

    public Guid AddService(string serviceUrl)
    {
        var service = new Service(serviceUrl);
        _services.Add(service.Id, service);
        return service.Id;
    }
}
