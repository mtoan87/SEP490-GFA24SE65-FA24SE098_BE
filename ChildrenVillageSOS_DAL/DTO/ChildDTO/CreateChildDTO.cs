using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.ChildDTO
{
    public class CreateChildDTO
    {
        public string ChildName { get; set; }

        public string HealthStatus { get; set; }

        public string WalletType { get; set; }

        //public int? FacilitiesWalletId { get; set; }

        //public int? SystemWalletId { get; set; }

        //public int? FoodStuffWalletId { get; set; }

        //public int? HealthWalletId { get; set; }

        //public int? NecessitiesWalletId { get; set; }

        public decimal? Amount { get; set; }

        public decimal? AmountLimit { get; set; }

        public string HouseId { get; set; }

        public string SchoolId { get; set; }

        public string Gender { get; set; }

        public DateTime Dob { get; set; }

        public string Status { get; set; }

        public List<IFormFile>? Img { get; set; }
    }
}
