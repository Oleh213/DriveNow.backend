namespace DriveNow.Rules;

public abstract class AbstractRuleset<T> : IRuleset<T>
{
    /// <summary>
    /// Executes a ruleset and throws an exception when one or more failures occurred.
    /// </summary>
    /// <param name="arg">The argument.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task object representing the ongoing operation.</returns>
    public async Task ExecuteAndThrowAsync(T arg, CancellationToken cancellationToken = default)
    {
        var result = await ExecuteAsync(arg, cancellationToken);
        result.ThrowIfFailed();
    }

    /// <summary>
    /// Executes a ruleset.
    /// </summary>
    /// <param name="arg">The argument.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task object representing the ongoing operation.</returns>
    public async Task<RulesetResult> ExecuteAsync(T arg, CancellationToken cancellationToken = default)
    {
        var result = new RulesetResult();
        await OnExecuteAsync(result, arg, cancellationToken);
        return result;
    }

    protected abstract Task OnExecuteAsync(RulesetResult result, T command, CancellationToken cancellationToken);
}