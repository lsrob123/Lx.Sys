namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface ICompletionState
    {
        decimal ProgressTotal { get; set; }
        decimal ProgressCompleted { get; set; }
        string ProgressMessage { get; set; }
    }
}