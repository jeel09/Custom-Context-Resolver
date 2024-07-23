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
using Sitecore.ContentSearch.Linq;

namespace Sv103.Feature.BasicContent.Controllers
{
    public class SearchController : Controller
    {
        protected readonly ISearchRepository SearchRepository;
        public SearchController(ISearchRepository searchRepository)
        {
            SearchRepository = searchRepository;
        }

        public JsonResult GetDataAndSearchResult(string query, List<string> brands, List<string> categories, int minPrice = 0, int maxPrice = 1000)
        {
            var searchResults = SearchRepository.SearchMethod(query, brands, categories, minPrice, maxPrice);

            var combinedFacets = SearchRepository.GetFacets();

            var combinedResults = new
            {
                SearchResults = searchResults,
                Facets = combinedFacets
            };

            return Json(combinedResults, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPriceFacets()
        {
            var priceRange = SearchRepository.GetPriceFacets();

            return Json(priceRange, JsonRequestBehavior.AllowGet);
        }
    }
}