using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
public class AnimalFactory
{
    private readonly IServiceProvider _provider;
    private readonly Dictionary<string, Type> _map;

    public AnimalFactory(IServiceProvider provider)
    {
        _provider = provider;

        _map = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                typeof(IAnimal).IsAssignableFrom(t) &&
                t.IsClass &&
                !t.IsAbstract)
            .ToDictionary(t => t.Name, t => t);
    }

    public IAnimal Create(string name)
    {
        if (!_map.TryGetValue(name, out var type))
            throw new ArgumentException($"Animal '{name}' não encontrado.");

        return (IAnimal)_provider.GetRequiredService(type);
    }
}
