using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.Learning
{
    class State
    {
        public Grid Map;
        public List<int[]> AgentPosition; 

        public State(
            Grid map,
            int[] initialPosition
        )
        {
            Map = map;
            AgentPosition = new List<int[]> { initialPosition };
        }
    }
}
