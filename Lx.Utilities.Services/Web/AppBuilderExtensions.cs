using System.Web.Http;
using Autofac;
using Lx.Utilities.Services.Authentication;
using Lx.Utilities.Services.OWIN;
using Lx.Utilities.Services.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace Lx.Utilities.Services.Web
{
    public static class AppBuilderExtensions
    {
        public static TAppBuilder UseEverything<TAppBuilder>(this TAppBuilder app, IContainer container,
            params string[] staticFileRootFolders)
            where TAppBuilder : IAppBuilder
        {
            app.UseAutofacMiddleware(container);
            app.UseCors(CorsOptions.AllowAll);
            foreach (var staticFileRootFolder in staticFileRootFolders)
                app.UseFileServer(new FileServerOptions().WithFolderAndDefaultFile(staticFileRootFolder));

            app.MapSignalR(container.Resolve<ISignalRConfig>().VirtualFolder,
                new HubConfiguration().WithAutofac(container));

            app.UseWebApi(new HttpConfiguration()
                .WithAutofac(container)
                .WithDefaultSettings(app, container)
            );

            return app;
        }

        public static TAppBuilder WithIdentityServer<TAppBuilder>(this TAppBuilder app, IContainer container)
            where TAppBuilder : IAppBuilder
        {
            app.UseAutofacMiddleware(container);
            app.UseCors(CorsOptions.AllowAll);

            app.UseIdentityServer(IdentityServerConfigurator.IdentityServerOptions(container));
            return app;
        }
    }
}