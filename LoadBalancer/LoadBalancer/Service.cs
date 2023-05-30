namespace LoadBalancer;

public record Service
{
    public Guid Id { get; set; }
    public string Url { get; set; }

    public Service(string url)
    {
        Id = new Guid();
        Url = url;
    }
}
