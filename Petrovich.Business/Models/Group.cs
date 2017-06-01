using Petrovich.Business.Models.Base;

namespace Petrovich.Business.Models
{
    public class Group : BaseEntity
    {
        public int GroupId { get; set; }
        public string Title { get; set; }

        //public int CategoryId { get; set; }

        //public Category Category { get; set; }
        //public ProductCollection Products { get; set; }
    }
}
