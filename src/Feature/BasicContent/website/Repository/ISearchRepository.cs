using Sv103.Feature.BasicContent.Models;
using System.Linq.Expressions;
using System;
using System.Web.Mvc;

namespace Sv103.Feature.BasicContent.Repository
{
    public interface ISearchRepository
    {
        object SearchMethod(string searchTerm);
        Expression<Func<SearchModel, bool>> BuildSearchPredicate(string[] searchTerms);
    }
}
