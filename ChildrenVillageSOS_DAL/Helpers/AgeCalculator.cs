using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.Helpers
{
    public static class AgeCalculator
    {
        public static int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age))
                age--;
            return age;
        }

        public static string GetAgeGroupLabel(int ageGroup)
        {
            return ageGroup switch
            {
                0 => "0-5",
                1 => "6-10",
                2 => "11-15",
                3 => "16-18",
                _ => string.Empty
            };
        }

        public static int GetAgeGroup(DateTime dob)
        {
            var age = CalculateAge(dob);
            if (age <= 5) return 0;
            if (age <= 10) return 1;
            if (age <= 15) return 2;
            if (age <= 18) return 3;
            return -1; // Ngoài độ tuổi quan tâm
        }
    }
}
