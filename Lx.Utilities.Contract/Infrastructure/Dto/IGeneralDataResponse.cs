namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public interface IGeneralDataResponse : IResponse {
        string DataType { get; }
        object Data { get; set; }
    }
}