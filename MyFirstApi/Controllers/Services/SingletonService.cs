using System.Data;

namespace  MyFirstApi.Services;

public class SingletonService : ISingletonService

{
    private readonly Guid _serviceId;
    private readonly DateTime _creaatedAt;

    public SingletonService()
    {
        _serviceId = Guid.NewGuid();
        _creaatedAt = DateTime.Now;
    }

    public string Name => nameof(SingletonService);
    public string SayHello()
    {
        return $"Hello!I am {Name}. My ID is {_serviceId}. I was created at {_creaatedAt:yyyy-MM-dd HH:mm:ss}";
    }
}