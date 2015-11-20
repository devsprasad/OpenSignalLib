using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Sources
{
    public class Sinusoidal : Signal
    {
        public Sinusoidal(float frequency, float phase = 0, float SampleRate = - 1, float amplitude = 1, int length = -1)
        {
            if (SampleRate == -1) SampleRate = 30 * frequency;
            if (length == -1) length = (int)SampleRate;
            this.SamplingRate = SampleRate;
            BaseSignalGenerator p = new BaseSignalGenerator(SignalType.Sine);
            p.Amplitude = amplitude;
            p.Frequency = frequency;
            p.Phase = phase;
            float tmp = 1.0f / SampleRate;
            float t = 0;
            this.Samples = new double[length];
            for (int i = 0 ; i < length ; i++)
            {
                this.Samples[i] = p.GetValue(t);
                t += tmp;
            }

        }


    }

    
}
