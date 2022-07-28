namespace FastFood.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using FastFood.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Employees;

    public class EmployeesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public EmployeesController(IMapper mapper, ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }


        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterEmployeeInputModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> All()
        {
            throw new NotImplementedException();
        }
    }
}
