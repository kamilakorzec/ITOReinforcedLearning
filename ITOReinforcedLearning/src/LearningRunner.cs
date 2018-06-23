using System.Collections.Generic;

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

        public List<int[]> Act(int[] agentPos)
        {
            State state = new State(map, agentPos);
            for (int j = 0; j < stepLimit * 1000; j++)
            {
                bool isDone = agent.Act(state, agent.ChooseAction(state, false), (1 - (double)j / stepLimit));

                if (isDone)
                {
                    learningDone = true;
                    break;
                }
            }

            return state.AgentPosition;
        }

        public void Learn(int[] agentPos)
        {
            for (int i = 0; i < LearningConstants.LearningRounds; i++)
            {
                State state = new State(map, agentPos);

                for (int j = 0; j < stepLimit; j++)
                {
                    bool isDone = agent.Act(state, agent.ChooseAction(state), (1 -(double)j/stepLimit));

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
