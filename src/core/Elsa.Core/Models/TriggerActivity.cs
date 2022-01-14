using Elsa.Contracts;

namespace Elsa.Models;

public class TriggerActivity : Activity, ITrigger
{
    protected TriggerActivity()
    {
    }

    protected TriggerActivity(string triggerType) : base(triggerType)
    {
    }

    public string TriggerType
    {
        get => NodeType;
        set => NodeType = value;
    }

    public virtual ValueTask<IEnumerable<object>> GetPayloadsAsync(TriggerIndexingContext context, CancellationToken cancellationToken = default)
    {
        var hashes = GetHashInputs(context);
        return ValueTask.FromResult(hashes);
    }

    protected virtual IEnumerable<object> GetHashInputs(TriggerIndexingContext context) => Enumerable.Empty<object>();
}