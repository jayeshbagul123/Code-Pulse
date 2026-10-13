using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test_1.Models
{
    public class Students
    {
        [Key]
        public Guid StdId { get; set; }

        [DisplayName("Student Name")]
        public string StdName { get; set; }
        public string Address { get; set; }
        public string Standard { get; set; }
        public string MobileNo { get; set; }
        public int Age { get; set; } 
    }
}
