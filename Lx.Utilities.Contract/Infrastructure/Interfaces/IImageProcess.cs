namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface IImageProcess
    {
        string Process(string sourceImageFile, bool deleteSourceImageFile = true);
    }
}