namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public interface IProgress : ICompletionState, IResponse {
        object Data { get; set; }
        string DataType { get; }
    }
}