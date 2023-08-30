namespace DriveNow.Rules;

public interface IRuleset<in T>
{
    Task<RulesetResult> ExecuteAsync(T arg, CancellationToken cancellationToken = default);
}