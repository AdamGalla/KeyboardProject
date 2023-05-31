namespace LoadBalancer;

public record Service
{
    public Guid Id { get; private set; }
    public string Url { get; private set; }

    private readonly int _maxTimeCount = 10;
    private readonly Queue<long> _times = new Queue<long>();
    public long AverageResponseTime => _times.Count > 0 ? (long)_times.Average() : 0;

    public Service(string url)
    {
        Id = Guid.NewGuid();
        Url = url;
    }

    public void AddLatestResponseTime(long timeInMs)
    {
        if (_times.Count >= _maxTimeCount)
        {
            _ = _times.Dequeue();
        }
        _times.Enqueue(timeInMs);
    }
}
