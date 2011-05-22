using System.Collections.Generic;

namespace Web.Models
{
    public class BagResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BagResult> Children { get; set; }
    }
}