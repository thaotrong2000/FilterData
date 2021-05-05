using FilterData.Core.Interface.Repository;
using FilterData.Core.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace importDataExcelToDatabase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterDataController : ControllerBase
    {
        /// <summary>
        /// Khai báo 2 thuộc tính của Class Controller
        /// </summary>
        private ICustomerRepository _customerRepository;

        private ICustomerService _customerService;

        /// <summary>
        /// Hàm tự khởi tạo khi Class được gọi đến
        /// Gọi đến Xử lý DataBase ( Repository )
        /// Gọi đến Xử lý Nghiệp vụ ( Service )
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="customerService"></param>
        public FilterDataController(ICustomerRepository customerRepository, ICustomerService customerService)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            var customers = _customerService.getAll();
            if (customers.Count() > 0)
            {
                return Ok(customers);
            }
            else
            {
                return NoContent();
            }
        }
    }
}