using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSignalLib.ComplexTypes;

namespace OpenSignalLib.Operations
{
   public  class Misc
    {

       public static double[] Abs(double[] samples)
       {
           for (int i = 0 ; i < samples.Length ; i++)
           {
               samples[i] = Math.Abs(samples[i]);
           }
           return samples;
       }

       public static double[] Abs(Complex[] samples)
       {
           double[] retval = new double[samples.Length];
           for (int i = 0 ; i < samples.Length ; i++)
           {
               retval[i] = samples[i].GetModulus();
           }
           return retval;
       }

       internal static ComplexF[] List_to_ComplexArray(IronPython.Runtime.List list_object)
       {
           ComplexF[] _fft = new ComplexF[list_object.Count];
           for (int i = 0 ; i < _fft.Length ; i++)
           {
               try
               {
                   if (list_object[i].GetType() == typeof(System.Numerics.Complex))
                   {
                       System.Numerics.Complex c = (System.Numerics.Complex)list_object[i];
                       _fft[i] = new ComplexF((float)c.Real, (float)c.Imaginary);
                   }
                   else
                   {
                       _fft[i] = new ComplexF(float.Parse(list_object[i].ToString()), 0);
                   }
               } catch (Exception)
               {
                   throw;
               }
           }
           return _fft;
       }

       internal static IronPython.Runtime.List ComplexArray_to_List(ComplexF[] complex_array)
       {
           IronPython.Runtime.List l = new IronPython.Runtime.List();
           foreach (var item in complex_array)
           {
               l.append(new System.Numerics.Complex(item.Re,item.Im));
           }
           return l;
       }


        public static int ToNext2Pow(int n)
        {
            double y = Math.Floor(Math.Log(n, 2));
            return (int)Math.Pow(2, y + 1);
        }

        public static int Pow2(int exponent)
        {
            if (exponent >= 0 && exponent < 31)
            {
                return 1 << exponent;
            }
            return 0;
        }

        static public bool IsPowerOf2(int x)
        {
            return (x & (x - 1)) == 0;
            //return	( x == Pow2( Log2( x ) ) );
        }

        public static float[] linspace(float start, float stop, int slices = -1)
        {
            if (start > stop) throw new InvalidOperationException("condition not met: start < stop");
            if (slices == -1) slices = (int)(stop - start + 1);
            float[] retval = new float[slices];
            float c = (stop - start) / (slices - 2);
            //y = d1 + (0:n1).*(d2 - d1)/n1;
            for (int i = 0 ; i < slices ; i++)
            {
                retval[i] = start + i * (stop - start) / (slices - 1);
            }
            return retval;
        }

    }
}
