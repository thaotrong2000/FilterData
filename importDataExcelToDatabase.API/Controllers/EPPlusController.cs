using FilterData.Core.Entities.Customer;
using FilterData.Core.Entities.Respon;
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
        [HttpPost("import")]
        public async Task<DemoResponse<List<Customer>>> Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return DemoResponse<List<Customer>>.GetResult(-1, "formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return DemoResponse<List<Customer>>.GetResult(-1, "Not Support file extension");
            }

            var list = new List<Customer>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        double dataExcel = Convert.ToDouble(worksheet.Cells[row, 6].Value.ToString());
                        Customer newCustomer = new Customer
                        {
                            CustomerId = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            FullName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            MemberCardId = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            GroupName = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            PhoneNumber = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            //DateOfBirth = dateBirth,
                            //DateOfBirth = worksheet.Cells[row, 6].Value.ToString(),
                            //CompanyName = worksheet.Cells[row, 7].Value.ToString().Trim(),
                            //CompanyTaxCode = worksheet.Cells[row, 8].Value.ToString().Trim(),
                            //Email = worksheet.Cells[row, 9].Value.ToString().Trim(),
                        };
                        // Thêm một bản ghi vào danh sách
                        list.Add(newCustomer);
                    }
                }
            }

            // add list to db ..
            // here just read and return

            return DemoResponse<List<Customer>>.GetResult(0, "OK", list);
        }
    }
}