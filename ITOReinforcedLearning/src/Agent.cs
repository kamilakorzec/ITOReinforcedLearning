using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class Agent
    {
        private State currentState;
        private QLearner learner;

        Agent(
            State initialState,
            QLearner qLearner
        )
        {
            learner = qLearner;
            currentState = initialState;
        }

        private void Move(State state, PossibleDirections action)
        {
            int[] pos = state.AgentPosition.Last();
            int[] newPos = pos;
            switch (action)
            {
                // todo - add restrictions:
                // don't go outside the grid
                // don't go if the wall is there
                case PossibleDirections.DOWN:
                    newPos[1]--;
                    break;
                case PossibleDirections.UP:
                    newPos[1]++;
                    break;
                case PossibleDirections.LEFT:
                    newPos[0]--;
                    break;
                case PossibleDirections.RIGHT:
                    newPos[0]++;
                    break;
            }

            state.AgentPosition.Add(newPos);
        }

        private void UpdateRewards(State state)
        {
            //if position didn't change, big fine
            //if position hits exit, big reward (discounted with time)
            //if position change, small fine
        }

        private PossibleDirections GetRandomAction()
        {
            return (PossibleDirections) new Random().Next(4);
        }

        public bool Act(State state, PossibleDirections action)
        {
            bool isDone = false;
            // move Agent
            Move(state, action);

            UpdateRewards(state);

            return isDone;
        }

        public PossibleDirections ChooseAction(State state)
        {
            double rand = new Random(LearningConstants.RandomSeed).NextDouble();

            // explore the environment
            if (rand < LearningConstants.Epsilon) return GetRandomAction();

            Dictionary<PossibleDirections, double> totalRewards = learner.Q(state);
            double[] rewardArr = totalRewards.Values.ToArray();
            double biggestReward = rewardArr.Max();
            
            return totalRewards.Keys.ToArray()[Array.IndexOf(rewardArr, biggestReward)];
        }
    }
}
