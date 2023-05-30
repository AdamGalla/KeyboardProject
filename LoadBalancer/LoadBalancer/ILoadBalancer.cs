using LoadBalancer.Strategies;

namespace LoadBalancer;

public interface ILoadBalancer
{
    public IEnumerable<Service> GetAllServices();
    public Guid AddService(Service service);
    public bool RemoveService(Service service);
    public IStrategy GetActiveStrategy();
    public bool SetActiveStrategy(IStrategy activeStrategy);
    public Service? GetNextService();
}
