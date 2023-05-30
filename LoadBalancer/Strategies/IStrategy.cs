namespace LoadBalancer.Strategies;

public interface IStrategy
{
    public Service? GetNextService(IEnumerable<Service> services);
}
