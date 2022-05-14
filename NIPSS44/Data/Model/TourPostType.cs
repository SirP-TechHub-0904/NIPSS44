using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44.Data.Model
{
    public class TourPostType
    {
        public long Id { get; set; }
        public string PostType { get; set; }
        public ICollection<TourPost> TourPosts { get; set; }
    }
}
