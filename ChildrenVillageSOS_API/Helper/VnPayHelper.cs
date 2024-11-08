using ChildrenVillageSOS_API.Model;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace ChildrenVillageSOS_API.Helper
{
    public static class VnPayHelper
    {
        public static bool VerifySignature(VNPayCallbackDto callback, string hashSecret)
        {
            // Lấy tất cả các tham số từ callback, trừ `vnp_SecureHash`
            var queryParams = new SortedList<string, string>();

            foreach (PropertyInfo prop in typeof(VNPayCallbackDto).GetProperties())
            {
                var value = prop.GetValue(callback)?.ToString();
                if (!string.IsNullOrEmpty(value) && prop.Name != "vnp_SecureHash" && prop.Name != "vnp_SecureHashType")
                {
                    queryParams.Add(prop.Name, value);
                }
            }

            // Tạo chuỗi dữ liệu để băm
            var rawData = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));

            // Tạo hash từ chuỗi dữ liệu với `hashSecret`
            var hash = GenerateHmacSHA512(hashSecret, rawData);

            // So sánh hash tính toán được với vnp_SecureHash trả về từ VNPay
            return hash.Equals(callback.vnp_SecureHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string GenerateHmacSHA512(string key, string data)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
