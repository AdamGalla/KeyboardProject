namespace LoadBalancer.Strategies;

public class RoundRobinStrategy : IStrategy
{
    private int _index = 0;

    public Service? GetNextService(IEnumerable<Service> services)
    {
        lock(this)
        {
            if(!services.Any())
            {
                return null;
            }
            if(_index >= services.Count())
            {
                _index = 0;
            }

            var service = services.ElementAtOrDefault(_index);
            _index++;
            return service;
        }
    }
}
