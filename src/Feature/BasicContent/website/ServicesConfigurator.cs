using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sv103.Feature.BasicContent.Controllers;
using Sv103.Feature.BasicContent.Repository;

namespace Sv103.Feature.BasicContent
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<BasicContentController>();
            serviceCollection.AddTransient<IBasicContentRepository, BasicContentRepository>();
            serviceCollection.AddTransient<SearchController>();
            serviceCollection.AddTransient<ISearchRepository, SearchRepository>();
        }
    }
}