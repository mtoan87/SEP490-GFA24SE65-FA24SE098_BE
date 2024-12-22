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
        Task<School> GetSchoolById(int id);
        Task<School> CreateSchool(CreateSchoolDTO createSchool);
        Task<School> UpdateSchool(int id, UpdateSchoolDTO updateSchool);
        Task<School> DeleteSchool(int id);
        Task<School> RestoreSchool(int id);
    }
}
