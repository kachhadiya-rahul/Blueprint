using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blueprint.Model
{
    public class EmployeePosition
    {
        [Key]
        public int Id { get; set; }
        public string PositionName { get; set; }
    }
}
