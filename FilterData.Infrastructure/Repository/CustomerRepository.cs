using Dapper;
using FilterData.Core.Entities.Customer;
using FilterData.Core.Interface.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// <summary>
        /// Kiểm tra xem CustomerId đã tồn tại trong hệ thống chưa
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>
        /// - true: Đã tồn tại trong hệ thống ( Không thể thêm )
        /// - false: Chưa tồn tại trong hệ thống ( Có thể thêm )
        /// </returns>
        /// CreatedBy: NTTHAO(6/5/2021)
        public bool CheckExistCustomerId(string customerId)
        {
            // Kết nối với Database để xem CustomerId đã tồn tại hay chưa
            // B1: Kết nối với cơ sở dữ liệu
            string connectString = "Host = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MF817-Import-NTTHAO;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "AllowZeroDateTime=True"
                ;
            IDbConnection dbConnection = new MySqlConnection(connectString);

            //
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@m_CustomerId", customerId);

            var checkCustomerIdExist = false;

            checkCustomerIdExist = dbConnection.QueryFirstOrDefault<bool>("proc_CheckExistCustomerId",
                param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return checkCustomerIdExist;
        }

        /// <summary>
        /// Thêm dữ liệu từ Excel vào DataBase
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreatedBy: NTTHAO(6/5/2021)
        public int PostData(List<Customer> customer)
        {
            // Lấy dữ liệu từ Excel để thêm vào DataBase
            var listCustomer = customer;

            // Xử lý dữ liệu trong Excel

            // Thêm vào Database

            string connectString = "Host = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MF817-Import-NTTHAO;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "AllowZeroDateTime=True"
                ;
            IDbConnection dbConnection = new MySqlConnection(connectString);
            var customers = 0;

            for (int countList = 0; countList < listCustomer.Count; countList++)
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                // Gán các tham số đầu vào của Procedure với các giá trị của mảng
                dynamicParameters.Add("@m_CustomerId", listCustomer[countList].CustomerId);
                dynamicParameters.Add("@m_FullName", listCustomer[countList].FullName);
                dynamicParameters.Add("@m_MemberCardId", listCustomer[countList].MemberCardId);
                dynamicParameters.Add("@m_GroupName", listCustomer[countList].GroupName);
                dynamicParameters.Add("@m_PhoneNumber", listCustomer[countList].PhoneNumber);
                dynamicParameters.Add("@m_DateOfBirth", listCustomer[countList].DateOfBirth);
                dynamicParameters.Add("@m_CompanyName", listCustomer[countList].CompanyName);
                dynamicParameters.Add("@m_CompanyTaxCode", listCustomer[countList].CompanyTaxCode);
                dynamicParameters.Add("@m_Email", listCustomer[countList].Email);
                dynamicParameters.Add("@m_Address", listCustomer[countList].Address);
                dynamicParameters.Add("@m_Note", listCustomer[countList].Note);

                var checkIdExist = false;
                // Kiểm tra xem CustomerId đã tồn tại trong hệ thống hay chưa
                checkIdExist = CheckExistCustomerId(listCustomer[countList].CustomerId);
                if (checkIdExist == true)
                {
                    continue;
                }

                // Thực hiện việc truyền dữ liệu vào Procedure

                customers = dbConnection.Execute("Proc_InsertCustomer", param: dynamicParameters, commandType: CommandType.StoredProcedure);
            }

            return customers;
        }
    }
}