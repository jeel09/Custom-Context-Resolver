using Sitecore.ContentSearch.Linq.Nodes;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Sv103.Feature.BasicContent.Models;
using System.Web.Http.Cors;
using Sv103.Feature.BasicContent.Repository;

namespace Sv103.Feature.BasicContent.Controllers
{
    public class SearchController : Controller
    {
        protected readonly ISearchRepository SearchRepository;
        public SearchController(ISearchRepository searchRepository)
        {
            SearchRepository = searchRepository;
        }

        public JsonResult SearchResult(string query)
        {
            var searchResults = SearchRepository.SearchMethod(query);

            return Json(searchResults, JsonRequestBehavior.AllowGet);
        }
    }
}