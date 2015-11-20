using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Runtime;

namespace OpenSignalLib.Sources
{
    public class Signal : AbstractSignal
    {
        private float _SampleRate;
        private double[] _Samples;

        public override float SamplingRate
        {
            get { return _SampleRate; }
            set { _SampleRate = value; }
        }

        public override double[] Samples
        {
            get { return _Samples; }
            set { _Samples = value; }
        }

        public Signal(float SampleRate, List Samples)
        {
            double[] tmp = new double[Samples.Count];
            for (int i = 0 ; i < tmp.Length ; i++)
            {
                tmp[i] = double.Parse( Samples[i].ToString());
            }
            this.Samples = tmp;
            this.SamplingRate = SampleRate;
        }
        public Signal(float SampleRate, double[] Samples)
        {
            this._SampleRate = SampleRate;
            this._Samples = new double[Samples.Length];
            Array.Copy(Samples, this._Samples, Samples.Length);
        }

        public Signal()
        {

        }


        public Signal Extend(Signal sig)
        {
            Signal x = new Signal();
            if (this.Samples == null) this.Samples = new double[0];
            x.Samples = this.Samples.Concat(sig.Samples).ToArray();
            if (this.SamplingRate == 0) this.SamplingRate = sig.SamplingRate;
            x.SamplingRate = this.SamplingRate;
            return x;
        }

        public Signal Invert()
        {
            return this * -1;
        }

        public void Display()
        {
            for (int i = 0 ; i < this.Samples.Length ; i++)
            {
                System.Diagnostics.Debug.Print(this.Samples[i].ToString());
            }
        }

        public void CopyToCB()
        {
            var s = string.Join(" ", this.Samples);
            System.Windows.Forms.Clipboard.SetText(s);
        }
        //
        //Operators
        //
        public static Signal operator *(Signal A, float x)
        {
            Signal tmp = new Signal(A.SamplingRate, A.Samples);
            for (int i = 0 ; i < tmp.Samples.Length ; i++)
            {
                tmp.Samples[i] = tmp.Samples[i] * x; 
            }
            return tmp;
        }

        public static Signal operator *(Signal A, Array x)
        {
            Signal tmp = new Signal(A.SamplingRate, A.Samples);
            if (A.Samples.Length != x.Length)
            {
                throw new InvalidOperationException("Signal and Array must be of same length");
            }
            for (int i = 0 ; i < tmp.Samples.Length ; i++)
            {
                tmp.Samples[i] = tmp.Samples[i] * (double)x.GetValue(i);
            }
            return tmp;
        }
       

        public static Signal operator *(Signal A, Signal B)
        {
            if (A.Samples.Length != B.Samples.Length)
            {
                throw new InvalidOperationException("Both the operand signals must have equal number of samples");
            }
            if (A.SamplingRate != B.SamplingRate)
            {
                System.Diagnostics.Debug.Print("WARNING: Two signals with different sampling rates are being multiplied");
            }
            Signal temp = new Signal();
            temp.Samples = new double[A.Samples.Length];
            for (int i = 0 ; i < A.Samples.Length ; i++)
            {
                temp.Samples[i] = A.Samples[i] * B.Samples[i];
            }
            temp.SamplingRate = Math.Max(A.SamplingRate, B.SamplingRate);
            return temp;
        }

        public static Signal operator +(Signal A, Signal B)
        {
            if (A.Samples.Length != B.Samples.Length)
            {
                throw new InvalidOperationException("Both the operand signals must have equal number of samples");
            }
            if (A.SamplingRate != B.SamplingRate)
            {
                System.Diagnostics.Debug.Print("WARNING: Two signals with different sampling rates are being added");
            }
            Signal temp = new Signal();
            temp.Samples = new double[A.Samples.Length];
            for (int i = 0 ; i < A.Samples.Length ; i++)
            {
                temp.Samples[i] = A.Samples[i] + B.Samples[i];
            }
            temp.SamplingRate = Math.Max(A.SamplingRate, B.SamplingRate);
            return temp;
        }

    }
}
