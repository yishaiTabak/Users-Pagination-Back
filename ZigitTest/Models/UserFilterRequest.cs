using System.ComponentModel.DataAnnotations;

namespace ZigitTest.Models
{
    public class UserFilterRequest
    {
        public Pagination Pagination { get; set; }
        public Sorting Sorting { get; set; }
        public Search Search { get; set; }
    }

    public class Pagination
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class Sorting
    {
        public string Field { get; set; } = "id";
        public string Order { get; set; } = "asc";
    }

    public class Search
    {
        public string Name { get; set; }
        public string[]? Email { get; set; }
    }
}
