using Sv103.Feature.BasicContent.Models;
using System.Linq.Expressions;
using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Sv103.Feature.BasicContent.Repository
{
    public interface ISearchRepository
    {
        object SearchMethod(string searchTerm, List<string> brands, List<string> categories);
        Expression<Func<SearchModel, bool>> BuildSearchPredicate(string[] searchTerms, List<string> brands, List<string> categories);
    }
}
