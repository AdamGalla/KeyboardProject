namespace LoadBalancer;

public record Service
{
    public Guid Id { get; private set; }
    public string Url { get; private set; }

    public Service(string url)
    {
        Id = Guid.NewGuid();
        Url = url;
    }
}
