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
        public async Task<ApiResponse<LoginReturnVm>> Login([FromBody] LoginVm vmx)
        {
            LoginReturnVm vm=new LoginReturnVm();
            vm.UserId = 0;
            vm.UserName=vmx.UserName;
            try
            {
                Users BlogUser = await db.Users.Where(x => x.UserName == vmx.UserName).FirstOrDefaultAsync();

                if (BlogUser == null)
                {
                    return new ApiResponse<LoginReturnVm>(vm, 404, "User not found with username " + vm.UserName);

                }

                if (vmx.Password!=BlogUser.Password )
                {
                    return new ApiResponse<LoginReturnVm>(vm, 500, "Login Failed - Incorrect Password" + vm.UserName);

                }

                vm.LastLogin=DateTime.Now;
                vm.UserId = BlogUser.Id;
                BlogUser.LastLogin=DateTime.Now;
                db.Update(BlogUser);
                db.SaveChanges();

                return new ApiResponse<LoginReturnVm>(vm,200,"Login Successful");
            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<LoginReturnVm>(vm, 500, ex.Message);

            }
        }
    }
}
