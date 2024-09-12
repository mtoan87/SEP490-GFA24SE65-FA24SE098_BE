using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.Helpers
{
    public static class IdGenerator
    {
        // Hàm GenerateId hỗ trợ cho các prefix khác nhau
        //prefix cứ hiểu nó là mấy cái chữ trước mấy con số VD: "UA" của "UA001"
        public static string GenerateId(IEnumerable<string> existingIds, string prefix, int length = 3)
        {
            if (!existingIds.Any())
            {
                return prefix + "001"; // Trả về "C001", "UA001", "V001", v.v nếu không có ID nào trong db
            }

            // Lấy tất cả số từ ID (bỏ prefix) và tìm số lớn nhất
            var maxNumber = existingIds
                .Select(id => int.Parse(id.Substring(prefix.Length)))
                .Max();

            // Tạo ID mới dựa trên số lớn nhất + 1
            return prefix + (maxNumber + 1).ToString($"D{length}");
        }
    }
}
