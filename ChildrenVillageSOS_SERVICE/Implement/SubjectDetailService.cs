using ChildrenVillageSOS_DAL.DTO.SubjectDetailDTO;
using ChildrenVillageSOS_DAL.DTO.SubjectDetailsDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class SubjectDetailService : ISubjectDetailService
    {
        private readonly ISubjectDetailRepository _subjectDetailRepository;

        public SubjectDetailService(ISubjectDetailRepository subjectDetailRepository)
        {
            _subjectDetailRepository = subjectDetailRepository;
        }

        public async Task<IEnumerable<SubjectDetail>> GetAllSubjectDetails()
        {
            return await _subjectDetailRepository.GetAllNotDeletedAsync();
        }

        public async Task<SubjectDetail> GetSubjectDetailById(int id)
        {
            return await _subjectDetailRepository.GetByIdAsync(id);
        }

        public async Task<SubjectDetail> CreateSubjectDetail(CreateSubjectDetailsDTO createSubjectDetail)
        {
            var newSubjectDetail = new SubjectDetail
            {
                AcademicReportId = createSubjectDetail.AcademicReportId,
                SubjectName = createSubjectDetail.SubjectName,
                Score = createSubjectDetail.Score,
                Remarks = createSubjectDetail.Remarks,
                CreatedBy = createSubjectDetail.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _subjectDetailRepository.AddAsync(newSubjectDetail);
            return newSubjectDetail;
        }

        public async Task<SubjectDetail> UpdateSubjectDetail(int id, UpdateSubjectDetailsDTO updateSubjectDetail)
        {
            var existingSubjectDetail = await _subjectDetailRepository.GetByIdAsync(id);
            if (existingSubjectDetail == null)
            {
                throw new Exception($"SubjectDetail with ID {id} not found!");
            }

            existingSubjectDetail.AcademicReportId = updateSubjectDetail.AcademicReportId;
            existingSubjectDetail.SubjectName = updateSubjectDetail.SubjectName;
            existingSubjectDetail.Score = updateSubjectDetail.Score;
            existingSubjectDetail.Remarks = updateSubjectDetail.Remarks;
            existingSubjectDetail.ModifiedBy = updateSubjectDetail.ModifiedBy;
            existingSubjectDetail.ModifiedDate = DateTime.Now;

            await _subjectDetailRepository.UpdateAsync(existingSubjectDetail);
            return existingSubjectDetail;
        }

        public async Task<SubjectDetail> DeleteSubjectDetail(int id)
        {
            var subjectDetail = await _subjectDetailRepository.GetByIdAsync(id);
            if (subjectDetail == null)
            {
                throw new Exception($"SubjectDetail with ID {id} not found");
            }

            if (subjectDetail.IsDeleted)
            {
                await _subjectDetailRepository.RemoveAsync(subjectDetail);
            }
            else
            {
                subjectDetail.IsDeleted = true;
                await _subjectDetailRepository.UpdateAsync(subjectDetail);
            }

            return subjectDetail;
        }

        public async Task<SubjectDetail> RestoreSubjectDetail(int id)
        {
            var subjectDetail = await _subjectDetailRepository.GetByIdAsync(id);
            if (subjectDetail == null)
            {
                throw new Exception($"SubjectDetail with ID {id} not found");
            }

            if (subjectDetail.IsDeleted)
            {
                subjectDetail.IsDeleted = false;
                await _subjectDetailRepository.UpdateAsync(subjectDetail);
            }

            return subjectDetail;
        }

        public async Task<List<SubjectDetail>> SearchSubjects(SearchSubjectDTO searchSubjectDTO)
        {
            return await _subjectDetailRepository.SearchSubjects(searchSubjectDTO);
        }

    }
}
