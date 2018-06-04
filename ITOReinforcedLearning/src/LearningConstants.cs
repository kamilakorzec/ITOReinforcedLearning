using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    static class LearningConstants
    {
        public static double Epsilon = 0.5;
        public static double Gamma = 1.0;
        public static double Alpha = 10;
        public static double GetAlpha()
        {
            return Alpha;
        }
    }
}
