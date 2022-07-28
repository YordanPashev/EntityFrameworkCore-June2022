namespace FastFood.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FastFood.Data;
    using FastFood.Models;
    using FastFood.Services.Interfaces;
    using FastFood.Services.Models.Positions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PositionService : IPositionService
    {
        private FastFoodContext dbContext;
        private readonly IMapper mapper;

        public PositionService(FastFoodContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Add(CreatePositionDTO positionDTO)
        {
            Position possition = this.mapper.Map<Position>(positionDTO);
            dbContext.Positions.Add(possition);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ListPositionDTOs>> GetAll()
        {
            ICollection<ListPositionDTOs> result = await this.dbContext.Positions
               .ProjectTo<ListPositionDTOs>(this.mapper.ConfigurationProvider)
               .ToArrayAsync();

            return result;
        }
    }
}
