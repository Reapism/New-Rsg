﻿using RSG.Core.Enums;
using RSG.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Text.Json.Serialization;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents properties of an RSG dictionary.
    /// </summary>
    public class DictionaryResult : IResult
    {
        public IRsgDictionary Dictionary { get; set; }
        public RandomizationType RandomizationType { get; set; }
        public BigInteger Iterations { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}