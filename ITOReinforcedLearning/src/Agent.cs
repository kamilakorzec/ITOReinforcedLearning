﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    enum Rewards
    {
        EXIT,
        WALL,
        SINGLE_STEP
    }

    class Agent
    {
        private State currentState;
        private QLearner learner;

        private Dictionary<Rewards, int> rewards = new Dictionary<Rewards, int>
        {
            {Rewards.EXIT, 1000},
            {Rewards.SINGLE_STEP, -1},
            {Rewards.WALL, -1000},
        };
            

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

        private double UpdateRewards(State state)
        {
            double reward = rewards[Rewards.SINGLE_STEP];
            //if position didn't change, big fine
            //if position hits exit, big reward (discounted with time)
            //if position change, small fine (-1)
            return reward;
        }

        private void UpdateQTableVals(State state, PossibleDirections action, double reward)
        {
            double biggestNextReward = learner.Q(state).Values.ToArray().Max();

            //todo: get second from end state
            learner.Q(state)[action] =
                learner.Q(state)[action] +
                LearningConstants.Alpha *
                    (reward + LearningConstants.Gamma * biggestNextReward - learner.Q(state)[action]);
        }

        private PossibleDirections GetRandomAction()
        {
            return (PossibleDirections) new Random().Next(4);
        }

        public bool Act(State state, PossibleDirections action)
        {
            // move Agent
            Move(state, action);

            double reward = UpdateRewards(state);

            UpdateQTableVals(state, action, reward);

            return reward == rewards[Rewards.EXIT];
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
