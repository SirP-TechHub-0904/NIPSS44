﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44.Data.Model
{
    public class LocalGoverment
    {
        public long Id { get; set; }
        public string LGAName { get; set; }

        public long StatesId { get; set; }
        public State States { get; set; }
    }
}
