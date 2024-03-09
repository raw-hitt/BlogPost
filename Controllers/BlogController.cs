using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        public AppdbContext db { get; set; }
        public BlogController(AppdbContext _db)
        {
            db = _db;
        }

        [HttpPost]
        [Route("CreateBlog")]
        public async Task<ApiResponse<BlogVm>> CreateBlogAsync(BlogVm vm)
        {
            try
            {
                Blogs _blog = new Blogs()
                {
                    Author = vm.Author,
                    Content = vm.Content,
                    CreatedBy = vm.CreatedBy,
                    CreatedOn = DateTime.Now,
                    Title = vm.Title,
                    ModifiedBy = vm.ModifiedBy,
                    Views = 0,
                    ModifiedOn = DateTime.Now,
                    PublishDate = vm.PublishDate,

                };

                await db.Blogs.AddAsync(_blog);
                await db.SaveChangesAsync();

                return new ApiResponse<BlogVm>(vm);
            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<BlogVm>(vm, 500, ex.Message);

            }
        }


        [HttpPost]
        [Route("UpdateBlog")]
        public ApiResponse<UpdateBlogVM> UpdateBlog(UpdateBlogVM vm)
        {
            try
            {
                var _blog = db.Blogs.Where(x => x.Id == vm.Id).FirstOrDefault();

                if (_blog == null)
                {
                    return new ApiResponse<UpdateBlogVM>(vm, 404);

                }
                else
                {
                    _blog.Author = vm.Author;
                    _blog.Content = vm.Content;
                    _blog.Title = vm.Title;
                    _blog.ModifiedBy = vm.ModifiedBy;
                    _blog.ModifiedOn = DateTime.Now;
                    _blog.PublishDate = vm.PublishDate;

                    db.Blogs.Update(_blog);
                    db.SaveChanges();

                    return new ApiResponse<UpdateBlogVM>(vm);
                }

            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<UpdateBlogVM>(vm, 500, ex.Message);

            }
        }

        [HttpPost]
        [Route("DeleteBlog")]
        public ApiResponse<bool> DeleteBlog(int id)
        {
            try
            {

                var _blog = db.Blogs.Where(x => x.Id == id).FirstOrDefault();

                if (_blog == null)
                {
                    return new ApiResponse<bool>(false, 404);

                }
                else
                {
                    db.Blogs.Remove(_blog);
                    db.SaveChanges();
                    return new ApiResponse<bool>(true);
                }

            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<bool>(false, 500, ex.Message);

            }
        }
    }
}
