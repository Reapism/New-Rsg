﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RSG.Core.Interfaces
{ 
    public interface IWordList
    {
        IDictionary<string, IEnumerable<string>> WordLists { get; set; }
    }
}
