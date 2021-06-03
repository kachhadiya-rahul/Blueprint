using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blueprint.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? position { get; set; }


    }
}
