using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCommerce.Feature.Products.Models.Glass
{
    public interface IBaseItem
    {
        [SitecoreId]
        Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the display name of the item.
        /// </summary>
        /// <value>
        /// The display name of the item.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.DisplayName)]
        string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the item full path.
        /// </summary>
        /// <value>
        /// The item full path.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.FullPath)]

        string FullPath { get; set; }


        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Name)]
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the item template id.
        /// </summary>
        /// <value>
        /// The item template id.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.TemplateId)]

        Guid TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the name of the item template.
        /// </summary>
        /// <value>
        /// The name of the item template.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.TemplateName)]

        string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets the item URL.
        /// </summary>
        /// <value>
        /// The item URL.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Url, UrlOptions = SitecoreInfoUrlOptions.SiteResolving)]

        string Url { get; set; }

        /// <summary>
        /// Gets or sets the item version.
        /// </summary>
        /// <value>
        /// The item version.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Version)]

        int Version { get; set; }
    }
}