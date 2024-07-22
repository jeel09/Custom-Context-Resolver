using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sv103.Feature.BasicContent.Models;
using SVCommerce.Feature.BasicContent;
using System;
using System.Collections.Generic;
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

        public object SearchMethod(string searchTerm, List<string> brands, List<string> categories)
        {
            if(searchTerm == null && (brands == null || !brands.Any()) && (categories == null || !categories.Any()))
            {
                return null;
            }

            var index = ContentSearchManager.GetIndex("sitecore_master_index");

            using (var context = index.CreateSearchContext())
            {
                var searchTerms = searchTerm?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];

                var predicate = BuildSearchPredicate(searchTerms, brands, categories);

                var results = context.GetQueryable<SearchModel>()
                    .Where(predicate)
                    .Where(item => item.ParentID == "e3f962c1a34d47f283ae1fd2bc247cad")
                    .OrderBy(item => item.Title)
                    .Select(item => new
                    {
                        Title = item.Title,
                        Description = item.Description,
                        Brand = item.Brand,
                        ProductCategory = item.Category,
                        Price = item.Price
                    })
                    .ToList();

                return results;
            }
        }

        public Expression<Func<SearchModel, bool>> BuildSearchPredicate(string[] searchTerms, List<string> brands, List<string> categories)
        {
            var predicate = PredicateBuilder.True<SearchModel>();

            // Adding search terms to the predicate
            foreach (var term in searchTerms)
            {
                var lowerTerm = term.ToLowerInvariant();
                predicate = predicate.And(item => item.Title.Contains(lowerTerm) || item.Description.Contains(lowerTerm));
            }

            // Adding brands to the predicate
            if (brands != null && brands.Any())
            {
                var brandPredicate = PredicateBuilder.False<SearchModel>();
                foreach (var brand in brands)
                {
                    var lowerBrand = brand.ToLowerInvariant();
                    brandPredicate = brandPredicate.Or(item => item.Brand.Contains(lowerBrand));
                }
                predicate = predicate.And(brandPredicate);
            }

            // Adding categories to the predicate
            if (categories != null && categories.Any())
            {
                var categoryPredicate = PredicateBuilder.False<SearchModel>();
                foreach (var category in categories)
                {
                    var lowerCategory = category.ToLowerInvariant();
                    categoryPredicate = categoryPredicate.Or(item => item.Category.Contains(lowerCategory));
                }
                predicate = predicate.And(categoryPredicate);
            }

            return predicate;
        }

        public object GetFacets()
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

                return combinedFacets;
            }
        }
    }
}
