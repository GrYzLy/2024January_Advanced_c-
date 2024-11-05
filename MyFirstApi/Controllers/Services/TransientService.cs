using System.Data;

namespace  MyFirstApi.Services;

public class TransientService : ITransientService

{
    private readonly Guid _serviceId;
    private readonly DateTime _creaatedAt;

    public TransientService()
    {
        _serviceId = Guid.NewGuid();
        _creaatedAt = DateTime.Now;
    }

    public string Name => nameof(TransientService);
    public string SayHello()
    {
        return $"Hello!I am {Name}. My ID is {_serviceId}. I was created at {_creaatedAt:yyyy-MM-dd HH:mm:ss}";
    }
}