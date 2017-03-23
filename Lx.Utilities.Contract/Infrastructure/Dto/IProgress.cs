namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public interface IProgress : ICompletionState, IResponse {
        object Data { get; set; }
        string DataType { get; }
    }
}