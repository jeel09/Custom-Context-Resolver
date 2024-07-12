using Glass.Mapper.Sc.Configuration.Attributes;
using SVCommerce.Feature.Products.Models.Glass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCommerce.Feature.Products.Models
{
    [SitecoreType(true, "D2E8495C-467B-4E23-BA94-B2A07F3D0456")]
    public class AttributeEnum : BaseItem
    {
        [SitecoreField("Enum ID")]
        public virtual int EnumId { get; set; }

        [SitecoreField("Enum Description")]
        public virtual string Description { get; set; }
    }
}