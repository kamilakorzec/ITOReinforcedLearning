using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class Agent
    {
        private PossibleDirections directions;
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

        private PossibleDirections getRandomAction()
        {
            return (PossibleDirections) new Random().Next(4);
        }

        public void Act(State state, PossibleDirections action)
        {
            // move Agent
            Move(state, action);

            // update rewards
            // do something
        }

        public PossibleDirections ChooseAction(State state)
        {
            double rand = new Random().NextDouble();

            // explore the environment
            if (rand < LearningConstants.Epsilon) return getRandomAction();

            Dictionary<PossibleDirections, double> totalRewards = learner.Q(state);
            double[] rewardArr = totalRewards.Values.ToArray();
            double biggestReward = rewardArr.Max();
            
            return totalRewards.Keys.ToArray()[Array.IndexOf(rewardArr, biggestReward)];
        }
    }
}
