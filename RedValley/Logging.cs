using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RedValley;

/// <summary>
/// Core Logging class to get access to the current <see cref="ILoggerFactory" /> and configures the logging.
/// </summary>
public static class Logging
{
    /// <summary>
    /// The name of the logging category for 'bootstrapping'
    /// </summary>
    public const string CategoryBootstrapping = "Bootstrapping";

    /// <summary>
    /// The name of the logging category for 'UI'
    /// </summary>
    public const string CategoryGame = "Game";

    /// <summary>
    /// The name of the logging category for core components
    /// </summary>
    public const string CategoryCore = "Core";

    /// <summary>
    /// Gets the current logger factory instance.
    /// </summary>
    public static ILoggerFactory LoggerFactory { get; private set; } = CreateDebugLoggerFactory();

    public static LoggerFactory CreateDebugLoggerFactory() => new(new[] {
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
    });
    
    /// <summary>
    /// Registers the current <see cref="ILoggerFactory"/>.
    /// </summary>
    /// <param name="services">The <see cref="ILoggerFactory"/> that should be used for retrieving the current <see cref="IServiceCollection"/>.</param>
    public static void RegisterFactory(this IServiceCollection services)
    {
        var loggingServiceProvider = services.BuildServiceProvider();
        LoggerFactory = loggingServiceProvider.GetRequiredService<ILoggerFactory>();

    }

    /// <summary>
    /// Adds Debug logging for debug mode and console logging for release mode. Additionally registers the current <see cref="ILoggerFactory"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> that should be used for retrieving the current <see cref="ILoggerFactory"/>.</param>
    public static void AddMinimalLogging(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
#if DEBUG
                    loggingBuilder.AddDebug();
#endif
            loggingBuilder.AddConsole();
#if DEBUG
                    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
#else
            loggingBuilder.SetMinimumLevel(LogLevel.Information);
#endif
        });

        services.RegisterFactory();
    }

    /// <summary>
    /// Creates a new logger for the specified <typeparamref name="TLoggerContext" />.
    /// </summary>
    /// <typeparam name="TLoggerContext">The type for which a logger should be created.</typeparam>
    /// <returns>ILogger.</returns>
    public static ILogger CreateLogger<TLoggerContext>() => LoggerFactory.CreateLogger<TLoggerContext>();

    /// <summary>
    /// Creates a new logger for the specified <paramref name="loggingCategory"/>.
    /// </summary>
    /// <param name="loggingCategory">The logging category for which a logger should be created.</param>
    public static ILogger CreateLogger(string loggingCategory) => LoggerFactory.CreateLogger(loggingCategory);


    /// <summary>
    /// Creates a new logger for the category 'Game' />.
    /// </summary>
    public static ILogger CreateGameLogger() => CreateLogger(Logging.CategoryGame);

    /// <summary>
    /// Creates a new logger for the category 'Bootstrapping' />.
    /// </summary>
    public static ILogger CreateBootstrappingLogger() => CreateLogger(Logging.CategoryBootstrapping);

    /// <summary>
    /// Creates a new logger for the category 'Core' />.
    /// </summary>
    public static ILogger CreateCoreLogger() => CreateLogger(Logging.CategoryCore);
}
