using LoadBalancer.Strategies;

namespace LoadBalancer;

public interface ILoadBalancer
{
    public IEnumerable<Service> GetAllServices();
    public Guid AddService(string serviceUrl);
    public bool RemoveService(Guid serviceId);
    public IStrategy? GetActiveStrategy();
    public void SetActiveStrategy(IStrategy activeStrategy);
    public Service? GetNextService();
}
