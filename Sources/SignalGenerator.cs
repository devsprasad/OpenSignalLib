﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Sources
{
   public  class SignalGenerator : Signal
    {
       public SignalGenerator(SignalType Type, float frequency, float phase = 0,
           float SampleRate = - 1, float amplitude = 1, int length = -1)
        {
            if (SampleRate == -1) SampleRate = 20 * frequency;
            if (length == -1) length = (int)SampleRate;
            BaseSignalGenerator bs = new BaseSignalGenerator(Type);
            this.SamplingRate = SampleRate;
            bs.Amplitude = amplitude;
            bs.Frequency = frequency;
            bs.Phase = phase;
            Samples = new double[length];
            float t = 0;
            float tmp = 1 / SampleRate;
            for (int i = 0 ; i < length ; i++)
            {
                Samples[i] = bs.GetValue(t);
                t += tmp;
            }
        }

    }
}
