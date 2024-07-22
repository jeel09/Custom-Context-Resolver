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

        public JsonResult SearchResult(string query, List<string> brands, List<string> categories)
        {
            var searchResults = SearchRepository.SearchMethod(query, brands, categories);

            return Json(searchResults, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData()
        {
            var index = ContentSearchManager.GetIndex("sitecore_master_index");
            using (var context = index.CreateSearchContext())
            {
                var brandQuery = context.GetQueryable<SearchModel>()
                    .FacetOn(item => item.Brand)
                    .Take(0);

                var categoryQuery = context.GetQueryable<SearchModel>()
                    .FacetOn(item => item.Category)
                    .Take(0);

                var brandResults = brandQuery.GetResults();
                var categoryResults = categoryQuery.GetResults();

                var combinedFacets = new
                {
                    Brands = brandResults.Facets,
                    Categories = categoryResults.Facets
                };

                return Json(combinedFacets, JsonRequestBehavior.AllowGet);
            }
        }

    }
}