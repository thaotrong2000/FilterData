using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterData.Core.Entities.Customer
{
    /// <summary>
    /// Entity của đối tượng khách hàng( thể hiện thông tin)
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Tuổi khách hàng
        /// </summary>
        public string Age { get; set; }
    }
}