namespace DriveNow.Rules;

public class RuleResult
{
    /// <remarks>
    /// The default value for the <see cref="RuleResult.Abort"/> flag is false.
    /// </remarks>
    public RuleResult(RuleResultSeverity severity, Exception exception)
        : this(severity, false, exception)
    {
    }

    public RuleResult(RuleResultSeverity severity, bool abort, Exception exception)
    {
        Severity = severity;
        Abort = abort;
        Exception = exception!;
        InnerRuleResults = Enumerable.Empty<RuleResult>();
    }

    public RuleResult(RuleResultSeverity severity, bool abort, Exception exception, Guid id)
        : this(severity, abort, exception, id.ToString())
    {
    }

    public RuleResult(RuleResultSeverity severity, bool abort, Exception exception, string identifier)
    {
        Abort = abort;
        Exception = exception!;
        Identifier = identifier;
        Severity = severity;
        InnerRuleResults = Enumerable.Empty<RuleResult>();
    }

    public RuleResult(IEnumerable<RuleResult> ruleResults)
    {
        Abort = ruleResults.Any(r => r.Abort);
        Exception = new AggregateException(ruleResults.Select(r => r.Exception));
        Identifier = default;
        Severity = ruleResults.Any(r => r.Severity == RuleResultSeverity.Error)
            ? RuleResultSeverity.Error
            : RuleResultSeverity.Warning;
        InnerRuleResults = ruleResults;
    }

    public RuleResult(params RuleResult[] ruleResults)
        : this(ruleResults.AsEnumerable())
    {
    }

    /// <summary>
    /// True if the ruleset execution should be aborted or false otherwise.
    /// </summary>
    public bool Abort { get; private set; }

    /// <summary>
    /// The exception that occured during the execution of the rule.
    /// </summary>
    public Exception Exception { get; private set; } = default!;

    /// <summary>
    /// An optional identifier for the associated item.
    /// </summary>
    public string? Identifier { get; private set; }

    /// <summary>
    /// Gets the inner <see cref="RuleResult"/>s if any.
    /// </summary>
    public IEnumerable<RuleResult> InnerRuleResults { get; private set; }

    /// <summary>
    /// The <see cref="RuleResult.Exception"/>'s message.
    /// </summary>
    public string? Message => Exception?.Message;

    /// <summary>
    /// The severity of the result.
    /// </summary>
    public RuleResultSeverity Severity { get; private set; }
}