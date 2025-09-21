using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Authentication;
using DevExpress.ExpressApp.Security.Authentication.ClientServer;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DXApplication3.Blazor.Server.Models;
using DXApplication3.Module.BusinessObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;

namespace DXApplication3.WebApi.JWT;

[ApiController]
[Route("api/[controller]")]
// This is a JWT authentication service sample.
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationTokenProvider tokenProvider;
    private readonly IServiceProvider serviceProvider;
    private readonly IObjectSpaceFactory objectSpaceFactory;

    public AuthenticationController(IAuthenticationTokenProvider tokenProvider, IServiceProvider serviceProvider, IObjectSpaceFactory objectSpaceFactory)
    {
        this.tokenProvider = tokenProvider;
        this.serviceProvider = serviceProvider;
        this.objectSpaceFactory = objectSpaceFactory;
    }
   
    [HttpPost("Authenticate")]
    [SwaggerOperation("Checks if the user with the specified logon parameters exists in the database. If it does, authenticates this user.", "Refer to the following help topic for more information on authentication methods in the XAF Security System: <a href='https://docs.devexpress.com/eXpressAppFramework/119064/data-security-and-safety/security-system/authentication'>Authentication</a>.")]
    //public IActionResult Authenticate(
    //    [FromBody]
    //    [SwaggerRequestBody(@"For example: <br /> { ""userName"": ""Admin"", ""password"": """" }")]
    //    AuthenticationStandardLogonParameters logonParameters
    //) {
    //    try {
    //        return Ok(tokenProvider.Authenticate(logonParameters));
    //    }
    //    catch(AuthenticationException) {
    //        return Unauthorized("User name or password is incorrect.");
    //    }
    //}

    public IActionResult Authenticate(
        [FromBody]
        [SwaggerRequestBody(@"For example: <br /> { ""UserName"": ""Admin"", ""Password"": """" }")]
        AuthenticationStandardLogonParameters logonParameters
    )
    {
        try
        {
            
            var tokenString = tokenProvider.Authenticate(logonParameters);

            // Parse JWT token để lấy thông tin exp và roles
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(tokenString);


            // Lấy roles từ database chú ý nhóm quyền phải được phân có quyền đọc thông tin từ Base User
            string[] roles = Array.Empty<string>();


            //// Lấy danh sách role names
            // roles = user.Roles.Select(r => r.Name).ToArray();
            using (var scope = serviceProvider.CreateScope())
            {
                // Tạo object space từ factory
                using IObjectSpace objectSpace = objectSpaceFactory.CreateObjectSpace(typeof(PermissionPolicyUser));

                var user = objectSpace.FirstOrDefault<ApplicationUser>(u => u.UserName == logonParameters.UserName);
                if (user is IPermissionPolicyUser permissionUser)
                {
                    roles = permissionUser.Roles.Select(r => r.Name).ToArray();
                }
            }

            // Tính thời gian còn lại (mili giây)
            var expiresInMs = (long)(jwtToken.ValidTo - DateTime.UtcNow).TotalSeconds;
            if (expiresInMs < 0) expiresInMs = 0; // Đảm bảo không trả về số âm nếu token đã hết hạn


            // Tạo response JSON tùy chỉnh
            var response = new AuthResponse
            {
                AccessToken = tokenString,
                UserName = logonParameters.UserName,
                Roles = roles,
                ExpiresAt = jwtToken.ValidTo,
                ExpiresIn = expiresInMs
            };

            return Ok(response);
        }
        catch (AuthenticationException)
        {
            return Unauthorized(new { Error = "User name or password is incorrect." });
        }
    }
}
