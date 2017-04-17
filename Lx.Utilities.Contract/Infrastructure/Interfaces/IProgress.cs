namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface IProgress : ICompletionState, IResponse
    {
        object Data { get; set; }
        string DataType { get; }
    }
}