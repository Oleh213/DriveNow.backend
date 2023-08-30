namespace DriveNow.Rules;

public static class RulesetResultExtensions
{


    /// <summary>
    /// Check the status of a ruleset result for failures and throws a RulesetException if there are any.
    /// </summary>
    /// <param name="result">The ruleset result that is checked.</param>
    public static void ThrowIfFailed(this RulesetResult result)
    {
        if (result != null && result.Errors.Any())
        {
            throw new RulesetException(result.Errors);
        }
    }
}