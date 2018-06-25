using System;
using System.Collections.Generic;

namespace ITOReinforcedLearning.Learning
{
    class LearningRunner
    {
        private int stepLimit;
        private Grid map;
        private Agent agent;
        private QLearner learner;
        public bool learningDone = false;

        public LearningRunner(
            Grid grid,
            int limit
        )
        {
            map = grid;
            stepLimit = limit;
        }

        public List<int[]> Act(int[] agentPos)
        {
            State state = new State(map, agentPos);
            agent = new Agent(state, learner);

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

        public void Learn()
        {
            Random rand = new Random();
            learner = new QLearner();

            for (int i = 0; i < LearningConstants.LearningRounds; i++)
            {
                int[] agentPos = new int[] { rand.Next(map.Dimension), rand.Next(map.Dimension) };
                State state = new State(map, agentPos);
                agent = new Agent(state, learner);

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
