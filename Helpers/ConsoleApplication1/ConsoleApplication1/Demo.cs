namespace ConsoleApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Demo")]
    public partial class Demo
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
