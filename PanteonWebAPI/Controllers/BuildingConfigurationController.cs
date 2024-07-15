using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PanteonWebAPI.Interfaces;
using PanteonWebAPI.Mappers;
using PanteonWebAPI.Models.Dtos;
using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingConfigurationController : ControllerBase
    {
        private IBuildingConfiguration _buildingConfiguration;
        private readonly BuildingConfigMapper _mapper;
        public BuildingConfigurationController(IBuildingConfiguration buildingConfiguration, BuildingConfigMapper mapper)
        {
            _buildingConfiguration = buildingConfiguration;
            _mapper = mapper;
        }

        [HttpGet]
        public Task<IEnumerable<BuildingConfiguration>> GetAllBuildingConfigurations()
        {
            return _buildingConfiguration.GetAllBuildingConfigurationsAsync();
        }

        [HttpGet("{buildingConfigurationId}")]
        public Task<BuildingConfiguration> GetBuildingConfigurationById([FromQuery] int buildingConfigurationId)
        {
            return _buildingConfiguration.GetBuildingConfigurationByIdAsync(buildingConfigurationId);
        }

        [HttpPost("addBuildingConfiguration")]

        public Task<BuildingConfiguration> AddBuildingConfiguration([FromBody] BuildingConfigurationDto buildingConfigurationDto)
        {
            var buildingConfiguration =  _mapper.MapToEntity(buildingConfigurationDto);
            return _buildingConfiguration.AddBuildingConfigurationAsync(buildingConfiguration);
        }

        [HttpDelete("deleteBuildingConfiguration")]

        public Task DeleteBuildingConfiguration([FromQuery] int buildingConfigurationId)
        {
            return _buildingConfiguration.DeleteBuildingConfigurationAsync(buildingConfigurationId);
        }
    }
}
