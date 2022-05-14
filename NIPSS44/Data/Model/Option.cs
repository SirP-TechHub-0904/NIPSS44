using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44.Data.Model
{
    public class Option
    {
        public long Id { get; set; }
        public OptionType OptionType { get; set; }
        public string ShortNote { get; set; }
        public string LongNote { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public string OptionList1 { get; set; }
        public string OptionList2 { get; set; }
        public string OptionList3 { get; set; }
        public string OptionList4 { get; set; }
        public string OptionList5 { get; set; }

        public long QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
