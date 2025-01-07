using ChildrenVillageSOS_DAL.DTO.SchoolDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface ISchoolService
    {
        Task<IEnumerable<School>> GetAllSchools();
        Task<School> GetSchoolById(string id);
        Task<School> CreateSchool(CreateSchoolDTO createSchool);
        Task<School> UpdateSchool(string id, UpdateSchoolDTO updateSchool);
        Task<School> DeleteSchool(string id);
        Task<School> RestoreSchool(string id);
        Task<SchoolResponseDTO[]> GetAllSchoolsIsDeleted();
        Task<SchoolResponseDTO> GetSchoolByIdWithImg(string schoolId);
        Task<IEnumerable<SchoolResponseDTO>> GetAllSchoolWithImg();
        Task<SchoolDetailsDTO> GetSchoolDetails(string schoolId);
        Task<List<School>> SearchSchools(SearchSchoolDTO searchSchoolDTO);
    }
}
