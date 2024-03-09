using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Interfaces;

namespace WebApplication1.Models
{
    public class Comments : IBaseModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public int Name { get; set; }
        public string Comment { get; set; }
        public int BlogId { get; set; }
        
    }
}
