using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCommerce.Feature.Products.Models.Glass
{
    public class BaseItem : IBaseItem
    {
        /// <summary>
        ///     Gets or sets the language of the item.
        /// </summary>
        /// <value>
        ///     The item's language.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Language)]
        public virtual Language Language { get; set; }

        /// <summary>
        ///     Gets or sets the item URL.
        /// </summary>
        /// <value>
        ///     The item URL.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Url, UrlOptions = SitecoreInfoUrlOptions.Default)]
        public virtual string RelativeUrl { get; set; }

        /// <summary>
        ///     Gets or sets the base template ids.
        /// </summary>
        /// <value>
        ///     The base template ids.
        /// </value>
        [SitecoreField("__Base template")]
        public virtual string BaseTemplateIds { get; set; }

        [SitecoreField("__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreField("_categories")]
        public virtual string Tags { get; set; }

        /// <summary>
        ///     Gets the base template list.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public IList<string> BaseTemplates
        {
            get
            {
                var result = new List<string>();

                if (!string.IsNullOrWhiteSpace(BaseTemplateIds))
                {
                    return BaseTemplateIds.Split('|').ToList();
                }

                return result;
            }
        }

        /// <summary>
        ///     Gets or sets the item id.
        /// </summary>
        /// <value>
        ///     The item id.
        /// </value>
        [SitecoreId]
        public virtual Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the display name of the item.
        /// </summary>
        /// <value>
        ///     The display name of the item.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.DisplayName)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the item full path.
        /// </summary>
        /// <value>
        ///     The item full path.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.FullPath)]
        public string FullPath { get; set; }


        /// <summary>
        ///     Gets or sets the name of the item.
        /// </summary>
        /// <value>
        ///     The name of the item.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Name)]
        public virtual string Name { get; set; }



        /// <summary>
        ///     Gets or sets the item template id.
        /// </summary>
        /// <value>
        ///     The item template id.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        public virtual Guid TemplateId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the item template.
        /// </summary>
        /// <value>
        ///     The name of the item template.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.TemplateName)]
        public virtual string TemplateName { get; set; }

        /// <summary>
        ///     Gets or sets the item URL.
        /// </summary>
        /// <value>
        ///     The item URL.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Url, UrlOptions = SitecoreInfoUrlOptions.SiteResolving)]
        public virtual string Url { get; set; }

        /// <summary>
        ///     Gets or sets the item version.
        /// </summary>
        /// <value>
        ///     The item version.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Version)]
        public virtual int Version { get; set; }

        public Sitecore.Data.Items.Item GetSitecoreItem()
        {
            return Context.Database.GetItem(new ID(Id));
        }

        protected bool Equals(BaseItem other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseItem)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseItem left, BaseItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseItem left, BaseItem right)
        {
            return !Equals(left, right);
        }
    }
}