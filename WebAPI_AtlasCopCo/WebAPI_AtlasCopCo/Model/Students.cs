using System.ComponentModel.DataAnnotations;

namespace WebAPI_AtlasCopCo.Model
{
    public class Students
    {
        [Key]
        public Guid StdId { get; set; }
        public string StdName { get; set; }
        public string Address { get; set; }
        public string Standard { get; set; }
        public string MobileNo { get; set; }
        public int Age { get; set; }
    }
}
