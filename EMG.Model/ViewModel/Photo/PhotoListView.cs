﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class PhotoListView
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public List<Photo> photoList { get; set; }
    }
}