using Microsoft.Extensions.Logging;
using RedValley.Extensions;
using RedValley.Properties;

namespace RedValley.Helper;

/// <summary>
/// Helper class for working with exception.
/// </summary>
public static class ExceptionHelper
{
    /// <summary>
    /// Tries to execute the specified <paramref name="action" /> for the specified <paramref name="context"/> and logs any errors using the specified <paramref name="logger" /> if something went wrong.
    /// </summary>
    /// <param name="context">Specifies the context for the current action (e.g. "Loading Data").</param>
    /// <param name="action">The action that should be executed.</param>
    /// <param name="logger">The logger that should be used to log any errors.</param>
    /// <param name="errorHandler">The error handler that should be used to handle errors.</param>
    public static async Task TryAsync(
        string context,
        Func<Task> action,
        ILogger logger,
        Action<Exception>? errorHandler = null
    )
    {
        await TryAsync(action, exception =>
        {
            errorHandler?.Invoke(exception);
            LogException(context, logger, exception);
        });
    }

    /// <summary>
    /// Tries to execute the specified <paramref name="action"/>.
    /// </summary>
    /// <param name="action">The action that should be executed.</param>
    /// <param name="errorHandler">The error handler that should be used to handle errors.</param>
    public static async Task TryAsync(
        Func<Task> action,
        Action<Exception>? errorHandler = null
    )
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            if (errorHandler is not null) errorHandler(ex);
        }
    }

    /// <summary>
    /// Tries to execute the specified <paramref name="action" /> for the specified <paramref name="context"/> and logs any errors using the specified <paramref name="logger" /> if something went wrong.
    /// </summary>
    /// <param name="context">Specifies the context for the current action (e.g. "Loading Data").</param>
    /// <param name="action">The action that should be executed.</param>
    /// <param name="logger">The logger that should be used to log any errors.</param>
    /// <param name="errorHandler">The error handler that should be used to handle errors.</param>
    public static void 
        Try(
        string? context,
        Action action,
        ILogger logger,
        Action<Exception>? errorHandler = null
    )
    {
        Try(action, exception =>
        {
            errorHandler?.Invoke(exception);
            LogException(context, logger, exception);
        });
    }

    /// <summary>
    /// Tries to execute the specified <paramref name="action" /> and logs any errors using the specified <paramref name="logger" /> if something went wrong.
    /// </summary>
    /// <param name="action">The action that should be executed.</param>
    /// <param name="logger">The logger that should be used to log any errors.</param>
    public static void Try(
        Action action,
        ILogger logger
    )
    {
        Try(null, action, logger);
    }

    /// <summary>
    /// Tries to execute the specified <paramref name="action"/>.
    /// </summary>
    /// <param name="action">The action that should be executed.</param>
    /// <param name="errorHandler">The error handler that should be used to handle errors.</param>
    public static void Try(
        Action action,
        Action<Exception>? errorHandler = null
    )
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            if (errorHandler is not null) errorHandler(ex);
        }
    }

    /// <summary>
    /// Tries to execute the specified <paramref name="action"/> and delivers the result
    /// or an <see cref="Exception"/> if something went wrong.
    /// </summary>
    /// <typeparam name="TResult">The type of the result of the specified action.</typeparam>
    /// <param name="action">The action that should be executed.</param>
    /// <param name="errorHandler">The error handler that should be used to handle errors.</param>
    public static TResult? Try<TResult>(
        Func<TResult> action,
        Action<Exception>? errorHandler = null
    ) 
    {
        TResult? result = default;
        Try(() =>
        {
            result = action();
        }, errorHandler);

        return result;
    }

    /// <summary>
    /// Tries to execute the specified <paramref name="action" /> for the specified <paramref name="context"/> and logs any errors using the specified <paramref name="logger" /> if something went wrong.
    /// </summary>
    /// <typeparam name="TResult">The type of the result of the specified action.</typeparam>
    /// <param name="context">Specifies the context for the current action (e.g. "Loading Data").</param>
    /// <param name="action">The action that should be executed.</param>
    /// <param name="logger">The logger that should be used to log any errors.</param>
    /// <param name="errorHandler">The error handler that should be used to handle errors.</param>
    public static TResult? Try<TResult>(
        string? context,
        Func<TResult> action,
        ILogger logger,
        Action<Exception>? errorHandler = null
    )
    {
        TResult? result = default;
        Try(context, () =>
        {
            result = action();
        }, logger, errorHandler);

        return result;
    }

    /// <summary>
    /// Tries to execute the specified <paramref name="action"/> and delivers the result
    /// or an <see cref="Exception"/> if something went wrong.
    /// </summary>
    /// <typeparam name="TResult">The type of the result of the specified action.</typeparam>
    /// <param name="action">The action that should be executed.</param>
    /// <param name="logger">The logger that should be used to log any errors.</param>
    public static TResult? Try<TResult>(
        Func<TResult> action,
        ILogger logger
    )
    {
        return Try<TResult?>(null, action, logger);
    }
    
    private static void LogException(string? context, ILogger logger, Exception exception)
    {
        if (context is null || context.IsEmpty())
        {
            logger.LogError(exception, Resources.ExceptionUnexpectedError.F(exception.Message));
        }
        else
        {
            logger.LogError(exception,Resources.ExceptionContextUnexpectedError.F(context, exception.Message));    
        }
        
        
    }


}