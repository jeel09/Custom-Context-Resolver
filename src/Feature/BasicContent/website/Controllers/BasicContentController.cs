using Sv103.Feature.BasicContent.Repository;
using System.Diagnostics;
using System.Web.Mvc;

namespace Sv103.Feature.BasicContent.Controllers
{
    public class BasicContentController : Controller
    {
        protected readonly IBasicContentRepository BasicContentRepository;
        public BasicContentController(IBasicContentRepository basicContentRepository)
        {
            Debug.Assert(basicContentRepository != null);
            BasicContentRepository = basicContentRepository;
        }
    }
}