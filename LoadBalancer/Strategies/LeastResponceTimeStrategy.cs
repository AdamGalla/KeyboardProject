namespace LoadBalancer.Strategies;

public class LeastResponceTimeStrategy : IStrategy
{
    public Service? GetNextService(IEnumerable<Service> services)
    {
        return services.MinBy(service => service.AverageResponseTime) ?? services.First();
    }
}
