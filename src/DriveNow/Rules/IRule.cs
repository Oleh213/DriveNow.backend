using Microsoft.AspNetCore.Rewrite;

namespace DriveNow.Rules;

    public interface IRule<T>
    {
        Task<RuleResult?> ExecuteAsync(Func<Task<T>> getValue, CancellationToken cancellationToken = default);
    }