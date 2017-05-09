namespace Lx.Utilities.Contract.Web {
    public interface ITypedModelHtmlGenerationService {
        string GetHtml<TModel>(TModel model);
    }
}