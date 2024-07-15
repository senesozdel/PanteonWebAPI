using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Interfaces
{
    public interface IBuildingConfiguration
    {
        Task<BuildingConfiguration> AddBuildingConfigurationAsync(BuildingConfiguration buildingConfiguration);

        Task<BuildingConfiguration> UpdateBuildingConfigurationAsync(BuildingConfiguration buildingConfiguration);
        Task DeleteBuildingConfigurationAsync(int buildingConfigurationId);

        Task<BuildingConfiguration> GetBuildingConfigurationByIdAsync(int buildingConfigurationId);

        Task<IEnumerable<BuildingConfiguration>> GetAllBuildingConfigurationsAsync();
    }
}
