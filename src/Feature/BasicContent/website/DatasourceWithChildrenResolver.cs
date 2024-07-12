using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;

namespace Sv103.Feature.BasicContent
{
    public class DatasourceWithChildrenResolver: RenderingContentsResolver
    {
        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            Assert.ArgumentNotNull(rendering, nameof(rendering));
            Assert.ArgumentNotNull(renderingConfig, nameof(renderingConfig));

            Item datasourceItem = this.GetContextItem(rendering, renderingConfig);

            if (datasourceItem == null)
                return null;

            JObject jobject = ProcessItem(datasourceItem, rendering, renderingConfig);

            IEnumerable<Item> items = GetItems(datasourceItem);
            List<Item> itemList = items != null ? items.ToList() : null;

            if (itemList == null || itemList.Count == 0)
                return jobject;

            jobject["items"] = ProcessItems(itemList, rendering, renderingConfig);
            return jobject;

        }
    }
}