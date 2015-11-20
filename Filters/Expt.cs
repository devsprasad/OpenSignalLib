using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Filters
{
    //public static class Expt
    //{
    //    public static double[] ButterworthFilter(OpenSignalLib.Sources.Signal signal, int order, double f0, double DCGain)
    //    {

    //        var sampleFrequency = signal.SamplingRate;
    //        // Apply forward FFT

    //        ComplexTypes.ComplexF[] fft = OpenSignalLib.Transforms.Fourier.FFT(signal);
    //        var N = fft.Length;
    //        double[] signalFFT = new double[N];
    //        for (int i = 0 ; i < fft.Length ; i++)
    //        {
    //            signalFFT[i] = fft[i].GetModulus();
    //        }
    //        if (f0 > 0)
    //        {

    //            var numBins = N / 2;  // Half the length of the FFT by symmetry
    //            var binWidth = sampleFrequency / N; // Hz

    //            // Filter
    //            System.Threading.Tasks.Parallel.For(1, N / 2, i => {
    //                var binFreq = binWidth * i;
    //                var gain = DCGain / (Math.Sqrt((1 +
    //                              Math.Pow(binFreq / f0, 2.0 * order))));
    //                signalFFT[i] *= gain;
    //                signalFFT[N - i] *= gain;
    //            });

    //        }
    //        for (int i = 0 ; i < signalFFT.Length ; i++)
    //        {
    //            fft[i] = new ComplexTypes.ComplexF((float)signalFFT[i], 0);
    //        }
    //        // Reverse filtered signal
    //        signalFFT = OpenSignalLib.Transforms.Fourier.iFFT(fft,(float)sampleFrequency).Samples;

    //        return signalFFT;
    //    }
    //}
}
