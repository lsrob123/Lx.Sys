using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Infrastructure.DTOs
{
    public abstract class Progress : CompletionState, IProgress
    {
        public string DataType => Data?.GetType().Name;
        public object Data { get; set; }
    }
}