using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sv103.Feature.BasicContent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sv103.Feature.BasicContent.Repository
{
    public class SearchRepository : ISearchRepository
    {
        public SearchRepository()
        {
        }

        public object SearchMethod(string searchTerm, List<string> brands, List<string> categories, float minPrice, float maxPrice)
        {
            if (searchTerm == null && (brands == null || brands.Count == 0) && (categories == null || categories.Count == 0))
            {
                return null;
            }
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
            {
                return null;
            }

            var index = ContentSearchManager.GetIndex("sitecore_master_index");

            using (var context = index.CreateSearchContext())
            {
                var searchTerms = searchTerm?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                var predicate = BuildSearchPredicate(searchTerms, brands, categories, minPrice, maxPrice);

                var results = context.GetQueryable<SearchModel>()
                    .Where(predicate)
                    .Where(item => item.ParentID == "e3f962c1a34d47f283ae1fd2bc247cad")
                    .OrderBy(item => item.Title)
                    .Select(item => new
                    {
                        item.Title,
                        item.Description,
                        item.Brand,
                        ProductCategory = item.Category,
                        item.Price
                    })
                    .ToList();

                return results;
            }
        }

        public Expression<Func<SearchModel, bool>> BuildSearchPredicate(string[] searchTerms, List<string> brands, List<string> categories, float minPrice, float maxPrice)
        {
            var predicate = PredicateBuilder.True<SearchModel>();

            foreach (var term in searchTerms)
            {
                var lowerTerm = term.ToLowerInvariant();
                predicate = predicate.And(item => item.Title.Contains(lowerTerm) || item.Description.Contains(lowerTerm));
            }

            if (brands?.Count > 0)
            {
                var brandPredicate = PredicateBuilder.False<SearchModel>();
                foreach (var brand in brands)
                {
                    var lowerBrand = brand.ToLowerInvariant();
                    brandPredicate = brandPredicate.Or(item => item.Brand.Contains(lowerBrand));
                }
                predicate = predicate.And(brandPredicate);
            }

            if (categories?.Count > 0)
            {
                var categoryPredicate = PredicateBuilder.False<SearchModel>();
                foreach (var category in categories)
                {
                    var lowerCategory = category.ToLowerInvariant();
                    categoryPredicate = categoryPredicate.Or(item => item.Category.Contains(lowerCategory));
                }
                predicate = predicate.And(categoryPredicate);
            }
            predicate = predicate.And(item => item.Price >= minPrice && item.Price <= maxPrice);

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

                var priceQuery = context.GetQueryable<SearchModel>()
                    .Where(item => item.ParentID == "e3f962c1a34d47f283ae1fd2bc247cad")
                    .Select(item => item.Price)
                    .ToList();

                var brandResults = brandQuery.GetResults();
                var categoryResults = categoryQuery.GetResults();

                var combinedFacets = new
                {
                    Brands = brandResults.Facets,
                    Categories = categoryResults.Facets,
                    Price = new
                    {
                        minPrice = priceQuery.Min(),
                        maxPrice = priceQuery.Max()
                    }
                };

                return combinedFacets;
            }
        }
    }
}
