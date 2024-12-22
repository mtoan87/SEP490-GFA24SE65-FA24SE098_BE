using ChildrenVillageSOS_DAL.DTO.SchoolDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public async Task<IEnumerable<School>> GetAllSchools()
        {
            return await _schoolRepository.GetAllNotDeletedAsync();
        }

        public async Task<School> GetSchoolById(int id)
        {
            return await _schoolRepository.GetByIdAsync(id);
        }

        public async Task<School> CreateSchool(CreateSchoolDTO createSchool)
        {
            var newSchool = new School
            {
                SchoolName = createSchool.SchoolName,
                Address = createSchool.Address,
                SchoolType = createSchool.SchoolType,
                PhoneNumber = createSchool.PhoneNumber,
                Email = createSchool.Email,
                CreatedBy = createSchool.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _schoolRepository.AddAsync(newSchool);
            return newSchool;
        }

        public async Task<School> UpdateSchool(int id, UpdateSchoolDTO updateSchool)
        {
            var existingSchool = await _schoolRepository.GetByIdAsync(id);
            if (existingSchool == null)
            {
                throw new Exception($"School with ID {id} not found!");
            }

            existingSchool.SchoolName = updateSchool.SchoolName;
            existingSchool.Address = updateSchool.Address;
            existingSchool.SchoolType = updateSchool.SchoolType;
            existingSchool.PhoneNumber = updateSchool.PhoneNumber;
            existingSchool.Email = updateSchool.Email;
            existingSchool.ModifiedBy = updateSchool.ModifiedBy;
            existingSchool.ModifiedDate = DateTime.Now;

            await _schoolRepository.UpdateAsync(existingSchool);
            return existingSchool;
        }

        public async Task<School> DeleteSchool(int id)
        {
            var school = await _schoolRepository.GetByIdAsync(id);
            if (school == null)
            {
                throw new Exception($"School with ID {id} not found");
            }

            if (school.IsDeleted)
            {
                await _schoolRepository.RemoveAsync(school);
            }
            else
            {
                school.IsDeleted = true;
                await _schoolRepository.UpdateAsync(school);
            }

            return school;
        }

        public async Task<School> RestoreSchool(int id)
        {
            var school = await _schoolRepository.GetByIdAsync(id);
            if (school == null)
            {
                throw new Exception($"School with ID {id} not found");
            }

            if (school.IsDeleted)
            {
                school.IsDeleted = false;
                await _schoolRepository.UpdateAsync(school);
            }

            return school;
        }
    }
}
