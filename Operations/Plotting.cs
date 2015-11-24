using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Operations
{
    public class Plotting
    {
        public static void plot(params object[] args)
        {
            IronPlot.PlotContext.OpenNextWindow();

        }

    }
}
