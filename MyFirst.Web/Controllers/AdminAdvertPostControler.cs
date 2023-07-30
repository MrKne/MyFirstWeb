using Microsoft.AspNetCore.Mvc;
using MyFirst.Web.Models.ViewModels;
using MyFirst.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using MyFirst.Web.Models.Domain;

namespace MyFirst.Web.Controllers
{
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



        public async Task<IActionResult> List()
        {
            // call repository

           var advertPosts = await advertPostRepository.GetAllAsync();
            return View(advertPosts);
        }

    }
}