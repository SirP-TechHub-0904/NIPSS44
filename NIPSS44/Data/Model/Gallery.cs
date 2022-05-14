using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44.Data.Model
{
    public class Gallery
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public bool DontShow { get; set; }
        public DateTime Date { get; set; }
    }
}
