﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Sources
{
    public abstract class AbstractSignal
    {
        public abstract float SamplingRate { get; set; }
        public abstract double[] Samples { get; set; }
    }

    
}
