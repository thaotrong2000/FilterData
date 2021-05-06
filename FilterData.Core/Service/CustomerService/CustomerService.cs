using FilterData.Core.Entities.Customer;
using FilterData.Core.Interface.Repository;
using FilterData.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterData.Core.Service.CustomerService
{
    /// <summary>
    /// Xử lý nghiệp vụ cho đối tượng Customer(Khách hàng)
    /// </summary>
    /// CreatedBy: NTTHAO(5/5/2021)
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Tạo một đối tượng kế thừa toàn bộ các tính chất của Repository
        /// Vì CustomerService chỉ xử lý nghiệp vụ nên cần Repository để kết nối với DataBase
        /// </summary>
        private ICustomerRepository _customerRepository;

        /// <summary>
        /// Trình khởi tạo này sẽ tự động chạy khi gọi Class: CustomerService
        /// </summary>
        /// <param name="customerRepository"></param>
        /// CreatedBy: NTTHAO(5/5/2021)
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// Lấy toàn bộ thông tin khách hàng kèm theo lọc dữ liệu
        public IEnumerable<Customer> getAll()
        {
            // Xử dụng để lấy DataBase
            var customers = _customerRepository.getAll();

            // Đây là nơi để xử lý nghiệp vụ của Customer
            // Đây là điểm khác giữa CustomerService và CustomerRepository

            // Trả về dữ liệu cần thiết
            throw new Exception("Thao nguyen trong");
        }

        public string dateOfBirth(string birth)
        {
            return birth;
        }
    }
}