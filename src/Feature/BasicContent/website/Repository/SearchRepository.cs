using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Utilities;
using Sv103.Feature.BasicContent.Models;
using SVCommerce.Feature.BasicContent;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Sv103.Feature.BasicContent.Repository
{
    public class SearchRepository : ISearchRepository
    {
        public SearchRepository()
        {
        }

        public object SearchMethod(string searchTerm)
        {
            if(searchTerm == null)
            {
                return null;
            }

            var index = ContentSearchManager.GetIndex("sitecore_master_index");

            using (var context = index.CreateSearchContext())
            {
                var searchTerms = searchTerm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var predicate = BuildSearchPredicate(searchTerms);

                var results = context.GetQueryable<SearchModel>()
                    .Where(predicate)
                    .Where(item => item.ParentID == "e3f962c1a34d47f283ae1fd2bc247cad")
                    .Select(item => new
                    {
                        Title = item.Title,
                        Description = item.Description,
                    })
                    .ToList();

                return results;
            }
        }

        public Expression<Func<SearchModel, bool>> BuildSearchPredicate(string[] searchTerms)
        {
            var predicate = PredicateBuilder.True<SearchModel>();
            foreach (var term in searchTerms)
            {
                var lowerTerm = term.ToLowerInvariant();
                predicate = predicate.And(item => item.Title.Contains(lowerTerm) || item.Description.Contains(lowerTerm));
            }

            return predicate;
        }
    }
}