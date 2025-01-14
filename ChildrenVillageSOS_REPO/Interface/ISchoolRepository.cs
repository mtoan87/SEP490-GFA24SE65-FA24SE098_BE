﻿using ChildrenVillageSOS_DAL.DTO.SchoolDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface ISchoolRepository : IRepositoryGeneric<School>
    {
        Task<SchoolResponseDTO[]> GetAllSchoolsIsDeleted();
        SchoolResponseDTO GetSchoolByIdWithImg(string schoolId);
        Task<SchoolDetailsDTO> GetSchoolDetails(string schoolId);
        Task<List<School>> SearchSchools(SearchSchoolDTO searchSchoolDTO);
    }
}
