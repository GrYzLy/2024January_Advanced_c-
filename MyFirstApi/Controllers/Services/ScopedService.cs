using System.Data;
using Microsoft.AspNetCore.Authentication;

namespace  MyFirstApi.Services;

public class ScopedService : IScopedService

{
    private readonly Guid _serviceId;
    private readonly DateTime _creaatedAt;

    private readonly ITransientService _transientService;

    private readonly ISingletonService _singletonService;

    public ScopedService(ITransientService transientService, ISingletonService singletonService)
    {
        _serviceId = Guid.NewGuid();
        _creaatedAt = DateTime.Now;

        _transientService = transientService;
        _singletonService = singletonService;

    }

    public string Name => nameof(ScopedService);
    public string SayHello()
    {
        var scopedServiceMessage =  $"Hello!I am {Name}. My ID is {_serviceId}. I was created at {_creaatedAt:yyyy-MM-dd HH:mm:ss}";

        var transientServiceMessage = $"{_transientService.SayHello()} I am from {Name}";

        var singletonServiceMessage = $"{_singletonService.SayHello()} I am from {Name}";

        return $"{scopedServiceMessage}{Environment.NewLine}{transientServiceMessage}{Environment.NewLine}{singletonServiceMessage}";
    }
}