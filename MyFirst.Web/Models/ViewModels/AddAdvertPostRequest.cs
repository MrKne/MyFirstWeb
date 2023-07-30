﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.ObjectPool;

namespace MyFirst.Web.Models.ViewModels
{
    public class AddAdvertPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Display tags

        public IEnumerable<SelectListItem> Tags { get; set; }
        // Collect Tag
        public string SelectedTag { get; set; }

        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
