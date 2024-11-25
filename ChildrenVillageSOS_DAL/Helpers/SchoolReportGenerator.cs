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

            if (gpa >= 9) return "Outstanding";
            if (gpa >= 8) return "Excellent";
            if (gpa >= 7) return "Good";
            if (gpa >= 6) return "Fairly Good";
            if (gpa >= 5) return "Average";
            if (gpa >= 4) return "Fail";
            return "Fail";
        }
    }
}
