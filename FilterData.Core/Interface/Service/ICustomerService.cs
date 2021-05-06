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
        public List<Customer> getAll(List<Customer> listCustomer);

        /// <summary>
        /// Xử lý dữ liệu ngày tháng năm từ Excel sang DataBase
        /// </summary>
        /// <param name="birth"></param>
        /// <returns>
        /// DateTime: DateOfBirth
        /// </returns>
        public DateTime dateOfBirth(string birth);

        public List<Customer> FilterDateExcel(List<Customer> listCustomer);
    }
}