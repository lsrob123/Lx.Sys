namespace Lx.Utilities.Contracts.Web
{
    public interface ITypedModelHtmlGenerationService
    {
        string GetHtml<TModel>(TModel model);
    }
}