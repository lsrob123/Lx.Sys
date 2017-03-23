namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public interface IGeneralDataResponse : IResponse {
        string DataType { get; }
        object Data { get; set; }
    }
}