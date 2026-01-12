using Microsoft.Extensions.DependencyInjection;
using Scrutor;

var services = new ServiceCollection();

services.Scan(scan => scan
    .FromAssemblyOf<IAnimal>()
    .AddClasses(classes => classes.AssignableTo<IAnimal>())
    .AsSelf()
    .WithTransientLifetime());

services.AddSingleton<AnimalFactory>();

var provider = services.BuildServiceProvider();

var factory = provider.GetRequiredService<AnimalFactory>();

Console.WriteLine(factory.Create("Cachorro").Falar());

Console.WriteLine(factory.Create("Gato").Falar());