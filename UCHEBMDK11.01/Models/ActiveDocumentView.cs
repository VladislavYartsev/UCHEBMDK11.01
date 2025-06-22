using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCHEBMDK11._01.Models
{
    [Keyless]
    internal class ActiveDocumentView
    {
        
            public int id { get; set; }
            public int document_number { get; set; }
            public string title { get; set; }
            public string document_type { get; set; }
            public string status { get; set; }
            public DateTime creation_date { get; set; }
            public string? first_side { get; set; }
            public string? second_side { get; set; }
        
    }
}
