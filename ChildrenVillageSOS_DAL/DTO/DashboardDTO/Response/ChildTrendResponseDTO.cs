using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.DashboardDTO.Response
{
    public class ChildTrendResponseDTO
    {
        public List<ChildTrendDTO> Data2023 { get; set; }
        public List<ChildTrendDTO> Data2024 { get; set; }
        public List<ChildTrendDTO> Data2025 { get; set; }
    }
}
