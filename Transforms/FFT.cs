//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using OpenSignalLib.ComplexTypes;
//using OpenSignalLib.Sources;
//using OpenSignalLib.Operations;

//namespace OpenSignalLib.Transforms
//{
//    public static class exFFT
//    {
       

       

//        /// <summary>
//        /// Arranges the FFT values to have 0 as the center frequency
//        /// </summary>
//        /// <param name="fftvalues">Array of type ComplexF which contain the FFT values of a Signal</param>
//        /// <returns>FFT values with the halves of the argument fftvalues swapped</returns>
//        public static double[] FFTShift(double[] fftvalues)
//        {
//            int half = fftvalues.Length / 2;
//            double[] f_half = new ArraySegment<double>(fftvalues, 0, half).ToArray();
//            double[] s_half = new ArraySegment<double>(fftvalues, half, fftvalues.Length / 2).ToArray();
//            double[] retval = s_half.Concat(f_half).ToArray();
//            return retval;
//        }

//        /// <summary>
//        /// Arranges the FFT values to have 0 as the center frequency
//        /// </summary>
//        /// <param name="fftvalues">Array of type ComplexF which contain the FFT values of a Signal</param>
//        /// <retu
//        public static ComplexF[] FFTShift(IronPython.Runtime.List fftvalues)
//        {
//            var _fft = Misc.List_to_ComplexArray(fftvalues);
//            return FFTShift(_fft);
//        }

        

        

//        public static Dictionary<float, float> FFTSpectrumValues(Signal sig, int N = 512)
//        {
//            ComplexF[] fftvalues = exFFT.FFT(sig,N);
//            int half = fftvalues.Length / 2;
//            ComplexF[] values = new ArraySegment<ComplexF>(fftvalues, 0, half).ToArray();
//            float[] keys =  Misc.linspace(0, sig.SamplingRate / 2, N / 2);
//            Dictionary<float, float> retval = new Dictionary<float, float>();
//            for (int i = 0 ; i < values.Length ; i++)
//            {
//                retval[keys[i]] = values[i].GetModulus();
//            }
//            return retval;

//        }

//        /// <summary>
//        /// Calculates the N-Point Discrete Fourier Transform of a Signal
//        /// </summary>
//        /// <param name="sig">Signal to find the transform</param>
//        /// <param name="N">Number of divisions in the angualar frequency scale (0 to pi) (default=512)</param>
//        /// <returns>FFT values</returns>
//        public static ComplexF[] FFT(this OpenSignalLib.Sources.Signal sig, int N = -1)
//        {
//            if (N == -1) N = sig.Samples.Length;
//            ComplexF[] f = new ComplexF[sig.Samples.Length];
//            for (int i = 0 ; i < f.Length ; i++)
//            {
//                f[i] = new ComplexF((float)sig.Samples[i],0);
//            }
//            if (!Misc.IsPowerOf2 (f.Length) || f.Length < N)
//            {
//                int newLen = Misc.ToNext2Pow(f.Length);//Math.Max(N, f.Length));
//                Array.Resize(ref f, newLen);
//            }
//            FourierBase.FFT(f, N, FourierDirection.Forward);
//            f = f.Take(N).ToArray();
//            return f;
//        }

        

//        public static ComplexF[] FFT(IronPython.Runtime.List vals, int N = 512)
//        {
//            ComplexF[] f = Misc.List_to_ComplexArray(vals);
//            if (!Misc.IsPowerOf2(f.Length) || f.Length < N)
//            {
//                int newLen = Misc.ToNext2Pow(f.Length);//Math.Max(N, f.Length));
//                Array.Resize(ref f, newLen);
//            }
//            FourierBase.FFT(f, N, FourierDirection.Forward);
//            f = f.Take(N).ToArray();
//            return f;
//        }

//        public static Signal iFFT(ComplexF[] fftvalues, float SampleRate, int N = -1)
//        {
//            if (N == -1) N = fftvalues.Length;
//            Signal sig = new Signal();
//            FourierBase.FFT(fftvalues, N, FourierDirection.Forward);
//            sig.Samples = new double[fftvalues.Length];
//            for (int i = 0 ; i < sig.Samples.Length ; i++)
//            {
//                sig.Samples[i] = fftvalues[i].GetModulus();
//            }
//            return sig;
//        }

//        public static Signal iFFT(IronPython.Runtime.List fftvalues, float SampleRate, int N = 512)
//        {
//            Signal sig = new Signal();
//            ComplexF[] _fft = Misc.List_to_ComplexArray(fftvalues);
//            FourierBase.FFT(_fft, N, FourierDirection.Forward);
//            sig.Samples = new double[_fft.Length];
//            for (int i = 0 ; i < sig.Samples.Length ; i++)
//            {
//                sig.Samples[i] = _fft[i].GetModulus();
//            }
//            return sig;
//        }
//    }
//}
