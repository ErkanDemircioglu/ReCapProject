using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RentCarDetailDto
    {
        public int id { get; set; }
        public string CustomerName { get; set; }
        public string BrandName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
