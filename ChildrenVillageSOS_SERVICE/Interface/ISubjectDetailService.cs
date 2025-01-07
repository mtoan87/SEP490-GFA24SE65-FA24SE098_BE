using ChildrenVillageSOS_DAL.DTO.SubjectDetailDTO;
using ChildrenVillageSOS_DAL.DTO.SubjectDetailsDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface ISubjectDetailService
    {
        Task<IEnumerable<SubjectDetail>> GetAllSubjectDetails();
        Task<SubjectDetail> GetSubjectDetailById(int id);
        Task<SubjectDetail> CreateSubjectDetail(CreateSubjectDetailsDTO createSubjectDetail);
        Task<SubjectDetail> UpdateSubjectDetail(int id, UpdateSubjectDetailsDTO updateSubjectDetail);
        Task<SubjectDetail> DeleteSubjectDetail(int id);
        Task<SubjectDetail> RestoreSubjectDetail(int id);
        Task<List<SubjectDetail>> SearchSubjects(SearchSubjectDTO searchSubjectDTO);
    }
}
