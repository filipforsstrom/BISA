using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{

    public class LibrisItemDTO
    {
        public Xsearch xsearch { get; set; }
    }

    public class Xsearch
    {
        public int from { get; set; }
        public int to { get; set; }
        public int records { get; set; }
        public List<List> list { get; set; }
    }

    public class List
    {
        public string identifier { get; set; }
        public string title { get; set; }
        public string isbn { get; set; }
        public string type { get; set; }
        public string publisher { get; set; }
        public string date { get; set; }
        public string language { get; set; }
        public string creator { get; set; }
    }
}
