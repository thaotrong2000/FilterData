using FilterData.Core.Entities.Customer;
using FilterData.Core.Entities.Respon;
using FilterData.Core.Interface.Repository;
using FilterData.Core.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace importDataExcelToDatabase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EPPlusController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        public ICustomerService _customerService;
        private List<Customer> listCustomer;

        public EPPlusController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
        }

        [HttpPost("import")]
        public async Task<DemoResponse<List<Customer>>> Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            // Tạo danh sách các Customer - Trả về khách hàng
            var list = new List<Customer>();

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            if (formFile == null || formFile.Length <= 0)
            {
                return DemoResponse<List<Customer>>.GetResult(-1, "formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return DemoResponse<List<Customer>>.GetResult(-1, "Not Support file extension");
            }

            // Lưu biến chưa các dữ liệu khách hàng, bao gồm trạng thái đã được cập nhật
            var customersGetAll = new List<Customer>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        // Tạo biến để lưu DateOfBirth
                        DateTime dateOfBirth = DateTime.Now;

                        // Nếu DateOfBirth không phải là Null thì thực hiện việc xử lý dữ liệu
                        if (worksheet.Cells[row, 6].Value != null)
                        {
                            // Lấy dữ liệu từ Excel
                            string dateExcel = worksheet.Cells[row, 6].Value.ToString();

                            // Validate dữ liệu ngày tháng năm
                            dateOfBirth = _customerService.dateOfBirth(dateExcel);
                        }

                        // Tạo mới một đối tượng
                        Customer newCustomer = new Customer
                        {
                            CustomerId = worksheet.Cells[row, 1].Value != null ? worksheet.Cells[row, 1].Value.ToString().Trim() : null,
                            FullName = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : null,
                            MemberCardId = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : null,
                            GroupName = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value.ToString().Trim() : null,
                            PhoneNumber = worksheet.Cells[row, 5].Value != null ? worksheet.Cells[row, 5].Value.ToString().Trim() : null,
                            DateOfBirth = worksheet.Cells[row, 6].Value != null ? dateOfBirth : DateTime.Now,
                            CompanyName = worksheet.Cells[row, 7].Value != null ? worksheet.Cells[row, 7].Value.ToString().Trim() : null,
                            CompanyTaxCode = worksheet.Cells[row, 8].Value != null ? worksheet.Cells[row, 8].Value.ToString().Trim() : null,
                            Email = worksheet.Cells[row, 9].Value != null ? worksheet.Cells[row, 9].Value.ToString().Trim() : null,
                            Address = worksheet.Cells[row, 10].Value != null ? worksheet.Cells[row, 10].Value.ToString().Trim() : null,
                            Note = worksheet.Cells[row, 11].Value != null ? worksheet.Cells[row, 11].Value.ToString().Trim() : null,
                            Status = string.Empty
                        };
                        // Thêm một bản ghi vào danh sách
                        list.Add(newCustomer);
                    }
                }
                listCustomer = list;

                // Xử lý những dữ liệu đã thêm vào trong danh sách List(listCustomer)
                customersGetAll = _customerService.getAll(listCustomer);
                // Xử lý dữ liệu trên Excel, chưa dùng đến DataBase
                customersGetAll = _customerService.FilterDateExcel(listCustomer);
            }

            return DemoResponse<List<Customer>>.GetResult(0, "OK", customersGetAll);
        }
    }
}