using System.ComponentModel.DataAnnotations;

namespace UnitTestingDemoApi.Models
{
    public class DemoEntity
    {
        [Required]
        public string ImportantProperty { get; set; }
        public string NotSoImportantProperty { get; set; }
    }
}