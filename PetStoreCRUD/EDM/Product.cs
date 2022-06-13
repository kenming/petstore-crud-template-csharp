using System;
using System.Collections.Generic;

namespace Thinksoft.crudTutorial.EDM
{
    public partial class Product
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long Price { get; set; }
        public string? Category { get; set; }
    }
}
