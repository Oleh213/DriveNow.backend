namespace DriveNow.Rules;

public abstract class AbstractRule<T> : IRule<T>
{
    /// <summary>
    /// Executes the rule.
    /// </summary>
    /// <param name="getValue">A function that returns the rule argument.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task object representing the ongoing operation.</returns>
    public abstract Task<RuleResult?> ExecuteAsync(Func<Task<T>> getValue,
        CancellationToken cancellationToken);
}