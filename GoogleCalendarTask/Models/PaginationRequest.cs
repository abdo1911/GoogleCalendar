using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleCalendarTask.Models
{
    public class PaginationRequest
    {
        public string? PageToken { get; set; }
        public int? Take { get; set; }
        //public string? CalenderId { get; set; }
    }
}
