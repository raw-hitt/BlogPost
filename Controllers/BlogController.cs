using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        #region HTTP Post Methods
        [HttpPost]
        [Route("CreateBlogAsync")]
        public async Task<ApiResponse<BlogVm>> CreateBlogAsync([FromBody] BlogVm vm)
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
        public ApiResponse<UpdateBlogVM> UpdateBlog([FromBody] UpdateBlogVM vm)
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
        public ApiResponse<bool> DeleteBlog([FromBody] DeleteBlogVm BlogId)
        {
            try
            {

                var _blog = db.Blogs.Where(x => x.Id == BlogId.BlogId).FirstOrDefault();

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

        [HttpPost]
        [Route("UpdateBlogViewCount")]
        public ApiResponse<int> UpdateBlogViewCount([FromBody] int id)
        {
            try
            {
                var _blog = db.Blogs.Where(x => x.Id == id).FirstOrDefault();

                if (_blog == null)
                {
                    return new ApiResponse<int>(id, 404, "No Blog Found with given id");

                }
                else
                {
                    int oldCount = _blog.Views;
                    _blog.Views = _blog.Views + 1;

                    db.Blogs.Update(_blog);
                    db.SaveChanges();

                    return new ApiResponse<int>(_blog.Views, 200, "Count increased by 1 old count = " + oldCount + " new count =" + (oldCount + 1));
                }

            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<int>(id, 500, ex.Message);

            }
        }
        #endregion

        #region Http Get Methods

        [HttpGet]
        [Route("GetAllBlogs")]
        public async Task<ApiResponse<List<Blogs>>> GetAllBlogs()
        {
            try
            {

                var allBlogs = await db.Blogs.Where(x => x.PublishDate < DateTime.Now).
                    OrderByDescending(x => x.PublishDate).ToListAsync();

                if (allBlogs != null)
                {

                    return new ApiResponse<List<Blogs>>(allBlogs);
                }
                else
                {
                    return new ApiResponse<List<Blogs>>(null, 404, "No Blogs Found");

                }
            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<List<Blogs>>(null, 500, ex.Message);

            }
        }
        
        [HttpGet]
        [Route("GetAllBlogsForAdmin")]
        public async Task<ApiResponse<List<Blogs>>> GetAllBlogsForAdmin()
        {
            try
            {

                var allBlogs = await db.Blogs.
                    OrderByDescending(x => x.PublishDate).ToListAsync();

                if (allBlogs != null)
                {

                    return new ApiResponse<List<Blogs>>(allBlogs);
                }
                else
                {
                    return new ApiResponse<List<Blogs>>(null, 404, "No Blogs Found");

                }
            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<List<Blogs>>(null, 500, ex.Message);

            }
        }

        [HttpGet]
        [Route("GetBlogById/{id}")]
        public async Task<ApiResponse<Blogs>> GetBlogById(int id)
        {
            try
            {

                var blog = await db.Blogs.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (blog != null)
                {
                    return new ApiResponse<Blogs>(blog);
                }
                else
                {
                    return new ApiResponse<Blogs>(null, 404, "No Blogs Found");

                }
            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<Blogs>(null, 500, ex.Message);

            }
        }

        [HttpGet]
        [Route("GetBlogByIdAndUpdateView/{id}")]
        public async Task<ApiResponse<Blogs>> GetBlogByIdAndUpdateView(int id)
        {
            try
            {

                var blog = await db.Blogs.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (blog != null)
                {
                    blog.Views = blog.Views + 1;
                    db.Blogs.Update(blog);
                    db.SaveChanges();
                    return new ApiResponse<Blogs>(blog);
                }
                else
                {
                    return new ApiResponse<Blogs>(null, 404, "No Blogs Found");

                }
            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<Blogs>(null, 500, ex.Message);

            }
        }

        [HttpGet]
        [Route("GetAllBlogs/{page}/{records}")]
        public async Task<ApiResponse<List<Blogs>>> GetAllBlogs(int page, int records)
        {
            try
            {

                var allBlogs = await db.Blogs.Skip(page * records).Take(records).
                    OrderByDescending(x => x.PublishDate).ToListAsync();

                if (allBlogs != null)
                {
                    return new ApiResponse<List<Blogs>>(allBlogs);
                }
                else
                {
                    return new ApiResponse<List<Blogs>>(null, 404, "No Blogs Found");

                }
            }
            catch (Exception ex)
            {
                ExceptionLogs exl = new ExceptionLogs(ex.Message);
                return new ApiResponse<List<Blogs>>(null, 500, ex.Message);

            }
        }

        #endregion
    }

  
}
