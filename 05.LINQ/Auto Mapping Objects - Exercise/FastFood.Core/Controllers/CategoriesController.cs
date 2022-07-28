namespace FastFood.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using FastFood.Services.Interfaces;
    using FastFood.Services.Models.Categories;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(@"Views\Shared\InvalidInputErrorPage.cshtml");
                //return RedirectToAction(nameof(Create), "Categories");
            }

            CreateCategoryDTO categoryDTO = this.mapper.Map<CreateCategoryDTO>(model);
            await this.categoryService.Add(categoryDTO);

            return RedirectToAction("All", "Categories");
        }

        public async Task<IActionResult> All()
        {
            ICollection<ListCategoryDTOs> categoryDTOs= await this.categoryService.GetAll();

            IList<CategoryAllViewModel> categoryAll = new List<CategoryAllViewModel>();

            foreach (ListCategoryDTOs categoryDTO in categoryDTOs)
            {
                categoryAll.Add(this.mapper.Map<CategoryAllViewModel>(categoryDTO));
            }

            return this.View(categoryAll);
        }
    }
}
