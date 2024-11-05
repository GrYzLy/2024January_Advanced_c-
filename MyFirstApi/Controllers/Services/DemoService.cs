using System.Data;

namespace  MyFirstApi.Services;

public class DemoService : IDemoService

{
    private readonly Guid _serviceId;
    private readonly DateTime _creaatedAt;

    public DemoService()
    {
        _serviceId = Guid.NewGuid();
        _creaatedAt = DateTime.Now;
    }
    public string SayHello()
    {
        return $"Hello! My ID is {_serviceId}. I was created at {_creaatedAt:yyyy-MM-dd HH:mm:ss}";
    }
}