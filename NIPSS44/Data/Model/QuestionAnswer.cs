using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44.Data.Model
{
    public class QuestionAnswer
    {
        public long Id { get; set; }
        public long AnswerId { get; set; }
        public Answer Answer { get; set; }
        public long QuestionId { get; set; }
        public Question Question { get; set; }

        public string AnswerContent { get; set; }
    }
}
