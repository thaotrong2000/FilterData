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
        private string connectString = "Host = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MF817-Import-NTTHAO;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "AllowZeroDateTime=True"
                ;

        private IDbConnection dbConnection;

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

            dbConnection = new MySqlConnection(connectString);

            //
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@m_CustomerId", customerId);

            var checkCustomerIdExist = false;

            checkCustomerIdExist = dbConnection.QueryFirstOrDefault<bool>("proc_CheckExistCustomerId",
                param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return checkCustomerIdExist;
        }

        /// <summary>
        /// Kiểm tra GroupName trong Excel có hợp lệ hay không ( Đã có sẵn trong DataBase là hợp lệ)
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>
        /// - true: Tên GroupName hợp lệ
        /// - false: Tên GroupName không hợp lệ ( Hãy sửa lại )
        /// </returns>
        public bool CheckExistGroupName(string groupName)
        {
            // Kết nối với Database để xem GroupName có hợp lệ hay không
            // B1: Kết nối với cơ sở dữ liệu
            dbConnection = new MySqlConnection(connectString);

            //
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@m_GroupName", groupName);

            var checkGroupName = false;

            checkGroupName = dbConnection.QueryFirstOrDefault<bool>("proc_CheckExistGroupName",
                param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return checkGroupName;
        }

        /// <summary>
        /// Kiểm tra xem PhoneNumber đã tồn tại trong hệ thống chưa
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>
        /// - true: Đã tồn tại trong hệ thống ( Không thể thêm )
        /// - false: Chưa tồn tại trong hệ thống ( Có thể thêm )
        /// </returns>
        /// CreatedBy: NTTHAO(6/5/2021)
        public bool CheckExistPhoneNumber(string phoneNumber)
        {
            // Kết nối với Database để xem CustomerId đã tồn tại hay chưa
            // B1: Kết nối với cơ sở dữ liệu
            dbConnection = new MySqlConnection(connectString);

            // Gán các biến trong Procedure
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@m_PhoneNumber", phoneNumber);

            var checkPhoneNumberExist = false;

            checkPhoneNumberExist = dbConnection.QueryFirstOrDefault<bool>("proc_CheckExistPhoneNumber",
                param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return checkPhoneNumberExist;
        }

        /// <summary>
        /// Thêm dữ liệu từ Excel vào DataBase
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreatedBy: NTTHAO(6/5/2021)
        public List<Customer> PostData(List<Customer> customer)
        {
            // Lấy dữ liệu từ Excel để thêm vào DataBase
            var listCustomer = customer;

            // Xử lý dữ liệu trong Excel

            // Thêm vào Database

            dbConnection = new MySqlConnection(connectString);
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
                    listCustomer[countList].Status = string.Concat(listCustomer[countList].Status, " Mã khách hàng đã tồn tại trong hệ thống.");
                    continue;
                }

                var checkPhoneNumber = false;
                // Kiểm tra xem PhoneNumber đã tồn tại trong hệ thống hay chưa
                checkPhoneNumber = CheckExistPhoneNumber(listCustomer[countList].PhoneNumber);
                if (checkPhoneNumber == true)
                {
                    listCustomer[countList].Status = string.Concat(listCustomer[countList].Status, $" SĐT {listCustomer[countList].PhoneNumber} đã có trong hệ thống.");
                    continue;
                }

                var checkGroupName = true;
                // Kiểm tra xem tên GroupName có hợp lệ hay không
                checkGroupName = CheckExistGroupName(listCustomer[countList].GroupName);
                if (checkGroupName == false)
                {
                    listCustomer[countList].Status = string.Concat(listCustomer[countList].Status, " Nhóm khách hàng không có trong hệ thống.");
                    continue;
                }

                // Thực hiện việc truyền dữ liệu vào Procedure

                customers = dbConnection.Execute("Proc_InsertCustomer", param: dynamicParameters, commandType: CommandType.StoredProcedure);
            }

            return listCustomer;
        }
    }
}