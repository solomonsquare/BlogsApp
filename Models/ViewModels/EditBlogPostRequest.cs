using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Bloggee.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }

        public string PageTitle { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDated { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }

        //Select the ID of the tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
