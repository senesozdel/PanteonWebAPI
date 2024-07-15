using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PanteonWebAPI.Interfaces;
using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingTypeController : ControllerBase
    {
        private IBuildingType _buildingType;

        public BuildingTypeController(IBuildingType buildingType)
        {
            _buildingType = buildingType;
        }

        [HttpGet]
        public Task<IEnumerable<BuildingType>> GetAllBuildingTypes()
        {
            return _buildingType.GetAllBuildingTypesAsync();
        }

        [HttpGet("{buildingTypeId}")]
        public Task<BuildingType> GetBuildingTypeById([FromQuery] int buildingTypeId)
        {
            return _buildingType.GetBuildingTypeByIdAsync(buildingTypeId);
        }

        [HttpPost("addBuildingType")]

        public Task<BuildingType> AddBuildingType([FromBody] BuildingType buildingType)
        {
            return _buildingType.AddBuildingTypeAsync(buildingType);
        }

        [HttpDelete("deleteBuildingType")]

        public Task DeleteBuildingTypeAsync([FromQuery] int buildingTypeId)
        {
            return _buildingType.DeleteBuildingTypeAsync(buildingTypeId);
        }
    }
}
