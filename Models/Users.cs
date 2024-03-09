using WebApplication1.Interfaces;

namespace WebApplication1.Models
{
    public class Users : IBaseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
