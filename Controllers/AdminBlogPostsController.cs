using Bloggee.Models.ViewModels;
using Bloggee.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bloggee.Models.Domain;
using System;
using System.Collections.Generic;

namespace Bloggee.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        //inject repository using constructor injection

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
       public async Task<IActionResult> Add()
        {
            //get tags from repository because the repository talks to the database

            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostsRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostsRequest addBlogPostsRequest)
        {

            var selectedTags = new List<Tag>();
            //Map view model to domain model.
            var blogPost = new BloggPost
            {
                Heading = addBlogPostsRequest.Heading,
                PageTitle = addBlogPostsRequest.PageTitle,
                Content = addBlogPostsRequest.Content,
                ShortDescription = addBlogPostsRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostsRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostsRequest.UrlHandle,
                PublishedDated = addBlogPostsRequest.PublishedDate,
                Author = addBlogPostsRequest.Author,
                Visible = addBlogPostsRequest.Visible,
            };

            foreach(var selecteTagId in addBlogPostsRequest.SelectedTags) 
            {
                var selectedTagAsGuid = Guid.Parse(selecteTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
                
            }

            //mapping Tags back to domain model.
            blogPost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var listBlogs = await blogPostRepository.GetAllAsync();

            return View(listBlogs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await blogPostRepository.GetAsync(id);
            var tagDomainModel = await tagRepository.GetAllAsync();

            if (blogPost != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDated = blogPost.PublishedDated,
                    Author = blogPost.Author,
                    Visible = blogPost.Visible,
                    Tags = tagDomainModel.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //map view model back to domain model as repositories deal only with domain models

            //submit information to repository to update

            //redierect back to GET method of Action method

            var blogPostModel = new BloggPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle =editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishedDated = editBlogPostRequest.PublishedDated,
                Author = editBlogPostRequest.Author,
                Visible = editBlogPostRequest.Visible

            };

            //map tags into domain model

            var selectedTags = new List<Tag>();

            foreach(var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tagID))
                {
                    var foundTag = await tagRepository.GetAsync(tagID);

                    if(foundTag != null) 
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogPostModel.Tags = selectedTags;

            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostModel);

            if (updatedBlog != null)
            {
                //show success message...
                return RedirectToAction("List");
            }
            //show error notificatation
            return RedirectToAction("Edit");

        }

        public async Task<IActionResult> Delete (EditBlogPostRequest editBlogPostRequest)
        {


            return View();
        }
    }
}
