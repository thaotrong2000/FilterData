using FilterData.Core.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterData.Core.Interface.Repository
{
    /// <summary>
    /// Interface xử lý, kết nối đến DataBase
    /// Interface này cơ sở để các Class ở tầng InFrastructure kế thừa và tùy biến tiếp
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Phương thức thực hiện việc Lấy toàn bộ dữ liệu từ DataBase
        /// </summary>
        /// <returns></returns>
        public List<Customer> PostData(List<Customer> customer);

        public bool CheckExistCustomerId(string customerId);

        public bool CheckExistPhoneNumber(string customerId);

        public bool CheckExistGroupName(string groupName);
    }
}