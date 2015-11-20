using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSignalLib.ComplexTypes;
using OpenSignalLib.Sources;
namespace OpenSignalLib.Transforms
{
    public static class Fourier
    {

        /// <summary>
        /// Arranges the FFT values to have 0 as the center frequency
        /// </summary>
        /// <param name="fftvalues">Array of type ComplexF which contain the FFT values of a Signal</param>
        /// <returns>FFT values with the halves of the argument fftvalues swapped</returns>
        public static Complex[] FFTShift(Complex[] fftvalues)
        {
            int half = fftvalues.Length / 2;
            Complex[] f_half = new ArraySegment<Complex>(fftvalues, 0, half).ToArray();
            Complex[] s_half = new ArraySegment<Complex>(fftvalues, half, fftvalues.Length / 2).ToArray();
            Complex[] retval = s_half.Concat(f_half).ToArray();
            return retval;
        }


        public static Dictionary<double, double> FFTSpectrumValues(this Signal sig, int N = 1024)
        {
            Complex[] fftvalues = FFT(sig, N).Take(N).ToArray();
            int half = fftvalues.Length / 2;
            Complex[] values = new ArraySegment<Complex>(fftvalues, 0, half).ToArray();
            float[] keys = OpenSignalLib.Operations.Misc.linspace(0, sig.SamplingRate/2, N / 2);
            Dictionary<double, double> retval = new Dictionary<double, double>();
            for (int i = 0 ; i < values.Length ; i++)
            {
                retval[keys[i]] = values[i].GetModulus();
            }
            return retval;
        }

        public static Complex[] FFT(this OpenSignalLib.Sources.Signal sig, int N = 1024)
        {
            Complex[] retval = new Complex[sig.Samples.Length];
            FFT2 f = new FFT2();
            f.init((uint)(Math.Log(N) / Math.Log(2)));
            double[] samRe = sig.Samples;
            double[] samIm = new double[samRe.Length];
            f.run(samRe, samIm);
            for (int i = 0 ; i < samRe.Length ; i++)
            {
                retval[i] = new Complex(samRe[i], samIm[i]);
            }
            return retval;
        }

        public static double[] iFFT(double[] samplesReal, double[] samplesImag, int N = 1024)
        {
            FFT2 f = new FFT2();
            f.init((uint)(Math.Log(N) / Math.Log(2)));
            f.run(samplesReal, samplesImag, true);
            return samplesReal;
        }

        public static double[] iFFT(Complex[] samples, int N = 1024)
        {
            double[] samRe = new double[samples.Length];
            double[] samIm = new double[samples.Length];
            for (int i = 0 ; i < samples.Length ; i++)
            {
                samRe[i] = samples[i].Re;
                samIm[i] = samples[i].Im;
            }
            return iFFT(samRe, samIm, N);
        }
    }
}
