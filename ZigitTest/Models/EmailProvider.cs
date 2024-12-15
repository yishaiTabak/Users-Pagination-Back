using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ZigitTest.Models
{
    public class EmailProvider
    {
        [Key]
        public string Name { get; set; }
    }

}
