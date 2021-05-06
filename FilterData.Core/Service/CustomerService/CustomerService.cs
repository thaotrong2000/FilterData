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
        public List<Customer> getAll(List<Customer> listCustomer)
        {
            // Xử dụng để lấy dữ liệu từ file Excel
            var customers = listCustomer;

            // Thêm dữ liệu từ file Excel vào DataBase ( hiện tại thì chưa lọc - updating)
            var filterCustomer = _customerRepository.PostData(customers);

            // Đây là nơi để xử lý nghiệp vụ của Customer
            // Đây là điểm khác giữa CustomerService và CustomerRepository

            // Trả về dữ liệu cần thiết
            return filterCustomer;
        }

        /// <summary>
        /// Xử lý dữ liệu ngày tháng cho hợp lệ
        /// </summary>
        /// <param name="birth"></param>
        /// <returns></returns>
        public DateTime dateOfBirth(string birth)
        {
            // Lọc phần tử trong mảng, và xem số phần tử trong mảng để xét điều kiện
            var splitDate = birth.Split("/");

            // Tạo ngày tháng trả về
            DateTime dateOfBirth = DateTime.Now;

            // Chuyển đổi kiểu của ngày tháng năm sang int :
            int year = int.Parse(splitDate[2]);
            int month = int.Parse(splitDate[1]);
            int day = int.Parse(splitDate[0]);
            // Kiểm tra điểu kiện, nếu có đủ cả ngày tháng năm:
            if (splitDate.Length == 3)
            {
                dateOfBirth = new DateTime(year, month, day);
            }

            // Nếu chỉ có tháng và năm thì hiển thị : 1/ month / year

            if (splitDate.Length == 2)
            {
                dateOfBirth = new DateTime(year, month, 1);
            }

            // Nếu chỉ có năm thì hiển thị : 1/1/year

            if (splitDate.Length == 1)
            {
                dateOfBirth = new DateTime(year, 1, 1);
            }

            // Trả về ngày tháng năm sau khi đã Format
            return dateOfBirth;
        }

        /// <summary>
        /// Lọc dữ liệu sợ từ Excel ( Tiền xử lý dữ liệu )
        /// </summary>
        /// <returns></returns>
        public List<Customer> FilterDateExcel(List<Customer> listCustomer)
        {
            List<Customer> list = listCustomer;

            // Sự dụng 2 vòng lặp For lồng nhau để tìm kiếm những liệu trùng nhau
            for (int start = 0; start < list.Count - 1; start++)
            {
                for (int end = start + 1; end < list.Count; end++)
                {
                    if (list[start].CustomerId == list[end].CustomerId)
                    {
                        list[start].Status = string.Concat(listCustomer[start].Status, " Mã khách hàng đã trùng với mã khách hàng khác trong tệp nhập khẩu.");
                        list[end].Status = string.Concat(listCustomer[end].Status, " Mã khách hàng đã trùng với mã khách hàng khác trong tệp nhập khẩu.");
                    }
                    if (list[start].PhoneNumber == list[end].PhoneNumber)
                    {
                        list[start].Status = string.Concat(listCustomer[start].Status, " SĐT đã trùng với SĐT khác trong tệp nhập khẩu.");
                        list[end].Status = string.Concat(listCustomer[end].Status, " SĐT đã trùng với SĐT khác trong tệp nhập khẩu.");
                    }
                }
            }

            // Trả về danh sách sau khi đã lọc dữ liệu trong Excel
            return list;
        }
    }
}