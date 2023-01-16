using Elsa.EntityFrameworkCore.Common;
using Elsa.Labels.Entities;
using Elsa.Labels.Services;

namespace Elsa.EntityFrameworkCore.Modules.Labels;

public class EFCoreWorkflowDefinitionLabelStore : IWorkflowDefinitionLabelStore
{
    private readonly Store<LabelsElsaDbContext, WorkflowDefinitionLabel> _store;
    public EFCoreWorkflowDefinitionLabelStore(Store<LabelsElsaDbContext, WorkflowDefinitionLabel> store) => _store = store;

    public async Task SaveAsync(WorkflowDefinitionLabel record, CancellationToken cancellationToken = default) => await _store.SaveAsync(record, cancellationToken);
    public async Task SaveManyAsync(IEnumerable<WorkflowDefinitionLabel> records, CancellationToken cancellationToken = default) => await _store.SaveManyAsync(records, cancellationToken);
    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default) => await _store.DeleteWhereAsync(x => x.Id == id, cancellationToken) > 0;

    public async Task<IEnumerable<WorkflowDefinitionLabel>> FindByWorkflowDefinitionVersionIdAsync(string workflowDefinitionVersionId, CancellationToken cancellationToken = default) =>
        await _store.FindManyAsync(x => x.WorkflowDefinitionVersionId == workflowDefinitionVersionId, cancellationToken);

    public async Task ReplaceAsync(IEnumerable<WorkflowDefinitionLabel> removed, IEnumerable<WorkflowDefinitionLabel> added, CancellationToken cancellationToken = default)
    {
        var idList = removed.Select(r => r.Id);
        await _store.DeleteWhereAsync(w => idList.Contains(w.Id), cancellationToken);
        await _store.SaveManyAsync(added, cancellationToken);
    }

    public async Task<int> DeleteByWorkflowDefinitionIdAsync(string workflowDefinitionId, CancellationToken cancellationToken = default) =>
        await _store.DeleteWhereAsync(x => x.WorkflowDefinitionId == workflowDefinitionId, cancellationToken);

    public async Task<int> DeleteByWorkflowDefinitionVersionIdAsync(string workflowDefinitionVersionId, CancellationToken cancellationToken = default) =>
        await _store.DeleteWhereAsync(x => x.WorkflowDefinitionVersionId == workflowDefinitionVersionId, cancellationToken);

    public async Task<int> DeleteByWorkflowDefinitionIdsAsync(IEnumerable<string> workflowDefinitionIds, CancellationToken cancellationToken = default)
    {
        var ids = workflowDefinitionIds.ToList();
        return await _store.DeleteWhereAsync(x => ids.Contains(x.WorkflowDefinitionId), cancellationToken);
    }

    public async Task<int> DeleteByWorkflowDefinitionVersionIdsAsync(IEnumerable<string> workflowDefinitionVersionIds, CancellationToken cancellationToken = default)
    {
        var ids = workflowDefinitionVersionIds.ToList();
        return await _store.DeleteWhereAsync(x => ids.Contains(x.WorkflowDefinitionVersionId), cancellationToken);
    }
}