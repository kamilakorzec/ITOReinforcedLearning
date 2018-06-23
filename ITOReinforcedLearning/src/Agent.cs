using System;
using System.Collections.Generic;
using System.Linq;

namespace ITOReinforcedLearning.Learning
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
        private Random random = new Random();

        private Dictionary<Rewards, int> rewards = new Dictionary<Rewards, int>
        {
            {Rewards.EXIT, 1000},
            {Rewards.SINGLE_STEP, -1},
            {Rewards.WALL, -1000},
        };
            

        public Agent(State initialState)
        {
            learner = new QLearner();
            currentState = initialState;
        }

        private void Move(State state, PossibleDirections action)
        {
            int[] pos = state.AgentPosition.Last();
            int[] newPos = { pos[0], pos[1] };
            int dimension = state.Map.Dimension;
            switch (action)
            {
                case PossibleDirections.UP:
                    if(
                        newPos[1] > 0 
                        && 
                        state.Map.GetTileByCoordinates(pos[0], pos[1]).Wall != PossibleDirections.UP
                        &&
                        state.Map.GetTileByCoordinates(pos[0], pos[1]-1).Wall != PossibleDirections.DOWN
                    )
                    {
                        newPos[1]--;
                    }
                    break;
                case PossibleDirections.DOWN:
                    if (
                        newPos[1] < dimension - 1
                        &&
                        state.Map.GetTileByCoordinates(pos[0], pos[1]).Wall != PossibleDirections.DOWN
                        &&
                        state.Map.GetTileByCoordinates(pos[0], pos[1]+1).Wall != PossibleDirections.UP
                    )
                    {
                        newPos[1]++;
                    }
                    break;
                case PossibleDirections.LEFT:
                    if (
                        newPos[0] > 0
                        &&
                        state.Map.GetTileByCoordinates(pos[0], pos[1]).Wall != PossibleDirections.LEFT
                        &&
                        state.Map.GetTileByCoordinates(pos[0]-1, pos[1]).Wall != PossibleDirections.RIGHT
                    )
                    {
                        newPos[0]--;
                    }
                    break;
                case PossibleDirections.RIGHT:
                    if (
                        newPos[0] < dimension - 1
                        &&
                        state.Map.GetTileByCoordinates(pos[0], pos[1]).Wall != PossibleDirections.RIGHT
                        &&
                        state.Map.GetTileByCoordinates(pos[0]+1, pos[1]).Wall != PossibleDirections.LEFT
                    )
                    {
                        newPos[0]++;
                    }
                    break;
            }

            state.AgentPosition.Add(newPos);
        }

        private int UpdateRewards(State state)
        {
            int reward = rewards[Rewards.SINGLE_STEP];

            //if position didn't change, big fine
            if (state.AgentPosition.Last() == state.AgentPosition[state.AgentPosition.Count - 2])
                reward = rewards[Rewards.WALL];

            //if position hits exit, big reward (discounted with time)
            if (state.Map.IsExit(state.AgentPosition.Last()))
                reward = rewards[Rewards.EXIT];

            //if position change, no fine (0)
            return reward;
        }

        private void UpdateQTableVals(State state, PossibleDirections action, double reward, double stepFactor)
        {

            double biggestNextReward = learner.Q(state.AgentPosition.Last()).Values.ToArray().Max();
            int[] previousState = state.AgentPosition[state.AgentPosition.Count - 2];
            
            learner.Q(previousState)[action] =
                (1- LearningConstants.Alpha) * learner.Q(previousState)[action] +
                LearningConstants.Alpha *
                    (reward + stepFactor * LearningConstants.Gamma * biggestNextReward);
        }

        private PossibleDirections GetRandomAction()
        {
            return (PossibleDirections) random.Next(4);
        }

        public bool Act(State state, PossibleDirections action, double stepFactor)
        {
            // move Agent
            Move(state, action);

            int reward = UpdateRewards(state);

            UpdateQTableVals(state, action, reward, stepFactor);
            state.Map
                .GetTileByCoordinates(state.AgentPosition.Last()[0], state.AgentPosition.Last()[1])
                .LearningHistory
                .Add(reward);

            return reward == rewards[Rewards.EXIT];
        }

        public PossibleDirections ChooseAction(State state, bool isLearning = true)
        {
            // explore the environment
            if (isLearning && random.NextDouble() < LearningConstants.Epsilon) return GetRandomAction();

            Dictionary<PossibleDirections, double> totalRewards = learner.Q(state.AgentPosition.Last());
            double[] rewardArr = totalRewards.Values.ToArray();
            double biggestReward = rewardArr.Max();
            
            return totalRewards.Keys.ToArray()[Array.IndexOf(rewardArr, biggestReward)];
        }
    }
}
