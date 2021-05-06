using FilterData.Core.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterData.Core.Interface.Service
{
    public interface ICustomerService
    {
        /// <summary>
        /// Thực hiên việc xử lý nghiệp vụ (Service)
        /// Để có thể xử lý với DataBase thì kế thừa từ Repository
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTTHAO(5/5/2021)
        public IEnumerable<Customer> getAll();

        public string dateOfBirth(string birth);
    }
}