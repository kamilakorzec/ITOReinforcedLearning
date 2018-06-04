using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class Constants
    {
        public double Epsilon = 0.5;
        public double Gamma = 1.0;
        public static double Alpha = 10;
        public double GetAlpha()
        {
            return Alpha;
        }
    }
}
