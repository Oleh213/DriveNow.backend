using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Rewrite;

namespace DriveNow.Rules;

public class RulesetResult
{
    private readonly ICollection<RuleResult> _ruleResults = new Collection<RuleResult>();


    public RulesetResult()
    {
        HasPassed = true;
    }


    public IEnumerable<RuleResult> Errors => _ruleResults.Where(r => r.Severity == RuleResultSeverity.Error);


    public bool HasPassed { get; private set; }


    public bool IsAborted { get; private set; }


    public IEnumerable<RuleResult> RuleResults => _ruleResults.AsEnumerable();


    public IEnumerable<RuleResult> Warnings => _ruleResults.Where(r => r.Severity == RuleResultSeverity.Warning);
    
    public void AddResult(RuleResult result)
    {
        _ruleResults.Add(result);
        IsAborted |= result.Abort;
        HasPassed &= result.Severity != RuleResultSeverity.Error;
    }
}