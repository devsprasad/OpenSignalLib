using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Filters
{

    public class FilterButterworth
    {
        /// <summary>
        /// rez amount, from sqrt(2) to ~ 0.1
        /// </summary>
        private readonly float resonance;

        private readonly float frequency;
        private readonly int sampleRate;
        private readonly PassType passType;

        private readonly float c, a1, a2, a3, b1, b2;

        /// <summary>
        /// Array of input values, latest are in front
        /// </summary>
        private float[] inputHistory = new float[2];

        /// <summary>
        /// Array of output values, latest are in front
        /// </summary>
        private float[] outputHistory = new float[3];

        public FilterButterworth(float frequency, int sampleRate, PassType passType, float resonance)
        {
            this.resonance = resonance;
            this.frequency = frequency;
            this.sampleRate = sampleRate;
            this.passType = passType;

            switch (passType)
            {
                case PassType.Lowpass:
                    c = 1.0f / (float)Math.Tan(Math.PI * frequency / sampleRate);
                    a1 = 1.0f / (1.0f + resonance * c + c * c);
                    a2 = 2f * a1;
                    a3 = a1;
                    b1 = 2.0f * (1.0f - c * c) * a1;
                    b2 = (1.0f - resonance * c + c * c) * a1;
                    break;
                case PassType.Highpass:
                    c = (float)Math.Tan(Math.PI * frequency / sampleRate);
                    a1 = 1.0f / (1.0f + resonance * c + c * c);
                    a2 = -2f * a1;
                    a3 = a1;
                    b1 = 2.0f * (c * c - 1.0f) * a1;
                    b2 = (1.0f - resonance * c + c * c) * a1;
                    break;
            }
        }

        public enum PassType
        {
            Highpass,
            Lowpass,
        }

        public void Update(float newInput)
        {
            float newOutput = a1 * newInput + a2 * this.inputHistory[0] + a3 * this.inputHistory[1] - b1 * this.outputHistory[0] - b2 * this.outputHistory[1];

            this.inputHistory[1] = this.inputHistory[0];
            this.inputHistory[0] = newInput;

            this.outputHistory[2] = this.outputHistory[1];
            this.outputHistory[1] = this.outputHistory[0];
            this.outputHistory[0] = newOutput;
        }

        public float Value
        {
            get { return this.outputHistory[0]; }
        }
    }

    public static class Butterworth
    {

        public static double[] Lowpass(double[] samples, int n, double sampleRate, double frequency)
        {
            double[] filt = new double[samples.Length];
            double a = Math.Tan(Math.PI * frequency / sampleRate);
            double a2 = a * a;
            double[] A = new double[n];
            double[] d1 = new double[n];
            double[] d2 = new double[n];
            double[] w0 = new double[n];
            double[] w1 = new double[n];
            double[] w2 = new double[n];
            double r, x;
            for (int i = 0 ; i < n ; i++)
            {
                r = Math.Sin(Math.PI * (2.0 * i + 1.0) / (4.0 * n));
                sampleRate = a2 + 2.0 * a * r + 1.0;
                A[i] = a2 / sampleRate;
                d1[i] = 2.0 * (1 - a2) / sampleRate;
                d2[i] = -(a2 - 2.0 * a * r + 1.0) / sampleRate;
            }
            
            for (int j = 0 ; j < samples.Length ; ++j)
            {
                x = samples[j];
                for (int i = 0 ; i < n ; ++i)
                {
                    w0[i] = d1[i] * w1[i] + d2[i] * w2[i] + x;
                    filt[j] = A[i] * (w0[i] + 2.0 * w1[i] + w2[i]);
                    w2[i] = w1[i];
                    w1[i] = w0[i];
                }
            }
            return filt;
        }

        public static double[] Highpass(double[] samples, int n, double sampleRate, double frequency)
        {
            double[] filt = new double[samples.Length];
            double a = Math.Tan(Math.PI * frequency / sampleRate);
            double a2 = a * a;
            double[] A = new double[n];
            double[] d1 = new double[n];
            double[] d2 = new double[n];
            double[] w0 = new double[n];
            double[] w1 = new double[n];
            double[] w2 = new double[n];
            double r, x;
            for (int i = 0 ; i < n ; ++i)
            {
                r = Math.Sin(Math.PI * (2.0 * i + 1.0) / (4.0 * n));
                sampleRate = a2 + 2.0 * a * r + 1.0;
                A[i] = 1.0 / sampleRate;
                d1[i] = 2.0 * (1 - a2) / sampleRate;
                d2[i] = -(a2 - 2.0 * a * r + 1.0) / sampleRate;
            }
            for (int j = 0 ; j < samples.Length ; ++j)
            {
                x = samples[j];
                for (int i = 0 ; i < n ; ++i)
                {
                    w0[i] = d1[i] * w1[i] + d2[i] * w2[i] + x;
                    filt[j] = A[i] * (w0[i] - 2.0 * w1[i] + w2[i]);
                    w2[i] = w1[i];
                    w1[i] = w0[i];
                }
            }
            return filt;
        }
    }
}
