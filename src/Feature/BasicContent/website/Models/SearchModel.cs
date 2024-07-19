﻿using Sitecore.ContentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sv103.Feature.BasicContent.Models
{
    public class SearchModel
    {
        [IndexField("title_t")]
        public string Title { get; set; }

        [IndexField("description_t")]
        public string Description { get; set; }

        [IndexField("_parent")]
        public string ParentID { get; set; }

        [IndexField("brand_t")]
        public string Brand { get; set; }

        [IndexField("productcategory_t")]
        public string Category { get; set; }

        [IndexField("price_tf")]
        public float Price { get; set; }
    }
}