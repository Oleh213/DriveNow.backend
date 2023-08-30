namespace DriveNow.Rules;

public sealed class RulesetException : Exception
{
    public RulesetException(IEnumerable<RuleResult> errors)
        : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }
    public IEnumerable<RuleResult> Errors { get; }


    private static string BuildErrorMessage(IEnumerable<RuleResult> errors)
    {
        var messageParts = new List<string>();
        var errorMessages = errors.GetErrors();

        foreach (var item in errorMessages)
        {
            var itemKey = $" -- {item.Key}: ";
            var itemKeyPadding = itemKey.Length;
            var paddingString = string.Join(string.Empty, Enumerable.Repeat(" ", itemKeyPadding));
            messageParts.Add($"{Environment.NewLine}{itemKey}{item.Value.First()}");
            messageParts.AddRange(item.Value.Skip(1).Select(e => $"{Environment.NewLine}{paddingString}{e}"));
        }

        return "Ruleset failed: " + string.Join(string.Empty, messageParts);
    }
}