using Microsoft.AspNetCore.Mvc;
using MyFirst.Web.Models.ViewModels;
using MyFirst.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using MyFirst.Web.Models.Domain;
using Microsoft.AspNetCore.Authorization;

namespace MyFirst.Web.Controllers
{
    [Authorize(Roles = "Developer,Admin")]


    public class AdminAdvertPostControler : Controller
    {
    private readonly ITagRepository tagRepository;
    private readonly IAdvertPostRepository advertPostRepository;

    public AdminAdvertPostControler(ITagRepository tagRepository, IAdvertPostRepository advertPostRepository)
    {
        this.tagRepository = tagRepository;
        this.advertPostRepository = advertPostRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {

        // get tags from repository
        var tags = await tagRepository.GetAllAsync();
        var model = new AddAdvertPostRequest
        { Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })

        };

        return View(model);

    }


    [HttpPost]
    public async Task<IActionResult> Add(AddAdvertPostRequest addAdvertPostRequest)
    {

        //Map view model to doamin model 
        var advertPost = new AdvertPost
        {
            Heading = addAdvertPostRequest.Heading,
            PageTitle = addAdvertPostRequest.PageTitle,
            Content = addAdvertPostRequest.Content,
            ShortDescription = addAdvertPostRequest.ShortDescription,
            FeaturedImageUrl = addAdvertPostRequest.FeaturedImageUrl,
            UrlHandle = addAdvertPostRequest.FeaturedImageUrl,
            PublishedDate = addAdvertPostRequest.PublishedDate,
            Author = addAdvertPostRequest.Author,
            Visible = addAdvertPostRequest.Visible,


        };

        /// Map tags from selected tags

        var selectedTags = new List<Tag>();

        foreach (var selectedTagId in addAdvertPostRequest.SelectedTags)
        {
            var selectedTagAsGuid = Guid.Parse(selectedTagId);
            var existingTag = await tagRepository.GetAsync(selectedTagAsGuid);

            if (existingTag != null) 
            {
                selectedTags.Add(existingTag);
            }
        }

        advertPost.Tags = selectedTags;

        await advertPostRepository.AddAsync(advertPost);
        return RedirectToAction("Add");
    }


    [HttpGet]

        public async Task<IActionResult> List()
        {
            // call repository

           var advertPosts = await advertPostRepository.GetAllAsync();
            return View(advertPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrieve the result from the repository
           var advertPost = await advertPostRepository.GetAsync(id);

            //map the domain model into view into the view model
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (advertPost != null)
            { 
            var model = new EditAdvertPostRequest
             {
                Id = advertPost.Id,
                Heading = advertPost.Heading,
                PageTitle = advertPost.PageTitle,
                Content = advertPost.Content,
                Author = advertPost.Author,
                FeaturedImageUrl = advertPost.FeaturedImageUrl,
                UrlHandle = advertPost.UrlHandle,
                ShortDescription = advertPost.ShortDescription,
                PublishedDate = advertPost.PublishedDate,
                Visible = advertPost.Visible,
                Tags = tagsDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Name, Value = x.Id.ToString(),
                }),
                SelectedTags = advertPost.Tags.Select(x => x.Id.ToString()).ToArray()
             };

            return View(model);
            }
            //pass data to view
           return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAdvertPostRequest editAdvertPostRequest)
        {
            // map view model back to domain model
            var advertPostDomainModel = new AdvertPost
            {
                Id = editAdvertPostRequest.Id,
                Heading = editAdvertPostRequest.Heading,
                PageTitle = editAdvertPostRequest.PageTitle,
                Content = editAdvertPostRequest.Content,
                Author = editAdvertPostRequest.Author,
                ShortDescription = editAdvertPostRequest.ShortDescription,
                FeaturedImageUrl = editAdvertPostRequest.FeaturedImageUrl,
                PublishedDate = editAdvertPostRequest.PublishedDate,
                UrlHandle = editAdvertPostRequest.UrlHandle,
                Visible = editAdvertPostRequest.Visible
            };

            // Map tags into domain model

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editAdvertPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);

                    }
                    
                }
            }
            advertPostDomainModel.Tags = selectedTags;
            // submit information to repository to update

           var updatedAdvert =  await advertPostRepository.UpdateAsync(advertPostDomainModel);

            if (updatedAdvert != null)
            {
                // show success notification
                return RedirectToAction("Edit");
            }
            // show error notification
            return RedirectToAction("Edit");

            // redirect to GET method
        }
        [HttpPost]
        public async Task<IActionResult> Delete (EditAdvertPostRequest editAdvertPostRequest)
        {
            // talk to repository to delete advert and tags
           var deletedAdvertPost = await advertPostRepository.DeleteAsync(editAdvertPostRequest.Id);

            if (deletedAdvertPost != null)
            {
                //show success notification
                return RedirectToAction("List");
            }

            //show error nitification
            return RedirectToAction("Edit", new {id = editAdvertPostRequest.Id} );
         
        }
    }
}