using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.Learning
{
    class LearningRunner
    {
        private int stepLimit;
        private Grid map;
        private Agent agent;
        public bool learningDone = false;

        public LearningRunner(
            Grid grid,
            int[] agentPosition,
            int limit
        )
        {
            map = grid;
            stepLimit = limit;
            agent = new Agent(new State(grid, agentPosition));
        }

        public void Act()
        {
            //what should be here?
        }

        public void Learn(int stepLimit, Grid map, int[] agentPos)
        {
            //probably to be moved to a separate function?
            //as a common part of Learn and Act
            for(int i = 0; i < LearningConstants.LearningRounds; i++)
            {
                for(int j = 0; j < stepLimit; j++)
                {
                    bool isDone = agent.Act(new State(map, agentPos), PossibleDirections.UP);

                    if(isDone)
                    {
                        learningDone = true;
                        break;
                    }
                }
            }
        }
    }
}
