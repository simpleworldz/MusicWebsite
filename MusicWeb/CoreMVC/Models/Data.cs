﻿using MusicHelpers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models
{
    public class Data
    {
        public MusicInfo[] data { get; set; }
        public int code { get; set; }
        public string error { get; set; }
    }
}
