using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCHEBMDK11._01.Models
{
    internal class Legal_document
    {

        public int id { get; set; }
        public int document_type_id { get; set; }
        public int document_number { get; set; }
        public string title {  get; set; }
        public int status_id {  get; set; }
        public DateTime creation_date { get; set; }
    }
}
