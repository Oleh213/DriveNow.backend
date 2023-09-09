using DriveNow.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Rules;

public class UserMustExist : AbstractRule<UserMustExist.RuleArguments>
{
    public override async Task<RuleResult?> ExecuteAsync(Func<Task<RuleArguments>> getValue, CancellationToken cancellationToken)
    {
        var value = await getValue();
        
        var user =  value.Context.users.SingleOrDefaultAsync(user => user.UserId == value.UserId);

        if (user != null)
        {
            return null;
        }
        return new RuleResult(RuleResultSeverity.Error, true, new Exception($"User is null"));
    }

    public class RuleArguments
    {
        public ShopContext Context { get; set; }

        public RuleArguments(Guid userId, ShopContext context)
        {
            UserId = userId;
            Context = context;
        }
        public Guid UserId { get; set; }
    }
}