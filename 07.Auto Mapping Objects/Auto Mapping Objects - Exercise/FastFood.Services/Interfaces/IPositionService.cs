namespace FastFood.Services.Interfaces
{
    using FastFood.Services.Models.Positions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPositionService
    {
        Task Add(CreatePositionDTO positionDTO);

        Task<ICollection<ListPositionDTOs>> GetAll();
    }
}
