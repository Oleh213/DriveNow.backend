namespace DriveNow.Rules;

public static class RulesetExceptionExtensions
{
    /// <summary>
    /// Gets all errors of a given RulesetException as key-values-pairs.
    /// </summary>
    /// <param name="exception">The RulesetException.</param>
    /// <returns>A key-values-pairs dictionary. Errors that are not bound to an identifier are collected under the '<global>' key.</returns>
    public static IDictionary<string, string[]> GetErrors(this RulesetException exception)
    {
        return GetErrors(exception.Errors);
    }

    /// <summary>
    /// Gets all errors of a given RulesetException as key-values-pairs.
    /// </summary>
    /// <param name="errors">A list of RuleFailures.</param>
    /// <returns>A key-values-pairs dictionary. Errors that are not bound to an identifier are collected under the '<global>' key.</returns>
    public static IDictionary<string, string[]> GetErrors(this IEnumerable<RuleResult> errors)
    {
        var result = new Dictionary<string, string[]>();
        if (!errors.Any())
        {
            return result;
        }

        var unboundErrors = errors.Where(e => string.IsNullOrEmpty(e.Identifier));
        if (unboundErrors.Any())
        {
            result.Add("<global>", unboundErrors.Select(e => e.Exception.Message).ToArray());
        }

        var boundErrors = errors.Where(e => !string.IsNullOrEmpty(e.Identifier)).GroupBy(e => e.Identifier!);
        foreach (var group in boundErrors)
        {
            result.Add($"'{group.Key}'", group.Select(e => e.Exception.Message).ToArray());
        }

        return result;
    }
}