using Castle.Core.Resource;
using DevExpress.CodeParser;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.Security;
using DXApplication3.Module.BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace DXApplication3.Blazor.Server.Controllers
{
    //endpoint sẽ là /api/MyData/GetChartData.
    [Route("api/[controller]")]
    [ApiController]
    public class MyDataController : ControllerBase
    {
        private readonly IObjectSpaceFactory objectSpaceFactory;
        private readonly ISecurityStrategyBase _security;



        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Lấy tên user từ claims
                var username = User.Identity.Name;
                return Ok(new { User = username });
            }
            return Unauthorized();
        }


        public MyDataController(IObjectSpaceFactory objectSpaceFactory, ISecurityStrategyBase security)
        {
            // XAF tự inject IObjectSpaceFactory
            this.objectSpaceFactory = objectSpaceFactory;
            _security = security;
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
            catch (Exception ex)
            {
                // Trả về lỗi 500 thay vì throw
                return StatusCode(500, new { message = "Đã xảy ra lỗi", detail = ex.Message });
            }
        }


        [HttpGet("GetChartData")]
        [AllowAnonymous] // 👈 Cho phép gọi API không cần đăng nhập
        public IActionResult GetChartData()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Đã đăng nhập
            }
            var data = new
            {
                labels = new[] { "A", "B", "C" },
                values = new[] { 10, 20, 30 },
                user = User.Identity.Name??"null"
            };
            return Ok(data);
        }


        [HttpGet("check-session")]
        public IActionResult CheckSession()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var userName = User.Identity.Name; // username đã đăng nhập
                return Ok(new { loggedIn = true, userName });
            }
            else
            {
                return Unauthorized(new { loggedIn = false });
            }
        }
    }
}
