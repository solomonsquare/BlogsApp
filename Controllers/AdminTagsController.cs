using Bloggee.Data;
using Bloggee.Models.Domain;
using Bloggee.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bloggee.Controllers;
using Microsoft.EntityFrameworkCore;
using Bloggee.Repositories;

namespace Bloggee.Controllers
{
    public class AdminTagsController : Controller
    {

        private BloggeeDbContext _bloggeeDbContext;
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public IActionResult Add()
        {


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // var name = Request.Form["name"];
            //var displayName = Request.Form["displayName"];

            //Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName

            };

            await tagRepository.AddAsync(tag); 


            return RedirectToAction("List");
        }

        public async Task<IActionResult> List() 
        {   
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //First method...
            //var tag = _bloggeeDbContext.Tags.Find(id);

            //second method..
            var tag = await tagRepository.GetAsync(id);

            if (tag != null) 
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName

                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            await tagRepository.UpdateAsync(tag);

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {

            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //show notification
                return RedirectToAction("List");

            }

            return RedirectToAction("Edit", new {editTagRequest.Id});
        }
    }
}
