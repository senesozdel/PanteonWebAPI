using Microsoft.EntityFrameworkCore;
using PanteonWebAPI.Interfaces;
using PanteonWebAPI.Models.Data;
using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Services
{
    public class BuildingTypeService : IBuildingType
    {
        private readonly AppDbContext _db;

        public BuildingTypeService(AppDbContext db)
        {

            _db = db;
        }
        public async Task<BuildingType> AddBuildingTypeAsync(BuildingType buildingType)
        {
            try
            {
                if (buildingType != null)
                {
                    await _db.BuildingTypes.AddAsync(buildingType);
                    await _db.SaveChangesAsync();
                    return buildingType;
                }
                else
                {
                    throw new ArgumentNullException(nameof(buildingType), "BuildingType cannot be null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingType eklenemedi: " + ex.Message);
            }
        }


        public async Task DeleteBuildingTypeAsync(int buildingTypeId)
        {
            try
            {
                var buildingType = await _db.BuildingTypes.FindAsync(buildingTypeId);

                if (buildingType != null)
                {
                    buildingType.IsDeleted = true; // IsDeleted sütununu true yaparak mantıksal silme işlemi yapılıyor
                    _db.BuildingTypes.Update(buildingType);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("BuildingType bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingType silinemedi: " + ex.Message);
            }
        }


        public async Task<IEnumerable<BuildingType>> GetAllBuildingTypesAsync()
        {
            try
            {
                return await _db.BuildingTypes.Where(bt => !bt.IsDeleted).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingType'lar getirilemedi: " + ex.Message);
            }
        }


        public async Task<BuildingType> GetBuildingTypeByIdAsync(int buildingTypeId)
        {
            try
            {
                var buildingType = await _db.BuildingTypes.FirstOrDefaultAsync(bt => bt.Id == buildingTypeId && !bt.IsDeleted);

                if (buildingType == null)
                {
                    throw new KeyNotFoundException("BuildingType bulunamadı.");
                }

                return buildingType;
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingType getirilemedi: " + ex.Message);
            }
        }


        public async Task<BuildingType> UpdateBuildingTypeAsync(BuildingType buildingType)
        {
            try
            {
                var existingBuildingType = await _db.BuildingTypes.FirstOrDefaultAsync(bt => bt.Id == buildingType.Id && !bt.IsDeleted);

                if (existingBuildingType == null)
                {
                    throw new KeyNotFoundException("BuildingType bulunamadı.");
                }

                existingBuildingType.Name = buildingType.Name;

                _db.BuildingTypes.Update(existingBuildingType);
                await _db.SaveChangesAsync();

                return existingBuildingType;
            }
            catch (Exception ex)
            {
                throw new Exception("BuildingType güncellenemedi: " + ex.Message);
            }
        }

    }
}
