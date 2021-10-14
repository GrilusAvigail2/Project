using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Category
    {
        public Category()
        {
            Points = new HashSet<Points>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Points> Points { get; set; }
    }
}
