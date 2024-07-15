using PanteonWebAPI.Interfaces;
using PanteonWebAPI.Models.Data;
using PanteonWebAPI.Models.Dtos;
using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Mappers
{
    public class BuildingConfigMapper
    {
        private IBuildingType _buildingType;

        public BuildingConfigMapper(IBuildingType buildingType)
        {
            _buildingType = buildingType;
        }
        public BuildingConfiguration MapToEntity(BuildingConfigurationDto dto)
        {
            var buildingType = _buildingType.GetBuildingTypeByIdAsync(dto.BuildingTypeId).Result;

            return new BuildingConfiguration
            {
                Id = dto.Id,
                Name = dto.Name,
                BuildingTypeId = dto.BuildingTypeId,
                BuildingCost = dto.BuildingCost,
                ConstructionTime = dto.ConstructionTime,
                IsDeleted = dto.IsDeleted,
                // BuildingType alanı için ayrı bir dönüşüm yapılabilir
                BuildingType = buildingType // Varsa BuildingType doldur
            };
        }

        public BuildingConfigurationDto MapToDto(BuildingConfiguration entity)
        {
            return new BuildingConfigurationDto
            {
                Id = entity.Id,
                Name = entity.Name,
                BuildingTypeId = entity.BuildingTypeId,
                BuildingCost = entity.BuildingCost,
                ConstructionTime = entity.ConstructionTime,
                IsDeleted = entity.IsDeleted
            };
        }
    }
}
