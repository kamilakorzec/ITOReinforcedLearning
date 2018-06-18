using System.Collections.Generic;

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
