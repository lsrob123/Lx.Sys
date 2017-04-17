using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.DTOs
{
    public abstract class Progress : CompletionState, IProgress
    {
        public string DataType => Data?.GetType().Name;
        public object Data { get; set; }
    }
}