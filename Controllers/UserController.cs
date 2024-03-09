using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public AppdbContext db { get; set; }
        public UserController(AppdbContext _db)
        {
            db = _db;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ApiResponse<LoginVm>> Login([FromBody] LoginVm vm)
        {
            try
            {
                Users BlogUser = await db.Users.Where(x => x.UserName == vm.UserName).FirstOrDefaultAsync();

                if (BlogUser == null)
                {
                    return new ApiResponse<LoginVm>(vm, 404, "User not found with username " + vm.UserName);

                }

                if (vm.Password!=BlogUser.Password )
                {
                    return new ApiResponse<LoginVm>(vm, 500, "Login Failed - Incorrect Password" + vm.UserName);

                }

                BlogUser.LastLogin=DateTime.Now;
                db.Update(BlogUser);
                db.SaveChanges();

                return new ApiResponse<LoginVm>(vm,200,"Login Successful");
            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<LoginVm>(vm, 500, ex.Message);

            }
        }
    }
}
