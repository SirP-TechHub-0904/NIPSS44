using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44.Data.Model
{
    public class Questionner
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
        public string Instruction { get; set; }
        public string ShortLink { get; set; }
        public string LongLink { get; set; }
        public string PreviewImage { get; set; }

        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
