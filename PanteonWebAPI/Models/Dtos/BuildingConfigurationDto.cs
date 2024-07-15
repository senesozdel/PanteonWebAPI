using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Models.Dtos
{
    public class BuildingConfigurationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BuildingTypeId { get; set; }

        public double BuildingCost { get; set; }

        public int ConstructionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
