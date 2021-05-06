using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterData.Core.Entities.Customer
{
    /// <summary>
    /// Thực thể ( đối tượng ) Khách hàng  _  Đây là thành phần khung của các đối tượng khách hàng
    /// </summary>
    /// CreatedBy: NTTHAO(5/5/2021)
    public class Customer
    {
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>
        public string MemberCardId { get; set; }

        /// <summary>
        /// Nhóm khách hàng
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Email cá nhân
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }

        public string Status { get; set; }
    }
}