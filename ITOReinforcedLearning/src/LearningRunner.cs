using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class LearningRunner
    {
        private int stepLimit;
        private int triesCount;
        private Grid map;
        private Agent agent;
        private bool learningDone;

        public void Act()
        {

        }

        public void Learn(int stepLimit, Grid map)
        {
            //probably to be moved to a separate function?
            //as a common part of Learn and Act
            for(int i = 0; i < LearningConstants.LearningRounds; i++)
            {
                for(int j = 0; j < stepLimit; j++)
                {
                    // go through a maze
                    // TODO
                    bool isDone = agent.Act(new State(), PossibleDirections.UP);

                    UpdateQFunction();

                    if(isDone)
                    {
                        break;
                    }
                }
            }
        }

        private void UpdateQFunction()
        {

        }
    }
}
