using Microsoft.EntityFrameworkCore;
using PanteonWebAPI.Interfaces;
using PanteonWebAPI.Models.Data;
using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Services
{
    public class BuildingConfigurationService : IBuildingConfiguration
    {
        private readonly AppDbContext _db;

        public BuildingConfigurationService(AppDbContext db)
        {

            _db = db;
        }
        public async Task<BuildingConfiguration> AddBuildingConfigurationAsync(BuildingConfiguration buildingConfiguration)
        {
            try
            {
                if (buildingConfiguration != null)
                {
                    buildingConfiguration.IsDeleted = false; // Yeni eklenen kayıtların IsDeleted değeri false olmalı
                    await _db.BuildingConfigurations.AddAsync(buildingConfiguration);
                    await _db.SaveChangesAsync();
                    return buildingConfiguration;
                }
                else
                {
                    throw new ArgumentNullException(nameof(buildingConfiguration), "BuildingConfiguration cannot be null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingConfiguration eklenemedi: " + ex.Message);
            }
        }


        public async Task DeleteBuildingConfigurationAsync(int buildingConfigurationId)
        {
            try
            {
                var buildingConfiguration = await _db.BuildingConfigurations.FindAsync(buildingConfigurationId);

                if (buildingConfiguration != null)
                {
                    buildingConfiguration.IsDeleted = true; // Mantıksal silme
                    _db.BuildingConfigurations.Update(buildingConfiguration);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("BuildingConfiguration bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingConfiguration silinemedi: " + ex.Message);
            }
        }


        public async Task<IEnumerable<BuildingConfiguration>> GetAllBuildingConfigurationsAsync()
        {
            try
            {

                var result = from config in _db.BuildingConfigurations
                join types in _db.BuildingTypes on config.BuildingTypeId equals types.Id
                select new BuildingConfiguration()
                {
                    Id = config.Id,
                    Name = config.Name, 
                    BuildingType = types,
                    BuildingTypeId = types.Id,
                    BuildingCost = config.BuildingCost,
                    ConstructionTime = config.ConstructionTime
                };

                return await result.ToListAsync();
                    
                   
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingConfiguration'lar getirilemedi: " + ex.Message);
            }
        }


        public async Task<BuildingConfiguration> GetBuildingConfigurationByIdAsync(int buildingConfigurationId)
        {
            try
            {
                var buildingConfiguration = await _db.BuildingConfigurations.FirstOrDefaultAsync(bc => bc.Id == buildingConfigurationId && !bc.IsDeleted);

                if (buildingConfiguration == null)
                {
                    throw new KeyNotFoundException("BuildingConfiguration bulunamadı.");
                }

                return buildingConfiguration;
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingConfiguration getirilemedi: " + ex.Message);
            }
        }


        public async Task<BuildingConfiguration> UpdateBuildingConfigurationAsync(BuildingConfiguration buildingConfiguration)
        {
            try
            {
                var existingBuildingConfiguration = await _db.BuildingConfigurations.FirstOrDefaultAsync(bc => bc.Id == buildingConfiguration.Id && !bc.IsDeleted);

                if (existingBuildingConfiguration == null)
                {
                    throw new KeyNotFoundException("BuildingConfiguration bulunamadı.");
                }

                existingBuildingConfiguration.BuildingCost = buildingConfiguration.BuildingCost;
                existingBuildingConfiguration.BuildingTypeId = buildingConfiguration.BuildingTypeId;
                existingBuildingConfiguration.ConstructionTime = buildingConfiguration.ConstructionTime;

                _db.BuildingConfigurations.Update(existingBuildingConfiguration);
                await _db.SaveChangesAsync();

                return existingBuildingConfiguration;
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingConfiguration güncellenemedi: " + ex.Message);
            }
        }

    }
}
