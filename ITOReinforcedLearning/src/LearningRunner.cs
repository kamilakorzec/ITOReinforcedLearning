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

        public void Act(int[] agentPos)
        {
            State state = new State(map, agentPos);
            for (int j = 0; j < stepLimit * 1000; j++)
            {
                bool isDone = agent.Act(state, agent.ChooseAction(state, false));

                if (isDone)
                {
                    learningDone = true;
                    break;
                }
            }
        }

        public void Learn(int[] agentPos)
        {
            //probably to be moved to a separate function?
            //as a common part of Learn and Act
            State state = new State(map, agentPos);
            for (int i = 0; i < LearningConstants.LearningRounds; i++)
            {
                for(int j = 0; j < stepLimit; j++)
                {
                    bool isDone = agent.Act(state, agent.ChooseAction(state));

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
