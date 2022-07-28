namespace FastFood.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using FastFood.Services.Interfaces;
    using FastFood.Services.Models.Positions;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Positions;

    public class PositionsController : Controller
    {
        private readonly IPositionService positionService;
        private readonly IMapper mapper;

        public PositionsController(IMapper mapper, IPositionService positionService)
        {
            this.positionService = positionService;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(@"Views\Shared\InvalidInputErrorPage.cshtml");
            }

            CreatePositionDTO positionDTO = this.mapper.Map<CreatePositionDTO>(model);

            await this.positionService.Add(positionDTO);

            return this.RedirectToAction("All", "Positions");
        }

        public async Task<IActionResult> All()
        {
            ICollection<ListPositionDTOs> categoryDTOs = await this.positionService.GetAll();

            IList<PositionsAllViewModel> categoryAll = new List<PositionsAllViewModel>();

            foreach (ListPositionDTOs categoryDTO in categoryDTOs)
            {
                categoryAll.Add(this.mapper.Map<PositionsAllViewModel>(categoryDTO));
            }

            return this.View(categoryAll);
        }
    }
}
