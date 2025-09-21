using Castle.Core.Resource;
using DevExpress.CodeParser;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DXApplication3.Module.BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DXApplication3.Blazor.Server.Controllers
{
    //endpoint sẽ là /api/MyData/GetChartData.
    [Route("api/[controller]")]
    [ApiController]
    public class MyDataController : ControllerBase
    {
        private readonly IObjectSpaceFactory objectSpaceFactory;

        public MyDataController(IObjectSpaceFactory objectSpaceFactory)
        {
            // XAF tự inject IObjectSpaceFactory
            this.objectSpaceFactory = objectSpaceFactory;
        }

        [HttpGet("GetEmployee")]
        [Authorize] // Đảm bảo controller/action yêu cầu đăng nhập
        public IActionResult GetCustomers()
        {
            try
            {
                using IObjectSpace objectSpace = objectSpaceFactory.CreateObjectSpace(typeof(Employee));
                var customers = objectSpace.GetObjects<Employee>();
                // Trả JSON đơn giản
                var result = customers.Select(c => new
                {
                    c.FirstName,
                    c.LastName
                }).ToList();
                return Ok(result);
            }
            catch (Exception ex) {
                // Trả về lỗi 500 thay vì throw
                return StatusCode(500, new { message = "Đã xảy ra lỗi", detail = ex.Message });
            }
        }


        [HttpGet("GetChartData")]
        [AllowAnonymous] // 👈 Cho phép gọi API không cần đăng nhập
        public IActionResult GetChartData()
        {
            var data = new
            {
                labels = new[] { "A", "B", "C" },
                values = new[] { 10, 20, 30 }
            };
            return Ok(data);
        }
    }
}
