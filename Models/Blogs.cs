using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Interfaces;

namespace WebApplication1.Models
{
    public class Blogs : IBaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        [ForeignKey("Id")]
        public Users uid { get; set; }
        public int ModifiedBy { get; set; }
        public int Views { get; set; }
    }
}
