using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Interfaces
{
    public interface IBuildingType
    {
        Task<BuildingType> AddBuildingTypeAsync(BuildingType buildingType);

        Task<BuildingType> UpdateBuildingTypeAsync(BuildingType buildingType);
        Task DeleteBuildingTypeAsync(int buildingTypeId);

        Task<BuildingType> GetBuildingTypeByIdAsync(int buildingTypeId);

        Task<IEnumerable<BuildingType>> GetAllBuildingTypesAsync();
    }
}
