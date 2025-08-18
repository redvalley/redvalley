using Microsoft.Extensions.DependencyInjection;

namespace RedValley;

/// <summary>
/// Use this class to directly resolve specific registered types using the current <see cref="IServiceProvider"/> instance.
/// </summary>
public static class Resolver
{
    private static IServiceProvider? _serviceProvider;

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/> instance for getting access to registered services.
    /// </summary>
    /// <value>The service provider.</value>
    public static IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException($"The current {nameof(IServiceProvider)} is not accessable as it was not already registered");

    /// <summary>
    /// Register the specified <paramref name="serviceProvider"/>
    /// </summary>
    public static void RegisterServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    /// <summary>
    /// Resolves the service specified by the <typeparamref name="TService"/>
    /// </summary>
    public static TService Resolve<TService>() where TService : notnull => ServiceProvider.GetRequiredService<TService>();
}