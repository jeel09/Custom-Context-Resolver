using Glass.Mapper.Sc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCommerce.Feature.Products.Models.Glass
{
    public class MapperBase
    {
        public static ISitecoreService GlassContext => ResolveSitecoreContext();

        private static ISitecoreService ResolveSitecoreContext()
        {
            try
            {
                return new SitecoreService(Sitecore.Context.Database);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("MapperBase.ResolveSitecoreContext", ex, typeof(MapperBase));
                return null;
            }
        }
    }
}