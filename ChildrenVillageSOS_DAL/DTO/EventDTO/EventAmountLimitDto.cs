using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.EventDTO
{
    public class EventAmountLimitDto
    {
        public int? EventId { get; set; }
        public decimal? AmountLimit { get; set; }
        public VillageExpenseDto? Village { get; set; }
    }

    public class VillageExpenseDto
    {
        public string? VillageId { get; set; }
        public string? VillageName { get; set; }
        public decimal? TotalHouseExpense { get; set; }
        public List<HouseExpenseDto>? Houses { get; set; }
    }

    public class HouseExpenseDto
    {
        public string? HouseId { get; set; }
        public string? HouseName { get; set; }
        public int? BadHealthChildrenCount { get; set; }
        public decimal? HouseExpense { get; set; }
    }

}
