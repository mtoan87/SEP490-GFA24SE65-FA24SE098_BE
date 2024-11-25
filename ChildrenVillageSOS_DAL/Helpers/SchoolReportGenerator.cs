using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.Helpers
{
    public static class SchoolReportGenerator
    {
        public static string GetSchoolReportFromGPA(decimal? gpa)
        {
            if (gpa == null) return null;

            if (gpa >= 9) return "Excellent";
            if (gpa >= 8) return "Very Good";
            if (gpa >= 7) return "Good";
            if (gpa >= 5) return "Average";
            return "Below Average";
        }
    }
}
