namespace PanteonWebAPI.Models.Entities
{
    public class BuildingConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BuildingTypeId { get; set; }
        public BuildingType BuildingType { get; set; }

        public double BuildingCost { get; set; }

        public int ConstructionTime { get; set; }
        public bool IsDeleted { get; set; }

    }
}
