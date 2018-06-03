using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITOReinforcedLearning.src;

namespace ITOReinforcedLearning.src
{
    class QLearner
    {
        private Dictionary<string, float[]> qTable;

        private float[] Zeros()
        {
            return new float[] { 0, 0, 0, 0 };
        }

        public float Q(string state, PossibleDirections? action)
        {
            if (!qTable.ContainsKey(state))
            {
                qTable.Add(state, Zeros());
            }

            if (action == null)
            {
                float chosenAction = new Random().Next(4);
                return qTable[state][(int) chosenAction];
            }
            return qTable[state][action.GetHashCode()];
        }
    }
}
