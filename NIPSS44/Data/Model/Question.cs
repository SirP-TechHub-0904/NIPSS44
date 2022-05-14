using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44.Data.Model
{
    public class Question
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Option Options { get; set; }

        public long QuestionnerId { get; set; }
        public Questionner Questionner { get; set; }
    }
}
