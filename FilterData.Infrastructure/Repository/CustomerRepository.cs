using FilterData.Core.Entities.Customer;
using FilterData.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterData.Infrastructure.Repository
{
    /// <summary>
    /// Xử lý các tác vụ Liên kết với DataBase
    /// Kế thừa các phương thức từ khung ICustomerRepository
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<Customer> getAll()
        {
            throw new Exception("Nguyen Trong Thao");
        }
    }
}