namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public abstract class Progress : CompletionState, IProgress {
        public string DataType => Data?.GetType().Name;
        public object Data { get; set; }
    }
}