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

        private PossibleDirections getRandomAction()
        {
            return PossibleDirections.DOWN;
        }

        public void Act(State state, PossibleDirections action)
        {
            //do something
        }

        public PossibleDirections ChooseAction(State state)
        {
            double rand = new Random().NextDouble();

            // explore the environment
            if (rand < new Constants().Epsilon) return getRandomAction();

            float[] totalRewards = learner.Q(state);
            float biggestReward = totalRewards.Max();

            return (PossibleDirections) Array.FindIndex(totalRewards, reward => reward == biggestReward);
        }
    }
}
