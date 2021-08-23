using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Customer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Budget { get; set; }

        public int Status { get; set; }

        public string Team { get; set; }
    }
}
