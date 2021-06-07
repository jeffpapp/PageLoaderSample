using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace PageLoaderSample.Pages
{
    public class Page2Model : PageModel
    {
        private readonly EndpointDataSource endpointDataSource;
        private readonly PageLoader pageLoader;

        public Page2Model(EndpointDataSource endpointDataSource,
            PageLoader pageLoader)
        {
            this.endpointDataSource = endpointDataSource;
            this.pageLoader = pageLoader;
        }

        public async Task OnGet()
        {
            var allEndpoints = endpointDataSource.Endpoints;

            foreach (var ep in allEndpoints)
            {
                if (ep.Metadata.GetMetadata<PageActionDescriptor>() != null)
                {
                    // Normally this method is called with the base endpoint metadata like defaultPageLoader.LoadAsync(page, endpoint.Metadata); in PageLoaderMatcherPolicy.  But since that overload is internal I can't use it here
                    var fullEndpoint = (await pageLoader.LoadAsync(ep.Metadata.GetMetadata<PageActionDescriptor>())).Endpoint;
                }
            }
        }
    }
}