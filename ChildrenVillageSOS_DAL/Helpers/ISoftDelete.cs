using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.Helpers
{
    // Tạo cái Interface này chủ yếu là để các Entity có thể xài chung cái soft delete (chỉ áp dụng các entity nào có IsDeleted)
    public interface ISoftDelete
    {
        bool? IsDeleted { get; set; }
    }
}
